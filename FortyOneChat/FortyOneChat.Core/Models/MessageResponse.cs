namespace FortyOneChat.Core
{
	public class MessageResponse
	{
		public Message[] Messages { get; set; }
		public MessageResponseStatusCode StatusCode { get; set; }
	}
}
