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
using Xamarin.Forms;

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

            OnlineUserCollection = new ObservableCollection<string>();

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
                        Author = _applicationContext.CurrentUser,
                        DateCreated = DateTime.Now
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            StopTimer = true;
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (Messages == null)
            {
                List<Message> messages = await this._chatService.GetChatHistory();
                Messages = new ObservableCollection<Message>(messages.Where(x => x.Id >= 0));
                ScrollToTheEnd(false);


            }

            _timer.RunTimer(TimeSpan.FromSeconds(5), TheLoop);
        }

        private bool TheLoop()
        {
            Task.Factory.StartNew(async () =>
            {
                int maxMessageId = -1;
                if (Messages.Any())
                {
                    maxMessageId = Messages.Select(x => x.Id).Max();
                }

                var unfilterdMessages = await _chatService.GetChatHistory();
                var newMessages = unfilterdMessages.Where(x => x.Id > maxMessageId).ToList();

                foreach (var message in newMessages)
                {
                    Messages.Add(message);
                }

                if (newMessages.Any())
                {
                    ScrollToTheEnd(true);
                }

                var authors = unfilterdMessages
                    .Where(x => x.Author.LastTimeOnline >= DateTime.UtcNow)
                    .GroupBy(x => x.Author.Name)
                    .Select(x => x.Key)
                    .ToList();
                OnlineUserCollection.Clear();
                foreach (var author in authors)
                {
                    if (!string.IsNullOrEmpty(author))
                    {
                        OnlineUserCollection.Add(author);
                    }
                }
            });
            return !StopTimer;
        }

        private void ScrollToTheEnd(bool animated)
        {
            if (Messages.Any())
            {
                var navPage = Application.Current.MainPage.Navigation.NavigationStack.Last() as NavigationPage;
                var chatPage = navPage?.CurrentPage as ChatPage;
                chatPage?.MessagesControl.ScrollTo(Messages.Last(), Xamarin.Forms.ScrollToPosition.End, animated);
            }
        }
    }
}