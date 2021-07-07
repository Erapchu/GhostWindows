using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GhostWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault()?.Show();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault()?.Hide();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SwitchCacheModeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RootGrid.CacheMode is null)
                RootGrid.CacheMode = new BitmapCache();
            else
                RootGrid.CacheMode = null;
        }
    }
}
