using Login.Application;
using Login.Context;
using Login.Extensions;
using Login.IRepositories;
using Login.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<LoginContext>(options => options.UseSqlite(configuration.GetConnectionString("sqlite")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<GetUsersUseCase>();
builder.Services.AddAuth(configuration);

builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddAuthentication();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LoginContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.MapControllers();

app.Run();