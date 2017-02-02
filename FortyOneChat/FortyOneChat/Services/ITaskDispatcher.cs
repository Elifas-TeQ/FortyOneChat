using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Services
{
    public interface ITaskDispatcher
    {
        void RunOnUIThread(Action action);
    }
}
