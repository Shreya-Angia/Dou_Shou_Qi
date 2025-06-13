using DouShouQiApp;
using DouShouQiApp.Pages;
using DouShouQiModel;

namespace DouShouQiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public Game CurrentGame { get; set; }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}