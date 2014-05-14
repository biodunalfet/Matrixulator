using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Input;

namespace Matrixulator
{
    public partial class determinant : PhoneApplicationPage
    {
        int dim;
        double[,] matA;
        double validEntry;
        bool isEmpty = false;
        string holder;
        int count = 0;
        List<int> ind = new List<int>();

        public determinant()
        {
            InitializeComponent();
            matdim.SelectionChanged += matdim_SelectionChanged;
        }
       
        private void solve_Click(object sender, RoutedEventArgs e)
        {


            bool cfe = checkforerrors();
            //  test.Text =  asdf;

            if (cfe == false)
            {
                
                result.FontSize = 60;
                result.Foreground = new SolidColorBrush(Colors.Black);
                result.Text = setdeterminantarray().ToString();
                result.Visibility = Visibility.Visible;
            }

        }

        private double setdeterminantarray()
        {
            double[,] myMatrix = new double[dim, dim];
            myMatrix = matA;
            return Determinant(myMatrix);
        }

        private int SignOfElement(int i, int j)
        {
            if ((i + j) % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        //this method determines the sub matrix corresponding to a given element
        private double[,] CreateSmallerMatrix(double[,] input, int i, int j)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            double[,] output = new double[order - 1, order - 1];
            int x = 0, y = 0;
            for (int m = 0; m < order; m++, x++)
            {
                if (m != i)
                {
                    y = 0;
                    for (int n = 0; n < order; n++)
                    {
                        if (n != j)
                        {
                            output[x, y] = input[m, n];
                            y++;
                        }
                    }
                }
                else
                {
                    x--;
                }
            }
            return output;
        }
        //this method determines the value of determinant using recursion
        private double Determinant(double[,] input)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            if (order > 2)
            {
                double value = 0;
                for (int j = 0; j < order; j++)
                {
                    double[,] Temp = CreateSmallerMatrix(input, 0, j);
                    value = value + input[0, j] * (SignOfElement(0, j) * Determinant(Temp));
                }
                return value;
            }
            else if (order == 2)
            {
                return ((input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]));
            }
            else
            {
                return (input[0, 0]);
            }
        }


        private bool checkforerrors()
        {
            isEmpty = false;
            matA = new double[dim, dim];
            dim = matdim.SelectedIndex + 1;

            for (int c = 0; c < dim; c++)
            {
                for (int d = 0; d < dim; d++)
                {
                    TextBox tx4 = new TextBox();
                    Grid.SetRow(tx4, c);
                    Grid.SetColumn(tx4, d);
                    int use = (dim * c) + d;
                    var t = Grid1.Children.ElementAt(use);

                    if (t is TextBox)
                    {
                        TextBox ttt = (TextBox)t;
                        holder = ttt.Text;


                        if ((double.TryParse(holder, out validEntry)) == false)
                        {
                            MessageBox.Show("Invalid entry at the cell in row " + c + " column " + d + ". Kindly correct your entry");
                            isEmpty = true;
                            break;
                        }

                        else if ((holder == ""))
                        {
                            MessageBox.Show("Empty Cell present at the cell in row " + c + " column " + d + " Make sure all cells are filled");
                            isEmpty = true;
                            break;
                        }
                        else
                        {
                            matA[c, d] = double.Parse(holder);
                            isEmpty = false;
                        }

                    }


                }

                if (isEmpty == true)
                {
                    break;
                }

            }
            return isEmpty;
            //   MessageBox.Show("Baba how far");
        }


        private void matdim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resu.ScrollToHorizontalOffset(0);
            result.Text = "";
            dim = matdim.SelectedIndex + 1;
            ind.Add(dim);

            count++;

            if (count > 2)
            {
                int q = ind[(ind.Count) - 2];
                for (int c = 0; c < q; c++)
                {
                    for (int d = 0; d < q; d++)
                    {
                        Grid1.Children.Clear();
                    }
                }
            }

            test.Text = count.ToString();

            Grid1.ColumnDefinitions.Clear();
            Grid1.RowDefinitions.Clear();

            if (dim >= 2)
            {
                solve.Visibility = Visibility.Visible;
                for (int a = 0; a < dim; a++)
                {
                    Grid1.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid1.RowDefinitions.Add(new RowDefinition());

                    for (int b = 0; b < dim; b++)
                    {
                        TextBox tx1 = new TextBox();
                        tx1.TextWrapping = TextWrapping.Wrap;
                        tx1.Padding = new Thickness(10, 10, 10, 10);
                        tx1.FontSize = 20;
                        tx1.TextAlignment = TextAlignment.Center;
                        tx1.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 135, 206, 235));
                        tx1.InputScope = new InputScope()
                        {
                            Names = { new InputScopeName() { NameValue = InputScopeNameValue.NumberFullWidth } }
                        };
                        Grid.SetRow(tx1, a);
                        Grid.SetColumn(tx1, b);
                        Grid1.Children.Add(tx1);


                    }
                }

            }
        }

    }
}