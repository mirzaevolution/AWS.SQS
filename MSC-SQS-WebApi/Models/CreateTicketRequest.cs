using System.ComponentModel.DataAnnotations;

namespace MSC.SQS.WebApi.Models
{
	public class CreateTicketRequest
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
	}
}
