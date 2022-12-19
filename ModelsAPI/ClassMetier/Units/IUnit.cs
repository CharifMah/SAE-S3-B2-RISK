using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ModelsAPI.ClassMetier.Units
{
    public interface IUnit
    {
        string Name { get; }
        string Description { get; }
        int Id { get; }
        Elements Element { get; set; }
    }
}
