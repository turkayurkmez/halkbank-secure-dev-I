using AuthNZProcess.Api.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("Basic")
                .AddScheme<BasicOptions, BasicHandler>("Basic", null);

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
