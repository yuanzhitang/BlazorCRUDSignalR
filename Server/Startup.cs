using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using BlazorCRUDSingalR.Server.Data;
using Microsoft.EntityFrameworkCore;
using BlazorCRUDSingalR31.Server.Hubs;

namespace BlazorCRUDSignalR.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSignalR();
			services.AddControllersWithViews();
			services.AddRazorPages();
			services.AddResponseCompression(option =>
			{
				option.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
					   new[]
					   {
							"application/octet-stream"
					   });
			});
			services.AddDbContext<EmployeeDbContext>(option =>
			{
				option.UseSqlServer(Configuration.GetConnectionString("EmployeeDBContext"));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EmployeeDbContext context)
		{
			app.UseResponseCompression();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			context.Database.Migrate();

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapHub<EmployeeHub>("/EmployeeHub");
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
