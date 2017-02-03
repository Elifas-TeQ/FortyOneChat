using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using FortyOneChat.Controls;
using FortyOneChat.Droid.Renderers;

[assembly: ExportRenderer(typeof(FABControl), typeof(FAB))]
namespace FortyOneChat.Droid.Renderers
{
    public class FAB : ViewRenderer
    {
        public FAB()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            var xfControl = Element as FABControl;
            
            if(Control == null)
            {
                var fabLayout = new FabLayout(Context);
                fabLayout.Click += (obj, sender) =>
                {
                    if(xfControl.Command != null)
                        xfControl.Command.Execute(obj);
                };

                SetNativeControl(fabLayout);
            }
            
        }

        
    }

    public class FabLayout : LinearLayout
    {
        public FabLayout(Context context)
            :base(context)
        {
            var li = LayoutInflater.From(context);

            var view = li.Inflate(Resource.Layout.FAB , null);

            this.AddView(view);
        }
    }
}