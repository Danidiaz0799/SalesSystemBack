using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SalesSystem.Core;
using SalesSystem.Core.Services;
using SalesSystem.Data;
using SalesSystem.Services;

namespace SalesSystem.Sales
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
            services.AddCors(); services.AddControllers();

            string connection = Configuration.GetConnectionString("Default") ?? String.Empty;

            string? connectionString = (Environment.GetEnvironmentVariable("SqlServerConnectionBS") != null) ?
                Environment.GetEnvironmentVariable("SqlServerConnectionBS") : connection;

            services.AddDbContext<SalesSystemDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("SalesSystem.Data")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISaleService, SaleService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales System", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyOrigin()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales System");
            });
        }
    }
}
