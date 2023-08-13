using WalletApi.Core.Extensions;
using WalletApi.Data.Extensions;
using WalletApi.Models.Options;
using WalletApi.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var databaseOptions = 
    builder.Configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();

if (databaseOptions is not null)
{
    builder.Services
        .AddWalletContext(databaseOptions.ConnectionString)
        .AddWalletApiCore()
        .AddWalletApiWeb();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();