namespace Demo05
{
    public partial class MainPage : ContentPage
    {
        string originUrl = "https://n8ndemo.jojangwon.com/webhook-test/imageupload/url?path=";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnUrl_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryUrl?.Text))
            {
                await DisplayAlert("오류", "URL을 입력해주세요.", "확인");
                return;
            }

            try
            {
                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{originUrl}{Uri.EscapeDataString(entryUrl.Text)}");
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                Console.WriteLine(result);
                await DisplayAlert("성공", "요청이 성공적으로 처리되었습니다.", "확인");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
                await DisplayAlert("오류", "요청 중 문제가 발생했습니다. 다시 시도해주세요.", "확인");
            }
        }


        private async void btnFileUpload_Clicked(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://n8ndemo.jojangwon.com/webhook-test/imageupload/file");

            var result = await PickAndShow(new PickOptions
            {
                PickerTitle = "Please select a file",
                FileTypes = FilePickerFileType.Images,
            });

            // MultipartFormDataContent를 생성하여 파일과 파일명을 추가
            var multipartContent = new MultipartFormDataContent();
            var fileStream = File.OpenRead(result.FullPath);
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // MIME 타입 설정

            // 파일명을 Content-Disposition에 추가
            multipartContent.Add(streamContent, "file", Path.GetFileName(result.FullPath));
            request.Content = multipartContent;
            // 요청 전송 및 응답 확인
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }


        public async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        var image = ImageSource.FromStream(() => stream);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }

}
