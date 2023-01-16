using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace JurassicRisk
{
    /// <summary>
    /// Image compatible avec le canal alpha
    /// </summary>
    /// <Author>Charif</Author>
    public class MyImage : Image
    {
        #region Attribues
        private Int16 _pixelIncrement;
        private Bitmap _bitmap;
        private double _x;
        private double _y;
        private Size _size;
        private Rect _rect;
        private List<Point> _points;

        #endregion

        #region Property

        public Size Size { get { return _size; } }
        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }
        public List<Point> Points { get => _points; set => _points = value; }

        #endregion

        public MyImage(BitmapSource source)
        {
            Source = source;
            _pixelIncrement = 10;

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(source));
                enc.Save(outStream);
                _bitmap = new System.Drawing.Bitmap(outStream);
                _bitmap.MakeTransparent();
            };
            _points = GetRect(_bitmap);
            _size = GetSize(_bitmap);
            _x = _points[0].X;
            _y = _points[0].Y;
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

        /// <summary>
        /// GetSize based on bitmapSource with Alpha CHANNEL.
        /// </summary>
        /// <param name="bitmap">BitmapSource</param>
        /// <returns>Size</returns>
        /// <Author>Mahmoud Charif</Author>
        private Size GetSize(Bitmap bitmap)
        {
            return new Size(_points[1].X - _points[0].X, _points[2].Y - _points[0].Y);
        }

        private List<Point> GetRect(Bitmap bitmap)
        {
            Point topLeft = new Point();
            Point topRight = new Point();
            Point bottomLeft = new Point();
            Point bottomRight = new Point();

            //TopLeft
            for (Int16 x = 0; x < bitmap.Width; x += _pixelIncrement)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    // Si le pixel n'est pas transparent
                    if (bitmap.GetPixel(x, y).A != 0)
                    {
                        topLeft.X = x;
                        topLeft.Y = y;
                        x = (short)bitmap.Width;
                        break;
                    }
                }
                if (topLeft.X != 0)
                {
                    break;
                }
            }

            //TopRight
            for (int x = bitmap.Width - 1; x > 0; x -= _pixelIncrement)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    // Si le pixel n'est pas transparent
                    if (bitmap.GetPixel(x, y).A != 0)
                    {
                        topRight.X = x;
                        topRight.Y = y;
                        break;
                    }
                }
                if (topRight.X != 0)
                {
                    break;
                }
            }

            //BottomRight
            for (int x = bitmap.Width - 1; x > 0; x -= _pixelIncrement)
            {
                for (int y = bitmap.Height - 1; y > 0; y--)
                {
                    // Si le pixel n'est pas transparent
                    if (bitmap.GetPixel(x, y).A != 0)
                    {
                        bottomRight.X = x;
                        bottomRight.Y = y;
                        x = (short)bitmap.Width;
                        break;
                    }
                }
                if (bottomRight.X != 0)
                {
                    break;
                }
            }

            //BottomLeft
            for (Int16 x = 0; x < bitmap.Width; x += _pixelIncrement)
            {
                for (int y = bitmap.Height - 1; y > 0; y--)
                {
                    // Si le pixel n'est pas transparent
                    if (bitmap.GetPixel(x, y).A != 0)
                    {
                        bottomLeft.X = x;
                        bottomLeft.Y = y;
                        x = (short)bitmap.Width;
                        break;
                    }
                }
                if (bottomLeft.X != 0)
                {
                    break;
                }
            }
            var l = new List<Point>
            {
                topLeft,
                topRight,
                bottomLeft,
                bottomRight
            };
            return l;
        }


    }
}
