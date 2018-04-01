using System.Collections.Generic;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Configuration
{
	internal class ConfigurationSection
	{
		[XmlAttribute]
		public int? NameMaxLength { get; set; }

		[XmlAttribute]
		public int? OperatorDataMaxLength { get; set; }

		/// <summary> The amount of pre-calculated noise seconds should be large enough not to hear artifacts. </summary>
		[XmlAttribute]
		public double CachedNoiseSeconds { get; set; }

		[XmlAttribute]
		public int CachedNoiseSamplingRate { get; set; }

		[XmlAttribute]
		public CalculationMethodEnum CalculationMethod { get; set; }

		[XmlAttribute]
		public int DefaultSamplingRate { get; set; }

		[XmlAttribute]
		public SpeakerSetupEnum DefaultSpeakerSetup { get; set; }

		[XmlAttribute]
		public int DefaultMaxConcurrentNotes { get; set; }

		[XmlAttribute]
		public bool IncludeSymbolsWithCompilation { get; set; }

		[XmlAttribute]
		public int AudioFileOutputBufferSizeInBytes { get; set; }

		/// <summary>
		/// This feature switch can temporarily disable a validation that forms a performance bottleneck.
		/// </summary>
		[XmlAttribute]
		public bool HiddenButInUseValidationEnabled { get; set; }

		[XmlAttribute]
		public string DefaultScaleName { get; set; }

		public IList<string> DefaultMidiMappingGroupNames { get; set; }
	}
}