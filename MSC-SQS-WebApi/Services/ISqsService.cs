using MSC.SQS.WebApi.Models;

namespace MSC.SQS.WebApi.Services
{
	public interface ISqsService
	{
		Task<CreateTicketResponse> Send(CreateTicketRequest ticket);
	}
}
