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
using System.Windows.Shapes;

namespace WeSplit
{
    public partial class SettingsScreen : Window
    {
        public SettingsScreen()
        {
            InitializeComponent();
        }

        private void closeUserButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void settingsScreen_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Global.data.parameterSettings;
        }
    }
}
