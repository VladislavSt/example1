
# ASP.NET CORE 8 Test task JWT auth

- Login & register with JWT tokens.
- JWT refresh token

## Endpoints
- POST - /api/account/register
- POST - /api/account/login
- POST - /api/account/refreshtoken

### Protected
- GET - /api/account/info

Projects:
Yam.API - web api with GWT auth
Yam.API.Model - project with dto models
Yam.ApplicationServices - project with services
Yam.Core - core project
Yam.Model - project with main models database
Yam.Tests - contains integration test for registration, login, and get user info

### Entity Framework core with Sqlite

Yam.DataAccess - project with dbContext and migrations with local database file 'yam.db'

## Project Client

Yam.API.Client - console application with httpclient to use Yam.API Rest API

With appsettings to web api

"ServerOptions": {
    "AccountUriRegister": "http://localhost:5281/api/account/register",
    "AccountUriLogin": "http://localhost:5281/api/account/login",
    "AccountUriRefreshtoken": "http://localhost:5281/api/account/refreshtoken",
    "AccountUriInfo": "http://localhost:5281/api/account/info"
  }


## Description

Before was registed with Yam.API using swagger ui example user using API point

For use swagger example link http://localhost:5281/swagger/index.html


http://localhost:5281/api/account/register

{
  "email": "superadmin@gmail.com",
  "username": "superadmin",
  "password": "Super12345@"
}


For check functionality need:

You can run solution via visual studio with current setting run multiple projects 'Yam.API' and 'Yam.API.Client'



Or using CMD.exe

1. run Yam.API.exe (web api application) with command

dotnet run --project S:\Example1\Yam.API\Yam.API.csproj


2. run Yam.API.Client.exe  with command

dotnet run --project S:\Example1\Yam.API.Client\Yam.API.Client.csproj

and run

S:\Example1\Yam.API.Client\bin\Debug\net8.0>start Yam.API.Client.exe

For login need input email, click enter
than input password

and after login will be call http://localhost:5281/api/account/info
for get user info


