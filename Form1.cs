using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingVisualizer
{
    public partial class Form1 : Form
    {
        private static readonly ThreadLocal<Random> threadLocalRandom =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
        private static int seed = Environment.TickCount;
        private int[] numbers;

        public Form1()
        {
            InitializeComponent();
            GenerateRandomData();
        }

        private void GenerateRandomData()
        {
            numbers = new int[100000];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = threadLocalRandom.Value.Next(1, 300);
            }
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Choose your sorting algorithm
            // await MergeSort(numbers, 0, numbers.Length - 1); 
            // await QuickSort(numbers, 0, numbers.Length - 1);
            // await BubbleSort();
            // await InsertionSort();
             await SelectionSort();

            stopwatch.Stop();
            long elapsedNanoseconds = (stopwatch.ElapsedTicks * 1000000000) / Stopwatch.Frequency;

            MessageBox.Show("RunTime " + elapsedNanoseconds.ToString("N0") + " ns", "Execution Time");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawNumbers(e.Graphics);
        }

        private void DrawNumbers(Graphics g)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                g.FillRectangle(Brushes.Blue, i * 10, Height - numbers[i], 8, numbers[i]);
            }
        }

        // BUBBLE SORT
        private async Task BubbleSort()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1 - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        var temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;

                        Invalidate();  
                        await Task.Delay(50);  
                    }
                }
            }
        }

        // INSERTION SORT
        private async Task InsertionSort()
        {
            int n = numbers.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = numbers[i];
                int j = i - 1;

                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j = j - 1;

                    Invalidate(); 
                    await Task.Delay(1);  
                }
                numbers[j + 1] = key;

                Invalidate();  
                await Task.Delay(50);  
            }
        }

        // SELECTION SORT (O(n^2))
        private async Task SelectionSort()
        {
            int n = numbers.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (numbers[j] < numbers[min_idx])
                        min_idx = j;

                int temp = numbers[min_idx];
                numbers[min_idx] = numbers[i];
                numbers[i] = temp;

                Invalidate();  
                //await Task.Delay(5); 
            }
        }

        // MERGE SORT
        private async Task MergeSort(int[] numbers, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                await MergeSort(numbers, left, middle);
                await MergeSort(numbers, middle + 1, right);
                await Merge(numbers, left, middle, right);
            }
        }

        private async Task Merge(int[] numbers, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;
            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];
            Array.Copy(numbers, left, leftArray, 0, n1);
            Array.Copy(numbers, middle + 1, rightArray, 0, n2);

            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    numbers[k] = leftArray[i];
                    i++;
                }
                else
                {
                    numbers[k] = rightArray[j];
                    j++;
                }
                k++;
                Invalidate();
                //await Task.Delay(50);
            }

            while (i < n1)
            {
                numbers[k] = leftArray[i];
                i++;
                k++;
                Invalidate();
                //await Task.Delay(50);
            }

            while (j < n2)
            {
                numbers[k] = rightArray[j];
                j++;
                k++;
                Invalidate();
                //await Task.Delay(50);
            }
        }

        // QUICK SORT (O(n log(n)))
        private async Task QuickSort(int[] numbers, int low, int high)
        {
            if (low < high)
            {
                int pi = await Partition(numbers, low, high);
                await QuickSort(numbers, low, pi - 1);
                await QuickSort(numbers, pi + 1, high);
            }
        }

        private async Task<int> Partition(int[] numbers, int low, int high)
        {
            int pivot = numbers[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (numbers[j] < pivot)
                {
                    i++;
                    int temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                    Invalidate();
                    //await Task.Delay(1);
                }
            }

            int temp1 = numbers[i + 1];
            numbers[i + 1] = numbers[high];
            numbers[high] = temp1;
            Invalidate();
            //await Task.Delay(1);

            return i + 1;
        }
    }



}
