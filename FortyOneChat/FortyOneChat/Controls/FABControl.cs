using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FortyOneChat.Controls
{
    public class FABControl : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create<FABControl, ICommand>(p => p.Command, null);
        
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        
    }
}
