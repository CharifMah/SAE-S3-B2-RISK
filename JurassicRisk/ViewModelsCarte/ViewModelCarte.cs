using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JurassicRisk.ViewModelsCarte
{
    internal class ViewModelCarte : observable.Observable
    {

        List<TerritoireBase> territoire = new List<TerritoireBase>();

        public List<TerritoireBase> CreerTerritoire()
        {
            BitmapImage theImage = new BitmapImage(new Uri("pack://application:.../Sprites/carte/1c1r.png"));
            TerritoireForet territoire1 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire1);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/1c2r.png"));
            TerritoireForet territoire2 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire2);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/1c3r.png"));
            TerritoireForet territoire3 = new TerritoireForet(theImage, 94, 51);


            territoire.Add(territoire3);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/1c4r.png"));
            TerritoireForet territoire4 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire4);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/1c5r.png"));
            TerritoireForet territoire5 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire5);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/1c6r.png"));
            TerritoireForet territoire6 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire6);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/1c7r.png"));
            TerritoireForet territoire7 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire7);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/2c1r.png"));
            TerritoireForet territoire8 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire8);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/2c2r.png"));
            TerritoireDesert territoire9 = new TerritoireDesert(theImage, 94, 51);

            territoire.Add(territoire9);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/2c3r.png"));
            TerritoireDesert territoire10 = new TerritoireDesert(theImage, 94, 51);

            territoire.Add(territoire10);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/2c4r.png"));
            TerritoirePrairie territoire11 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire11);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/2c5r.png"));
            TerritoirePrairie territoire12 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire12);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/2c6r.png"));
            TerritoireDesert territoire13 = new TerritoireDesert(theImage, 94, 51);

            territoire.Add(territoire13);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/2c7r.png"));
            TerritoireDesert territoire14 = new TerritoireDesert(theImage, 94, 51);

            territoire.Add(territoire14);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c1r.png"));
            TerritoirePrairie territoire15 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire15);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c2r.png"));
            TerritoirePrairie territoire16 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire16);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c3r.png"));
            TerritoireForet territoire17 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire17);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c4r.png"));
            TerritoireForet territoire18 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire18);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c5r.png"));
            TerritoireForet territoire19 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire19);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c6r.png"));
            TerritoireForet territoire20 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire20);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c7r.png"));
            TerritoireForet territoire21 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire21);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/3c8r.png"));
            TerritoireForet territoire22 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire22);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/4c1r.png"));
            TerritoireForet territoire23 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire23);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/4c2r.png"));
            TerritoireForet territoire24 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire24);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/4c3r.png"));
            TerritoireForet territoire25 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire25);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/4c4r.png"));
            TerritoirePrairie territoire26 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire26);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/4c5r.png"));
            TerritoirePrairie territoire27 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire27);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/4c6r.png"));
            TerritoirePrairie territoire28 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire28);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/4c7r.png"));
            TerritoirePrairie territoire29 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire29);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/5c1r.png"));
            TerritoireForet territoire30 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire30);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/5c2r.png"));
            TerritoireForet territoire31 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire31);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/5c3r.png"));
            TerritoireForet territoire32 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire32);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/5c4r.png"));
            TerritoireForet territoire33 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire33);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/5c5r.png"));
            TerritoireForet territoire34 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire34);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/6c1r.png"));
            TerritoireForet territoire35 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire35);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/6c2r.png"));
            TerritoireForet territoire36 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire36);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/6c3r.png"));
            TerritoireForet territoire37 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire37);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/6c4r.png"));
            TerritoireForet territoire38 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire38);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/6c5r.png"));
            TerritoireForet territoire39 = new TerritoireForet(theImage, 94, 51);

            territoire.Add(territoire39);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/6c6r.png"));
            TerritoirePrairie territoire40 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire40);

            theImage = new BitmapImage(new Uri("pack://application:,,,/Sprites/Carte/6c7r.png"));
            TerritoirePrairie territoire41 = new TerritoirePrairie(theImage, 94, 51);

            territoire.Add(territoire41);

            return territoire;

        }
    }
}
