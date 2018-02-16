using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Configuration
{
	internal class ConfigurationSection
	{
		[XmlAttribute]
		public int NodeClickableRegionSizeInPixels { get; set; }

		[XmlAttribute]
		public int PatchLineSegmentCount { get; set; }

		[XmlAttribute]
		public int CurveLineSegmentCount { get; set; }
	}
}
