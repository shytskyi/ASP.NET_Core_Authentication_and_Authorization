using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthenticationCookies
{
    public class Program
    {
        record class Person(string Email, string Password);

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var people = new List<Person>
            {
                new Person("ivan@gmail.com", "123"),
                new Person("ela@gmail.com", "111")
            };

            builder.Services.AddAuthentication("Cookies")
                            .AddCookie(option => option.LoginPath = "/login");
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) exactly that (builder.Services.AddAuthentication("Cookies");)
            
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/login", async (context) =>
            {
                context.Response.ContentType = "text/html; charset = utf-8";
                string loginForm = @"<!DOCTYPE html>
                <html>
                    <head>
                        <meta charset='utf-8' />
                        <title>Authentication Cookies</title>
                    </head>
                    <body>
                        <h2>Login Form</h2>
                        <form method='post'>
                            <p>
                                <label>Email</label><br />
                                <input name='email' />
                            </p>
                            <p>
                                <label>Password</label><br />
                                <input type='password' name='password' />
                            </p>
                            <input type='submit' value='Login' />
                        </form>
                    </body>
                 </html>";
                await context.Response.WriteAsync(loginForm);
            });
            app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
            {
                var form = context.Request.Form;
                if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                    return Results.BadRequest("incorrect Email or Password");

                string email = form["email"];   
                string password = form["password"];
                Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
                if (person == null) 
                    return Results.Unauthorized();
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await context.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
                return Results.Redirect(returnUrl??"/");
            });

            app.MapGet("/logout", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/login");
            });

            app.MapGet("/", [Authorize] () => "Hello World!");

            app.Run();

        }
    }
}