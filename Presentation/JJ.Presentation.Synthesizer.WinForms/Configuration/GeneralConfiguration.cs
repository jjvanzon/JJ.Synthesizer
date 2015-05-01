using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class GeneralConfiguration
    {
        [XmlAttribute]
        public string TitleBarExtraText { get; set; }
    }
}
