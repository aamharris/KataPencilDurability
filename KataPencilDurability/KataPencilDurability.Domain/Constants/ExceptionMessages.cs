using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataPencilDurability.Domain.Constants
{
    public class ExceptionMessages
    {
        public const string PencilOutOfSharpening = "Pencil can not be sharpened anymore. Please buy a new pencil.";
        public const string NoPreviousErasedTextToEdit = "Cannot Edit Text because there has been no text erased.";
        public const string NoPaperFound = "No paper was provided. Can not write without paper.";
        public const string PointDegradationNotGreaterThanZero = "Point Degradation must be greater than 0.";
        public const string MaxSharpeningNotZeroOrGreater = "Maximum sharpenings must be 0 or greater.";
    }
}
