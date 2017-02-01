using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services
{
    public interface IApplicationContext
    {
        User CurrentUser { get; }
		string ServiceUri { get; }
    }
}