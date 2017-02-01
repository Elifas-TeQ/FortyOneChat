using System;

namespace FortyOneChat.Core.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		DateTime LastDateOnline { get; set; }
	}
}