using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using AutoDog.Logics;


namespace AutoDog.Views
{
    /// <summary>
    /// ApiFileTemplateView.xaml 的交互逻辑
    /// </summary>
    public partial class APIFileTemplateView : UserControl
    {
        public static string response = null;
        public static string cookies = null;
        public static string headers = null;  

        public APIFileTemplateView()
        {
            InitializeComponent();
        }

        private void AddParams_Click(object sender, RoutedEventArgs e)
        {

        }


        private void RunAPI_Click(object sender, RoutedEventArgs e)
        {
            RunAPI(myUrl.Text, myMethod.Text, Encoding.UTF8);
            myResponseEditor.WordWrap = true;
            myResponseEditor.Text =response;

            myCookiesEditor.WordWrap = true;
            myCookiesEditor.Text = cookies;

            myHeadersEditor.WordWrap = true;
            myHeadersEditor.Text = headers;
        }
        public static void RunAPI(string url, string method, Encoding encoding)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Credentials = CredentialCache.DefaultCredentials;
                webRequest.Method = method;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                StreamReader sr = new StreamReader(webResponse.GetResponseStream(), encoding);
                response = sr.ReadToEnd();
                cookies = webResponse.Headers.Get("Set-Cookie");
                headers = webResponse.Headers.ToString();
            }
            catch(Exception ex)
            {
                response = cookies = headers = ex.Message;
            }
            
        }

        private void myExcepted_TextChanged(object sender, EventArgs e)
        {
            bool foundMatch = false;
            try
            {
                foundMatch = Regex.IsMatch(myResponseEditor.Text, myExcepted.Text);
            }
            catch (ArgumentException ex)
            {
                myResultErrorInfo.Text = ex.Message;
            }
            if (foundMatch)
            {
                myResultImage.Source = Common.ConvertImageToBitMap("pack://application:,,/Images/Results/Pass.png");
                myResultMsg.Text = "Body matches string";
            }
            else
            {
                myResultImage.Source = Common.ConvertImageToBitMap("pack://application:,,/Images/Results/Failed.png");
                myResultMsg.Text = "Body not matches string";
            }
        }
    }
}
