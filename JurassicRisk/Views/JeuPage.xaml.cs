using JurassicRisk.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class JeuPage : Page
    {
        private BitmapImage theImage;
        private Window mainwindow;
        private int zi = 0;
        public JeuPage()
        {
            InitializeComponent();
            InitRegion();
            mainwindow = (Window.GetWindow(App.Current.MainWindow) as MainWindow);
            mainwindow.SizeChanged += JeuPage_SizeChanged;
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;
        }

        private void JeuPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;
        }

        private void DrawRegions(string imagePath, double x, double y, int height, int width)
        {
           theImage = new BitmapImage
               (new Uri("pack://application:,,,/Sprites/Carte/" + imagePath));

            ImageBrush myImageBrush = new ImageBrush(theImage);
            Canvas myCanvas = new Canvas();

            myCanvas.Background = myImageBrush;
            myCanvas.Height = height;
            myCanvas.Width = width;
            Canvas.SetLeft(myCanvas, x);
            Canvas.SetTop(myCanvas, y);
            myCanvas.MouseEnter += MyCanvas_MouseEnter;
            myCanvas.MouseLeave += MyCanvas_MouseLeave;
            CarteCanvas.Children.Add(myCanvas);
        }

        private void MyCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            c.Width -= 10;
            c.Height -= 10;
        }

        private void MyCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            Canvas.SetZIndex(c, zi);
            c.Width += 10;
            c.Height += 10;
            zi++;
        }

        private void InitRegion()
        {
            DrawRegions("1c1r.png", 94, 51, 152, 70);
            DrawRegions("1c2r.png", 114, 149, 97, 130);
            DrawRegions("1c3r.png", 148, 1, 160, 194);
            DrawRegions("1c4r.png", 268, 32, 90, 146);
            DrawRegions("1c5r.png", 332, 38, 182, 150);
            DrawRegions("1c6r.png", 222, 114, 146, 138);
            DrawRegions("1c7r.png", 290, 157, 151, 220);
            DrawRegions("2c1r.png", 112, 580, 188, 217);
            DrawRegions("2c2r.png", 107, 735, 226, 158);
            DrawRegions("2c3r.png", 163, 807, 165, 188);
            DrawRegions("2c4r.png", 249, 637, 182, 161);
            DrawRegions("2c5r.png", 388, 620, 215, 161);
            DrawRegions("2c6r.png", 296, 735, 302, 146);
            DrawRegions("2c7r.png", 400, 751, 250, 215);
            DrawRegions("3c1r.png", 510, 424, 177, 188);
            DrawRegions("3c2r.png", 641, 484, 168, 266);
            DrawRegions("3c3r.png", 745, 332, 184, 162);
            DrawRegions("3c4r.png", 635, 302, 193, 139);
            DrawRegions("3c5r.png", 617, 127, 201, 238);
            DrawRegions("3c6r.png", 728, 184, 214, 244);
            DrawRegions("3c7r.png", 921, 202, 125, 272);
            DrawRegions("3c8r.png", 837, 120, 132, 357);
            DrawRegions("4c1r.png", 895, 321, 205, 145);
            DrawRegions("4c2r.png", 998, 287, 154, 274);
            DrawRegions("4c3r.png", 1123, 327, 245, 199);
            DrawRegions("4c4r.png", 911, 389, 201, 244);
            DrawRegions("4c5r.png", 746, 516, 142, 212);
            DrawRegions("4c6r.png", 893, 571, 231, 232);
            DrawRegions("4c7r.png", 1049, 502, 300, 186);
            DrawRegions("5c1r.png", 1415, 130, 149, 134);
            DrawRegions("5c2r.png", 1440, 194, 146, 194);
            DrawRegions("5c3r.png", 1519, 40, 170, 267);
            DrawRegions("5c4r.png", 1709, 49, 319, 191);
            DrawRegions("5c5r.png", 1587, 182, 146, 182);
            DrawRegions("6c1r.png", 1549, 296, 136, 286);
            DrawRegions("6c2r.png", 1740, 326, 225, 148);
            DrawRegions("6c3r.png", 1416, 349, 224, 245);
            DrawRegions("6c4r.png", 1493, 422, 191, 309);
            DrawRegions("6c5r.png", 1592, 523, 262, 260);
            DrawRegions("6c6r.png", 1413, 550, 212, 194);
            DrawRegions("6c7r.png", 1475, 668, 218, 213);
        }

        ViewModelCarte ViewModelCarte = new ViewModelCarte();
    }
}
