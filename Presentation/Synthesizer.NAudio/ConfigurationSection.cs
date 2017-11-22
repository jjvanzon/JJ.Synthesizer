using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.NAudio
{
	internal class ConfigurationSection
	{
		[XmlAttribute]
		public bool AudioOutputEnabled { get; set; }

		[XmlAttribute]
		public bool MidiInputEnabled { get; set; }
	}
}
