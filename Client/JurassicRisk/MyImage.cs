using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
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
        private Int16 _mult;
        private Bitmap _bitmap;
        private double _x;
        private double _y;
        private Size _size;

        #endregion

        #region Property

        public Size Size { get { return _size; } }
        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }

        #endregion

        public MyImage(BitmapSource source)
        {
            Source = source;
            _mult = 1;
            _x = 0;
            _y = 0;

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(source));
                enc.Save(outStream);
                _bitmap = new System.Drawing.Bitmap(outStream);
                _bitmap.MakeTransparent();
            };

            _size = GetSize(_bitmap);
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
            Int32 h = 0;
            Int32 w = 0;
            Bitmap bitmap1=new Bitmap(bitmap);
            Bitmap bitmap2= new Bitmap(bitmap);
            Thread firstThread = new Thread(() => { h = GetHeight(bitmap1); });
            Thread secondThread = new Thread(()=> { w = GetWidth(bitmap2); });
            firstThread.Start();
            secondThread.Start();
            firstThread.Join();
            secondThread.Join();

            return new Size(w, h);
        }

        private Int16 GetHeight(Bitmap bitmap)
        {
            Int16 totalHeight = 0;
            Int16 heightTemp = 0;
            // Pour chaque colonne de l'image
            for (Int16 x = 0; x < bitmap.Width; x+= _mult)
            {
                heightTemp = 0;

                // Pour chaque pixel de la colonne
                for (Int16 y = 0; y < bitmap.Height; y += _mult)
                {
                    // Si le pixel n'est pas transparent
                    if (bitmap.GetPixel(x, y).A != 0)
                    {
                        heightTemp += _mult; // Incrémente la hauteur temporaire
                        if (heightTemp > totalHeight) // test si la hauteur temporaire est plus élever
                        {
                            if (heightTemp == 1)
                            {
                                _x = x;
                                _y = y;
                            }
                           
                            totalHeight = heightTemp;
                        }
                    }
                    else
                    {
                        heightTemp = 0; // Réinitialise la hauteur temporaire
                    }
                }
            }

            return totalHeight;
        }

        private Int16 GetWidth(Bitmap bitmap)
        {
            Int16 totalWidth = 0;
            Int16 widthTemp = 0;
            // Pour chaque ligne de l'image
            for (Int16 y = 0; y < bitmap.Height; y += _mult)
            {
                widthTemp = 0;

                // Pour chaque pixel de la ligne
                for (Int16 x = 0; x < bitmap.Width; x+=  _mult)
                {
                    // Si le pixel n'est pas transparent
                    if (bitmap.GetPixel(x, y).A != 0)
                    {
                        widthTemp += _mult; // Incrémenter la largeur temporaire
                        if (widthTemp > totalWidth) // Mettre à jour la largeur totale si nécessaire
                        {
                            if (widthTemp == 1)
                            {
                                _x = x;
                                _y = y;
                            }
                            totalWidth = widthTemp;
                        }
                    }
                    else
                    {
                        widthTemp = 0; // Réinitialiser la largeur temporaire

                    }
                }
            }
            return totalWidth;
        }      
    }
}
