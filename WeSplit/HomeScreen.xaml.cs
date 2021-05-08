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
    public partial class HomeScreen : Window
    {
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void homeScreenWindow_Closed(object sender, EventArgs e)
        {
            Global.data.save(Global.data, "Data/Data.ntp");
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0.2;
            ConfirmationScreen confirmationScreen = new ConfirmationScreen(0);
            confirmationScreen.Owner = this;
            confirmationScreen.ShowDialog();

            if (DialogResult == true)
            {
                mainGrid.Opacity = 1;
            }

            else
            {
                mainGrid.Opacity = 1;
            }
        }

        private void minimizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maximizeWindowButton_Checked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void maximizeWindowButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0.2;
            AboutScreen aboutScreen = new AboutScreen();
            aboutScreen.Owner = this;
            aboutScreen.ShowDialog();

            if (DialogResult == true)
            {
                mainGrid.Opacity = 1;
            }

            else
            {
                mainGrid.Opacity = 1;
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0.2;
            SettingsScreen settingsScreen = new SettingsScreen();
            settingsScreen.Owner = this;
            settingsScreen.ShowDialog();

            if (DialogResult == true)
            {
                mainGrid.Opacity = 1;
            }

            else
            {
                mainGrid.Opacity = 1;
            }
        }

        private void navigationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (navigationListView.SelectedIndex == 0)
            {
                ListUserControl listUserControl = new ListUserControl();
                mainGrid.Children.Clear();
                mainGrid.Children.Add(listUserControl);

                listUserControl.commandExecutionHandler += commandExecution;
            }

            if (navigationListView.SelectedIndex == 2)
            {
                AddingUserControl addingUserControl = new AddingUserControl(0, -1);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(addingUserControl);

                addingUserControl.commandExecutionHandler += commandExecution;
            }
        }

        private void mainGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (menuButton.IsChecked == true)
            {
                menuButton.IsChecked = false;
            }
        }

        private void commandExecution(int command, int id)
        {
            if (command == 1)
            {
                navigationListView.SelectedIndex = 1;
                DetailUserControl detailUserControl = new DetailUserControl(id);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(detailUserControl);

                detailUserControl.commandExecutionHandler += commandExecution;
            }

            else if (command == 2)
            {
                navigationListView.SelectedIndex = 2;
                AddingUserControl addingUserControl = new AddingUserControl(1, id);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(addingUserControl);

                addingUserControl.commandExecutionHandler += commandExecution;
            }

            else if (command == 3)
            {
                navigationListView.SelectedIndex = 0;
                ListUserControl listUserControl = new ListUserControl();
                mainGrid.Children.Clear();
                mainGrid.Children.Add(listUserControl);

                listUserControl.commandExecutionHandler += commandExecution;
            }

            else if (command == 4)
            {
                navigationListView.SelectedIndex = 1;
                DetailUserControl detailUserControl = new DetailUserControl(id);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(detailUserControl);

                detailUserControl.commandExecutionHandler += commandExecution;
            }

            else if (command == 5)
            {
                if (id != -1)
                {
                    navigationListView.SelectedIndex = 1;
                    DetailUserControl detailUserControl = new DetailUserControl(id);
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(detailUserControl);

                    detailUserControl.commandExecutionHandler += commandExecution;
                }
                
                else
                {
                    navigationListView.SelectedIndex = 0;
                    ListUserControl listUserControl = new ListUserControl();
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(listUserControl);

                    listUserControl.commandExecutionHandler += commandExecution;
                }
            }
        }
    }
}
