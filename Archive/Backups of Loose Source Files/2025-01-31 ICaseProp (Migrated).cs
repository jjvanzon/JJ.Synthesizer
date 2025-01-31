using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal interface ICaseProp
    {
        void CloneFrom(ICaseProp obj);
        string Descriptor { get; }
    }
}
