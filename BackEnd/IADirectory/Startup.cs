using Api.ChatProject.Configurations;
using Api.ChatProject.Filters;
using Application.Cqrs.User.Commands;
using Application.Interfaces.Auths;
using Application.Interfaces.ConnectionChatGPT;
using Application.Interfaces.DataInfo;
using Application.Interfaces.Email;
using Application.Interfaces.History;
using Application.Interfaces.SaveImage;
using Application.Interfaces.Scraping;
using Application.Interfaces.SendEmail;
using Application.Interfaces.User;
using Application.Options;
using Application.Services.Auths;
using Application.Services.ConnectionChatGPT;
using Application.Services.DataInfo;
using Application.Services.Email;
using Application.Services.History;
using Application.Services.SaveImage;
using Application.Services.Scraping;
using Application.Services.SendEmail;
using Application.Services.User;
using Application.Services.WeeklyTaskService;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ChatProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChatProjectApplicationDBContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("ChatProjectConnection"));
            });

            services.AddControllers();

            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISendEmailService, SendEmailService>();
            services.AddScoped<ISaveImage, SaveImage>();
            services.AddScoped<IDataInfoService, DataInfoService>();
            services.AddScoped<IScrapingService, ScrapingService>();
            services.AddScoped<IConnectionChatGPT, ConnectionChatGPT>();
            services.AddHostedService<WeeklyTaskService>();
            services.AddCors();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))

                };
            });

            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(PostUserCommand).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatProject", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.TagActionsBy(api => api.GroupName);
                c.DocInclusionPredicate((name, api) => true);
            });
            services.RegisterAutoMapper();

            services.AddMvc(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "PublicFiles", "Images")),
            //    RequestPath = "/Images"
            //});

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatProject");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
