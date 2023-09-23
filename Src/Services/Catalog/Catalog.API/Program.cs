var configuration = GetConfiguration();


var host = CreateHostBuilder(configuration, args);

host.Run();



IConfiguration GetConfiguration()
{
    var path = Directory.GetCurrentDirectory();

    var builder = new ConfigurationBuilder()
        .SetBasePath(path)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build(); 
}




IHost CreateHostBuilder(IConfiguration configuration, string[] args) =>

Host.CreateDefaultBuilder(args)
   .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Srartup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseWebRoot("Pics")
        .ConfigureAppConfiguration(app => app.AddConfiguration(configuration))
        .CaptureStartupErrors(false);
    })

    .Build();



   
