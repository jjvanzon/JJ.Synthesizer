using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class ConfigurationSection
    {
        [XmlAttribute]
        public string DefaultCulture { get; set; }

        [XmlAttribute]
        public bool ExecuteOperatorMoveActionWhileDragging { get; set; }

        [XmlAttribute]
        public string PlayActionOutputFilePath { get; set; }

        [XmlAttribute]
        public double PlayActionDurationInSeconds { get; set; }

        /// <summary>
        /// Setting this to false will prevent a lot of logic going off upon activating a form,
        /// which is really annoying when debugging.
        /// The main reason a lot is done in the MainForm_Activated is to reclaim ownership
        /// of the MIDI device, but
        /// </summary>
        [XmlAttribute]
        public bool MustHandleMainFormActivated { get; set; }
    }
}
