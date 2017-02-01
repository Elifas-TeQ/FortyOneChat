using Xamarin.Forms;

namespace FortyOneChat.Views
{
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            InitializeComponent();
        }

        private void MessageList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessageList.SelectedItem = null;
        }

        private void MessageList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessageList.SelectedItem = null;
        }
    }
}