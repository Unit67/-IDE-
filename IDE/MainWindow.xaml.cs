using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDE
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _SystemLocalization = System.IO.Path.GetPathRoot(Environment.SystemDirectory);

            Lang.CheckLanguage();

            if(_EditedFileLocalization == null)
            {
                if (File.ReadAllText("LokalizacjaCS.txt") != "")
                {
                    _EditedFileLocalization = File.ReadAllText("LokalizacjaCS.txt");
                }
            }


            if(_CompilerLocalization == null)
            {
                if(File.ReadAllText("LokalizacjaCSC.txt") != "")
                {
                    _CompilerLocalization = File.ReadAllText("LokalizacjaCSC.txt");
                }
            }


            if(!Directory.Exists(_SystemLocalization + "IDE"))
            {
                if(Lang.SelectedLang == "PL")
                {
                    _TheIDEfolderWasNotfoundMessage = "Nie znaleziono folderu 'IDE' na dysku systemowym czy zezwalasz na jego utworzenie? (wymagane aby kontynowac)";
                }
                else if(Lang.SelectedLang == "ENG")
                {
                    _TheIDEfolderWasNotfoundMessage = "The 'IDE' folder was not found on the system disk  do you allow it to be created? (required to continue)";
                }
                MessageBoxResult result = MessageBox.Show(_TheIDEfolderWasNotfoundMessage,"IDE", MessageBoxButton.YesNo); 
               
               if(result == MessageBoxResult.Yes)
               {
                    try
                    {
                        Directory.CreateDirectory(_SystemLocalization + "IDE");
                    }
                    catch(Exception Ex)
                    {
                        MessageBox.Show("Error: " + Ex.Message);
                    }
               }
               else
               {
                    Close();
               }
            }
        }
        private string _SystemLocalization;
        private string _CompilerLocalization;
        private string _EditedFileLocalization;
        private string _TheIDEfolderWasNotfoundMessage;
        Lang Lang = new Lang();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if(CodeEdit.Text != "")
                {
                    MessageBoxResult result = MessageBox.Show("Zapisanie pliku w tym momencie może spodować utratę jego zawartosci czy jesteś pewny?", "IDE", MessageBoxButton.YesNo);
                    if(result == MessageBoxResult.Yes)
                    {
                        SaveFile();
                    }
                }
                else
                {
                    SaveFile();
                }
            }
        }
        void SaveFile()
        {
            try
            {
                File.WriteAllText(_EditedFileLocalization, CodeEdit.Text);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error:  " + Ex.Message);
            }
        }
        private void Button_Click_Compile(object sender, RoutedEventArgs e)
        {
            if(_CompilerLocalization != null)
            {
                if(File.ReadAllText("LokalizacjaCS.txt") != "")
                {
                    Process.Start("Compile.bat");
                }
                else
                {
                    MessageBox.Show("Przed rozpoczeciem kompilacji otworz obojetnie jaki plik z rozszerzeniem cs lub napisz go i zapisz z rozszerzeniem cs", "IDE");
                }
            }
            else if(_CompilerLocalization == null)
            {
                MessageBox.Show("Przed kompilacją wskaz lokalizacje kompilatora", "IDE");
            }
        }

        private void Button_Click_Compiler_Localization(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                try
                {
                    
                    _CompilerLocalization = openFileDialog.FileName;
                    File.WriteAllText("LokalizacjaCSC.txt", _CompilerLocalization);
                }
                catch(Exception Ex)
                {
                    MessageBox.Show("Error: " + Ex.Message);
                }
                
            }
        }

        private void Button_Click_Save_File(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.cs,)|*.cs;|Wszystkie pliki (*.*)|*.*";
            if(saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, CodeEdit.Text);
                File.WriteAllText("LokalizacjaCS.txt",saveFileDialog.FileName);
                _EditedFileLocalization = saveFileDialog.FileName;
            }
        }

        private void Button_Click_Open_File(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.cs,)|*.cs;|Wszystkie pliki (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                CodeEdit.Text = File.ReadAllText(openFileDialog.FileName);
                File.WriteAllText("LokalizacjaCS.txt", openFileDialog.FileName);
                _EditedFileLocalization = openFileDialog.FileName;
            }
        }

        private void ComboBoxItem_Selected_CS(object sender, RoutedEventArgs e)
        {
            CodeEdit.ShowLineNumbers = true;
            CodeEdit.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("C#");
        }

        private void Button_Click_Settings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
