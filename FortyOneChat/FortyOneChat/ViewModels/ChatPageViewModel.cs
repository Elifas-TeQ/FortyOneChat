using System;
using System.Collections.ObjectModel;
using FortyOneChat.Core;
using FortyOneChat.Core.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace FortyOneChat.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        private readonly IChatService _chatService;

        public ObservableCollection<Message> Messages { get; set; }
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