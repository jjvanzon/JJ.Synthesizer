using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class TestingConfiguration
    {
        //[XmlAttribute]
        //public bool MustCreateMockPatch { get; set; }

        //[XmlAttribute]
        //public int TestPatchID { get; set; }

        [XmlAttribute]
        public bool MustShowInvisibleElements { get; set; }

        //[XmlAttribute]
        //public bool ForceStateless { get; set; }

        [XmlAttribute]
        public bool AlwaysRecreateDiagram { get; set; }

        [XmlAttribute]
        public bool ToolTipsFeatureEnabled { get; set; }
    }
}
