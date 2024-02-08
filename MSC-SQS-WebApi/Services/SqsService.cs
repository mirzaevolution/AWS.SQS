using Amazon.SQS;
using Amazon.SQS.Model;
using MSC.Shared;
using MSC.SQS.WebApi.Models;
using MSC.SQS.WebApi.Options;
using System.Text.Json;

namespace MSC.SQS.WebApi.Services
{
	public class SqsService : ISqsService
	{
		private readonly IAmazonSQS _sqsClient;
		private readonly SqsOption _sqsOption;

		public SqsService(
				IAmazonSQS sqsClient,
				SqsOption sqsOption
			)
		{
			_sqsClient = sqsClient;
			_sqsOption = sqsOption;
		}

		public async Task<CreateTicketResponse> Send(CreateTicketRequest createTicketRequest)
		{
			if (createTicketRequest == null)
			{
				throw new ArgumentNullException(nameof(createTicketRequest));
			}
			var ticket = new TicketRequest
			{
				Email = createTicketRequest.Email,
				Name = createTicketRequest.Name
			};
			var response = await _sqsClient.SendMessageAsync(new SendMessageRequest
			{
				QueueUrl = _sqsOption.TicketRequestQueue?.Url,
				MessageBody = JsonSerializer.Serialize(ticket, new JsonSerializerOptions
				{
					WriteIndented = true,
				}),
				MessageAttributes = new Dictionary<string, MessageAttributeValue>
				{
					{ "Creator", new MessageAttributeValue{ DataType = "String", StringValue = "MSC-SQS-WebApi" } }
				}
			});
			return new CreateTicketResponse
			{
				MessageId = response.MessageId
			};
		}
	}
}
