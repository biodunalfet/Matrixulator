using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Input;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace Matrixulator
{
    public partial class mult : PhoneApplicationPage
    {
        int dim;
        string holder;
        string holder2;
        string asdf;
        double[,] matA;
        List<double> answer;
        List<int> ind = new List<int>();
        int count = 0;
        double p;
        double[,] matB;
        double[,] matC;
        bool isEmpty = false;
        public mult()
        {
            InitializeComponent();
            matdim.SelectionChanged+=matdim_SelectionChanged;
        }

        private void solve_Click(object sender, RoutedEventArgs e)
        {
            count++;
            if (count > 1)
            {
                int q = ind[(ind.Count) - 2];
                for (int c = 0; c < q; c++)
                {
                    for (int d = 0; d < q; d++)
                    {

                        Grid3.Children.Clear();
                    }
                }
            }

            Grid3.ColumnDefinitions.Clear();
            Grid3.RowDefinitions.Clear();
            Grid3.Visibility = Visibility.Visible;
            dim = matdim.SelectedIndex + 1;
            bool cfe = checkforerrors();
            //  test.Text =  asdf;

            if (cfe == false)
            {
                Grid3.ColumnDefinitions.Clear();
                Grid3.RowDefinitions.Clear();

                if (dim >= 2)
                {
                    multiply();
                    for (int a = 0; a < dim; a++)
                    {
                        Grid3.ColumnDefinitions.Add(new ColumnDefinition());
                        Grid3.RowDefinitions.Add(new RowDefinition());
                        Grid3.MaxHeight = (70 * dim) + 50;
                        Grid3.VerticalAlignment = VerticalAlignment.Top;
                        for (int b = 0; b < dim; b++)
                        {
                            TextBlock tx3 = new TextBlock();
                            tx3.HorizontalAlignment = HorizontalAlignment.Center;
                            tx3.VerticalAlignment = VerticalAlignment.Center;
                            tx3.TextAlignment = TextAlignment.Center;
                            tx3.Text = "";
                            tx3.Text = (answer[(dim * a) + b]).ToString();
                            int ss = (answer[(dim * a) + b]).ToString().Length;
                            tx3.Width = ss * 30;
                            // Grid3.Width = ((ss * 150) *dim);
                            //  Grid3.MaxWidth = Double.NaN;
                            tx3.Padding = new Thickness(5, 5, 5, 5);
                            tx3.TextWrapping = TextWrapping.Wrap;
                            tx3.Foreground = new SolidColorBrush(Colors.Black);
                            tx3.FontSize = 30;


                            Grid.SetRow(tx3, a);
                            Grid.SetColumn(tx3, b);
                            Grid3.Children.Add(tx3);

                        }
                    }
                    resu.ScrollToHorizontalOffset(2000);
                }
            }
        }

        private void multiply()
        {
            for (int x = 0; x < dim; x++)//change
            {
                for (int y = 0; y < dim; y++)//change
                {
                    looprow(x, y, dim);
                }
            }

        }

        public void looprow(int x, int y, int dim)
        {
            for (int z = 0; z < dim; z++) //change
            {
                p += matA[x, z] * matB[z, y % dim];  //change

            }

            answer.Add(p);

            p = 0;
        }

        private bool checkforerrors()
        {
            isEmpty = false;
            asdf = "";
            answer = new List<double>();
            matA = new double[dim, dim];
            matB = new double[dim, dim];
            matC = new double[dim, dim];
            for (int c = 0; c < dim; c++)
            {
                for (int d = 0; d < dim; d++)
                {
                    TextBox tx4 = new TextBox();
                    Grid.SetRow(tx4, c);
                    Grid.SetColumn(tx4, d);
                    int use = (dim * c) + d;
                    var t = Grid1.Children.ElementAt(use);
                    var t2 = Grid2.Children.ElementAt(use);


                    if (t is TextBox)
                    {
                        TextBox ttt = (TextBox)t;
                        TextBox ttt2 = (TextBox)t2;
                        holder = ttt.Text;
                        holder2 = ttt2.Text;
                        holder.TrimEnd('.');
                        holder2.TrimEnd('.');
                        holder.Trim();
                        holder2.Trim();
                        if ((holder == "") || (holder2 == ""))
                        {
                            MessageBox.Show("Empty Cell present at the cell in row " + c + " column " + d + " Make sure all cells are filled");
                            isEmpty = true;
                            break;
                        }
                        else
                        {
                            asdf += holder2;
                            matA[c, d] = double.Parse(holder);
                            matB[c, d] = double.Parse(holder2);
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
            // 
        }

        private void matdim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // matdim = new ListPicker();
            isEmpty = false;
            resu.ScrollToHorizontalOffset(0);
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
                        Grid2.Children.Clear();
                        Grid3.Children.Clear();
                    }
                }
            }

            Grid1.ColumnDefinitions.Clear();
            Grid1.RowDefinitions.Clear();
            Grid2.ColumnDefinitions.Clear();
            Grid2.RowDefinitions.Clear();
            if (dim >= 2)
            {
                solve.Visibility = Visibility.Visible;
                for (int a = 0; a < dim; a++)
                {
                    Grid1.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid1.RowDefinitions.Add(new RowDefinition());
                    Grid2.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid2.RowDefinitions.Add(new RowDefinition());
                    for (int b = 0; b < dim; b++)
                    {
                        TextBox tx1 = new TextBox();
                        //tx1.Width = 80;
                        //tx1.Height = 80;
                        tx1.TextWrapping = TextWrapping.Wrap;
                        tx1.Padding = new Thickness(10, 10, 10,10);
                        tx1.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 135, 206, 235));
                        tx1.FontSize = 20;
                        tx1.TextAlignment = TextAlignment.Center;
                        tx1.InputScope = new InputScope()
                        {
                            Names = { new InputScopeName() { NameValue = InputScopeNameValue.Number } }
                        };
                        Grid.SetRow(tx1, a);
                        Grid.SetColumn(tx1, b);
                        Grid1.Children.Add(tx1);

                        TextBox tx2 = new TextBox();
                        //tx2.Width = 80;
                        //tx2.Height = 80;
                        tx2.TextWrapping = TextWrapping.Wrap;
                        tx2.Padding = new Thickness(10, 10, 10, 10);
                        tx2.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 135, 206, 235));
                        tx2.FontSize = 20;
                        tx2.TextAlignment = TextAlignment.Center;
                        tx2.InputScope = new InputScope()
                        {
                            Names = { new InputScopeName() { NameValue = InputScopeNameValue.Number } }
                        };
                        Grid.SetRow(tx2, a);
                        Grid.SetColumn(tx2, b);
                        Grid2.Children.Add(tx2);
                    }
                }


            }
        }
    }
}