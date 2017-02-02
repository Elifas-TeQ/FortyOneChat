using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services.Fakes
{
    public class ApplicationContext : IApplicationContext
    {
        private User FakedUser;
		private string serviceUri = @"http://10.129.68.199:8888/api/Chat";
        public ApplicationContext()
        {
            FakedUser = new User { Id = 0, Name = "Vasya" };
        }
        public User CurrentUser
        {
            get
            {
                return FakedUser;
            }
        }
		public string ServiceUri
		{
			get
			{
				return this.serviceUri;
			}
		}
    }
}