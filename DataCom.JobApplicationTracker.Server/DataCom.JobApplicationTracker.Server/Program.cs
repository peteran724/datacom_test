using DataCom.JobApplicationTracker.Contract;
using DataCom.JobApplicationTracker.Repository;
using DataCom.JobApplicationTracker.Server.Mapper;
using DataCom.JobApplicationTracker.Server.MiddleWare;
using DataCom.JobApplicationTracker.Service;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


namespace DataCom.JobApplicationTracker.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(_ => true);
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                    });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var Configuration = builder.Configuration;

            builder.Services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = Configuration["Version"];
                    document.Info.Title = "DataCom JobApplicationTracker API";
                    document.Info.Description = "A RESTful API based on ASP.NET CORE WEBAPI";
                };
            });

            builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
            builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();

            builder.Services.AddAutoMapper(typeof(AutomapperConfig));

            builder.Services.AddMvc(options =>
            {
                options.OutputFormatters.RemoveType(typeof(HttpNoContentOutputFormatter));
                options.OutputFormatters.Insert(0, new HttpNoContentOutputFormatter
                {
                    TreatNullValueAsNoContent = false
                });
            });

            // Add services to the container.

            builder.Services.AddDbContext<JobApplicationDbContext>(
                options => options.UseInMemoryDatabase("JobApplicationsDb"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<JobApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            app.UseExHandlerMiddleware();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseRouting().UseCors().UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi();
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.Run();
        }
    }
}
