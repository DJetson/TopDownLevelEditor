using System.Windows;
using TopDownLevelEditor.ViewModels;
using TopDownLevelEditor.Views;

namespace TopDownLevelEditor
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            MainWindow.Show();
        }
    }
}
