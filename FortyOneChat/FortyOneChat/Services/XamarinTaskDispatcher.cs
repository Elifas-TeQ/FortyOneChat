using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Services
{
    public class XamarinTaskDispatcher : ITaskDispatcher
    {
        public void RunOnUIThread(Action action)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(action);
        }
    }
}
