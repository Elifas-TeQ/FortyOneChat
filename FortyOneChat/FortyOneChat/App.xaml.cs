using FortyOneChat.Core.Services;
using FortyOneChat.Services;
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
            NavigationService.NavigateAsync("MainNavigationPage/PreferencePage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IChatService, FortyOneChat.Core.Services.ChatService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IApplicationContext, Core.Services.ApplicationContext>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITaskDispatcher, XamarinTaskDispatcher>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITimer, XamarinTimer>(new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<ChatPage>();
            Container.RegisterTypeForNavigation<PreferencePage>();
            Container.RegisterTypeForNavigation<MainNavigationPage>();
        }
    }
}