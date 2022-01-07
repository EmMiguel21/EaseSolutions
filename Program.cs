using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseSolutions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build the matrix from txt file (open README.txt file)
            int[,] matrix = MatrixReader.BuildMatrix();

            Console.WriteLine("Press a Key to calculate the path");
            Console.ReadKey();
            
            var results = Start(matrix);
            GetLongestAndSteepest(results);

        }

        private static List<TestResult> Start(int[,] matrix)
        {
            List<int> path = new List<int>();
            int x = 0,
                y = 0;

            List<TestResult> results = new List<TestResult>();
            TestResult result = new TestResult();

            for (int i = 0; i <= 999;)
            {
                for (int j = 0; j <= 999;)
                {

                    int num = matrix[i, j];
                    path.Add(num);

                    // Cheking east path
                    x = i;
                    y = j + 1;
                    result = LookForPath(x, y, num, matrix, path);
                    if (result != null)
                        results.Add(BuildResult(path));
                    path = new List<int>();
                    path.Add(num);

                    // Checking west path
                    x = i;
                    y = j - 1;
                    result = LookForPath(x, y, num, matrix, path);
                    if (result != null)
                        results.Add(BuildResult(path));
                    path = new List<int>();
                    path.Add(num);

                    // Checking north path
                    x = i - 1;
                    y = j;
                    result = LookForPath(x, y, num, matrix, path);
                    if (result != null)
                        results.Add(BuildResult(path));
                    path = new List<int>();
                    path.Add(num);

                    //Checking south path
                    x = i + 1;
                    y = j;
                    result = LookForPath(x, y, num, matrix, path);
                    if (result != null)
                        results.Add(BuildResult(path));

                    j++;
                    path = new List<int>();
                }
                i++;
            }
            return results;
        }

        public static TestResult LookForPath(int x, int y, int num, int[,] matrix, List<int> path)
        {
            if (x < 0 || x > 999 || y < 0 || y > 999)
            {
                return null;
            }

            TestResult result = null;

            int num2 = matrix[x, y];
            if (num2 < num)
            {
                List<int> altPath = path;
                
                int w = x;
                int z = y;
                // Checking east path
                x = w;
                y = z + 1;
                result = LookForPath(x, y, num2, matrix, altPath);
                if (result != null)
                {
                    altPath.Add(num2);
                    return new TestResult();
                }

                // Checking west path
                x = w;
                y = z - 1;
                result = LookForPath(x, y, num2, matrix, path);
                if (result != null)
                {
                    altPath.Add(num2);
                    return new TestResult();
                }

                // Checking north path
                x = w - 1;
                y = z;
                result = LookForPath(x, y, num2, matrix, path);
                if (result != null)
                {
                    altPath.Add(num2);
                    return new TestResult();
                }

                //Checking south path
                x = w + 1;
                y = z;
                result = LookForPath(x, y, num2, matrix, path);
                if (result != null)
                {
                    altPath.Add(num2);
                    return new TestResult();
                }

                if (w == 999)
                {
                    altPath.Add(num2);
                    return new TestResult();
                }

            }

            return result;
        }

        private static TestResult BuildResult(List<int> path)
        {
            TestResult result = new TestResult();
            
            path.Sort((a, b) => b.CompareTo(a));

            string pathString = string.Empty;
            path.ForEach(p =>
            {
                pathString += string.Concat(p, "-");
            });

            result.path = pathString;
            result.drop = path.First() - path.Last();
            result.distance = path.Count;
            

            return result;
        }

        private static TestResult GetLongestAndSteepest(List<TestResult> results)
        {
            TestResult result = new TestResult();

            result = results.OrderByDescending(r => r.distance).
                ThenByDescending(x => x.drop).First();
            
            return result;
        }
    }
}
