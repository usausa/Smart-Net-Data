# Smart.Data .NET

[![NuGet](https://img.shields.io/nuget/v/Usa.Smart.Data.svg)](https://www.nuget.org/packages/Usa.Smart.Data)

A lightweight data access utility library for .NET.

## Features

### IDbProvide

`IDbProvider` is a simple interface for creating `DbConnection` instances.

```csharp
// Synchronous
provider.Using(con => {
    // con is already open
});

// Asynchronous
await provider.UsingAsync(async con => {
    // con is already open
});

// With return value
var result = await provider.UsingAsync(async con => {
    return await con.QueryAsync(...);
});
```

### IDbProviderSelector

Selects an `IDbProvider` by name, enabling multi-database configurations.

```csharp
var selector = new NamedDbProviderSelector();
selector.AddProvider("main", mainProvider);
selector.AddProvider("sub", subProvider);

var provider = selector.GetProvider("main");
```

### IDialect

An abstraction interface that encapsulates database-specific behavior, including duplicate key detection and LIKE clause escaping.

```csharp
// LIKE escaping
var keyword = dialect.LikeContains("50%OFF"); // => "%50[%]OFF%"
var prefix  = dialect.LikeStartWith("abc");    // => "abc%"
var suffix  = dialect.LikeEndWith("xyz");      // => "%xyz"

// Duplicate key detection
if (dialect.IsDuplicate(ex)) { /* handle duplicate */ }
```
