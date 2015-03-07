using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Infrastructure.Synthesizer
{
    public static class SampleLoadHelper
    {
        public static void LoadSample(Sample sample, string filePath)
        {
            ISampleLoader sampleLoader = SampleLoaderFactory.CreateSampleLoader(sample);
            sampleLoader.Load(filePath);
        }

        /// <summary>
        /// Only works when stream supports the Length property.
        /// </summary>
        public static void LoadSample(Sample sample, Stream stream)
        {
            ISampleLoader sampleLoader = SampleLoaderFactory.CreateSampleLoader(sample);
            sampleLoader.Load(stream);
        }
    }
}
