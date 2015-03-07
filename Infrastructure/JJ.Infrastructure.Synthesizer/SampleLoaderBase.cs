using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Infrastructure.Synthesizer
{
    internal abstract class SampleLoaderBase : ISampleLoader
    {
        protected Sample _sample;

        public SampleLoaderBase(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            _sample = sample;
        }

        public void Load(string filePath)
        {
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Load(stream);
            }
        }

        public abstract void Load(Stream stream);
    }
}
