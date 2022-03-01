using BetEcommerce.Repository.Helpers;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Implementation;
using BetEcommerce.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "BET Ecommerce Api Documentation",
        Description = "Provides documentation about BET Ecommerce api endpoints"
    });
});
builder.Services.AddDbContext<BetEcommerceDBContext>();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "BET Ecommerce api");
    });
}
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
