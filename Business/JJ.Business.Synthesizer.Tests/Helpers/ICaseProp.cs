using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal interface ICaseProp
    {
        void CloneFrom(object obj);
        string PropDescriptor { get; }
    }
}
