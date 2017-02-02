using System;
using FortyOneChat.Controls.ViewCells;
using FortyOneChat.Core.Models;
using FortyOneChat.Core.Services;
using Xamarin.Forms;

namespace FortyOneChat.Controls
{
    public class ChatDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _incomingMessageTemplate;
        private readonly DataTemplate _outcommingMessageTemplate;
        
        public static int CurrentUserId { get; set; }

        public ChatDataTemplateSelector()
        {
            _incomingMessageTemplate = new DataTemplate(typeof(IncomingViewCell));
            _outcommingMessageTemplate = new DataTemplate(typeof(OutcomingViewCell));
        }
        
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as Message;
            if (message == null) return null;
			return message.Author.Id != CurrentUserId ? _incomingMessageTemplate : _outcommingMessageTemplate;  
        }
    }
}
