using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GhostWindows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            var window1 = new MainWindow();
            window1.Show();
            await Task.Delay(1000);
            window1.Hide();
            var window2 = new MainWindow();
            window2.Show();

            // Then tap Ctrl + ALt + Del or Win + L

            base.OnStartup(e);
        }
    }
}
