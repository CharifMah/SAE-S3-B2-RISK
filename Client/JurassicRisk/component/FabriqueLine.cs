using Models.Graph.lieux;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VoyageurDeCommerce.vue.composants
{
    /// <summary>
    /// Fabrique de ligne à partir de route
    /// </summary>
    class FabriqueLine
    {
        /// <summary>
        /// Construction d'une ligne
        /// </summary>
        /// <param name="route">Route modèle</param>
        /// <param name="minX">Offset X</param>
        /// <param name="minY">Offset Y</param>
        /// <returns>La ligne à afficher</returns>
        public static Line Creer(Route route, int minX, int minY)
        {
            //Dessin de la ligne
            Line ligne = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 60 / 10,
                X1 = 60 * 3 * (route.Depart.X - minX) + 60 / 2,
                X2 = 60 * 3 * (route.Arrivee.X - minX) + 60 / 2,
                Y1 = 60 * 3 * (route.Depart.Y - minY) + 60 / 2,
                Y2 = 60 * 3 * (route.Arrivee.Y - minY) + 60 / 2
            };
            //Menu contextuel
            ToolTip tt = new ToolTip
            {
                Content = "Route entre " + route.Depart.Nom + " et " + route.Arrivee.Nom + "\nDistance : " + route.Distance.ToString()
            };
            ligne.ToolTip = tt;
            return ligne;
        }

    }
}
