using System;
using System.Collections.ObjectModel;
using FortyOneChat.Core.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Prism.Commands;
using Prism.Services;
using System.Collections.Generic;
using FortyOneChat.Core.Models;

namespace FortyOneChat.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly IChatService _chatService;
		private readonly IUserService _userService;
		private readonly IApplicationContext _applicationContext;

        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
		private ObservableCollection<string> _onlineUserCollection;
		public ObservableCollection<string> OnlineUserCollection
		{
			get { return _onlineUserCollection; }
			set { SetProperty(ref _onlineUserCollection, value); }
		}
        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set { SetProperty(ref _newMessage, value); }
        }
        public ICommand SendMessageCommand { get; set; }
		public ChatPageViewModel(IChatService chatService, INavigationService navigationService, IPageDialogService pageDialogService, IUserService userService, IApplicationContext applicationContext)
        {
			_userService = userService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
			this._applicationContext = applicationContext;
            SendMessageCommand = new DelegateCommand(SendMessage);
			//List<string> users = this._userService.GetOnlineUsers().Result;
			//OnlineUserCollection = new ObservableCollection<string>(users);
			OnlineUserCollection = new ObservableCollection<string>();
            OnlineUserCollection.Add("Vasyl");
            OnlineUserCollection.Add("Petro");

            if (chatService == null) throw new ArgumentNullException(nameof(chatService));
            _chatService = chatService;

        }
        public void SendMessage()
        {
			var message = new Message
			{
				Text = this.NewMessage,
				Author = new User { Id = this._applicationContext.CurrentUser.Id }
			};
			bool isSuccess = this._chatService.SendMessage(message).Result;
			if (!isSuccess)
			{
				_pageDialogService.DisplayAlertAsync("Message", "Message wasn't successfuly sended.", "OK");
			}
        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (Messages == null)
            {
				List<Message> messages = await this._chatService.GetChatHistory(); 
				Messages = new ObservableCollection<Message>(messages);
            }
        }
    }
}