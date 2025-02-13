using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using REcommEND.Data;
using REcommEND.Data.Mappings;
using REcommEND.Services;
using REcommEND.Services.Configuration;
using REcommEND.Services.IMDBApi;
using System;
using System.Threading.Tasks;

namespace REcommEND
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
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Populate);

            /*The registration of UserService as a singleton object that�ll be automatically available for injection throughout the project;
The Cors policy previously mentioned. Here, you�re just allowing any method, origin, header and credentials that arrive at the house
(which wouldn�t be appropriate for production security reasons).*/

            services.AddDbContext<RecommendationsDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new IMDBMoviesMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IIMDBApiClient, IMDBApiClient>();
            services.AddCors(o => o.AddPolicy("ReactPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       /*.AllowCredentials()*/;
            }));
            services.AddTransient<UserService>();



            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Place Info Service API",
                    Version = "v2",
                    Description = "Sample service for Learner",
                });
            });


            // Setup options with DI
            services.AddOptions();

            // Configure MyOptions using config by installing Microsoft.Extensions.Options.ConfigurationExtensions
            services.Configure<IMDBConfigurationOptions>(Configuration);
            
            // Configure MyOptions using code
            services.Configure<IMDBConfigurationOptions>(myOptions =>
            {
                myOptions.apiKey = Environment.GetEnvironmentVariable("IMDBAPIKEY", EnvironmentVariableTarget.User);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
               // options.RoutePrefix = "";
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services");
                options.RoutePrefix = string.Empty;
            });

            app.UseAuthorization();
            app.UseCors("ReactPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
