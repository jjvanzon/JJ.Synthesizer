using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class ConfigurationSection
    {
        [XmlAttribute]
        public int MaxVisiblePageNumbers { get; set; }

        [XmlAttribute]
        public int PageSize { get; set; }
    }
}
