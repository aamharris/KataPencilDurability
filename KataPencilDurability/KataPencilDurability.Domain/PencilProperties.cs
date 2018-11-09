using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataPencilDurability.Domain
{
    public class PencilProperties
    {
        public int PointDegradation { get; set; }
        public int MaxNumberOfSharpenings { get; set; }
        public Paper Paper { get; set; }
    }
}
