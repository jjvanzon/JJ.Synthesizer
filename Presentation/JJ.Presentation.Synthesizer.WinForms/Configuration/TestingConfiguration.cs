using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class TestingConfiguration
    {
        [XmlAttribute]
        public bool MustShowInvisibleElements { get; set; }

        [XmlAttribute]
        public bool AlwaysRecreateDiagram { get; set; }

        [XmlAttribute]
        public bool ToolTipsFeatureEnabled { get; set; }
    }
}
