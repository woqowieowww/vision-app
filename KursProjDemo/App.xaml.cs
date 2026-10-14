using System.Windows;

namespace KursProjDemo
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseService.InitializeDatabase();
        }
    }
}