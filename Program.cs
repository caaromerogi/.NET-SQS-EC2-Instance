using SQSService.Receiver;
using SQSService.SQS;

namespace SQSService
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
            builder.Services.Configure<SQSOptions>(builder.Configuration.GetSection("SqsOptions"));
            builder.Services.AddSingleton<ISqsClientFactory, SQSClientFactory>();
            builder.Services.AddTransient < ISQSService, SQSService.SQS.SQSService>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var service = app.Services.GetRequiredService<ISQSService>();

            service.Listen();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}