using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortyOneChat.Controls.ViewCells;
using FortyOneChat.Core;
using FortyOneChat.Core.Services;
using Xamarin.Forms;

namespace FortyOneChat.Controls
{
    class ChatDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        private readonly DataTemplate _incomingMessageTemplate;
        private readonly DataTemplate _outcommingMessageTemplate;
        private readonly IApplicationContext _context;

        public ChatDataTemplateSelector()
        {
            _incomingMessageTemplate = new DataTemplate(typeof(IncomingViewCell));
            _outcommingMessageTemplate = new DataTemplate(typeof(OutcomingViewCell));
        }

        public ChatDataTemplateSelector(IApplicationContext context)
            :this()
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
           
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as Message;
            if (message == null) return null;
            return message.AuthorId == 1 ? _incomingMessageTemplate : _outcommingMessageTemplate;  
        }
    }
}
