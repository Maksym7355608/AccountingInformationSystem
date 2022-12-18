using AccountingInformationSystem.DI;
using AccountingInformationSystem.Infrastructure.AutoMappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionName = "DefaultLocalConnection";
var connectionString = builder.Configuration.GetConnectionString(connectionName);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDatabase(connectionString);
builder.Services.AddAdministrations(); 
builder.Services.AddFinances();
builder.Services.AddShedules();
builder.Services.AddAutoMappers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var secret = builder.Configuration["JWT:Key"];

var key = Encoding.UTF8.GetBytes(secret ?? "sth key");
builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
