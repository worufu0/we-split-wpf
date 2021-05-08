using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class AddingUserControl : UserControl
    {
        public delegate void CommandExecution(int command, int id);
        public event CommandExecution commandExecutionHandler;

        List<string> imageAbsolutePaths = new List<string>();
        
        public Journey newJourney { get; set; }
        public Journey currentJourney { get; set; }
        public int saveCommand { get; set; }
        public int saveID { get; set; }

        public AddingUserControl(int command, int id)
        {
            InitializeComponent();

            saveCommand = command;
            saveID = id;
        }
        
        private void addingUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            currentJourney = journeyIdentification(saveID);

            if (saveCommand == 0)
            {
                initInitializeAdding();
            }

            else if (saveCommand == 1)
            {
                initInitializeUpdating();
            }
        }

        private void removeImageButton_Click(object sender, RoutedEventArgs e)
        {
            imageAbsolutePaths.RemoveAt(imageListView.SelectedIndex);
            imageListView.Items.RemoveAt(imageListView.SelectedIndex);
        }

        private void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();

            if (File.Exists(openFileDialog.FileName))
            {
                Image image = new Image()
                {
                    Source = new BitmapImage(new Uri(openFileDialog.FileName)),
                    Stretch = Stretch.UniformToFill,
                    Height = 80,
                };
                RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);

                imageListView.Items.Add(image);
                imageAbsolutePaths.Add(openFileDialog.FileName);
            }
        }

        private void addExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel1 = (sender as FrameworkElement).Parent as StackPanel;
            StackPanel panel2 = panel1.Children[0] as StackPanel;
            createExpenseBox(panel2, string.Empty, string.Empty);
        }

        private void addMemberButton_Click(object sender, RoutedEventArgs e)
        {
            createMemberBox(string.Empty, string.Empty, string.Empty);
        }

        private void costBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            commandExecutionHandler?.Invoke(5, saveID);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            newJourney = new Journey();
            newJourney.members = new List<Member>();

            newJourney.id = currentJourney.id;
            if (string.IsNullOrEmpty(journeyNameBox.Text) || string.IsNullOrWhiteSpace(journeyNameBox.Text))
            {
                journeyNameBox.Text = "(Chưa cập nhật)";
            }
            newJourney.journeyName = journeyNameBox.Text;

            if (string.IsNullOrEmpty(placeBox.Text) || string.IsNullOrWhiteSpace(placeBox.Text))
            {
                placeBox.Text = "(Chưa cập nhật)";
            }
            newJourney.place = placeBox.Text;

            newJourney.stage = stageBox.SelectedIndex;

            newJourney.images = createRelativePaths(imageAbsolutePaths);
            copyImages(imageAbsolutePaths);

            for (int i = 0; i < membersPanel.Children.Count; i++)
            {
                Member member = new Member();
                member.expenses = new List<Expense>();

                StackPanel panel1 = membersPanel.Children[i] as StackPanel;
                StackPanel panel2 = panel1.Children[0] as StackPanel;
                StackPanel panel3 = panel1.Children[1] as StackPanel;
                StackPanel panel4 = panel3.Children[0] as StackPanel;
                TextBox box1 = panel2.Children[1] as TextBox;

                if (string.IsNullOrEmpty(box1.Text) || string.IsNullOrWhiteSpace(box1.Text))
                {
                    box1.Text = "(Chưa cập nhật)";
                }
                member.memberName = box1.Text;

                for (int j = 0; j < panel4.Children.Count; j++)
                {
                    StackPanel panel5 = panel4.Children[j] as StackPanel;
                    StackPanel panel6 = panel5.Children[0] as StackPanel;
                    StackPanel panel7 = panel5.Children[1] as StackPanel;
                    TextBox box2 = panel6.Children[1] as TextBox;
                    TextBox box3 = panel7.Children[1] as TextBox;

                    if (string.IsNullOrEmpty(box2.Text) || string.IsNullOrWhiteSpace(box2.Text))
                    {
                        box2.Text = "(Chưa cập nhật)";
                    }

                    if (string.IsNullOrEmpty(box3.Text) || string.IsNullOrWhiteSpace(box3.Text))
                    {
                        box3.Text = "0";
                    }
                    member.expenses.Add(new Expense { spendingName = box2.Text , cost = int.Parse(box3.Text) });
                }

                newJourney.members.Add(member);
            }

            if (saveCommand == 0)
            {
                newJourney.id = searchValidID();
                saveID = newJourney.id;
                Global.data.journeys.Add(newJourney);
            }

            else if (saveCommand == 1)
            {
                int index = Global.data.journeys.IndexOf(currentJourney);
                Global.data.journeys[index] = newJourney;
            }

            NotificationScreen notificationScreen = new NotificationScreen(saveCommand);
            notificationScreen.Owner = Window.GetWindow(this);
            notificationScreen.Show();

            commandExecutionHandler?.Invoke(4, saveID);
        }

        /// <summary>
        /// Khởi tạo việc thêm mới chuyến đi
        /// </summary>
        private void initInitializeAdding()
        {
            title.Text = "THÊM MỚI CHUYẾN ĐI";
            createMemberBox(string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Khởi tạo việc cập nhật chuyến đi
        /// </summary>
        private void initInitializeUpdating()
        {
            title.Text = "CẬP NHẬT CHUYẾN ĐI";
            journeyNameBox.Text = currentJourney.journeyName;
            placeBox.Text = currentJourney.place;
            stageBox.SelectedIndex = currentJourney.stage;
            imageAbsolutePaths = createAbsolutePaths(currentJourney.images);

            foreach (string path in imageAbsolutePaths)
            {
                Image image = new Image()
                {
                    Source = new BitmapImage(new Uri(path)),
                    Stretch = Stretch.UniformToFill,
                    Height = 80,
                };
                RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);
                imageListView.Items.Add(image);
            }

            for (int i = 0; i < currentJourney.members.Count; i++)
            {
                Member member = currentJourney.members[i];

                createMemberBox(member.memberName, member.expenses[0].spendingName, member.expenses[0].cost.ToString());

                if (member.expenses.Count > 1)
                {
                    StackPanel panel1 = membersPanel.Children[i] as StackPanel;
                    StackPanel panel2 = panel1.Children[0] as StackPanel;
                    StackPanel panel3 = panel1.Children[1] as StackPanel;
                    StackPanel panel4 = panel3.Children[0] as StackPanel;

                    for (int j = 1; j < member.expenses.Count; j++)
                    {
                        createExpenseBox(panel4, member.expenses[j].spendingName, member.expenses[j].cost.ToString());
                    }
                }
            }
         }

        /// <summary>
        /// Tạo thêm ô nhập khoản chi
        /// </summary>
        private void createExpenseBox(StackPanel stackPanel, string spendingName, string cost)
        {
            Style addingHeaderTextStyle = this.FindResource("addingHeaderTextStyle") as Style;
            Style fillTextBoxStyle = this.FindResource("fillTextBoxStyle") as Style;
            Style addExpenseButtonStyle = this.FindResource("addExpenseButtonStyle") as Style;

            TextBlock expenseNameBlock = new TextBlock()
            {
                Text = "Khoản chi: ",
                FontSize = 16,
                Style = addingHeaderTextStyle
            };

            TextBlock costBlock = new TextBlock()
            {
                Text = "Chi phí: ",
                FontSize = 16,
                Style = addingHeaderTextStyle
            };

            TextBox expenseNameBox = new TextBox()
            {
                Text = spendingName,
                ToolTip = "Khoản chi này gọi là gì ?",
                MaxLength = 16,
                Width = 180,
                FontSize = 16,
                Style = fillTextBoxStyle
            };

            TextBox costBox = new TextBox()
            {
                Text = cost,
                ToolTip = "Chi phí là bao nhiêu ?",
                MaxLength = 16,
                Width = 160,
                FontSize = 16,
                Style = fillTextBoxStyle
            };
            costBox.PreviewTextInput += costBox_PreviewTextInput;

            Button button = new Button()
            {
                Margin = new Thickness(10),
                Style = addExpenseButtonStyle
            };
            button.Click += addExpenseButton_Click;

            StackPanel detailExpensePanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };

            StackPanel expenseNamePanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            StackPanel costPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            expenseNamePanel.Children.Add(expenseNameBlock);
            expenseNamePanel.Children.Add(expenseNameBox);
            costPanel.Children.Add(costBlock);
            costPanel.Children.Add(costBox);
            detailExpensePanel.Children.Add(expenseNamePanel);
            detailExpensePanel.Children.Add(costPanel);
            stackPanel.Children.Add(detailExpensePanel);
        }

        /// <summary>
        /// Tạo thêm khung ô nhập thành viên 
        /// </summary>
        private void createMemberBox(string memberName, string expenseName, string cost)
        {
            Style addingHeaderTextStyle = this.FindResource("addingHeaderTextStyle") as Style;
            Style fillTextBoxStyle = this.FindResource("fillTextBoxStyle") as Style;
            Style addExpenseButtonStyle = this.FindResource("addExpenseButtonStyle") as Style;

            TextBlock memberNameBlock = new TextBlock()
            {
                Text = "Tên thành viên: ",
                FontSize = 16,
                Style = addingHeaderTextStyle
            };

            TextBlock expenseNameBlock = new TextBlock()
            {
                Text = "Khoản chi: ",
                FontSize = 16,
                Style = addingHeaderTextStyle
            };

            TextBlock costBlock = new TextBlock()
            {
                Text = "Chi phí: ",
                FontSize = 16,
                Style = addingHeaderTextStyle
            };

            TextBox memberNameBox = new TextBox()
            {
                Text = memberName,
                ToolTip = "Tên của thành viên, tối đa 16 ký tự",
                FontSize = 16,
                MaxLength = 16,
                Width = 180,
                Style = fillTextBoxStyle
            };

            TextBox expenseNameBox = new TextBox()
            {
                Text = expenseName,
                ToolTip = "Khoản chi này gọi là gì ?",
                MaxLength = 16,
                Width = 180,
                FontSize = 16,
                Style = fillTextBoxStyle
            };

            TextBox costBox = new TextBox()
            {
                Text = cost,
                ToolTip = "Chi phí là bao nhiêu ?",
                MaxLength = 16,
                Width = 160,
                FontSize = 16,
                Style = fillTextBoxStyle
            };
            costBox.PreviewTextInput += costBox_PreviewTextInput;

            Button button = new Button()
            {
                Margin = new Thickness(10),
                Style = addExpenseButtonStyle
            };
            button.Click += addExpenseButton_Click;

            StackPanel expensesPanel = new StackPanel();
            StackPanel supportButtonPanel = new StackPanel();

            StackPanel detailMemberPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };

            StackPanel detailExpensePanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };

            StackPanel memberNamePanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10)
            };

            StackPanel expenseNamePanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            StackPanel costPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            expenseNamePanel.Children.Add(expenseNameBlock);
            expenseNamePanel.Children.Add(expenseNameBox);
            costPanel.Children.Add(costBlock);
            costPanel.Children.Add(costBox);
            detailExpensePanel.Children.Add(expenseNamePanel);
            detailExpensePanel.Children.Add(costPanel);
            expensesPanel.Children.Add(detailExpensePanel);
            supportButtonPanel.Children.Add(expensesPanel);
            supportButtonPanel.Children.Add(button);
            memberNamePanel.Children.Add(memberNameBlock);
            memberNamePanel.Children.Add(memberNameBox);
            detailMemberPanel.Children.Add(memberNamePanel);
            detailMemberPanel.Children.Add(supportButtonPanel);
            membersPanel.Children.Add(detailMemberPanel);
        }

        /// <summary>
        /// Sao chép hình ảnh
        /// </summary>
        /// <param name="absolutePaths">Danh sách đường dẫn tuyệt đối</param>
        private void copyImages(List<string> absolutePaths)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath, myPath;
            string[] pathSplit;

            foreach (string absolutePath in absolutePaths)
            {
                pathSplit = absolutePath.Split('\\');
                relativePath = pathSplit.Last();
                myPath = $"{baseDirectory}\\Data\\Images\\{relativePath}";

                if (!File.Exists(myPath))
                {
                    File.Copy(absolutePath, myPath);
                }
            }    
        }

        /// <summary>
		/// Tìm ID hợp lệ
		/// </summary>
		/// <returns>ID hợp lệ</returns>
		public int searchValidID()
        {
            int result = 0;

            List<int> ids = new List<int>();
            foreach (Journey journey in Global.data.journeys)
            {
                ids.Add(journey.id);
            }

            for (int i = 13; i < int.MaxValue; i++)
            {
                if (!ids.Contains(i))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Xác định chuyến đi theo ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Chuyến đi</returns>
        private Journey journeyIdentification(int id)
        {
            Journey result = new Journey();

            foreach (Journey journey in Global.data.journeys)
            {
                if (id == journey.id)
                {
                    result = journey;
                }
            }

            return result;
        }

        /// <summary>
        /// Tạo ra danh sách đường dẫn tương đối
        /// </summary>
        /// <param name="absolutePaths">Danh sách đường dẫn tuyệt đối</param>
        /// <returns>Danh sách đường dẫn tương đối</returns>
        private List<string> createRelativePaths(List<string> absolutePaths)
        {
            List<string> relativePaths = new List<string>();
            string[] pathSplit;

            foreach (string absolutePath in absolutePaths)
            {
                pathSplit = absolutePath.Split('\\');
                relativePaths.Add(pathSplit.Last());
            }

            return relativePaths;
        }

        /// <summary>
        /// Tạo ra danh sách đường dẫn tuyệt đối
        /// </summary>
        /// <param name="relativePaths">Danh sách đường dẫn tương đối</param>
        /// <returns>Danh sách đường dẫn tuyệt đối</returns>
        private List<string> createAbsolutePaths(List<string> relativePaths)
        {
            List<string> absolutePath = new List<string>();
            string tempPath;

            foreach (string relativePath in relativePaths)
            {
                tempPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Data\\Images\\{relativePath}";
                absolutePath.Add(tempPath);
            }

            return absolutePath;
        }
    }
}
