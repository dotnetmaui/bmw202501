using Microsoft.Extensions.Logging;

namespace Demo03
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            AllowMultiLineTruncation();
            return builder.Build();
        }
        static void AllowMultiLineTruncation()
        {
            static void UpdateMaxLines(Microsoft.Maui.Handlers.LabelHandler handler, ILabel label)
            {

#if ANDROID
                var textView = handler.PlatformView;
                if (label is Label controlsLabel
                    && textView.Ellipsize == Android.Text.TextUtils.TruncateAt.End)
                {
                    textView.SetMaxLines(controlsLabel.MaxLines);
                }
#elif IOS
            var textView = handler.PlatformView;
            if (label is Label controlsLabel
                && textView.LineBreakMode == UIKit.UILineBreakMode.TailTruncation) {
                textView.Lines = controlsLabel.MaxLines;
            }
#endif
            };

            Label.ControlsLabelMapper.AppendToMapping(
               nameof(Label.LineBreakMode), UpdateMaxLines);

            Label.ControlsLabelMapper.AppendToMapping(
              nameof(Label.MaxLines), UpdateMaxLines);
        }
    }


}
