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
using FortyOneChat.ViewModels;
using FortyOneChat.Core.Helpers;
using FortyOneChat.Services;
using System.Threading.Tasks;
using System.Linq;
using FortyOneChat.Views;

namespace FortyOneChat.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly IChatService _chatService;
		private readonly IUserService _userService;
		private readonly IApplicationContext _applicationContext;
        private readonly ITimer _timer;

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

        public bool StopTimer;

        public ChatPageViewModel(
            IChatService chatService, 
            IPageDialogService pageDialogService, 
            IUserService userService, 
            IApplicationContext applicationContext,
            ITimer timer)
        {
			this._userService = userService;
            this._pageDialogService = pageDialogService;
			this._applicationContext = applicationContext;
            this._timer = timer;
            this.SendMessageCommand = new DelegateCommand(async () => { await SendMessage(); });
           
            List<string> users = this._userService.GetOnlineUsers().Result;
            OnlineUserCollection = new ObservableCollection<string>(users);
            OnlineUserCollection.Add("Andriy");
            OnlineUserCollection.Add("Valeriy");
            OnlineUserCollection.Add("Oleksiy");
            OnlineUserCollection.Add("Shasha");
            OnlineUserCollection.Add("Ruslan");
            OnlineUserCollection.Add("Petro Glazurko");

            if (chatService == null)
            {
                throw new ArgumentNullException(nameof(chatService));
            }
            _chatService = chatService;
        }
        public async Task SendMessage()
        {
            if (!String.IsNullOrEmpty(_newMessage))
            {
                _newMessage = _newMessage.TrimEmptyTape().Trim();

                if (!String.IsNullOrEmpty(_newMessage))
                {
                    var message = new Message
                    {
                        Text = _newMessage,
                        Author = new User { Id = this._applicationContext.CurrentUser.Id }
                    };

                    bool isSuccess = await this._chatService.SendMessage(message);

                    if (!isSuccess)
                    {
                        _pageDialogService.DisplayAlertAsync("Message", "Message wasn't successfuly sended.", "OK");
                    }
                }
            }
            NewMessage = string.Empty;
        }

        private void AddMessage(Message message)
        {
            if(!String.IsNullOrEmpty(message.Text))
            {
                Messages.Add(message);
                NewMessage = String.Empty;
            }
        }
        
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            StopTimer = true;
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (Messages == null)
            {
				List<Message> messages = await this._chatService.GetChatHistory(); 
				Messages = new ObservableCollection<Message>(messages);
            }

            _timer.RunTimer(TimeSpan.FromSeconds(5), TheLoop);
        }

        private bool TheLoop()
        {
            Task.Factory.StartNew(async () => {
                int maxMessageId = 0;
                if (Messages.Any())
                {
                    maxMessageId = Messages.Select(x => x.Id).Max();
                }

                //var messagesFromServer = (await _chatService.GetChatHistory()).Where(x => x.Id > maxMessageId).ToList();

                //foreach(var message in messagesFromServer)
                //{
                //    Messages.Add(message);
                //}

                Messages.Clear();

                var messages = await _chatService.GetChatHistory();
                foreach (var item in messages)
                {
                    Messages.Add(item);
                }
                

                var page = (App.Current.MainPage.Navigation.NavigationStack.Last() as ChatPage);
                page.MessagesControl.ScrollTo(Messages.LastOrDefault(), Xamarin.Forms.ScrollToPosition.MakeVisible, true);

            });
            return !StopTimer;
        }
    }
}