using CommandInterpreter.Calculator.Container;
using System;
using System.Collections.Generic;
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

namespace CrossMath
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int[,] mi_operants;
		private char[,] mi_vOperators;
		private char[,] mi_hOperators;
		private int[] mi_vResults;
		private int[] mi_hResults;

		public MainWindow()
		{
			InitializeComponent();
			mi_operants = new int[3, 3];
			mi_vOperators = new char[3, 2];
			mi_hOperators = new char[3, 2];
			mi_vResults = new int[3];
			mi_hResults = new int[3];

			fi_cleanField();
			fi_createField();
		}

		private void Clean_button_Click(object sender, RoutedEventArgs e)
		{
			fi_cleanField();
		}

		private void Create_button_Click(object sender, RoutedEventArgs e)
		{
			fi_createField();

		}



		private void Solve_button_Click(object sender, RoutedEventArgs e)
		{
			//fi_updateOperants();
			fi_readArray();
			fi_solve();
		}

		private void fi_cleanField()
		{
			operant_1_1.Text = "";
			operant_1_2.Text = "";
			operant_1_3.Text = "";
			operant_2_1.Text = "";
			operant_2_2.Text = "";
			operant_2_3.Text = "";
			operant_3_1.Text = "";
			operant_3_2.Text = "";
			operant_3_3.Text = "";
		}

		private void fi_updateOperants()
		{
			operant_1_1.Text = mi_operants[0, 0].ToString();
			operant_1_2.Text = mi_operants[0, 1].ToString();
			operant_1_3.Text = mi_operants[0, 2].ToString();
			operant_2_1.Text = mi_operants[1, 0].ToString();
			operant_2_2.Text = mi_operants[1, 1].ToString();
			operant_2_3.Text = mi_operants[1, 2].ToString();
			operant_3_1.Text = mi_operants[2, 0].ToString();
			operant_3_2.Text = mi_operants[2, 1].ToString();
			operant_3_3.Text = mi_operants[2, 2].ToString();
		}

		private void fi_updateCore()
		{
			operator_v_1_1.Text = mi_vOperators[0, 0].ToString();
			operator_v_2_1.Text = mi_vOperators[0, 1].ToString();
			operator_v_1_2.Text = mi_vOperators[1, 0].ToString();
			operator_v_2_2.Text = mi_vOperators[1, 1].ToString();
			operator_v_1_3.Text = mi_vOperators[2, 0].ToString();
			operator_v_2_3.Text = mi_vOperators[2, 1].ToString();

			operator_h_1_1.Text = mi_hOperators[0, 0].ToString();
			operator_h_1_2.Text = mi_hOperators[0, 1].ToString();
			operator_h_2_1.Text = mi_hOperators[1, 0].ToString();
			operator_h_2_2.Text = mi_hOperators[1, 1].ToString();
			operator_h_3_1.Text = mi_hOperators[2, 0].ToString();
			operator_h_3_2.Text = mi_hOperators[2, 1].ToString();

			result_v_1.Text = mi_vResults[0].ToString();
			result_v_2.Text = mi_vResults[1].ToString();
			result_v_3.Text = mi_vResults[2].ToString();

			result_h_1.Text = mi_hResults[0].ToString();
			result_h_2.Text = mi_hResults[1].ToString();
			result_h_3.Text = mi_hResults[2].ToString();
		}

		private void fi_createField()
		{
			fi_cleanField();
			System.Random rnd = new Random();

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					mi_operants[x, y] = rnd.Next(1, 10);
				}
			}

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 2; y++)
				{
					mi_vOperators[x, y] = fi_intToChar(rnd.Next(5));
					mi_hOperators[x, y] = fi_intToChar(rnd.Next(5));
				}
			}

			string text;
			ContainerList cl;
			for (int i = 0; i < 3; i++)
			{
				text = "" + mi_operants[i, 0] + mi_hOperators[i, 0] + mi_operants[i, 1] + mi_hOperators[i, 1] + mi_operants[i, 2];
				cl = AContainer.Read(text);
				mi_hResults[i] = (int)cl.Calculate();

				text = "" + mi_operants[0, i] + mi_vOperators[i, 0] + mi_operants[1, i] + mi_vOperators[i, 1] + mi_operants[2, i];
				cl = AContainer.Read(text);
				mi_vResults[i] = (int)cl.Calculate();

			}

			fi_updateCore();

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					mi_operants[x, y] = 0;
				}
			}
		}

		private char fi_intToChar(int _int)
		{
			switch (_int)
			{
				case 0:
					return '+';
				case 1:
					return '-';
				case 2:
					return '*';
				case 3:
					return '/';
				case 4:
					return '%';
				default:
					throw new ArgumentException("out of range");
			}
		}

		private void fi_readArray()
		{
			try
			{

				Regex number = new Regex(@"\d+");
				Regex op = new Regex(@"[\+\-\*\/\%]{1}");

				if (!(number.IsMatch(result_h_1.Text)
					&& number.IsMatch(result_h_2.Text)
					&& number.IsMatch(result_h_3.Text)
					&& number.IsMatch(result_v_1.Text)
					&& number.IsMatch(result_v_2.Text)
					&& number.IsMatch(result_v_3.Text)))
				{
					throw new System.ArgumentException();
				}

				mi_hResults[0] = int.Parse(result_h_1.Text);
				mi_hResults[1] = int.Parse(result_h_2.Text);
				mi_hResults[2] = int.Parse(result_h_3.Text);

				mi_vResults[0] = int.Parse(result_v_1.Text);
				mi_vResults[1] = int.Parse(result_v_2.Text);
				mi_vResults[2] = int.Parse(result_v_3.Text);

				if (!(op.IsMatch(operator_h_1_1.Text)
					&& op.IsMatch(operator_h_1_2.Text)
					&& op.IsMatch(operator_h_2_1.Text)
					&& op.IsMatch(operator_h_2_2.Text)
					&& op.IsMatch(operator_h_3_1.Text)
					&& op.IsMatch(operator_h_3_2.Text)
					&& op.IsMatch(operator_v_1_1.Text)
					&& op.IsMatch(operator_v_1_2.Text)
					&& op.IsMatch(operator_v_1_3.Text)
					&& op.IsMatch(operator_v_2_1.Text)
					&& op.IsMatch(operator_v_2_2.Text)
					&& op.IsMatch(operator_v_2_3.Text)))
				{
					throw new System.ArgumentException();
				}

				mi_hOperators[0, 0] = char.Parse(operator_h_1_1.Text);
				mi_hOperators[0, 1] = char.Parse(operator_h_1_2.Text);
				mi_hOperators[1, 0] = char.Parse(operator_h_2_1.Text);
				mi_hOperators[1, 1] = char.Parse(operator_h_2_2.Text);
				mi_hOperators[2, 0] = char.Parse(operator_h_3_1.Text);
				mi_hOperators[2, 1] = char.Parse(operator_h_3_2.Text);

				mi_vOperators[0, 0] = char.Parse(operator_v_1_1.Text);
				mi_vOperators[0, 1] = char.Parse(operator_v_2_1.Text);
				mi_vOperators[1, 0] = char.Parse(operator_v_1_2.Text);
				mi_vOperators[1, 1] = char.Parse(operator_v_2_2.Text);
				mi_vOperators[2, 0] = char.Parse(operator_v_1_3.Text);
				mi_vOperators[2, 1] = char.Parse(operator_v_2_3.Text);
			}
			catch (System.NullReferenceException)
			{ }
		}

		private void fi_solve()
		{
			List<Tuple<Constant, Constant, Constant>>[] possiblePairs = new List<Tuple<Constant, Constant, Constant>>[3];
			for (int i = 0; i < possiblePairs.Length; i++)
			{
				possiblePairs[i] = new List<Tuple<Constant, Constant, Constant>>();
			}
			ContainerList[] hor = new ContainerList[3];
			for (int i = 0; i < 3; i++)
			{
				hor[i] = AContainer.Read($"{(char)(65 + 0 + 3 * i)}{mi_hOperators[i, 0]}{(char)(65 + 1 + 3 * i)}{mi_hOperators[i, 1]}{(char)(65 + 2 + 3 * i)}");
			}


			double value;
			Constant c1, c2, c3;
			for (int first = 0; first < 10; first++)
			{
				for (int second = 0; second < 10; second++)
				{
					for (int third = 0; third < 10; third++)
					{
						for (int cl = 0; cl < hor.Length; cl++)
						{
							c1 = new Constant(((char)(65 + 0 + 3 * cl)).ToString(), first);
							c2 = new Constant(((char)(65 + 1 + 3 * cl)).ToString(), second);
							c3 = new Constant(((char)(65 + 2 + 3 * cl)).ToString(), third);

							hor[cl].SetConstants(c1);
							hor[cl].SetConstants(c2);
							hor[cl].SetConstants(c3);

							value = ((ContainerList)hor[cl].Clone()).Calculate();
							if ((int)value == mi_hResults[cl])
							{

								possiblePairs[cl].Add(new Tuple<Constant, Constant, Constant>(c1, c2, c3));
							}
						}
					}
				}
			}


			ContainerList[] ver = new ContainerList[3];
			int[] values = new int[3];
			for (int i = 0; i < 3; i++)
			{
				string text = $"{(char)(65 + i + 3 * 0)}{mi_vOperators[i, 0]}{(char)(65 + i + 3 * 1)}{mi_vOperators[i, 1]}{(char)(65 + i + 3 * 2)}";
				ver[i] = AContainer.Read(text);
			}
			for (int first = 0; first < possiblePairs[0].Count; first++)
			{
				for (int i = 0; i < 3; i++)
				{
					ver[i].SetConstants(possiblePairs[0][first].Item1, possiblePairs[0][first].Item2, possiblePairs[0][first].Item3);
				}

				for (int second = 0; second < possiblePairs[1].Count; second++)
				{
					for (int i = 0; i < 3; i++)
					{
						ver[i].SetConstants(possiblePairs[1][second].Item1, possiblePairs[1][second].Item2, possiblePairs[1][second].Item3);
					}

					for (int third = 0; third < possiblePairs[2].Count; third++)
					{
							for (int i = 0; i < 3; i++)
							{
								ver[i].SetConstants(possiblePairs[2][third].Item1, possiblePairs[2][third].Item2, possiblePairs[2][third].Item3);
								values[i] = (int)((ContainerList)ver[i].Clone()).Calculate();
							}

							if (values[0] == mi_vResults[0]
								&& (values[1] == mi_vResults[1])
								&& (values[2] == mi_vResults[2]))
							{

								mi_operants[0, 0] = (int) possiblePairs[0][first].Item1.mu_value;
								mi_operants[0, 1] = (int) possiblePairs[0][first].Item2.mu_value;
								mi_operants[0, 2] = (int) possiblePairs[0][first].Item3.mu_value;

								mi_operants[1, 0] = (int)possiblePairs[1][second].Item1.mu_value;
								mi_operants[1, 1] = (int)possiblePairs[1][second].Item2.mu_value;
								mi_operants[1, 2] = (int)possiblePairs[1][second].Item3.mu_value;

								mi_operants[2, 0] = (int)possiblePairs[2][third].Item1.mu_value;
								mi_operants[2, 1] = (int)possiblePairs[2][third].Item2.mu_value;
								mi_operants[2, 2] = (int)possiblePairs[2][third].Item3.mu_value;

								fi_updateOperants();

								return;
							}
					}
				}
			}

			throw new System.NotImplementedException();

		}
	}
}
