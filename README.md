
# .NET Yuxi Test - Andres Hernandez

For this project I used a library class developed by me and deployed on AzureDevOps through Pipelines and Artifacts where was created a Feed with the NuGet packages. 

See the SharedKernelCode here:
https://github.com/andresdev16/shared-kernel

# Requirements
.NET 7
SQL Server Instance or Azure Data Edge

# Environment
For this app is necessary go to WebApi project and manage the User Secrets (secrets.json) and add the following code:

```json
{
    "ConnectionStrings": {
        "TestConnectionSqlServer": "Server=localhost;Database=YuxiTest;User Id=sa;Password=andres16;TrustServerCertificate=True;"
    }
}
```

This is allow the connection to the Database



## Authors

- [@andresdev](https://www.github.com/andresdev16)

