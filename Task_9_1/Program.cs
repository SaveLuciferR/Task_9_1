using System;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace Task_9_1 
{
	class Program
	{
		static void Main()
		{
			try
			{
				Console.WriteLine("Введите последовательность чисел через пробел");
				string num = Console.ReadLine();
				Regex regex = new Regex(@"(-|)(\d+)");

				for (int i = 0; i < num.Length; i++)
				{
					if (!(char.IsDigit(num[i]) || num[i] == ' ' || num[i] == '-'))
					{
						throw new Exception();
					}
				}

				FileStream f = new FileStream("t.dat", FileMode.OpenOrCreate);
				BinaryWriter fOut = new BinaryWriter(f);

				Console.Write("Введите делитель: ");
				int n = Convert.ToInt32(Console.ReadLine());
				foreach (Match it in regex.Matches(num))
				{
					if (it.Success && (Convert.ToInt32(it.Value) % n != 0))
					{
						fOut.Write(Convert.ToInt32(it.Value));
					}
				}
				fOut.Close();

				f = new FileStream("t.dat", FileMode.Open);
				BinaryReader fIn = new BinaryReader(f);
				long m = f.Length;

				using (StreamWriter sw = new StreamWriter(File.Open("convert.txt", FileMode.Create), Encoding.UTF8))
				{
					for (long i = 0; i < m; i += 4)
					{
						f.Seek(i, SeekOrigin.Begin);
						int a = fIn.ReadInt32();
						Console.Write(a + " ");
						sw.Write(a + " ");
					}
				}
				f.Close();
				fIn.Close();
			}
			catch (FormatException)
			{
				Console.WriteLine("Введите корректные числа");
			}
			catch
			{
				Console.WriteLine("В строке должны быть только целые числа и пробелы");
			}
		}
	}
}