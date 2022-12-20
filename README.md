# ASP.NET Core Web API - Clean Architecture
</br>

In this repository I want to give a plain starting point at how to build a WebAPI with ASP.NET Core connected with database following the principles of Clean Architecture.
This repository contains a controllers which are dealing with Products, Categories and Brands. You can Secured GET/POST/PUT/PATCH and DELETE them.

## **Technologies:**
- .NET 7 
- Database: Microsoft SQL server.
- Framework/ library: Entity framework 
- JWT

- Generated JWS key : https://8gwifi.org/jwsgen.jsp

## **Default Roles & Credentials:**
As soon you build and run your application, default users and roles get added to the database.

Default Roles are as follows.

- SuperAdmin
- Admin
- Moderator
- User
Here are the credentials for the default users.

Email - haitham.abass49@gmail.com / Password - 123456
Email - Maged.sobhy50@gmail.com / Password - 123456


## **Features:**
- User registration;
- Email verification 
- User login
- Password hashing;
- Role-based authorization;
- Identity Seeding
- Database Seeding
- Assign roles to users who signed up 
- Hide or display different parts of a page based on the user's roles.
- Endpoints request required authorized access.
- Login via access token creation;
- Refresh tokens, to create new access tokens when access tokens expire;
- Cookies to store refresh tokens in it. 
- Revoking refresh tokens.
- Secured  CRUD operations(Create (POST) - Read (GET) - Update (PUT) - Delete (DELETE))
 
 

</br>

**If you have ideas on how to improve the API or if you want to add a new functionality or fix a bug, please, send a pull request.**
