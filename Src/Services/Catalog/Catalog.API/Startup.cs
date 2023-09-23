using Microsoft.Extensions.Logging;

namespace eShop.Services.Catalog.CatalogAPI;

public class Srartup
{
    public IConfiguration Configuration { get;}

    public Srartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigurationServices (IServiceCollection services)
    {
        services.AddCustomMVC(Configuration)
        .AddSwagger(Configuration);
    }

    //this method is called in runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory LoggerFactory)
    {

        var pathBase = Configuration["PATH_BASE"];

        if (!string.IsNullOrEmpty(pathBase))
        {
            LoggerFactory.CreateLogger<Srartup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
            app.UsePathBase(pathBase);
        }

        app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase:string.Empty)}/swagger/v1/swagger.json", "Catalog.API V1");
                });


        app.UseRouting ();
        app.UseCors("CorsP{olicy"); 
        app.UseEndpoints (endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
       
        });      
    }
}


public static class CustomExtensionMethods
{
    public static IServiceCollection AddCustomMVC(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);


        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }


    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "eShop-Catalog HTTP API",
                Version = "v1",
                Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
            });

        });

        return services;
    }










}




