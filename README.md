# goSolve - Tools
This repository includes the tools packages used for goSolve's back-end services.

## Api tools
The Tools.Api package includes the default configurations for the goSolve api's. This includes:
- Analyzers
- Error handling & ProblemDetails

To include these in an API, use the following in your Program.cs:
```csharp
using GoSolve.Tools.ExtensionMethods.Api;

builder.Services.AddApiTools();

// ...

app.UseApiTools();
```

## License
[![License: AGPL v3](https://img.shields.io/badge/License-AGPL_v3-blue.svg)](https://www.gnu.org/licenses/agpl-3.0)
goSolve is open-source. We use the [GNU AGPLv3 licensing strategy](LICENSE).
