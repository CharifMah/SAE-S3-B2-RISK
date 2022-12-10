using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace DBStorage.ClassMetier.Units
{
    public interface IUnit
    {
        string Name { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        Elements Element { get; set; }
    }
}
