using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFormsIssue_4223
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            MyWebView.Navigating += MyWebView_Navigating;
            MyWebView.Navigated += MyWebView_Navigated;

            MyWebView.Source = new HtmlWebViewSource()
            {
                Html = @"<a href='xamforms4223://custom'>Navigate to Custom xamforms4223 scheme</a><br/><br/><a href='https://www.google.com'/>Google</a>"
            };
        }

        void MyWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            LogToScreen($"Navigating: {e.Url}");

            if(e.Url.StartsWith("xamforms4223", StringComparison.OrdinalIgnoreCase))
            {
                LogToScreen("Caught custom sceme, cancelling navigation.");
                e.Cancel = true;
            }
        }

        void MyWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            LogToScreen($"Navigated: ({e.Result}) {e.Url}");
        }

        void LogToScreen(string text)
        {
            Log.Text += $"{DateTime.Now.ToLongTimeString()}: {text}\n";
            InvalidateMeasure();
            LogScrollView.ScrollToAsync(Log, ScrollToPosition.End, false);
        }

        void Back_Clicked(object sender, EventArgs e)
        {
            if (MyWebView.CanGoBack)
            {
                MyWebView.GoBack();
            }
        }
    }
}
