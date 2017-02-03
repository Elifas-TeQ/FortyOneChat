using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;
using FortyOneChat.Controls;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using FortyOneChat.Core.Models;
using FortyOneChat.Core.Services;
using FortyOneChat.Core.Helpers;
using Prism.Services;

namespace FortyOneChat.ViewModels
{
    public class PreferencePageViewModel : BindableBase
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly IApplicationContext _applicationContext;
        
        public ICommand LoginCommand { get; set; }
        private const string _serviceUri = @"http://10.129.68.199:8888/api/Chat";

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }


        public PreferencePageViewModel(INavigationService navigationService, IUserService userService, IPageDialogService pageDialogService, IApplicationContext applicationContext)
        {
            _navigationService = navigationService;
            _userService = userService;
            _pageDialogService = pageDialogService;
            _applicationContext = applicationContext;

            LoginCommand = new DelegateCommand(LogIn);
        }

        public async void LogIn()
        {
            if (!string.IsNullOrEmpty(_userName))
            {
                _userName = _userName.TrimEmptyTape().Trim();
                   
                var responce = await _userService.LogIn(_userName).ContinueWith(x => x.Result);

                if (responce == null)
                {
                    await _pageDialogService.DisplayAlertAsync("Alyarm!!!!!!", "Server returned response with NOT succcess status code during login post request.", "OK");
                }
                else
                {
                    _applicationContext.CurrentUser = responce;
                    ChatDataTemplateSelector.CurrentUserId = responce.Id;
                    await _navigationService.NavigateAsync("ChatPage");
                }

            }
        }

    }
}