using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);

            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0, cts.Token);

                sw.Stop();
                Console.WriteLine($"花費時間: {sw.ElapsedMilliseconds} ms");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{Environment.NewLine}下載已經取消");
            }
            catch (Exception)
            {
                Console.WriteLine($"{Environment.NewLine}下載發現例外異常，已經中斷");
            }

            Console.ReadKey();
        }
    }
}
