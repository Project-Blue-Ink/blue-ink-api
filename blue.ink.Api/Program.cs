using blue.ink.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Blue.Ink.Api.Security;
using Microsoft.AspNetCore.Authorization.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

string authrity = builder.Configuration["Auth0:Authority"] ??
    throw new ArgumentNullException("Auth0:Authority");

string audience = builder.Configuration["Auth0:Audience"] ??
    throw new ArgumentNullException("Auth0:Audience");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = authrity;
        options.Audience = audience;
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("delete:catalog", policy =>
            policy.RequireAuthenticatedUser().RequireClaim("scope","delete: catalog"));
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite("Data Source=../Registrar.sqlite",
    b => b.MigrationsAssembly("blue.ink.Api"))
);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5215")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.UseAutentication();
app.UseAuthorization();
app.MapControllers();
app.Run();