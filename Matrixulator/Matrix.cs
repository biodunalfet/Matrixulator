using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrixulator
{
    class Matrix
    {
        public int nrows;
        public int ncols;
        public double[,] data;
        public Matrix(double[,] dat)
        {
            this.data = dat;
            this.nrows = dat.GetLength(0);
            this.ncols = dat.Length / dat.GetLength(0);
        }

        public Matrix(int nrow, int ncol)
        {		//creates an entirely new matrix
            this.nrows = nrow;
            this.ncols = ncol;
            data = new double[nrow, ncol];
        }

        public static Matrix transpose(Matrix matrix)
        {
            Matrix transposedMatrix = new Matrix(matrix.ncols, matrix.nrows);

            for (int i = 0; i < matrix.nrows; i++)
            {
                for (int j = 0; j < matrix.ncols; j++)
                {
                    transposedMatrix.data[j, i] = matrix.data[i, j];
                    //transposedMatrix.setValueAt(j, i, matrix.getValueAt(i, j));
                }
            }

            return transposedMatrix;
        }

        public void printMatrix(Matrix matrix)
        {
            for (int i = 0; i < matrix.nrows; i++)
            {
                for (int j = 0; j < matrix.ncols; j++)
                {
                    Console.Write(matrix.data[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static bool isSquare(Matrix matrix)
        {
            if (matrix.ncols == matrix.nrows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int CheckSign(int i)
        {
            if ((i % 2) == 0)
            {
                return 1;
            }

            else
            {
                return -1;
            }
        }

        public static Matrix createSubMatrix(Matrix matrix, int excluding_row, int excluding_col)
        {
            Matrix mat = new Matrix(matrix.nrows - 1, matrix.ncols - 1);
            int r = -1;
            for (int i = 0; i < matrix.nrows; i++)
            {
                if (i == excluding_row)
                {
                    continue;
                }
                r++;
                int c = -1;
                for (int j = 0; j < matrix.ncols; j++)
                {
                    if (j == excluding_col)
                    {
                        continue;
                    }
                    mat.data[r, ++c] = matrix.data[i, j];
                }
            }
            return mat;
        }

        public static double determinant(Matrix matrix)
        {
            if (isSquare(matrix) == false)
            {
                return 0;
            }

            else
            {
                if (matrix.ncols == 2)
                {
                    return (matrix.data[0, 0] * matrix.data[1, 1]) -
                        (matrix.data[0, 1] * matrix.data[1, 0]);
                }

                else
                {
                    double sum = 0;
                    for (int i = 0; i < matrix.ncols; i++)
                    {
                        sum += CheckSign(i) * matrix.data[0, i] * determinant(createSubMatrix(matrix, 0, i));
                    }
                    return sum;
                }
            }
        }

        public static Matrix cofactor(Matrix matrix)
        {
            Matrix mat = new Matrix(matrix.nrows, matrix.ncols);
            if (matrix.ncols == 2)
            {
                mat.data[0, 0] = matrix.data[1, 1];
                mat.data[0, 1] = -1 * matrix.data[1, 0];
                mat.data[1, 0] = -1 * matrix.data[0, 1];
                mat.data[1, 1] = matrix.data[0, 0];
            }
            else
            {
                for (int i = 0; i < matrix.nrows; i++)
                {
                    for (int j = 0; j < matrix.ncols; j++)
                    {
                        mat.data[i, j] = CheckSign(i) * CheckSign(j) * determinant(createSubMatrix(matrix, i, j));
                    }
                }
            }
            return mat;
        }

        public static Matrix inverse(Matrix matrix)
        {
            if ((matrix.ncols) == 2)
            {
                Matrix fortwo = new Matrix(2, 2);
                fortwo.data[0, 0] = matrix.data[1, 1];
                fortwo.data[0, 1] = -1 * matrix.data[0, 1];
                fortwo.data[1, 0] = -1 * matrix.data[1, 0];
                fortwo.data[1, 1] = matrix.data[0, 0];

                return multiplyByConstant((1 / determinant(matrix)), fortwo);
            }
            return multiplyByConstant((1 / determinant(matrix)), transpose(cofactor(matrix)));
        }

        public static Matrix multiplyByConstant(double det, Matrix matrix)
        {
            Matrix matmul = new Matrix(matrix.nrows, matrix.ncols);
            for (int i = 0; i < matrix.nrows; i++)
            {
                for (int j = 0; j < matrix.ncols; j++)
                {
                    matmul.data[i, j] = det * matrix.data[i, j];
                }
            }
            return matmul;
        }

        public static Matrix variables(Matrix mat, Matrix res)
        {
            Matrix ade = inverse(mat);
            Matrix variable = new Matrix(mat.nrows, 1);

            for (int i = 0; i < mat.nrows; i++)
            {
                double sum = 0;
                for (int j = 0; j < mat.ncols; j++)
                {
                    sum += ade.data[i, j] * res.data[j, 0];
                }
                variable.data[i, 0] = sum;
            }
            return variable;

        }
    }
}