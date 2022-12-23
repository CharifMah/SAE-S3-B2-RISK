using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tours
{
    public interface ITour
    {
        public bool TourEnd { get; }
        public void TerminerTour();
    }
}
