﻿namespace MSC.Shared
{
	public class TicketRequest
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
