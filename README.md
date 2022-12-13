# goSolve - Tools
This repository includes the tools packages used for goSolve's back-end services.

## Api tools
The Tools.Api package includes the default configurations for the goSolve api's. This includes:
- Analyzers
- Error handling & ProblemDetails
- Json configuration
- Api versioning
- JsonPatch mapping & validation functionality
- Swagger documentation
- Endpoint routing & controller mapping
- Database setup & migration

To include these in an API, use the following in your Program.cs:
```csharp
using GoSolve.Tools.Api.ExtensionMethods;

builder.Services.AddApiTools();
builder.Services.AddDatabaseTools<MyDbContext>(builder.Configuration); // Optional: Adds database context

// ...

app.UseApiTools();
app.UseDatabaseTools<MyDbContext>(); // Optional: Migrates database on startup
```

To use the database tools, the connection string to the postgresql database needs to be added:
```json
{
    "ConnectionStrings": {
        "DbConnection": "[your database connection string]"
    }
}
```

### Add an internal http client
All HttpClients can be viewed in the Clients repository.
To register a HttpClient in your Program.cs for dependency injection, use the following extension method (Book api as example):
```csharp
builder.Services.AddInternalHttpClient<IBookHttpClient, BookHttpClient>(builder.Configuration, "book");
```
And add the following to your appsettings.json and appsettings.Development.json:
```json
{
    "HttpClients": [
        {
            "Name": "book",
            "BaseAddressUri": "<base address of the api>", // Example: "https://localhost:5001/" (trailing slash is required!)
            "Accept": "application/json" // (Optional, default value: "application/json")
        }
    ]
}
```

### Other appsetting properties
```json
{
    "PathBase": "/my-api" // Prefix all your endpoints with this base path. Should only be used for development appsettings in combination with reverse proxy prefixes.
}
```

### Json
For all json serialization & deserialization logic, we will use the package Newtonsoft.Json.  
Reason: The native .NET System.Text.Json does not support customization with projects using .AddControllers() instead of .AddMvc().  

## EditorConfig rules
Our analyzer rules are set up in the .editorconfig file. If specific rules should be ignored, or vice versa, then this .editorconfig file should be updated. It will be distributed through the GoSolve.Tools.Common package to all of goSolve's back-end services.  
To make sure the .editorconfig file is propagated, include any of the tools packages like so:
```xml
<PackageReference Include="GoSolve.Tools.<Project>" Version="<Version>">
  <IncludeAssets>All</IncludeAssets>
  <PrivateAssets>None</PrivateAssets>
</PackageReference>
```

## License
[![License: AGPL v3](https://img.shields.io/badge/License-AGPL_v3-blue.svg)](https://www.gnu.org/licenses/agpl-3.0)  
goSolve is open-source. We use the [GNU AGPLv3 licensing strategy](LICENSE).
