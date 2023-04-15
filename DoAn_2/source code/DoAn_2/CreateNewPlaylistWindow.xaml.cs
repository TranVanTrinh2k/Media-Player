using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Path = System.IO.Path;

namespace DoAn_2
{
    /// <summary>
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        public SaveWindow()
        {
            InitializeComponent();
        }
      
        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }
        private void Create_Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxFileName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên file !");
            }
            else
            {
                var path = GetThisFilePath();
                var directory = Path.GetDirectoryName(path);
                string newpath = directory + @"\" + TextBoxFileName.Text + ".txt";
                if (!File.Exists(newpath))
                {
                    var file = File.Create(newpath);
                    file.Close();
                    AutoClosingMessageBox.Show("Created","Media Player", 1000);
                }
                else
                {
                    AutoClosingMessageBox.Show($"Đã tồn tại file {TextBoxFileName.Text}.txt ! Hãy nhập tên khác.","Media Player",1000);
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxFileName.Text = "default_name";
        }
    }
}
