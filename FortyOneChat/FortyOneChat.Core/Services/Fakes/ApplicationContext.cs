using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services.Fakes
{
    public class ApplicationContext : IApplicationContext
    {
        private User FakedUser;
		private string serviceUri = @"http://192.168.127.2:7777/api/Chat";
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
            set
            {
                
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