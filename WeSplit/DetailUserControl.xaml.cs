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
using LiveCharts;
using LiveCharts.Wpf;

namespace WeSplit
{
    public partial class DetailUserControl : UserControl
    {
		public delegate void CommandExecution(int command, int id);
		public event CommandExecution commandExecutionHandler;

		public Journey currentJourney { get; set; }
		public List<Summary> summaries { get; set; }
		public string totalCost { get; set; }
		public string averageCost { get; set; }
		public int saveID { get; set; }

		public DetailUserControl(int id)
        {
            InitializeComponent();
			saveID = id;
        }

        private void detailUserControl_Loaded(object sender, RoutedEventArgs e)
        {
			Global.data.getCollectionView();
			currentJourney = journeyIdentification(saveID);

			totalCost = exchangeTotalCost(calculateTotalCost());
            averageCost = exchangeTotalCost(calculateTotalCost() / currentJourney.members.Count);
            createChart();
            journeySummary();

            DataContext = this;
            imageListView.ItemsSource = currentJourney.images;
            summaryListView.ItemsSource = summaries;
        }

		private void updateButton_Click(object sender, RoutedEventArgs e)
		{
			commandExecutionHandler?.Invoke(2, saveID);
		}

		private void deleteButton_Click(object sender, RoutedEventArgs e)
		{
			int index = Global.data.journeys.IndexOf(currentJourney);
			Global.data.journeys.Remove(currentJourney);

			NotificationScreen notificationScreen = new NotificationScreen(3);
			notificationScreen.Owner = Window.GetWindow(this);
			notificationScreen.Show();

			commandExecutionHandler?.Invoke(3, saveID);
		}

		/// <summary>
		/// Tổng kết chuyến đi
		/// </summary>
		private void journeySummary()
        {
			summaries = new List<Summary>();
			double average = (double)calculateTotalCost() / currentJourney.members.Count;

			foreach (Member member in currentJourney.members)
			{
				summaries.Add(new Summary { memberName = member.memberName, cost = exchangeTotalCost(totalPay(member) - average) });
			}
		}

		/// <summary>
		/// Tính tổng chi phí của chuyến đi
		/// </summary>
		/// <returns>Tổng chi phí</returns>
		private double calculateTotalCost()
        {
			double result = 0;

			foreach (Member member in currentJourney.members)
			{
				foreach (Expense expense in member.expenses)
				{
					result += expense.cost;
				}
			}

			return result;
		}

		/// <summary>
		/// Qui đổi dạng số sang dạng tiền tệ
		/// </summary>
		/// <param name="money">Dạng số</param>
		/// <returns>Dạng tiền tệ</returns>
		private string exchangeTotalCost(double money)
        {
			double exchange;
			string result = string.Empty, sign = string.Empty;

			if (money < 0)
            {
				money = Math.Abs(money);
				sign = "-";
			}

			if (money >= 1000000000)
            {
				exchange = money / 1000000000;
				result = $"{sign}{Math.Round(exchange, 2).ToString()} Tỷ";
			}

			else
            {
				if (money >= 1000000)
				{
					exchange = money / 1000000;
					result = $"{sign}{Math.Round(exchange, 2).ToString()} Triệu";
				}

				else
                {
					if (money >= 1000)
					{
						exchange = money / 1000;
						result = $"{sign}{Math.Round(exchange, 2).ToString()} Nghìn";
					}

					else
                    {
						exchange = money;
						result = $"{sign}{Math.Round(exchange, 2).ToString()}";
					}
				}
			}

			return result;
        }

		/// <summary>
		/// Tạo ra các biểu đồ
		/// </summary>
		private void createChart()
        {
            List<Expense> cartesianSource = distinctExpenses(currentJourney.members, getExpenses(currentJourney.members));

            foreach (Member member in currentJourney.members)
            {
                pieChart.Series.Add(new PieSeries { Title = member.memberName, Values = new ChartValues<double> { totalPay(member) } });
            }

            foreach (Expense expense in cartesianSource)
            {
                cartesianChart.Series.Add(new ColumnSeries { Title = expense.spendingName, Values = new ChartValues<double> { expense.cost } });
            }
        }

		/// <summary>
		/// Tính tổng chi trả của một thành viên
		/// </summary>
		/// <param name="member">Thành viên</param>
		/// <returns>Tổng chi trả</returns>
		private double totalPay(Member member)
		{
			double result = 0;

			foreach (Expense expense in member.expenses)
			{
				result += expense.cost;
			}

			return result;
		}

		/// <summary>
		/// Tạo ra danh sách tên các khoản chi không trùng nhau
		/// </summary>
		/// <param name="members">Danh sách thành viên</param>
		/// <returns>Danh sách tên khoản chi</returns>
		private List<string> getExpenses(List<Member> members)
        {
			List<string> result = new List<string>();

			foreach (Member member in members)
			{
				foreach (Expense expense in member.expenses)
				{
					if (!result.Contains(expense.spendingName))
                    {
						result.Add(expense.spendingName);
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Lập ra danh sách các khoản chi
		/// </summary>
		/// <param name="members">Danh sách tên khoản chi</param>
		/// <returns>Danh sách khoản chi</returns>
		private List<Expense> distinctExpenses(List<Member> members, List<string> expenses)
		{
			List<Expense> result = new List<Expense>();

			foreach (string expense in expenses)
            {
				result.Add(new Expense { spendingName = expense, cost = 0 });
            }

			foreach (Member member in members)
			{
				foreach (Expense expense in member.expenses)
                {
					foreach (Expense expense1 in result)
                    {
						if (expense.spendingName == expense1.spendingName)
                        {
							expense1.cost += expense.cost;
						}
                    }
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
    }
}
