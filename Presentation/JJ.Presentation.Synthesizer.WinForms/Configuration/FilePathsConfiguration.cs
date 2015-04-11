using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class FilePathsConfiguration
    {
        [XmlAttribute]
        public string SampleFilePath { get; set; }

        [XmlAttribute]
        public string OutputFilePath { get; set; }
    }
}
