using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Input;

namespace WeSplit
{
	// Toàn cục
	public class Global
	{
		// Dữ liệu
		public static Data data = new Data();
	}

	// Khoản chi
	public class Expense
	{
		// Tên khoản chi.
		public string spendingName { get; set; }

		// Chi phí.
		public double cost { get; set; }
	}

	// Thành viên
	public class Member
    {
		// Tên thành viên
		public string memberName { get; set; }

		// Các khoản chi.
		public List<Expense> expenses { get; set; }
	}

	// Chuyến đi
	public class Journey
	{
		// Số thứ tự
		public int id { get; set; }
		// Tên chuyến đi
		public string journeyName { get; set; }
		// Địa điểm
		public string place { get; set; }

		// Giai đoạn hiện tại của chuyến đi.
		public int stage { get; set; }

		// Tên các thành viên
		public List<Member> members { get; set; }

		// Các hình ảnh chuyến đi.
		public List<string> images { get; set; }

		// Dạng hiển thị tên các thành viên
		public string membersView { get; set; }
	}

	// Thông số cài đặt
	public class ParameterSettings
	{
		// Vô hiệu hóa màn hình splash screen
		public bool splashScreenDisabled { get; set; }
	}

	public class Summary
    {
		// Tên thành viên
		public string memberName { get; set; }

		// Chi phí
		public string cost { get; set; }
	}

	// Toàn bộ dữ liệu chương trình.
	public class Data
	{
		// Thông số cài đặt chương trình
		public ParameterSettings parameterSettings { get; set; }
		// Danh sách chuyến đi.
		public List<Journey> journeys { get; set; }
		// Danh sách thông tin hữu ích.
		public List<string> tips { get; set; }

		/// <summary>
		/// Tạo ra dạng hiển thị cho dữ liệu chuyến đi
		/// </summary>
		public void getCollectionView()
		{
			foreach (Journey journey in journeys)
			{
				journey.membersView = string.Empty;

				for (int i = 0; i < journey.members.Count; i++)
				{
					if (i == journey.members.Count - 1)
					{
						journey.membersView += $"{journey.members[i].memberName}.";
					}
					else
					{
						journey.membersView += $"{journey.members[i].memberName}, ";
					}
				}
			}
		}

		/// <summary>
		/// Chọn ra thông tin hữu ích ngẫu nhiên từ mảng tips.
		/// </summary>
		/// <returns> Thông tin hữu ích </returns>
		public string getTip()
		{
			Random rng = new Random();
			string tip = tips[rng.Next(tips.Count())];
			return tip;
		}

		/// <summary>
		/// Lưu dữ liệu vào file
		/// </summary>
		/// <param name="obj"> Dữ liệu cần lưu </param>
		/// <param name="fileName"> Đường dẫn file </param>
		public void save(object obj, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(obj.GetType());
			StreamWriter writer = new StreamWriter(fileName);

			serializer.Serialize(writer, obj);
			writer.Close();
		}

		/// <summary>
		/// Đọc dữ liệu từ file
		/// </summary>
		/// <param name="data"> Kiểu của dữ liệu </param>
		/// <param name="fileName"> Đường dẫn file </param>
		/// <returns> Dữ liệu chương trình </returns>
		public Data load(Data data, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Data));
			StreamReader reader = new StreamReader(fileName);

			data = (Data)serializer.Deserialize(reader);
			reader.Close();
			return data;
		}
	}
}