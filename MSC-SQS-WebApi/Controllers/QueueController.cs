using Microsoft.AspNetCore.Mvc;
using MSC.SQS.WebApi.Models;
using MSC.SQS.WebApi.Services;

namespace MSC.SQS.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QueueController : ControllerBase
	{
		private readonly ISqsService _sqsService;

		public QueueController(ISqsService sqsService)
		{
			_sqsService = sqsService;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateTicketRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var response = await _sqsService.Send(request);
			return Ok(response);
		}
	}
}
