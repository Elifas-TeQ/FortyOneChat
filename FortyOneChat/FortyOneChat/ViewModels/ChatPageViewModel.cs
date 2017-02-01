
using System;
using System.Collections.ObjectModel;
using FortyOneChat.Core;
using FortyOneChat.Core.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using FortyOneChat.Core.Services.Fakes;
using Prism.Commands;
using Prism.Services;
using System.Threading.Tasks;

namespace FortyOneChat.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly IChatService _chatService;

        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
        public ObservableCollection<User> OnlineUserCollection { get; set; }


        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set { SetProperty(ref _newMessage, value); }
        }
        public ICommand SendMessageCommand { get; set; }
        public ChatPageViewModel(IChatService chatService, INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            SendMessageCommand = new DelegateCommand(SendMessage);
            OnlineUserCollection = new ObservableCollection<User>();
            OnlineUserCollection.Add(new User { Id = 1, Name = "Vasyl" });
            OnlineUserCollection.Add(new User { Id = 2, Name = "Petro" });

            if (chatService == null) throw new ArgumentNullException(nameof(chatService));
            _chatService = chatService;

        }
        public void SendMessage()
        {
            var ignor = _pageDialogService.DisplayAlertAsync("Message", NewMessage, "OK");
        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (Messages == null)
            {
                Messages = new ObservableCollection<Message>(await _chatService.GetMessages());
            }
        }
    }
}