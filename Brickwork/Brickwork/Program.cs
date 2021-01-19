using System;
using System.Linq;

namespace Brickwork
{
    class Program
    {
        private static int n;
        private static int m;
        private static int[,] firstLayer;
        private static int[,] secondLayer;
        private static int[] bricks;

        static void Main()
        {
            ReadFirstLayer();
            Console.WriteLine();
            bool isFirstLayerValid = IsValid(firstLayer);

            if (isFirstLayerValid)
            {
                CreateSecondLayer();
                PrintLayer(secondLayer);
                PrintBricksWithOutline(secondLayer);
            }
            else
            {
                PrintInvalidInputMsg();
            }
        }

        // prints layer of bricks 
        private static void PrintLayer(int[,] layer)
        {
            for (int i = 0; i < layer.GetLength(0); i++)
            {
                for (int j = 0; j < layer.GetLength(1); j++)
                {
                    Console.Write(layer[i,j]+ " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }
        private static void PrintInvalidInputMsg()
        {
            Console.WriteLine(-1);
            Console.WriteLine("The second layer of bricks can not be created.");
        }

        // validates if layer is valid
        private static bool IsValid(int[,] layer)
        {
            bool result = false;
            int n = layer.GetLength(0);
            int m = layer.GetLength(1);

            if (n < 100 && m < 100)
            {
                result = true;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j + 2 < m)
                    {
                        if (layer[i, j] == layer[i, j + 1])
                        {

                            if (layer[i, j] == layer[i, j + 2])
                            {
                                result = false;
                                return result;
                            }
                        }
                    }
                    if (i + 2 < n)
                    {
                        if (layer[i, j] == layer[i + 1, j])
                        {
                            if (layer[i, j] == layer[i + 2, j])
                            {
                                result = false;
                                return result;
                            }
                        }

                    }
                }
            }
            return result;
        }

        // reads first layer input
        private static void ReadFirstLayer()
        {
            int[] layerDimensions = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            n = layerDimensions[0];
            m = layerDimensions[1];

            //calculate the total number of bricks
            bricks = new int[n * m / 2];
            int count = 0;

            // initialize the first layer
            firstLayer = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                int[] row = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                for (int j = 0; j < m; j++)
                {
                    firstLayer[i, j] = row[j];

                    if (!bricks.Contains(row[j]))
                    {
                        bricks[count] = row[j];
                        count++;
                    }
                }
            }
        }

        // initializes second layer
        static void CreateSecondLayer()
        {
            // swap the positions of bricks in the first layer, 
            // to be sure their order will be diffrent and 
            // they will take diffrent positions at the second layer
            SwapPositions(bricks);

            secondLayer = new int[n, m];

            // bricks list index
            int brickIndex = 0;

            // fill in the second layer 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (secondLayer[i, j] != 0)
                    {
                        continue;
                    }

                    secondLayer[i, j] = bricks[brickIndex];

                    if (j + 1 > m - 1 || firstLayer[i, j + 1] != firstLayer[i, j])
                    {
                        secondLayer[i + 1, j] = bricks[brickIndex];
                    }
                    else
                    {
                        secondLayer[i, j + 1] = bricks[brickIndex];
                    }
                    brickIndex++;
                }
            }
        }

        // prints layer of bricks with visible outline
        private static void PrintBricksWithOutline(int[,] layer)
        {
            int numCols = layer.GetLength(1);
            int numRows = layer.GetLength(0);

            // PrintResult first line
            string line = new String('*', numCols * 4 + 1);
            Console.WriteLine(line);

            // print table body
            for (int i = 0; i < numRows; i++)
            {
                // start each row with "*"
                Console.Write("*");
                for (int j = 0; j < numCols; j++)
                {
                    // checks if number of brick is smaller or bigger than 10
                    // to know how many spaces should be between the two numbers of brick
                    if (layer[i, j] < 10)
                    {
                        // if it is on the last column
                        if (j == numCols - 1)
                        {
                            Console.Write($" {layer[i, j]} *");
                            break;
                        }
                        // checks if brick is vertical or horizontal
                        if (layer[i, j] != layer[i, j + 1])
                        {
                            Console.Write($" {layer[i, j]} *");
                        }
                        else
                        {
                            Console.Write($" {layer[i, j]}  ");
                        }
                    }
                    else
                    {
                        // if j is on the last column
                        if (j == numCols - 1)
                        {
                            Console.Write($" {layer[i, j]}*");
                            break;
                        }
                        // if i is on the last row
                        if (i == numRows - 1)
                        {
                            // check if the part of the brick is from the last printed brick,
                            // horizontal or vertical one
                            if (layer[i, j] != layer[i, j + 1] && layer[i, j] != layer[i, j - 1])
                            {
                                Console.Write($" {layer[i, j]} *");
                            }
                            else if (layer[i, j] == layer[i, j + 1])
                            {
                                Console.Write($" {layer[i, j]} ");
                            }
                            else if (layer[i, j] != layer[i, j + 1] && layer[i, j] == layer[i, j - 1])
                            {
                                Console.Write($"{layer[i, j]} *");
                            }
                            continue;
                        }

                        // check if the part of the brick is from the last printed brick,
                        // horizontal or vertical one
                        if (layer[i, j] != layer[i, j + 1] && layer[i, j] == layer[i + 1, j])
                        {
                            Console.Write($" {layer[i, j]}*");
                        }
                        else if (layer[i, j] == layer[i, j + 1])
                        {
                            Console.Write($" {layer[i, j]} ");
                        }
                        else if (layer[i, j] != layer[i, j + 1] && layer[i, j] != layer[i + 1, j])
                        {
                            if (layer[i, j] == layer[i, j - 1])
                            {
                                Console.Write($"{layer[i, j]} *");
                            }
                            else
                            {
                                Console.Write($" {layer[i, j]}*");
                            }
                        }
                    }
                }
                Console.WriteLine();

                // create line between bricks starting with one "*"
                line = "*";

                for (int col = 0; col < numCols; col++)
                {
                    if (i == numRows - 1)
                    {
                        line = new String('*', numCols * 4 + 1);
                        break;
                    }
                    if (layer[i, col] == layer[i + 1, col])
                    {
                        if (layer[i, col] < 10)
                        {
                            line += "   *";
                        }
                        else
                        {
                            line += "   *";
                        }
                    }
                    else
                    {
                        line += "****";
                    }
                }
                // print line between bricks
                Console.WriteLine(line);
            }
        }

        // swaps positions of elements in array a1 <-> a2, a3 <-> a4 etc.
        private static void SwapPositions(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i += 2)
            {
                Swap(i, i + 1, arr);
            }
        }

        // swaps the values of two elemnts in an array
        private static void Swap(int i, int j, int[] arr)
        {
            int t = arr[i];
            arr[i] = arr[j];
            arr[j] = t;
        }

    }
}
