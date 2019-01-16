using alxbrn_uwp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace alxbrn_uwp.Views
{
    public sealed partial class MainPage : Page
    {
        public static HttpClient ApiClient { get; set; } = new HttpClient();

        public const string BaseUrl = "http://localhost:52343/api/";

        private JwtToken token;

        int PageSize = 2;
        int PageNumber = 1;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string AppInfo = "{'apptoken': 'v34rSMHPSeQYyXv4ghvThjXPX7Xtg4C6', 'appsecret':'NUTef7x3ptvGDdfSzPbMX2hsWSTP45Qm'}";

            HttpResponseMessage Response = await ApiClient.PostAsync(BaseUrl + "Authorize",
                 new StringContent(AppInfo, Encoding.UTF8, "application/json"));

            if (Response != null)
            {
                string json = Response.Content.ReadAsStringAsync().Result;
                token = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtToken>(json);

                Token.Text = "Token: " + token.Token;
                Expires.Text = "Expires: " + token.Expires;

                ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            }
            else
            {
                Token.Text = "token null";
                Expires.Text = "expires null";
            }
        }

        private async void LoadUsers_Click(object sender, RoutedEventArgs e)
        {
            ListViewUsers.ItemsSource = await GetUserSearch(PageSize, PageNumber, SearchQuery.Text);
        }

        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            ListViewUsers.ItemsSource = await GetUserSearch(PageSize, PageNumber--, SearchQuery.Text);
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            ListViewUsers.ItemsSource = await GetUserSearch(PageSize, PageNumber++, SearchQuery.Text);
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            ListViewUsers.ItemsSource = await GetUserSearch(PageSize, 1, SearchQuery.Text);
        }

        private async Task<ObservableCollection<User>> GetUserSearch(int pageSize, int pageNumber, string query)
        {
            HttpResponseMessage Response = await ApiClient.GetAsync(BaseUrl + $"Users/Search?PageNumber={PageNumber}&PageSize={PageSize}&QuerySearch={query}");

            if (Response != null)
            {
                string json = Response.Content.ReadAsStringAsync().Result;
                return new ObservableCollection<User>(Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json));
            }
            else
            {
                return null;
            }
        }
    }
}
