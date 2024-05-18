using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Trainer.Database;
using Trainer.Database.DependencyInjection;
using Trainer.Database.Entities.Auth;
using Trainer.WebApi.Controllers.Auth.Yandex.DependencyInjection;
using Trainer.WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTrainerContext(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddYandexServices(builder.Configuration);

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<TrainerContext>();

builder.Services
    .AddAuthorization()
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TrainerContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(routeBuilder => routeBuilder.MapControllers());

app.Run();