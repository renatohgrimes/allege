# Allege

Provides runtime validation utilities to improve execution with concise exception details.

## Installation

### From NuGet

You can install Allege directly from NuGet:

```bash
dotnet add package Allege
```

NuGet Package URL: [https://www.nuget.org/packages/Allege](https://www.nuget.org/packages/Allege)

## Usage

```csharp
void ModifyUser(User user)
{
    Allege.NotNull(user);
    Allege.Condition(user.Age > 10, "user must be over 10 years");
    (...)
}
```


```csharp
void Send(byte[] buffer)
{
    Allege.NotEmpty(buffer);
    Allege.Condition(socket.IsConnected, "socket must be connected");
    (...)
}    
```


## Contributing

Contributions are welcome! Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.