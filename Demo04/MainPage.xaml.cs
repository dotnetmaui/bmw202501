namespace Demo04
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "https://n8ndemo.jojangwon.com/webhook/getcoupon";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            // 쿠폰 코드 입력 여부 확인
            if (string.IsNullOrWhiteSpace(entryCoupon.Text))
            {
                await ShowAlert("알림", "쿠폰코드를 정확히 입력해주세요");
                return;
            }

            // 키보드 내리기
            entryCoupon.Unfocus();

            // API 호출 및 결과 처리
            await FetchCouponAsync(entryCoupon.Text);

            // 입력 필드 초기화
            entryCoupon.Text = string.Empty;
        }

        private async Task FetchCouponAsync(string couponCode)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var fullUrl = $"{Url}?code={couponCode}";
                    var response = await client.GetAsync(fullUrl);
                    var message = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await ShowAlert("성공 알림", message);
                    }
                    else
                    {
                        await ShowAlert("실패 알림", message);
                    }
                }
            }
            catch (Exception ex)
            {
                await ShowAlert("오류", $"쿠폰 처리 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private async Task ShowAlert(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "확인");
        }

        /*
         샘플 쿠폰 리스트:
         0: HV5YPJ9W
         1: K1MX7C66
         2: SV0BM8AR
         3: 87SWFH67
         4: 9DF0EF6I
         5: F6SBRZW0
         6: KDHRC1LY
         7: 3L7J98X2
         8: 3OR6B9FT
         9: 7EB09A5E
         */
    }
}
