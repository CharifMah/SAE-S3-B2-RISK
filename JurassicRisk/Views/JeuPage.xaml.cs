using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class JeuPage : Page
    {
        private BitmapImage theImage;
        public JeuPage()
        {
            InitializeComponent();
            DrawRegions("1c1r.png",0,0);
            DrawRegions("1c2r.png",0, 0);




        }

        private void DrawRegions(string imagePath,double x,double y)
        {
            theImage = new BitmapImage
               (new Uri("pack://application:,,,/Sprites/Carte/" + imagePath));

            ImageBrush myImageBrush = new ImageBrush(theImage);

            Canvas myCanvas = new Canvas();
            myCanvas.Width = theImage.Width;
            myCanvas.Height = theImage.Height;
            myCanvas.Background = myImageBrush;
            Canvas.SetLeft(myCanvas, x);
            Canvas.SetTop(myCanvas, y);
            CarteCanvas.Children.Add(myCanvas);
        }
    }
}
