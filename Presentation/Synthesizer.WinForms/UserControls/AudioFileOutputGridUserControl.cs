using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputGridUserControl : GridUserControlBase
    {
        public AudioFileOutputGridUserControl()
        {
            InitializeComponent();

            Title = ResourceFormatter.AudioFileOutputList;
            IDPropertyName = PropertyNames.ID;
            ColumnHeadersVisible = true;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddColumn(PropertyNames.ID, null, visible: false);
            AddColumn(PropertyNames.Name, CommonResourceFormatter.Name, autoSize: true);
            AddColumn(PropertyNames.AudioFileFormat, ResourceFormatter.AudioFileFormat);
            AddColumn(PropertyNames.SampleDataType, ResourceFormatter.SampleDataType);
            AddColumn(PropertyNames.SpeakerSetup, ResourceFormatter.SpeakerSetup);
            AddColumn(PropertyNames.SamplingRate, ResourceFormatter.SamplingRate);
        }

        // Binding

        public new AudioFileOutputGridViewModel ViewModel
        {
            get { return (AudioFileOutputGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
