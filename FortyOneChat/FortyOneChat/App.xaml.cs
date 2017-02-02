using FortyOneChat.Core.Services;
using FortyOneChat.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;

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
            //Container.RegisterType<IChatService, ChatService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IChatService, FortyOneChat.Core.Services.Fake.ChatServiceFake>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IApplicationContext, Core.Services.Fakes.ApplicationContext>(new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<ChatPage>();
            Container.RegisterTypeForNavigation<PreferencePage>();

        }
    }
}