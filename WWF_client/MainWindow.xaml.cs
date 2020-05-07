using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;


namespace WWF_client
{

    public partial class MainWindow : Window
    {
        private string _connectionUrl = SourceUrl + "News/";
        private const string SourceUrl = " http://f5961c5b.ngrok.io/api/";
        private const string NewsUrl = "News/";
        private const string NewsDUrl = "NewsDetails/";
        private const string ContinentsUrl = "Continent/";
        private const string StatusUrl = "Status/";
        private const string CategoryUrl = "Category/";
        private const string AnimalUrl = "Animal/";
        private const string AocUrl = "AnimalOnContinents/";



        public MainWindow()
        {
            InitializeComponent();
            RequestTextbox.Text = Get(_connectionUrl);
        }

       
        private string Get(string url)
        {
            var req = WebRequest.Create(url);
            var resp = req.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var Out = JsonConvert.DeserializeObject(sr.ReadToEnd());
            sr.Close();
            return Out.ToString();
        }

        private string GetId(string url, int id)
        {
            var req = WebRequest.Create(url + id);
            var resp = req.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var Out = JsonConvert.DeserializeObject(sr.ReadToEnd());
            sr.Close();
            return Out.ToString();
        }
        private string Post(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = data;
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                streamReader.ReadToEnd();
                return "Запись успешно добавлена.";
            }
        }
        private string Put(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = data;
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                streamReader.ReadToEnd();
                return "Запись успешно отредактирована.";
            }
        }
        private string Delete(string url, int deleteId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.DeleteAsync(_connectionUrl + deleteId).Result;
                if (response.IsSuccessStatusCode)
                {
                    return "Успешно удалена запись с ID " + deleteId;
                }
                return "Удалить не получилось.";
            }
        }

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button)?.Name)
            {
                case "NewsButton":
                    _connectionUrl = SourceUrl + NewsUrl;
                    UrlTextBlock.Text = _connectionUrl;
                    RequestTextbox.Text = Get(_connectionUrl);
                    break;
                case "NewsDetailsButton":
                    _connectionUrl = SourceUrl + NewsDUrl;
                    UrlTextBlock.Text = _connectionUrl;
                    RequestTextbox.Text = Get(_connectionUrl);
                    break;
                case "ContinentsButton":
                    _connectionUrl = SourceUrl + ContinentsUrl;
                    UrlTextBlock.Text = _connectionUrl;
                    RequestTextbox.Text = Get(_connectionUrl);
                    break;
                case "StatusButton":
                    _connectionUrl = SourceUrl + StatusUrl;
                    UrlTextBlock.Text = _connectionUrl;
                    RequestTextbox.Text = Get(_connectionUrl);
                    break;
                case "CategoriesButton":
                    _connectionUrl = SourceUrl + CategoryUrl;
                    UrlTextBlock.Text = _connectionUrl;
                    RequestTextbox.Text = Get(_connectionUrl);
                    break;
                case "AnimalsButton":
                    _connectionUrl = SourceUrl + AnimalUrl;
                    UrlTextBlock.Text = _connectionUrl;
                    RequestTextbox.Text = Get(_connectionUrl);
                    break;
                case "AnimalsOnContinentsButton":
                    _connectionUrl = SourceUrl + AocUrl;
                    UrlTextBlock.Text = _connectionUrl;
                    RequestTextbox.Text = Get(_connectionUrl);
                    break;
            }
        }

        private void RequestTextbox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (RequestTextbox != null && UrlTextBlock != null)
            {
                UrlTextBlock.Text = _connectionUrl;
            }
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button)?.Name)
            {
                case "GetButton":
                    try
                    {
                        RequestTextbox.Text = Get(_connectionUrl);
                    }
                    catch (Exception ex)
                    {
                        RequestTextbox.Text = ex.ToString();
                    }
                    break;
                case "PostButton":
                    try
                    {
                        RequestTextbox.Text = Post(_connectionUrl, RequestTextbox.Text);
                    }
                    catch (Exception ex)
                    {
                        RequestTextbox.Text = ex.ToString();
                    }
                    break;
                case "PutButton":
                    try
                    {
                        RequestTextbox.Text = Put(_connectionUrl, RequestTextbox.Text);
                    }
                    catch (Exception ex)
                    {
                        RequestTextbox.Text = ex.ToString();
                    }
                    break;
                case "DeleteButton":
                    try
                    {
                        var deleteId = Convert.ToInt32(DeteleIdTextbox.Text);
                        RequestTextbox.Text = Delete(_connectionUrl, deleteId);
                    }
                    catch (Exception ex)
                    {
                        RequestTextbox.Text = ex.ToString();
                    }
                    break;

                case "GetIdButton":
                    try
                    {
                        var getId = Convert.ToInt32(DeteleIdTextbox.Text);
                        RequestTextbox.Text = GetId(_connectionUrl, getId);
                    }
                    catch (Exception ex)
                    {
                        RequestTextbox.Text = ex.ToString();
                    }
                    break;
            }
        }
    }
}
