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
    public partial class NotificationScreen : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public int saveCommand { get; set; }

        public NotificationScreen(int command)
        {
            InitializeComponent();
            saveCommand = command;
        }

        private void notificationScreen_Loaded(object sender, RoutedEventArgs e)
        {
            if (saveCommand == 0)
            {
                textBlock.Text = "Thêm mới thành công";
            }

            else if (saveCommand == 1)
            {
                textBlock.Text = "Cập nhật thành công";
            }

            else if (saveCommand == 3)
            {
                textBlock.Text = "Xóa thành công";
            }

            dispatcherTimer.Interval = TimeSpan.FromSeconds(2);
            dispatcherTimer.Start();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            this.Close();
        }
    }
}
