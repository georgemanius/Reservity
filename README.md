# Reservity
### ASP.NET Core Web API with Swagger documentation 

Reservity is an API allowing users to find a place to rest (venue), view information about it and book for a certain period of time. Likewise, any individual or legal entity (owner) is able to register their property on the platform for further booking by other users (customers).

> API was developed within the scope of university course "Analysis and code refactoring" and it serves as a server-side to the whole project including "Vision & Scope" document, Backend part, Frontend part, Mobile application, IoT.

## Features

- Registration and authorization with JWT
- CRUD operations for venues
- Viewing information about all users (administration feature)
- Viewing information about the particular user (administration feature)
- Deleting the particular user (administration feature)
- CRUD operations for reservations

## Tech

Reservity uses following technologies:

- [ASP.NET] - web framework for building web apps with .NET and C#
- [Entity Framework] - object-database mapper for .NET
- [LINQ] - .NET Framework component that adds native data querying capabilities to .NET technologies
- [Microsoft SQL Server] - relational database management system
- [JWT] - method for representing claims securely between two parties

Used design patterns:
- Dependency injection 
- Repository
- Model-View-Controller (MVC)



   [ASP.NET]: <https://dotnet.microsoft.com/en-us/apps/aspnet>
   [Entity Framework]: <https://docs.microsoft.com/en-us/ef/>
   [LINQ]: <https://docs.microsoft.com/en-us/dotnet/csharp/linq/>
   [Microsoft SQL Server]: <https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15>
   [JWT]: <https://jwt.io/>
