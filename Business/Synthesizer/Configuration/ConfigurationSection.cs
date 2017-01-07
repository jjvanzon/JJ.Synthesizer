using System;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Configuration
{
    public class ConfigurationSection
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
        public double SecondsBetweenApplyFilterVariables { get; set; }

        [XmlAttribute]
        public CalculationEngineConfigurationEnum CalculationEngine { get; set; }

        [XmlAttribute]
        public int DefaultSamplingRate { get; set; }

        [XmlAttribute]
        public SpeakerSetupEnum DefaultSpeakerSetup { get; set; }

        [XmlAttribute]
        public int DefaultMaxConcurrentNotes { get; set; }

        [XmlAttribute]
        public bool IncludeSymbolsWithCompilation { get; set; }
    }
}