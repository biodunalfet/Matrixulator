using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media;
using System.Windows.Input;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Media;

namespace Matrixulator
{
    public partial class addition : PhoneApplicationPage
    {
        bool rowset = false;
        bool colset = false;
        int row, col, count;
        bool isEmpty;
        double[,] matA, matB;
        string holder;
        double validEntry;
        public addition()
        {
            InitializeComponent();
            rowpick.ItemsSource = new string[] {"Row", "1", "2", "3", "4", "5" };
            columnpick.ItemsSource = new string[] { "Column", "1", "2", "3", "4", "5" };
            rowpick.SelectionChanged+=rowpick_SelectionChanged;
            columnpick.SelectionChanged+=columnpick_SelectionChanged;
        }

        private void rowpick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
                    Grid2.Children.Clear();

                }
            }
            startcalc();
        }

        private void startcalc()
        {
            if ((rowset == true) && (colset == true))
            {
                Grid1.Visibility = Visibility.Visible;
                Grid2.Visibility = Visibility.Visible;
                solve.Visibility = Visibility.Visible;

                row = rowpick.SelectedIndex;
                col = columnpick.SelectedIndex;
                resu.ScrollToHorizontalOffset(0);

                inflateGrids();

            }
            else
            {
                Grid1.Visibility = Visibility.Collapsed;
                Grid2.Visibility = Visibility.Collapsed;
                solve.Visibility = Visibility.Collapsed;

            }

        }

        private void inflateGrids()
        {
            for (int q = 0; q < col; q++)
            {
                Grid1.ColumnDefinitions.Add(new ColumnDefinition());
                Grid2.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int w = 0; w < row; w++)
            {
                Grid1.RowDefinitions.Add(new RowDefinition());
                Grid2.RowDefinitions.Add(new RowDefinition());

            }

            for (int a = 0; a < col; a++)
                {

                    for (int b = 0; b < row; b++)
                    {
                        TextBox tx1 = new TextBox();
                        //tx1.Width = 80;
                        //tx1.Height = 80;
                        tx1.TextWrapping = TextWrapping.Wrap;
                        tx1.Padding = new Thickness(10, 10, 10, 10);
                        tx1.FontSize = 20;
                        tx1.TextAlignment = TextAlignment.Center;
                        tx1.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 135, 206, 235));

                        tx1.InputScope = new InputScope()
                        {
                            Names = { new InputScopeName() { NameValue = InputScopeNameValue.Number } }
                        };
                        Grid.SetRow(tx1, b);
                        Grid.SetColumn(tx1, a);
                        Grid1.Children.Add(tx1);

                        TextBox tx2 = new TextBox();
                        //tx2.Width = 80;
                        //tx2.Height = 80;
                        tx2.TextWrapping = TextWrapping.Wrap;
                        tx2.Padding = new Thickness(10, 10, 10, 10);
                        tx2.FontSize = 20;

                        tx2.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 135,206,235));
                        tx2.TextAlignment = TextAlignment.Center;
                        tx2.InputScope = new InputScope()
                        {
                            Names = { new InputScopeName() { NameValue = InputScopeNameValue.Number } }
                        };
                        Grid.SetRow(tx2, b);
                        Grid.SetColumn(tx2, a);
                        Grid2.Children.Add(tx2);

                        TextBlock tx3 = new TextBlock();
                       
                        tx3.FontSize = 25;
                        tx3.TextAlignment = TextAlignment.Center;
                        Grid.SetRow(tx3, b);
                        Grid.SetColumn(tx3, a);
                        Grid3.Children.Add(tx3);
                    }
                }
            
        }

        private void columnpick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
                    Grid2.Children.Clear();

                }
            }

            startcalc();
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

                        Grid3.Children.Clear();
                    }
                }
            }

            Grid3.ColumnDefinitions.Clear();
            Grid3.RowDefinitions.Clear();
            bool cfe = checkforerrors();

            if (cfe == false)
            {

                for (int q = 0; q < col; q++)
                {
                    Grid3.ColumnDefinitions.Add(new ColumnDefinition());

                }

                for (int w = 0; w < row; w++)
                {
                    Grid3.RowDefinitions.Add(new RowDefinition());

                }
                if (row >= 1)
                {

                    for (int a = 0; a < col; a++)
                    {

                        Grid3.MaxHeight = (70 * row) + 50;
                        Grid3.VerticalAlignment = VerticalAlignment.Top;
                        for (int b = 0; b < row; b++)
                        {
                            TextBlock tx6 = new TextBlock();
                            tx6.HorizontalAlignment = HorizontalAlignment.Center;
                            tx6.VerticalAlignment = VerticalAlignment.Center;
                            tx6.TextAlignment = TextAlignment.Center;
                            tx6.Text = "";
                            double km = matA[a, b] + matB[a, b];

                            tx6.Text = km.ToString();
                            int ss = km.ToString().Length;
                            tx6.Width = ss * 30;
                           
                            //tx6.Width = 70;
                            //tx6.Height = 70;
                            tx6.Padding = new Thickness(5, 5, 5, 5);
                            tx6.TextWrapping = TextWrapping.Wrap;

                            tx6.Foreground = new SolidColorBrush(Colors.Black);
                            tx6.FontSize = 30;


                            Grid.SetRow(tx6, b);
                            Grid.SetColumn(tx6, a);
                            Grid3.Children.Add(tx6);

                        }
                    }
                    Grid3.Visibility = Visibility.Visible;
                    resu.ScrollToHorizontalOffset(2000);

                }
            }
        }

          
        private bool checkforerrors()
        {
            isEmpty = false;
            matA = new double[col, row];
            matB = new double[col, row];

            for (int c = 0; c < col; c++)
            {
                for (int d = 0; d < row; d++)
                {
                    //TextBox tx4 = new TextBox();
                    //Grid.SetRow(tx4, c);
                    //Grid.SetColumn(tx4, d);

                    int use = (row * c) + d;
                    var t = Grid1.Children.ElementAt(use);
                    
                    //TextBox tx5 = new TextBox();
                    //Grid.SetRow(tx5, c);
                    //Grid.SetColumn(tx5, d);
                    var p = Grid2.Children.ElementAt(use);

                    if (t is TextBox)
                    {
                        TextBox ttt = (TextBox)t;
                        holder = ttt.Text;

                        if ((double.TryParse(holder, out validEntry)) == false)
                        {
                            MessageBox.Show("Invalid entry at the cell in row " + c + " column " + d + " of Matrix 1. Kindly correct your entry");
                            isEmpty = true;
                            break;
                        }

                        else if ((holder == ""))
                        {
                            MessageBox.Show("Empty Cell present at the cell in row " + c + " column " + d + " of Matrix 1. Make sure all cells are filled");
                            isEmpty = true;
                            break;
                        }

                        else
                        {
                            matA[c, d] = double.Parse(holder);
                            isEmpty = false;
                        }

                    }

                    if (p is TextBox)
                    {
                        TextBox ttt = (TextBox)p;
                        holder = ttt.Text;

                        if ((double.TryParse(holder, out validEntry)) == false)
                        {
                            MessageBox.Show("Invalid entry at the cell in row " + c + " column " + d + "in Matrix 2. Kindly correct your entry");
                            isEmpty = true;
                            break;
                        }

                        else if ((holder == ""))
                        {
                            MessageBox.Show("Empty Cell present at the cell in row " + c + " column " + d + "in Matrix 2. Make sure all cells are filled");
                            isEmpty = true;
                            break;
                        }

                        else
                        {
                            matB[c, d] = double.Parse(holder);
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
