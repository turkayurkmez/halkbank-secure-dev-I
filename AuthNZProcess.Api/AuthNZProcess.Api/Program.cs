using AuthNZProcess.Api.Security;
using AuthNZProcess.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication("Basic")
//                .AddScheme<BasicOptions, BasicHandler>("Basic", null);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "api.halkbank",
                        ValidateAudience = true,
                        ValidAudience = "client.api",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-cümle-kritik-bir-cümledir-aman-önemli")),
                        ValidateIssuerSigningKey = true
                    };
                });


builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddCors(option => option.AddPolicy("allow", builder =>
{
    /*
     * http://www.halkbank.com.tr/hakkimizda
     * https://www.halkbank.com.tr
     * https://posts.halkbank.com.tr
     * https://www.halkbank.com.tr:8080
     * 
     */

    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();

}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("allow");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
