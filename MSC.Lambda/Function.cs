using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using MSC.Shared;
using System;
using System.Text.Json;
using System.Threading.Tasks;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MSC.Lambda
{
	public class Function
	{
		/// <summary>
		/// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
		/// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
		/// region the Lambda function is executed in.
		/// </summary>
		public Function()
		{

		}


		/// <summary>
		/// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
		/// to respond to SQS messages.
		/// </summary>
		/// <param name="evnt"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
		{
			string creator = Environment.GetEnvironmentVariable("Creator");
			if (!string.IsNullOrEmpty(creator))
			{
				context.Logger.Log($"Lambda creator: {creator}");
			}
			foreach (var message in evnt.Records)
			{
				await ProcessMessageAsync(message, context);
			}
		}

		private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
		{
			TicketRequest ticketRequest = null;
			try
			{
				ticketRequest = JsonSerializer.Deserialize<TicketRequest>(message.Body);
			}
			catch (Exception ex)
			{
				context.Logger.LogError(ex.ToString());
			}
			if (ticketRequest != null)
			{
				context.Logger.LogLine($"Processed message {message.Body}");
				context.Logger.Log("-----------------------------");
				context.Logger.Log($"Id: {ticketRequest.Id}");
				context.Logger.Log($"Name: {ticketRequest.Name}");
				context.Logger.Log($"Email: {ticketRequest.Email}");
				context.Logger.Log("-----------------------------");
			}
			else
			{
				context.Logger.Log("Cannot process the message. The deserialized message is null.");

			}
			await Task.CompletedTask;
		}
	}
}
