using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEngineering
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageProcessor pcs = new ImageProcessor();

            Console.WriteLine("Execute SimpleReadBW");
            Console.Write("Input source path: ");
            string srcPath = Console.ReadLine();
            Console.Write("Input destination path: ");
            string dstPath = Console.ReadLine();
            if(System.IO.File.Exists(srcPath))
            {
                pcs.SimpleReadBW(srcPath, dstPath);
            }
            else
            {
                Console.WriteLine($"No such file or directory: {srcPath}");
            }

            Console.WriteLine("Execute SimpleColorChange");
            Console.Write("Input source path: ");
            srcPath = Console.ReadLine();
            Console.Write("Input destination path: ");
            dstPath = Console.ReadLine();
            Console.Write("Transcolor from (b, g, r): ");
            string color1 = Console.ReadLine();
            Console.Write("Transcolor to (b, g, r): ");
            string color2 = Console.ReadLine();
            if (System.IO.File.Exists(srcPath))
            {
                pcs.SimpleColorChange(srcPath, ToColors(color1), ToColors(color2), dstPath);
            }
            else
            {
                Console.WriteLine($"No such file or directory: {srcPath}");
            }

            Console.WriteLine("Execute SimpleMix");
            Console.Write("Input mixing-base file path: ");
            string mixBasePath = Console.ReadLine();
            Console.Write("Input mixing file path: ");
            string mixPath = Console.ReadLine();
            Console.Write("Input mix percentage (0.0-1.0): ");
            string percentageString = Console.ReadLine();
            float percentage = 0;
            if(!float.TryParse(percentageString, out percentage) || (percentage > 1.0 && percentage < 0.0))
            {
                Console.WriteLine($"Invalid Value: \"{percentageString}\", use 0 for percentage");
            }
            Console.Write("Input destination path: ");
            dstPath = Console.ReadLine();
            if (System.IO.File.Exists(mixBasePath) && System.IO.File.Exists(mixPath))
            {
                if (pcs.SimpleMix(mixBasePath, mixPath, percentage, dstPath) != 0)
                {
                    Console.WriteLine("Failed to mix");
                }
            }
            else
            {
                Console.WriteLine($"No such file or directory: {mixBasePath} or {mixPath}");
            }

            Console.WriteLine("Execute SimpleShift");
            Console.Write("Input source path: ");
            srcPath = Console.ReadLine();
            Console.Write("Input destination path: ");
            dstPath = Console.ReadLine();
            Console.Write("Input shift value(>=0): ");
            string shiftString = Console.ReadLine();
            int shiftValue = 0;
            if (!int.TryParse(shiftString, out shiftValue))
            {
                Console.WriteLine($"Invalid Value: \"{shiftString}\", use 0 for shift value");
            }
            if (pcs.SimpleShift(srcPath, shiftValue, dstPath) != 0)
            {
                Console.WriteLine("Failed to shift");
            }

            Console.WriteLine("Press any key to continue ...");
            Console.Read();
        }

        static ImageProcessor.Colors ToColors(string colorString)
        {
            switch (colorString[0])
            {
                case 'b':
                    return ImageProcessor.Colors.B;
                case 'g':
                    return ImageProcessor.Colors.G;
                default:
                    return ImageProcessor.Colors.R;
            }
        }
    }
}
