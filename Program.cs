using CookieAuthentication_CoreWebAPI.Common;
using CookieAuthentication_CoreWebAPI.Implementation;
using CookieAuthentication_CoreWebAPI.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCookieAuthentication(builder.Configuration);
builder.Services.Configure<ContextService>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IDbUtility, DbUtility>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
