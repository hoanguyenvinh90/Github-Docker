using Automobile.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Automobile
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");
      builder.Services.AddAuthorization();
      builder.Services.AddApplication(builder.Configuration);
      //builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
      //.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
      //builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, 
      //    options =>
      //    {
      //        options.ResponseType = "code";
      //        options.UsePkce = true;
      //    });

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Automobile api",
          Version = "v1"
        });
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
          Description = "OAuth2.0 Auth Code with PKCE",
          Name = "oauth2",
          Type = SecuritySchemeType.OAuth2,
          Flows = new OpenApiOAuthFlows
          {
            AuthorizationCode = new OpenApiOAuthFlow
            {
              AuthorizationUrl = new Uri(builder.Configuration["SwaggerAuthentication:AuthorizationUrl"]!),
              TokenUrl = new Uri(builder.Configuration["SwaggerAuthentication:TokenUrl"]!),
              Scopes = new Dictionary<string, string>
              {
                { builder.Configuration["SwaggerAuthentication:ApiScope"]!, "read the weather data" }
              }
            }
          }
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { builder.Configuration["SwaggerAuthentication:ApiScope"] }
          }
        });
      });

      // JSON serializer
      builder.Services
          .AddControllers()
          .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
          .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerAADdemo v1");
          c.OAuthClientId(builder.Configuration["SwaggerAuthentication:OpenIdClientId"]);
          c.OAuthUsePkce();
          c.OAuthScopeSeparator(" ");
        });
      }

      // Enable CORS
      app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .AllowAnyOrigin());

      app.UseAuthentication();
      app.UseHttpsRedirection();
      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
