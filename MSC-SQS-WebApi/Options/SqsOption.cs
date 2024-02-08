namespace MSC.SQS.WebApi.Options
{
	public class SqsOption
	{
		public TicketRequestQueue? TicketRequestQueue { get; set; }
	}
	public class TicketRequestQueue
	{
		public string? Url { get; set; }
	}
}
