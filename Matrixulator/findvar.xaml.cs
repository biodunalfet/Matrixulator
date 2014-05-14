using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Matrixulator
{
    public partial class findvar : PhoneApplicationPage
    {
        bool rowset = false;
        bool colset = false;
        int row, col, count, dim;
        bool isEmpty;
        double[,] matA, matB;
        string holder;
        double validEntry;
        public findvar()
        {
            InitializeComponent();
            matdim.SelectionChanged += matdim_SelectionChanged;
        }

        void matdim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resu.ScrollToHorizontalOffset(0);
            if (matdim.SelectedIndex > 0)
            {
                rowset = true;
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
            else
            {
                rowset = false;
            }

           
        }

        

        private void startcalc()
        {
            if ((rowset == true))
            {
                Grid1.Visibility = Visibility.Visible;
                result.Visibility = Visibility.Visible;
                Grid2.Visibility = Visibility.Visible;
                matrix.Visibility = Visibility.Visible;
                solve.Visibility = Visibility.Visible;




                dim = matdim.SelectedIndex + 1;
                row = dim;
                inflateGrids();
            }
            else
            {
                Grid1.Visibility = Visibility.Collapsed;
                Grid2.Visibility = Visibility.Collapsed;
                result.Visibility = Visibility.Collapsed;
                matrix.Visibility = Visibility.Collapsed;

            }

        }

        private void inflateGrids()
        {
            for (int q = 0; q < dim; q++)
            {
                Grid1.ColumnDefinitions.Add(new ColumnDefinition());
                Grid1.RowDefinitions.Add(new RowDefinition());
                Grid2.RowDefinitions.Add(new RowDefinition());

            }

            Grid2.ColumnDefinitions.Add(new ColumnDefinition());


            for (int a = 0; a < dim; a++)
            {

                for (int b = 0; b < dim; b++)
                {
                    TextBox tx1 = new TextBox();
                    tx1.Padding = new Thickness(10, 10, 10, 10);
                    tx1.TextWrapping = TextWrapping.Wrap;
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

                }
            }

            for (int d = 0; d < dim; d++)
            {

                TextBox tx2 = new TextBox();
                tx2.Padding = new Thickness(10, 10, 10, 10);
                tx2.TextWrapping = TextWrapping.Wrap;
                tx2.FontSize = 20;
                tx2.TextAlignment = TextAlignment.Center;
                tx2.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 135, 206, 235));
                tx2.InputScope = new InputScope()
                {
                    Names = { new InputScopeName() { NameValue = InputScopeNameValue.Number } }
                };
                Grid.SetRow(tx2, d);
                Grid.SetColumn(tx2, 0);
                Grid2.Children.Add(tx2);
            }

        }

        private void solve_Click(object sender, RoutedEventArgs e)
        {
            count++;
            if (count > 1)
            {
                for (int c = 0; c < Grid3.Children.Count(); c++)
                {
                    for (int d = 0; d < Grid3.Children.Count(); d++)
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
                resu.ScrollToHorizontalOffset(2000);
                    Grid3.ColumnDefinitions.Add(new ColumnDefinition());

                

                for (int w = 0; w < row; w++)
                {
                    Grid3.RowDefinitions.Add(new RowDefinition());

                }
                if (row >= 2)
                {
                    Matrix ax = new Matrix(matA);
                    Matrix bx = new Matrix(matB);
                    double[,] holdv = Matrix.variables(ax, bx).data;
                   

                        Grid3.MaxHeight = (70 * row) + 50;
                        Grid3.VerticalAlignment = VerticalAlignment.Top;
                        for (int b = 0; b < row; b++)
                        {
                            TextBlock tx6 = new TextBlock();
                            tx6.HorizontalAlignment = HorizontalAlignment.Center;
                            tx6.VerticalAlignment = VerticalAlignment.Center;
                            tx6.TextAlignment = TextAlignment.Center;
                            tx6.Text = "";
                            double km = holdv[b, 0];
                            tx6.Text = km.ToString();
                            int ss = km.ToString().Length;
                            tx6.Width = ss * 30;
                            tx6.Foreground = new SolidColorBrush(Colors.Black);
                            tx6.Padding = new Thickness(5, 5, 5, 5);
                            tx6.TextWrapping = TextWrapping.Wrap;
                            tx6.FontSize = 30;


                            Grid.SetRow(tx6, b);
                            Grid.SetColumn(tx6, 0);
                            Grid3.Children.Add(tx6);

                        }
                    
                    Grid3.Visibility = Visibility.Visible;

                }
            }
        }


        private bool checkforerrors()
        {
            isEmpty = false;
            matA = new double[row, row];
            matB = new double[row, 1];

            for (int c = 0; c < row; c++)
            {
                for (int d = 0; d < row; d++)
                {
                   

                    int use = (row * c) + d;
                    var t = Grid1.Children.ElementAt(use);

                  
                    var p = Grid2.Children.ElementAt(d);

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
                            MessageBox.Show("Empty Cell present at the cell in row " + c + " column " + d + "of Matrix 1. Make sure all cells are filled");
                            isEmpty = true;
                            break;
                        }

                        else
                        {
                            matA[d, c] = double.Parse(holder);
                            isEmpty = false;
                        }

                    }

                    if (isEmpty == true)
                    {
                        return isEmpty;

                       
                    }
                }
                

                for (int y=0; y<dim; y++)
                {
                    var p = Grid2.Children.ElementAt(y);

                    if (p is TextBox)
                    {
                        TextBox ttt = (TextBox)p;
                        holder = ttt.Text;

                        if ((double.TryParse(holder, out validEntry)) == false)
                        {
                            MessageBox.Show("Invalid entry at the cell in row " + "0" + " column " + y + "in Matrix 2. Kindly correct your entry");
                            isEmpty = true;
                            break;
                        }

                        else if ((holder == ""))
                        {
                            MessageBox.Show("Empty Cell present at the cell in row " + "0" + " column " + y + "in Matrix 2. Make sure all cells are filled");
                            isEmpty = true;
                            break;
                        }

                        else
                        {
                            matB[y, 0] = double.Parse(holder);
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