using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseSolutions
{
    public class MatrixReader
    {

        public static int[,] BuildMatrix()
        {
            //File content must be 1000x1000 matrix number split by space (column) and line jump (row)
            string fileContent = File.ReadAllText(@"C:\map.txt");

            int i = 0, j = 0;
            int[,] matrix = new int[1000, 1000];

            // Build the matrix 1000x1000 from @fileContent
            foreach(string row in fileContent.Split('\n'))
            {
                j = 0;
                foreach(string num in row.Split(' '))
                {
                    matrix[i, j] = int.Parse(num);
                    j++;
                }
                i++;
            }

            return matrix;
        }

    }
}
