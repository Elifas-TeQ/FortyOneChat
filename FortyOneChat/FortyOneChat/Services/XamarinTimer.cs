using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Services
{
    public class XamarinTimer : ITimer
    {
        public void RunTimer(TimeSpan frequency, Func<bool> callback)
        {
            Xamarin.Forms.Device.StartTimer(frequency, callback);
        }
    }
}
