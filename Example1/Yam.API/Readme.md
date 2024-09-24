
# ASP.NET CORE 8 Test task JWT auth

- Login & register with JWT tokens.
- JWT refresh token

## Endpoints
- POST - /api/account/register
- POST - /api/account/login
- POST - /api/account/refreshtoken

### Protected
- GET - /api/account/info

### JWT Bearer
Yam.API

### Entity Framework Core MSSql Server

Yam.DataAccess

## Project Client

Yam.API.Client 


For start need:
1) run Yam.API.exe (web api application)
2) run Yam.API.Client.exe and input email and password for login
