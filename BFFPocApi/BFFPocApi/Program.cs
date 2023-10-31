var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient()
                .AddDataProtection();

builder.Services.AddCors(options =>
{
    options.AddPolicy("APIAllowOrigins", builder =>
    {
    builder.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
           .SetIsOriginAllowed(hostName => true);
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
})
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_cookie";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.HttpOnly = false;

        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var cookiePolicyOptions = new CookiePolicyOptions
{
};

app.UseCookiePolicy(cookiePolicyOptions);
app.UseCors("APIAllowOrigins");

app.MapControllers();

app.Run();
