using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JurassicRisk
{
    public class MyImage : Image
    {
        private int _height;
        private int _width;

        public MyImage()
        {

        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            var source = (BitmapSource)Source;
            var x = (int)(hitTestParameters.HitPoint.X / ActualWidth * source.PixelWidth);
            var y = (int)(hitTestParameters.HitPoint.Y / ActualHeight * source.PixelHeight);
            if (x == source.PixelWidth)
                x--;
            if (y == source.PixelHeight)
                y--;

            var pixels = new byte[4];
            source.CopyPixels(new Int32Rect(x, y, 1, 1), pixels, 4, 0);

            return (pixels[3] < 1) ? null : new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            BitmapSource source = (BitmapSource)Source;
            double totalWidth = source.Width;
            double totalHeight = source.Height;

            GetSize(source);

            return new Size(totalWidth, totalHeight);
        }

        private static Size GetSize(BitmapSource source)
        {
            Int16 totalWidth = 0;
            Int16 totalHeight = 0;
            Int16 heightTemp = 0;
            Int16 widthTemp = 0;
            for (int i = 0; i < source.Height; i++)
            {
                widthTemp = 0;
                for (int ii = 0; ii < source.Width; ii++)
                {
                    if (GetPixelColor(source, ii, i).A != 0)
                    {
                        widthTemp++;
                        if (widthTemp > totalWidth)
                        {
                            totalWidth = widthTemp;
                        }
                    }
                    else
                    {
                        widthTemp = 0;
                    }
                }
            }
            for (int i = 0; i < source.Width; i++)
            {
                heightTemp = 0;
                for (int ii = 0; ii < source.Height; ii++)
                {
                    if (GetPixelColor(source, i, ii).A != 0)
                    {
                        heightTemp++;
                        if (heightTemp > totalHeight)
                        {
                            totalHeight = heightTemp;
                        }
                    }
                    else
                    {
                        heightTemp = 0;
                    }
                }
            }
           

            return new Size();
        }

        

        private static Color GetPixelColor(BitmapSource bitmap, int x, int y)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            int stride = width * bitmap.Format.BitsPerPixel; // 8 bytes par pixel
            byte[] pixels = new byte[height * stride];
            bitmap.CopyPixels(pixels, stride, 0);

            // Calculer l'index du pixel dans le tableau
            int pixelIndex = y * stride + bitmap.Format.BitsPerPixel * x;

            // Extraire les valeurs ARGB du pixel
            byte b = ;
            byte g = ;
            byte r = ;
            byte a = ;

            // Retourner la couleur du pixel
            return Color.FromArgb(pixels[pixelIndex + 3], pixels[pixelIndex + 2], pixels[pixelIndex + 1], pixels[pixelIndex]);
        }
    }
}
