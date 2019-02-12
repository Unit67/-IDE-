using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace IDE
{
    /// <summary>
    /// Logika interakcji dla klasy Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }
        Lang Lang = new Lang();
        private void Button_Click_Accept(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            this.Close();
        }

        private void ComboBoxItem_Selected_Polish(object sender, RoutedEventArgs e)
        {
            Lang.SelectedLang = "PL";
            File.WriteAllText("Language.txt", "PL");
        }

        private void ComboBoxItem_Selected_English(object sender, RoutedEventArgs e)
        {
            Lang.SelectedLang = "ENG";
            File.WriteAllText("Language.txt", "ENG");
        }
    }
}
