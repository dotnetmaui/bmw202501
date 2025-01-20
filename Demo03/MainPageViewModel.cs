using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo03.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using static Supabase.Realtime.PostgresChanges.PostgresChangesOptions;

namespace Demo03
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<YoutubeSummary> youtubeSummaries;

        Supabase.Client client;                

        [RelayCommand]
        public async void BtnCopyAllClicked(YoutubeSummary youtubeSummary)
        {
            await Clipboard.SetTextAsync(youtubeSummary.OriginText);
            await App.Current.MainPage.DisplayAlert("알림", "복사가 완료되었습니다", "확인");
        }

        [RelayCommand]
        public async void BtnYoutubeClicked(YoutubeSummary youtubeSummary)
        {
            await Launcher.OpenAsync("https://www.youtube.com/watch?v=HjTUxVa03VU");
        }

        public MainPageViewModel()
        {
            client = new Supabase.Client("https://unypozjstlkzhakzqyrt.supabase.co",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVueXBvempzdGxremhha3pxeXJ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzYzMzkzODMsImV4cCI6MjA1MTkxNTM4M30.SDPMQdIhf00_LmynTh_1Bf6cuXyiidPZAHSU2vZ865Q");

            _= Init();
        }

        private async Task Init()
        {
            await client.Realtime.ConnectAsync();

            var result = await client.From<YoutubeSummary>().Get();

            YoutubeSummaries = new ObservableCollection<YoutubeSummary>(result.Models.ToObservableCollection().OrderByDescending(_=>_.CreateAt));

            await client.From<YoutubeSummary>().On(ListenType.Inserts, (sender, change) =>
            {
                var model = change.Model<YoutubeSummary>();
                YoutubeSummaries.Insert(0,model);

            });

            await client.From<YoutubeSummary>().On(ListenType.Deletes, (sender, change) =>
            {
                var model = change.OldModel<YoutubeSummary>();
                var item = YoutubeSummaries.FirstOrDefault(x => x.Id == model.Id);
                if (item != null)
                {
                    YoutubeSummaries.Remove(item);
                }
            });

        }

    }

    public static class CollectionExtensions
    {
        /// <summary>
        /// Converts an IEnumerable<T> to an ObservableCollection<T>.
        /// </summary>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            // Null 체크
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new ObservableCollection<T>(source);
        }
    }
}
