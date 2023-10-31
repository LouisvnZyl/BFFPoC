using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
                .AddTransforms(tCtx =>
                {
                    tCtx.AddRequestTransform(async rc =>
                    {
                        if (rc.DestinationPrefix == "https://www.googleapis.com")
                        {
                            var accessToken = await rc.HttpContext.GetTokenAsync("access_token");
                        }
                    });
                });

builder.Services.AddHttpClient()
                .AddDataProtection();

builder.Services.AddAuthentication("auth-cookie")
    .AddCookie("auth-cookie")
    .AddOAuth("youtube", auth =>
    {
        auth.SignInScheme = "auth-cookie";
        auth.ClientId = "someClientID";
        auth.ClientSecret = "someClientSecret";
        auth.SaveTokens = true;

        auth.Scope.Clear();
        auth.Scope.Add("https://www.googleapis.com/auth/youtube.readonly");

        auth.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        auth.TokenEndpoint = "https://oauth.googleapis.com/token";
        auth.CallbackPath = "/oauth/yt-cb";
    });

var app = builder.Build();

app.MapGet("/login", () => Results.Challenge(new AuthenticationProperties()
{
    RedirectUri = "/"
}, authenticationSchemes: new List<string>() { "youtube" }));

app.MapGet("/api-yt", async (IHttpClientFactory clientFactory, HttpContext ctx) =>
{
    var accessToken = await ctx.GetTokenAsync("access_token");
    var client = clientFactory.CreateClient();

    Console.WriteLine(accessToken);


    using var req = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/youtube/v3/channels?part=snippet&mine=true");
    req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    using var response = await client.SendAsync(req);
    return await response.Content.ReadAsStringAsync();
}).RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
