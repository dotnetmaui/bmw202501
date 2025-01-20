using Demo01.Models;
using Newtonsoft.Json;
using System.Text;

namespace Demo01
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void btnNo_Clicked(object sender, EventArgs e)
        {
            await SendFeedbackAsync(false);
        }

        private async void btnYes_Clicked(object sender, EventArgs e)
        {
            await SendFeedbackAsync(true);
        }

        private async Task SendFeedbackAsync(bool isRecommended)
        {
            // 사용자 입력 데이터 수집
            var feedback = new FeedbackModel
            {
                Name = textName.Text,
                Query = textQnA.Text,
                IsRecommended = isRecommended
            };

            try
            {
                // JSON 직렬화
                string json = JsonConvert.SerializeObject(feedback);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // API URL 설정
                string apiUrl = "http://n8ndemo.jojangwon.com/webhook/feedback";

                // POST 요청
                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("성공", "피드백이 전송되었습니다.", "확인");
                }
                else
                {
                    await DisplayAlert("오류", $"서버 오류: {response.StatusCode}", "확인");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("오류", $"예외 발생: {ex.Message}", "확인");
            }
        }
    }
}
