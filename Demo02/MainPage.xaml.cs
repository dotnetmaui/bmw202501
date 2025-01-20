using Newtonsoft.Json;

namespace Demo02
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
#if ANDROID
            if (!OneSignalSDK.DotNet.OneSignal.Notifications.Permission)
            {
                var result = await OneSignalSDK.DotNet.OneSignal.Notifications.RequestPermissionAsync(true);
                if (!result)
                {
                    await App.Current.MainPage.DisplayAlert("안내", "알림 기능을 켜주세요", "확인");
                }
            }
#endif
            using (HttpClient _httpClient = new HttpClient())
            {


                busy.IsRunning = true;
                busy.IsVisible = true;

                var apiUrl = "https://n8ndemo.jojangwon.com/webhook/movie";
                var response = await _httpClient.GetAsync(apiUrl);

                try
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var json = await response.Content.ReadAsStringAsync();
                        // JSON을 dynamic 객체로 변환
                        dynamic data = JsonConvert.DeserializeObject<dynamic>(json);

                        // 동적으로 값 접근
                        string movie = data.movie;
                        string quote = data.quote;

                        await DisplayAlert("안내", $"영화 {movie}의 명대사 : {quote}", "확인");
                    }
                    else
                    {
                        await DisplayAlert("오류", $"서버 오류: {response.StatusCode}", "확인");
                    }
                }
                catch (Exception ex)
                {

                }
            }

            busy.IsRunning = false;
            busy.IsVisible = false;            
        }
    }
}
