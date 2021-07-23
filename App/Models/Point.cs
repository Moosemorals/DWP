
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.moosemorals.DWP.Models
{
    public record Point(double Latitude, double Longitude) : IPoint;
}
