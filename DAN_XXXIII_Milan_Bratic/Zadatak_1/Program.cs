using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        //path to the file
        static string path = "../../FileByThread_1.txt";
        static string path2 = "../../FileByThread_22.tx";
        /// <summary>
        /// a method that generates a matrix and places them in a file
        /// </summary>
        private static void IdentityMatrix()
        {

            int[,] matrix = new int[100, 100];
            using (TextWriter tw = new StreamWriter(path))
            {
                for (int i = 0; i < 100; ++i)
                {
                    for (int j = 0; j < 100; ++j)
                    {
                        if (i == j)
                            matrix[i, j] = 1;

                        tw.Write(matrix[i, j]);
                    }
                    tw.WriteLine();
                }
            }
        }
        /// <summary>
        /// method for generating odd numbers and places them in the file
        /// </summary>
        private static void OddNumbers()
        {

            Random rnd = new Random();
            int counter = 0;
            using (TextWriter tw = new StreamWriter(path2))
            {
                while (counter < 1000)
                {
                    int i = rnd.Next(0, 10000);
                    if (i % 2 != 0)
                    {
                        tw.WriteLine(i);
                        counter++;
                    }
                }
            }

        }
        /// <summary>
        /// method for read matrix from the file
        /// </summary>
        private static void ReadMatrixFromTheFile()
        {
            using (TextReader tr = new StreamReader(path))
            {
                Console.WriteLine(tr.ReadToEnd());
            }
        }
        /// <summary>
        /// sum calculate method
        /// </summary>
        private static void Sum()
        {
            Thread.Sleep(1000);
            //read all lines from the file in array
            string[] lines = File.ReadAllLines(path2);
            int sum = 0;
            //going through the loop and adding
            foreach (var item in lines)
            {
                sum += int.Parse(item);
            }
            Console.WriteLine("Sum: " + sum);
        }
        static void Main(string[] args)
        {
            string[] threadNames = new string[4];
            //a loop that puts the required names in array of strings
            for (int i = 0; i < threadNames.Length; i++)
            {
                if (i % 2 != 0)
                {
                    threadNames[i] = "THREAD_" + (i + 1) + "" + (i + 1);
                    continue;
                }
                threadNames[i] = "THREAD_" + (i + 1);
            }
            Thread thread1 = new Thread(IdentityMatrix);
            thread1.Name = threadNames[0];
            Console.WriteLine(thread1.Name + " created");

            Thread thread2 = new Thread(OddNumbers);
            thread2.Name = threadNames[1];
            Console.WriteLine(thread2.Name + " created");

            Thread thread3 = new Thread(ReadMatrixFromTheFile);
            thread3.Name = threadNames[2];
            Console.WriteLine(thread3.Name + " created");

            Thread thread4 = new Thread(Sum);
            thread4.Name = threadNames[3];
            Console.WriteLine(thread4.Name + " created");
            //stopwatch help us to calculate elapsed time
            Stopwatch sw = new Stopwatch();
            sw.Start();
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine("Elapsed time: " + ts);
            thread3.Start();
            thread4.Start();
            Console.ReadLine();
        }
    }
}
