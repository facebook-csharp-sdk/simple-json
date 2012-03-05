# SimpleJson
Small and fast JSON library for .NET 2.0+/SL4+/WP7+/WinRT and powershell. Includes support for dynamic in .NET 4.0+/SL4+/WinRT. Also includes support for DataContract and DataMember. 

# Using SimpleJson

SimpleJson is not distributed as a compiled binary .dll file but rather as a single .cs file or a powershell module .psm1.

**Use nuget to add SimpleJson.cs file to your project.**

```powershell
Install-Package SimpleJson
```

## Preparing Simple Json for your .NET Framework/Platform
Depending on the .net framework or platform you might want to enable/disable certain features of SimpleJson. You can read
more about it at https://github.com/facebook-csharp-sdk/simple-json/wiki/Conditional-Compilation-Symbols