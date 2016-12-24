using System;
using System.Xml.Serialization;

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
    }
}