# .NET-WalletApi
This project created for sample Wallet API demo.

After cloning project, please add setting to appsettings (or to your secrets) with this params. 

ATTENTION!!! Change connection string to your own.

"DatabaseOptions": {
    "ConnectionString": "Server=127.0.0.1;Port=5432;Database=walletdb;User Id=postgres;Password=11111;"
  }

Then run migrations with 'dotnet ef database update' in powershell from admin in cloned project directory.

Here used:
- .NET 7
- EF Core
- PostgreSQL
- Automapper

Thanks for attention.
