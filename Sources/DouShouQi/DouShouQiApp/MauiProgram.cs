using DouShouQiApp.Pages;
using DouShouQiModel;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI.Windowing;
using Serialize;


namespace DouShouQiApp
{
    public static class MauiProgram
    {
        public static Manager Manager { get; private set;  } = new Manager(new FilePersistance(FileSystem.Current.AppDataDirectory));
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(windows =>
                {
                    windows.OnWindowCreated( window =>
                    {
                        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                        var appWindow = AppWindow.GetFromWindowId(windowId);
                        if (appWindow.Presenter is OverlappedPresenter presenter)
                        {
                            presenter.SetBorderAndTitleBar(false, false);
                            presenter.Maximize();
                        }
                    });
                });
#endif
            });

            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddTransient<HomePage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
 }