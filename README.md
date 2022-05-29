# goSolve - Tools
This repository includes the tools packages used for goSolve's back-end services.

## Api tools
The Tools.Api package includes the default configurations for the goSolve api's. This includes:
- Analyzers
- Error handling & ProblemDetails

To include these in an API, use the following in your Program.cs:
```csharp
using GoSolve.Tools.Api.ExtensionMethods;

builder.Services.AddApiTools();

// ...

app.UseApiTools();
```

## EditorConfig rules
Our analyzer rules are set up in the .editorconfig file. If specific rules should be ignored, or vice versa, then this .editorconfig file should be updated. It will be distributed through the GoSolve.Tools.Common package to all of goSolve's back-end services.

## License
[![License: AGPL v3](https://img.shields.io/badge/License-AGPL_v3-blue.svg)](https://www.gnu.org/licenses/agpl-3.0)  
goSolve is open-source. We use the [GNU AGPLv3 licensing strategy](LICENSE).
