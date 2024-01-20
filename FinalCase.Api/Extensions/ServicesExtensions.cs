using FinalCase.BackgroundJobs.QueueOperations;
using FinalCase.BackgroundJobs.QueueService;
using FinalCase.Base.Token;
using FinalCase.Business.Assembly;
using FinalCase.Business.Features.Pipelines.Cache;
using FinalCase.Data.Constants.Storage;
using FinalCase.Data.Contexts;
using FinalCase.Services.NotificationService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using StackExchange.Redis;
using System.Configuration;
using System.Text;

namespace FinalCase.Api.Extensions;

public static class ServicesExtensions
{


    public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) => services.AddDbContext<FinalCaseDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString(DbKeys.SqlServer)));
    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        JwtConfig jwtConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(schema =>
        {
            schema.RequireHttpsMetadata = true;
            schema.SaveToken = true;
            schema.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                ValidAudience = jwtConfig.Audience,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });
    }

    public static void AddSwagger(this IServiceCollection services)
    {



        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Final Case Api Management", Version = "v1.0" });

            //c.SelectSchemasForMediaType("application/json", _ => true);
            //c.PostProcess = document => { };


            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Final Case",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }

            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
        });
    }

    public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("HangfireSqlConnection"), new SqlServerStorageOptions
            {
                //TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
                //InvisibilityTimeout = TimeSpan.FromMinutes(5),   //Background jobs re-queued instantly even after ungraceful shutdown now. Will be removed 2.0.0s
                QueuePollInterval = TimeSpan.FromMinutes(5),
            }));
        services.AddHangfireServer();
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<INotificationService, QueueNotificationService>();
        services.AddSingleton<IQueueService, QueueService>();
    }

    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
             {
                 cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
                 cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
                 cfg.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
             });
    }

    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = new ConfigurationOptions();
        redisConfig.EndPoints.Add(configuration["Redis:Host"], Convert.ToInt32(configuration["Redis:Port"]));
        redisConfig.DefaultDatabase = 0;
        services.AddStackExchangeRedisCache(opt => opt.ConfigurationOptions = redisConfig);

    }
}