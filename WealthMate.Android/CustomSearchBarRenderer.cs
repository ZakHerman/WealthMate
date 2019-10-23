using Android.Content;
using Android.Widget;
using WealthMate.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace WealthMate.Droid
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                LinearLayout linearLayout = this.Control.GetChildAt(0) as LinearLayout;
                linearLayout = linearLayout?.GetChildAt(2) as LinearLayout;
                linearLayout = linearLayout?.GetChildAt(1) as LinearLayout;

                linearLayout.Background = null; //removes underline

                AutoCompleteTextView textView = linearLayout.GetChildAt(0) as AutoCompleteTextView; //modify for text appearance customization 
            }
        }
    }
}