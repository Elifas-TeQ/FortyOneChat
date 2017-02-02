using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Services
{
    public interface ITimer
    {
        void RunTimer(TimeSpan frequency, Func<bool> callback);
    }

    
}
