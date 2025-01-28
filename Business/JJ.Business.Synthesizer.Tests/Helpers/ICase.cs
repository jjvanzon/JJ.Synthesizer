using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public interface ICase
    { 
        string Name { get; }
        string Descriptor { get; }
        object[] DynamicData { get; }
    }
}