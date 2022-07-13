using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PasswordManagementApi.Infrastructure;
using PasswordManagementApi.Infrastructure.DataAccess;
using PasswordManagementApi.Infrastructure.DataAccess.InMemory;
using PasswordManagementApi.Infrastructure.PasswordGenerator;

namespace PasswordManagementApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appConfig = new AppConfig();
            Configuration.Bind(appConfig);
            services.AddTransient(factory => appConfig);

            services.AddControllers();

            services.AddTransient<IPasswordGenerator, NumericPassworgGenerator>();
            
            services.AddTransient<IRepository, InMemoryRepository>();

            services.AddTransient<PasswordManagementService>(f => new PasswordManagementService(
                f.GetRequiredService<IRepository>(),
                f.GetRequiredService<IPasswordGenerator>(),
                appConfig.PasswordTimeToLiveSeconds
                ));
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
                app.UseHsts();
                app.UseExceptionHandler("/error");
            }

            var repo = app.ApplicationServices.GetRequiredService<IRepository>();
            var pwdGenerator = app.ApplicationServices.GetRequiredService<IPasswordGenerator>();
            var seeder = new RepositorySeeder(repo, pwdGenerator);
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
