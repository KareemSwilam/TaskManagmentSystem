

using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagemenSystem.Infrastructure.Persistence;
using TaskManagemenSystem.Infrastructure.Rspository;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.IRepository;
using TaskManagementSystem.Domain.Models;

namespace TaskManagmentSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedAccount = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<IUniteOFWork, UniteOfWork>();
            builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
            builder.Services.AddScoped<ITeamRepository, TeamRepository>();
            builder.Services.AddScoped<ITAskRepository, TAskRepository>();
            builder.Services.AddScoped<ISetupServices, SetupServices>();
            builder.Services.AddScoped<IUserServices , UserServices>();
            builder.Services.AddScoped<ITaskUserRepository, TaskUserRepository>();
            builder.Services.AddScoped<ITeamUserRepository, TeamUserRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectServices, ProjectServices>();
            builder.Services.AddScoped<ITeamServices, TeamServices>();
            builder.Services.AddScoped<ITeamUserServices, TeamUserServices>();
            builder.Services.AddScoped<ITaskServices, TaskServices>();
            builder.Services.AddScoped<ITaskUserServices, TaskUserServices>();
            //var config = TypeAdapterConfig.GlobalSettings;
            //config.Scan(typeof(Program).Assembly);
            //builder.Services.AddSingleton(config);
            //builder.Services.AddScoped<IMapper, ServiceMapper>();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AuthTest API",
                    Version = "v1"
                });

                // ?? Add JWT Authentication to Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {your token}' in the text input below.\n\nExample: 'Bearer abc123xyz'"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                            }
                          },
                          Array.Empty<string>()
                     }
                });
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(builder.Configuration["JWTConfig:Secert"]!);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                };
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") // Angular app URL
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();
            app.UseCors("AllowAngular");
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
        }
    }
}
