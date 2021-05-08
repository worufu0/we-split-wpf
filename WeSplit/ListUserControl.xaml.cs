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

namespace WeSplit
{
    public partial class ListUserControl : UserControl
    {
        public delegate void CommandExecution(int command, int id);
        public event CommandExecution commandExecutionHandler;

        public ListUserControl()
        {
            InitializeComponent();
        }

        private void listUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Global.data.getCollectionView();
            dataListView1.ItemsSource = Global.data.journeys;
            dataListView2.ItemsSource = Global.data.journeys.Where(journey => journey.stage != 4);
            dataListView3.ItemsSource = Global.data.journeys.Where(journey => journey.stage == 4);
        }

        private void filterSearchButton_Click(object sender, RoutedEventArgs e)
        {
            filterAndSearch();
        }

        private void placeRadio_Checked(object sender, RoutedEventArgs e)
        {
            filterAndSearch();
        }

        private void memberRadio_Checked(object sender, RoutedEventArgs e)
        {
            filterAndSearch();
        }

        private void detailButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int id = (item as Journey).id;
            commandExecutionHandler?.Invoke(1, id);
        }

        /// <summary>
        /// Lọc và tìm kiếm
        /// </summary>
        private void filterAndSearch()
        {
            if (placeRadio.IsChecked == false && memberRadio.IsChecked == false)
            {
                radioBorder.BorderThickness = new Thickness(1, 1, 1, 1);
            }

            else
            {
                radioBorder.BorderThickness = new Thickness(0, 0, 0, 0);
                if (string.IsNullOrEmpty(searchBox.Text))
                {
                    dataListView1.ItemsSource = Global.data.journeys;
                    dataListView2.ItemsSource = Global.data.journeys.Where(journey => journey.stage != 4);
                    dataListView3.ItemsSource = Global.data.journeys.Where(journey => journey.stage == 4);
                }
                else
                {
                    if (placeRadio.IsChecked == true)
                    {
                        dataListView1.ItemsSource = Global.data.journeys.Where(journey =>
                                                    removeUnicode(journey.place.ToLower()).Contains(removeUnicode(searchBox.Text).ToLower()));
                        dataListView2.ItemsSource = Global.data.journeys.Where(journey => journey.stage != 4 &&
                                                     removeUnicode(journey.place.ToLower()).Contains(removeUnicode(searchBox.Text).ToLower()));
                        dataListView3.ItemsSource = Global.data.journeys.Where(journey => journey.stage == 4 &&
                                                    removeUnicode(journey.place.ToLower()).Contains(removeUnicode(searchBox.Text).ToLower()));
                    }
                    else if (memberRadio.IsChecked == true)
                    {
                        dataListView1.ItemsSource = Global.data.journeys.Where(journey =>
                                                    removeUnicode(journey.membersView.ToLower()).Contains(removeUnicode(searchBox.Text).ToLower()));
                        dataListView2.ItemsSource = Global.data.journeys.Where(journey => journey.stage != 4 &&
                                                     removeUnicode(journey.membersView.ToLower()).Contains(removeUnicode(searchBox.Text).ToLower()));
                        dataListView3.ItemsSource = Global.data.journeys.Where(journey => journey.stage == 4 &&
                                                    removeUnicode(journey.membersView.ToLower()).Contains(removeUnicode(searchBox.Text).ToLower()));
                    }
                }
            }
        }

        /// <summary>
        /// Bỏ tất cả dấu tiếng Việt trong chuỗi
        /// </summary>
        /// <param name="text">Chuỗi</param>
        /// <returns>Chuỗi đã khử dấu</returns>
        public static string removeUnicode(string text)
        {
            string result = text;
            string[] signedChars = new string[] {"á","à","ả","ã","ạ","â","ấ","ầ","ẩ","ẫ","ậ","ă","ắ","ằ","ẳ","ẵ","ặ",
                                                 "đ",
                                                 "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                                 "í","ì","ỉ","ĩ","ị",
                                                 "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                                 "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                                 "ý","ỳ","ỷ","ỹ","ỵ"};
            string[] unsignedChars = new string[] {"a","a","a","a","a","a","a","a","a","a","a","a","a","a","a","a","a",
                                                   "d",
                                                   "e","e","e","e","e","e","e","e","e","e","e",
                                                   "i","i","i","i","i",
                                                   "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                                   "u","u","u","u","u","u","u","u","u","u","u",
                                                   "y","y","y","y","y",};
            for (int i = 0; i < signedChars.Length; i++)
            {
                result = result.Replace(signedChars[i], unsignedChars[i]);
                result = result.Replace(signedChars[i].ToUpper(), unsignedChars[i].ToUpper());
            }
            return result;
        }
    }
}
