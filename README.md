# Repository Description:
This Git repository houses an ASP.NET Core project implemented in Visual Studio using the "ASP.NET Core Empty" template. 
The project focuses on authentication and authorization functionalities through the use of Microsoft.AspNetCore.Authentication and Microsoft.AspNetCore.Authorization libraries.

Key Aspects and Features:

1. Authentication Setup
Utilizes the ASP.NET Core empty template.
Implements cookie-based authentication using Microsoft.AspNetCore.Authentication.Cookies.
Middleware defines HTTP endpoints for login ("/login"), logout ("/logout"), and the home page ("/").
A secure ("/") route is implemented using the [Authorize] attribute, ensuring only authenticated users can access it.
The middleware orchestrates the display of a basic login form at "/login" for user authentication.

2.Code Organization and Middleware Structure
Core authentication and authorization processes are encapsulated within middleware components.

3. Authorization Configuration
Integrates authorization capabilities using Microsoft.AspNetCore.Authorization.
Sets up authorization services within the application.

4. User Data Handling
Maintains a list of users (people) with email and password combinations.
Implements a simple login form accessible at "/login".
Validates user credentials on form submission and signs in authenticated users.
Supports user logout with a "/logout" endpoint.

5. Code Organization and Structure
Utilizes the C# record type for defining a simple "Person" class to represent users.

6. Usage
The application demonstrates the complete cycle of user authentication, including login, logout, and access control to secured routes.
