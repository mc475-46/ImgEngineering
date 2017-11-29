using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace ImageEngineering
{
    class ImageProcessor
    {
        public enum Colors { B, G, R };

        public void SimpleReadBW(string srcPath, string dstPath)
        {
            var srcImage = Cv2.ImRead(srcPath, ImreadModes.GrayScale);
            using (new Window(srcPath, srcImage))
            {
                Cv2.WaitKey();
            }
            Cv2.ImWrite(dstPath, srcImage);
        }
        public void SimpleColorChange(string srcPath, Colors color1, Colors color2, string dstPath)
        {
            var srcImage = Cv2.ImRead(srcPath, ImreadModes.Color);
            var dstImage = srcImage.Clone();
            srcImage.ExtractChannel((int)color1).InsertChannel(dstImage, (int)color2);
            srcImage.ExtractChannel((int)color2).InsertChannel(dstImage, (int)color1);
            using (new Window(dstPath, dstImage))
            {
                Cv2.WaitKey();
            }
            Cv2.ImWrite(dstPath, dstImage);
        }
        public int SimpleMix(string srcPath1, string srcPath2, double percentage, string dstPath)
        {
            var srcImage1 = Cv2.ImRead(srcPath1, ImreadModes.GrayScale);
            var srcImage2 = Cv2.ImRead(srcPath2, ImreadModes.GrayScale);
            var dstImage = srcImage1.EmptyClone();
            for(int x = 0; x < srcImage1.Rows; ++x)
            {
                for(int y = 0; y < srcImage1.Cols; ++y)
                {
                    var srcPix1 = srcImage1.At<byte>(x, y);
                    var srcPix2 = srcImage2.At<byte>(x, y);
                    int dstPix = (int)(srcPix1 * percentage + srcPix2 * (1 - percentage));
                    if (dstPix > 255) return 1;
                    dstImage.Set<byte>(x, y, (byte)dstPix);
                }
            }
            using (new Window(dstPath, dstImage))
            {
                Cv2.WaitKey();
            }
            Cv2.ImWrite(dstPath, dstImage);
            return 0;
        }
        public int SimpleShift(string srcPath, int shiftValue, string dstPath)
        {
            var srcImage = new Mat(srcPath, ImreadModes.GrayScale);
            var dstImage = srcImage.EmptyClone();
            for (int x = 0; x < srcImage.Rows; ++x)
            {
                for (int y = 0; y < srcImage.Cols; ++y)
                {
                    var srcPix = srcImage.At<byte>(x, y);
                    int dstPix;
                    if(srcPix + shiftValue > 255)
                    {
                        if(srcPix - shiftValue < 0)
                        {
                            return 1;
                        }
                        dstPix = srcPix - shiftValue;
                    }
                    else
                    {
                        dstPix = srcPix + shiftValue;
                    }
                    dstImage.Set<byte>(x, y, (byte)dstPix);
                }
            }
            using (new Window(dstPath, dstImage))
            {
                Cv2.WaitKey();
            }
            Cv2.ImWrite(dstPath, dstImage);
            return 0;
        }
    }
}
