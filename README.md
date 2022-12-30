# goSolve - Tools
This repository includes the tools packages used for goSolve's back-end services.

## Api tools
The Tools.Api package includes the default configurations for the goSolve api's. This includes:
- Error handling & ProblemDetails
- Json configuration
- Api versioning
- JsonPatch validation functionality
- Swagger documentation
- Endpoint routing & controller mapping
- Database tools
  - Migrations
  - Seeding test data

The Tools.Common package includes the default configuration for all generic goSolve services. This includes:
- Serilog logging
- Database
  - Setup
  - Seeding core application data
- Analyzers
- JsonPatch mapping functionality

To include these in an API, use the following in your Program.cs:
```csharp
using GoSolve.Tools.Api.ExtensionMethods;

builder.Services.AddCommonTools(); // Don't use in combination with AddApiTools()
builder.Services.AddApiTools(); // Also adds AddCommonTools()
builder.Services.AddDatabaseTools<MyDbContext>(builder.Configuration); // Optional: Adds database context

// ...

app.UseApiTools();
app.MigrateDatabase<MyDbContext>(); // Optional: Migrates database on startup

// Optional: Seeds test data (should not be run before database migration)
if (EnvironmentHelper.IsDevelopment())
{
    app.SeedTestData<BookDbContext>(builder =>
        builder.AddSeeder<BookTestDataSeeder>());
}
```

To use the database tools, the connection string to the postgresql database needs to be added:
```javascript
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
```javascript
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
```javascript
{
    "PathBase": "/my-api" // Prefix all your endpoints with this base path. Should only be used for development appsettings in combination with reverse proxy prefixes.
}
```

### Json
For all json serialization & deserialization logic, we will use the package Newtonsoft.Json.  
Reason: The native .NET System.Text.Json does not support customization with projects using .AddControllers() instead of .AddMvc().

## Seed data to database
### Core application seed data
Core application seed data refers to the data that is essential for the application to function in any environment.
For example, this could include data such as user roles, system configurations, and default values that are required for the application to operate properly.

To add core seed data, use the `modelBuilder.SeedCoreData` extension method in the `OnModelCreating` method of your `DbContext`:
```csharp
public class BookDbContext : BaseDbContext<BookDbContext>
{
    ...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...

        
        modelBuilder.SeedCoreData(builder =>
            builder.AddSeeder<BookGenreSeeder>());
    }
}
```

To create a core data seeder, inherit from the ICoreDataSeeder interface:
```csharp
public class BookGenreSeeder : ICoreDataSeeder
{
    public IEnumerable<TimestampedEntity> BuildData()
    {
        return new BookGenre[]
        {
            new BookGenre
            {
                Id = 1, // Ids should be defined for seed data
                Name = "Fiction",
                Description = "Narrative storytelling with imaginary characters and events."
            },
            ...
        };
    }
}
```

:exclamation: **You need to create a new migration after adding / updating core seed data.**

### Test seed data
Test seed data refers to the data used in development environment for testing purposes.  

To add test seed data, use the `app.SeedTestData` extension method on the WebApplication class as defined above.  
To create a test data seeder, inherit from the ITestDataSeeder interface (works the same way as the ICoreDataSeeder).

**Note: The test seed data is only added on creation of the database. If you want to have the latest test data, you need to remove (drop) the database and restart the application.**

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
