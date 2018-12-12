using System.Reflection;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.JsonBlobZip;

namespace JJ.Data.Synthesizer.JsonBlobZip
{
    public class SynthesizerJsonBlobZipContext : JsonBlobZipContext<Document>
    {
        public SynthesizerJsonBlobZipContext(string location, Assembly modelAssembly, Assembly mappingAssembly, string dialect)
            : base(location, modelAssembly, mappingAssembly, dialect)
        { }
    }
}