using System;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main(string[] args)
    {
        string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string imagesDir = Path.Combine(solutionDir, "images");
        string inputImagePath = Path.Combine(imagesDir, "resized_image_128x128.jpg");
        string outputImagePath = Path.Combine(imagesDir, "filtered_image_128x128.jpg");

        if (!File.Exists(inputImagePath))
        {
            Console.WriteLine($"Error: File {inputImagePath} does not exist.");
            return;
        }

        Console.WriteLine("Image found!:)\nProcessing...");

        using (var image = Image.Load<Rgba32>(inputImagePath))
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            image.Mutate(x => x.GaussianBlur(30));
            stopwatch.Stop();

            Console.WriteLine("Processing done!:D");
            var time = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Filtering took {stopwatch.ElapsedMilliseconds} ms");
            if (!Directory.Exists("images"))
            {
                Directory.CreateDirectory("images");
            }

            image.Save(outputImagePath);
            Console.WriteLine($"Filtered image saved as {outputImagePath}");
        }
    }
}