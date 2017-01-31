using FortyOneChat.Views;
using Prism.Unity;
using Xamarin.Forms;

namespace FortyOneChat
{
	public partial class App : PrismApplication
    {
		public App()
		{
			InitializeComponent();
		}
        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("ChatPage");
        }

        protected override void RegisterTypes()
        {
            
            Container.RegisterTypeForNavigation<ChatPage>();
            Container.RegisterTypeForNavigation<PreferencePage>();
        }
    }
}
