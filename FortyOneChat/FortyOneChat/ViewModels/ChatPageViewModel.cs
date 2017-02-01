
using System;
using System.Collections.ObjectModel;
using FortyOneChat.Core;
using FortyOneChat.Core.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;

namespace FortyOneChat.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        private readonly IChatService _chatService;

        public ObservableCollection<Message> Messages { get; set; }

        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set { SetProperty(ref _newMessage, value); }
        }

        public ICommand SendMessage { get; set; }
        public ChatPageViewModel(IChatService chatService)
        {
            if (chatService == null) throw new ArgumentNullException(nameof(chatService));

            _chatService = chatService;
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