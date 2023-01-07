
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
            double totalWidth = 0;
            double totalHeight = 0;
            var source = (BitmapSource)Source;
            GetPixelColor(source, 0, 0);

            GetPixelColor(source, 2000, 0);

            GetPixelColor(source, 4000, 2000);

            return new Size(totalWidth, totalHeight);
        }

        private static Color GetPixelColor(BitmapSource bitmap, int x, int y)
        {
            Color color;
            int stride = ((int)bitmap.Width * bitmap.Format.BitsPerPixel + 7) / 8;
            var bytes = new byte[stride];
            var rect = new Int32Rect(x, y, (int)bitmap.Width,(int)bitmap.Height);

            bitmap.CopyPixels(rect, bytes, stride, 0);

            if (bitmap.Format == PixelFormats.Bgra32)
            {
                color = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
            }
            else if (bitmap.Format == PixelFormats.Bgr32)
            {
                color = Color.FromRgb(bytes[2], bytes[1], bytes[0]);
            }
            // handle other required formats
            else
            {
                color = Colors.Black;
            }

            return color;
        }
    }
}
