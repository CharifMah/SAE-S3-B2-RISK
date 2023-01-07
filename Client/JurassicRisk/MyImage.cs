using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JurassicRisk
{
    /// <summary>
    /// Image compatible avec le canal alpha
    /// </summary>
    /// <Author>Charif</Author>
    public class MyImage : Image
    {
        private static int _height;
        private static int _width;
        private static int _x;
        private static int _y;
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
            return GetSize(source);
        }

        /// <summary>
        /// GetSize based on bitmapSource with Alpha CHANNEL.
        /// </summary>
        /// <param name="source">BitmapSource</param>
        /// <returns>Size</returns>
        /// <Author>Mahmoud Charif</Author>
        private static Size GetSize(BitmapSource source)
        {
            Int16 h = GetHeight(source);
            Int16 w = GetWidth(source);
            return new Size(w,h);
        }

        private static Int16 GetHeight(BitmapSource source)
        {
            Int16 totalHeight = 0;
            Int16 heightTemp = 0;


            // Pour chaque colonne de l'image
            for (Int16 x = 0; x < source.Width; x++)
            {
                heightTemp = 0;

                // Pour chaque pixel de la colonne
                for (Int16 y = 0; y < source.Height; y++)
                {
                    // Si le pixel n'est pas transparent
                    if (GetPixelColor(source, x, y).A != 0)
                    {
                        heightTemp++; // Incrémente la hauteur temporaire
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

        private static Int16 GetWidth(BitmapSource source)
        {
            Int16 totalWidth = 0;
            Int16 widthTemp = 0;

            // Pour chaque ligne de l'image
            for (Int16 y = 0; y < source.Height; y++)
            {
                widthTemp = 0;

                // Pour chaque pixel de la ligne
                for (Int16 x = 0; x < source.Width; x++)
                {
                    // Si le pixel n'est pas transparent
                    if (GetPixelColor(source, x, y).A != 0)
                    {
                        widthTemp++; // Incrémenter la largeur temporaire
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

        /// <summary>
        /// Obtient la couleur d'un pixel à une position x, y
        /// </summary>
        /// <param name="bitmap">source</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>Color</returns>
        private static Color GetPixelColor(BitmapSource bitmap, int x, int y)
        {
            int stride = bitmap.PixelWidth * bitmap.Format.BitsPerPixel; // 8 bytes par pixel en théorie sauf si on change l'image
            byte[] pixels = new byte[bitmap.PixelHeight * stride];
            bitmap.CopyPixels(pixels, stride, 0);

            // Calcule l'index du pixel dans le tableau
            int pixelIndex = y * stride + bitmap.Format.BitsPerPixel * x;

            // Retourne la couleur du pixel
            return Color.FromArgb(pixels[pixelIndex + 3], pixels[pixelIndex + 2], pixels[pixelIndex + 1], pixels[pixelIndex]);
        }
    }
}
