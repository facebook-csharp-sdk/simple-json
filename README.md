# SimpleJson
Small and fast JSON library for .NET 2.0+/SL4+/WP7+/WinRT and powershell. Includes support for dynamic in .NET 4.0+/SL4+/WinRT. Also includes support for DataContract and DataMember. 

# Using SimpleJson

SimpleJson is not distributed as a compiled binary .dll file but rather as a single .cs file or a powershell module .psm1.

**Use nuget to add SimpleJson.cs file to your project.**

```powershell
Install-Package SimpleJson
```

**Define appropriate conditional compilcation symbol depending on your framework**

_**.NET 2.0+ (including .NET 3.0/3.5/4.0/4.5)**_

```csharp
#define SIMPLE_JSON_REFLECTIONEMIT
```

_**Windows Runtime (Windows Metro Style Apps)**_

```csharp
#define NETFX_CORE
```

`NETFX_CORE` is automatically defined for new WinRT projects by Visual Studio.

_**Silverlight 4+, .NET 4.0+ and WinRT (.net frameworks which supports dynamic)**_

```csharp
#define SIMPLE_JSON_DYNAMIC
```

_**DataContract/DataMember support**_

```csharp
#define SIMPLE_JSON_DATACONTRACT
```

You are also required to reference `System.Runtime.Serialization` library.

_**Hiding SimpleJson class**_

```csharp
#define SIMPLE_JSON_INTERNAL
```

This will make the `SimpleJson` class to internal class instead of public.

_**Hiding JsonArray and JsonObject class**_

```csharp
#define SIMPLE_JSON_OBJARRAYINTERNAL
```

This will make the `JsonArray` and `JsonObject` classes to internal class instead of public. You can cast `JsonObject`
to `IDictionary<string,object>` or `IDictionary<string,dynamic>` and `JsonArray` to `IList<object>` or `IList<dynamic>`.