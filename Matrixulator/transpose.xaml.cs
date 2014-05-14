using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Input;
using Microsoft.Phone.Shell;

namespace Matrixulator
{
      
    public partial class transpose : PhoneApplicationPage
    {
        bool rowset = false;
        bool isEmpty = false;
        double[,] matA;
        string holder;
        double conval = 0;
        bool colset = false;
        int count;
        List<int> ind = new List<int>();
        double validEntry;
        int row;
        int col;
        public transpose()
        {
            InitializeComponent();
            rowpick.SelectionChanged+=rowpick_SelectionChanged;
            columnpick.SelectionChanged+=columnpick_SelectionChanged;
        }

        private void columnpick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resu.ScrollToHorizontalOffset(0);
            if (columnpick.SelectedIndex > 0)
            {
                colset = true;
            }
            else
            {
                colset = false;
            }

            for (int c = 0; c < Grid2.Children.Count(); c++)
            {
                for (int d = 0; d < Grid2.Children.Count(); d++)
                {

                    Grid1.Children.Clear();

                }
            }

            startcalc();
        }

        void rowpick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resu.ScrollToHorizontalOffset(0);
            if (rowpick.SelectedIndex > 0)
            {
                rowset = true;
            }
            else
            {
                rowset = false;
            }

            for (int c = 0; c < Grid2.Children.Count(); c++)
            {
                for (int d = 0; d < Grid2.Children.Count(); d++)
                {

                    Grid1.Children.Clear();

                }
            }
            startcalc();
        }

        private void startcalc()
        {
            if ((rowset == true) && (colset == true))
            {
                row = rowpick.SelectedIndex + 1;
                col = columnpick.SelectedIndex + 1;

                solve.Visibility = Visibility.Visible;
                Grid1.Visibility = Visibility.Visible;

                for (int q = 0; q < col; q++)
                {
                    Grid1.ColumnDefinitions.Add(new ColumnDefinition());

                }

                for (int w = 0; w < row; w++)
                {
                    Grid1.RowDefinitions.Add(new RowDefinition());

                }

                for (int a = 0; a < col; a++)
                {

                    for (int b = 0; b < row; b++)
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
                        Grid.SetRow(tx1, b);
                        Grid.SetColumn(tx1, a);
                        Grid1.Children.Add(tx1);
                    }
                }
                Grid1.Visibility = Visibility.Visible;
            }

            else
            {
                solve.Visibility = Visibility.Collapsed;
                Grid1.Visibility = Visibility.Collapsed;
            }

           
        }

        private void solve_Click(object sender, RoutedEventArgs e)
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
                resu.ScrollToHorizontalOffset(2000);
                Matrix ade= new Matrix(matA);
                double[,] holdtr= Matrix.transpose(ade).data;

                for (int q = 0; q < row; q++)
                {
                    Grid2.ColumnDefinitions.Add(new ColumnDefinition());

                }

                for (int w = 0; w < col; w++)
                {
                    Grid2.RowDefinitions.Add(new RowDefinition());

                }
                if (row >= 2)
                {
                    for (int a = 0; a < col; a++)
                    {

                        Grid2.MaxHeight = (70 * row) + 50;
                        Grid2.VerticalAlignment = VerticalAlignment.Top;
                        for (int b = 0; b < row; b++)
                        {
                            TextBlock tx3 = new TextBlock();
                            tx3.HorizontalAlignment = HorizontalAlignment.Center;
                            tx3.VerticalAlignment = VerticalAlignment.Center;
                            tx3.TextAlignment = TextAlignment.Center;
                            tx3.Text = "";
                            double km = holdtr[b, a];
                            tx3.Text = km.ToString();
                            int ss = km.ToString().Length;
                            tx3.Width = ss * 30;
                            tx3.Padding = new Thickness(5, 5, 5, 5);
                            tx3.TextWrapping = TextWrapping.Wrap;
                            tx3.Foreground = new SolidColorBrush(Colors.Black);
                            tx3.FontSize = 30;


                            Grid.SetRow(tx3, a);
                            Grid.SetColumn(tx3, b);
                            Grid2.Children.Add(tx3);

                        }
                    }
                    Grid2.Visibility = Visibility.Visible;

                }
            }
        }



        private bool checkforerrors()
        {
            isEmpty = false;
            matA = new double[col, row];

            for (int c = 0; c < col; c++)
            {
                for (int d = 0; d < row; d++)
                {
                    TextBox tx4 = new TextBox();
                    Grid.SetRow(tx4, c);
                    Grid.SetColumn(tx4, d);
                    int use = (row * c) + d;
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
        }
    }
}