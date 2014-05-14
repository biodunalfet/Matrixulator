using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using System.Windows.Media;

namespace Matrixulator
{
    public partial class inverse : PhoneApplicationPage
    {
        bool isEmpty = false;
        double[,] matA;
        List<int> ind = new List<int>();
        int dim;
        int count;
        double validEntry;
        string holder;
        public inverse()
        {
            InitializeComponent();
            matdim.SelectionChanged += matdim_SelectionChanged;

        }

        private void matdim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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


        private void solve_Click_1(object sender, RoutedEventArgs e)
        {
            count++;
            if (count > 1)
            {
                for (int c = 0; c < Grid2.Children.Count(); c++)
                {
                    for (int d = 0; d < Grid2.Children.Count(); d++)
                    {

                        Grid2.Children.Clear();
                    }
                }
            }

            Grid2.ColumnDefinitions.Clear();
            Grid2.RowDefinitions.Clear();
            bool cfe = checkforerrors();

            if (cfe == false)
            {
                Matrix forinverse = new Matrix(matA);
                double[,] holdinv = Matrix.inverse(forinverse).data;
                for (int q = 0; q < dim; q++)
                {
                    Grid2.ColumnDefinitions.Add(new ColumnDefinition());

                }

                for (int w = 0; w < dim; w++)
                {
                    Grid2.RowDefinitions.Add(new RowDefinition());

                }
                if (dim >= 2)
                {
                    for (int a = 0; a < dim; a++)
                    {
                        Grid2.MaxHeight = (70 * dim) + 50;
                        Grid2.VerticalAlignment = VerticalAlignment.Top;
                        for (int b = 0; b < dim; b++)
                        {
                            TextBlock tx3 = new TextBlock();
                            tx3.HorizontalAlignment = HorizontalAlignment.Center;
                            tx3.VerticalAlignment = VerticalAlignment.Center;
                            tx3.TextAlignment = TextAlignment.Center;
                            tx3.Text = "";
                            double km = Math.Round(holdinv[b, a], 2);
                            tx3.Text = km.ToString();
                            int ss = km.ToString().Length;
                            tx3.Width = ss * 30;
                            tx3.TextWrapping = TextWrapping.Wrap;
                            tx3.Padding = new Thickness(5, 5, 5, 5);
                            tx3.Foreground = new SolidColorBrush(Colors.Black);
                            tx3.FontSize = 30;


                            Grid.SetRow(tx3, b);
                            Grid.SetColumn(tx3, a);
                            Grid2.Children.Add(tx3);

                        }
                    }
                    Grid2.Visibility = Visibility.Visible;

                }
            }
        }
    }
}
