﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Graph_Coloring
{
    class GraphColoringAlgorithm
    {
        static int AdjacencyMatrixSize = 0;
        // a[n][n] is the adjacency matrix of the graph
        // a[i][j] = 1 if i-th and j-th vertices are adjacent
        static int[,] AdjacencyMatrix;
        // array color[n] stores the colors of the vertices
        // color[i] = 0 if we 've not colored it yet
        static int[] color;
        // array degree[n] stores the degrees of the vertices
        static int[] degree;
        // array NN[] stores all the vertices that is not adjacent to current vertex
        static int[] NN;
        // NNCount is the number of that set
        static int NNCount;
        // unprocessed is the number of vertices with which we 've not worked
        static int unprocessed;
        //=========================================================
        //===========================================================
        //					WORKING FUNCTION
        //===========================================================
        //=========================================================
        // initializing function
        static void Init()
        {
            color = new int[AdjacencyMatrixSize];
            degree = new int[AdjacencyMatrixSize];
            NN = new int[AdjacencyMatrixSize];

            for (int i = 0; i < AdjacencyMatrixSize; i++)
            {
                color[i] = 0;
                degree[i] = 0;
                for (int j = 0; j < AdjacencyMatrixSize; j++)
                    if (AdjacencyMatrix[i, j] == 1)
                        degree[i]++;
            }
            NNCount = 0;
            unprocessed = AdjacencyMatrixSize;
        }
        // this function finds the unprocessed vertex of which degree is maximum
        static int MaxDegreeVertex()
        {
            int max = -1;
            int max_i = 0;
            for (int i = 0; i < AdjacencyMatrixSize; i++)
                if (color[i] == 0)
                    if (degree[i] > max)
                    {
                        max = degree[i];
                        max_i = i;
                    }
            return max_i;
        }
        // this function is for UpdateNN function
        // it reset the value of scanned array
        static void scannedInit(int[] scanned)
        {
            for (int i = 0; i < AdjacencyMatrixSize; i++)
                scanned[i] = 0;
        }
        // this function updates NN array
        static void UpdateNN(int ColorNumber)
        {
            NNCount = 0;
            for (int i = 0; i < AdjacencyMatrixSize; i++)
                if (color[i] == 0)
                {
                    NN[NNCount] = i;
                    NNCount++;
                }
            for (int i = 0; i < AdjacencyMatrixSize; i++)
                if (color[i] == ColorNumber)
                    for (int j = 0; j < NNCount; j++)
                        while (AdjacencyMatrix[i, NN[j]] == 1 && NNCount > 0)
                        {
                            NN[j] = NN[NNCount - 1];
                            NNCount--;
                        }
        }
        // this function will find suitable y from NN
        static int FindSuitableY(int ColorNumber, ref int VerticesInCommon)
        {
            int temp, tmp_y, y = -1;
            int[] scanned = new int[AdjacencyMatrixSize];
            VerticesInCommon = 0;
            for (int i = 0; i < NNCount; i++)
            {
                tmp_y = NN[i];
                temp = 0;
                scannedInit(scanned);
                for (int x = 0; x < AdjacencyMatrixSize; x++)
                    if (color[x] == ColorNumber)
                        for (int k = 0; k < AdjacencyMatrixSize; k++)
                            if (color[k] == 0 && scanned[k] == 0)
                                if (AdjacencyMatrix[x, k] == 1 && AdjacencyMatrix[tmp_y, k] == 1)
                                {
                                    temp++;
                                    scanned[k] = 1;
                                }
                if (temp > VerticesInCommon)
                {
                    VerticesInCommon = temp;
                    y = tmp_y;
                }
            }
            return y;
        }
        // find the vertex in NN of which degree is maximum
        static int MaxDegreeInNN()
        {
            int tmp_y = -1;
            int temp, max = 0;
            for (int i = 0; i < NNCount; i++)
            {
                temp = 0;
                for (int j = 0; j < AdjacencyMatrixSize; j++)
                    if (color[NN[j]] == 0 && AdjacencyMatrix[i, NN[j]] == 1)
                        temp++;
                if (temp > max)
                {
                    max = temp;
                    tmp_y = NN[i];
                }
            }
            return tmp_y;
        }
        // processing function
        static void Coloring()
        {
            int x, y;
            int ColorNumber = 0;
            int VerticesInCommon = 0;
            while (unprocessed > 0)
            {
                x = MaxDegreeVertex();
                ColorNumber++;
                color[x] = ColorNumber;
                unprocessed--;
                UpdateNN(ColorNumber);
                while (NNCount > 0)
                {
                    y = FindSuitableY(ColorNumber, ref VerticesInCommon);
                    if (VerticesInCommon == 0)
                        y = MaxDegreeInNN();
                    color[y] = ColorNumber;
                    unprocessed--;
                    UpdateNN(ColorNumber);
                }
            }
        }

        public static IEnumerable<int> Run(int[,] _AdjacencyMatrix)
        {
            AdjacencyMatrix = _AdjacencyMatrix;
            AdjacencyMatrixSize = AdjacencyMatrix.GetLength(0);
            Init();
            Coloring();
            for (int i = 0; i < AdjacencyMatrixSize; i++)
            {
                yield return color[i];
            }
        }
    }
}
