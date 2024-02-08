
using Amazon.SQS;
using MSC.SQS.WebApi.Options;
using MSC.SQS.WebApi.Services;

namespace MSC.SQS.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddSingleton<SqsOption>((sp) =>
			{
				SqsOption sqsOption = new SqsOption();
				sp.GetService<IConfiguration>()?.GetSection("SQS").Bind(sqsOption);
				return sqsOption;
			});

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddAWSService<IAmazonSQS>(builder.Configuration.GetAWSOptions());
			builder.Services.AddSingleton<ISqsService, SqsService>();
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}