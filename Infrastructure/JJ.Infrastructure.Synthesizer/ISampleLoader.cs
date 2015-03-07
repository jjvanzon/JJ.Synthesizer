using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Infrastructure.Synthesizer
{
    internal interface ISampleLoader
    {
        void Load(string filePath);
        void Load(Stream stream);
    }
}
