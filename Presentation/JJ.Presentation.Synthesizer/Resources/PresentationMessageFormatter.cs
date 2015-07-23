using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Resources
{
    public static class PresentationMessageFormatter
    {
        public static string SampleFileDoesNotExistWithLocation(string filePath)
        {
            return String.Format(PresentationMessages.SampleFileDoesNotExistWithLocation, filePath);
        }
    }
}
