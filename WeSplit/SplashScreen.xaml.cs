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
using System.Windows.Threading;

namespace WeSplit
{
    public partial class SplashScreen : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void splashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = Global.data.parameterSettings;
                tipBlock.Text = Global.data.getTip();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
                dispatcherTimer.Start();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
            this.Close();
        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
            this.Close();
        }
    }
}
