using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Graph_Coloring
{
    class Program
    {
        static int[,] ReadAdjacencyMatrix(string DataFilePath)
        {
            int[,] AdjacencyMatrix = null;
            string[] Data = File.ReadAllLines(DataFilePath);
            int AdjacencyMatrixSize = Data.Length;
            string[] Split = null;
            AdjacencyMatrix = new int[AdjacencyMatrixSize, AdjacencyMatrixSize];
            for (int i = 0; i < AdjacencyMatrixSize; i++)
            {
                Split = Data[i].Split(new char[] { ' ' });
                for (int j = 0; j < Split.Length; j++)
                    AdjacencyMatrix[i, j] = Convert.ToInt32(Split[j]);
            }
            return AdjacencyMatrix;
        }

        static void GetInput()
        {
            ReadAdjacencyMatrix(System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\input.txt");
        }


        static void Main(string[] args)
        {
            IEnumerable<int> Color = GraphColoringAlgorithm.Run(ReadAdjacencyMatrix(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\input.txt"));
            int i=0;
            foreach (int C in Color)
            {
                i++;
                Console.Write("Vertex ");
                Console.Write(i);
                Console.Write(" is colored ");
                Console.Write(C);
                Console.Write("\n");
            }
            Console.ReadKey(true);
        }
    }
}
