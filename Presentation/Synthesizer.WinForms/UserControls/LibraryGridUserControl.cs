using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class LibraryGridUserControl : GridUserControlBase
    {
        public LibraryGridUserControl()
        {
            InitializeComponent();
        }

        protected override string IDPropertyName => PropertyNames.ID;
        protected override string Title => ResourceFormatter.LowerDocuments;
        protected override bool ColumnHeadersVisible => false;
        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddColumn(PropertyNames.ID, CommonResourceFormatter.ID, visible: false);
            AddColumn(PropertyNames.Name, CommonResourceFormatter.Name, autoSize: true);
            AddColumn(PropertyNames.AudioFileFormat, ResourceFormatter.AudioFileFormat);
            AddColumn(PropertyNames.SampleDataType, ResourceFormatter.SampleDataType);
            AddColumn(PropertyNames.SpeakerSetup, ResourceFormatter.SpeakerSetup);
            AddColumn(PropertyNames.SamplingRate, ResourceFormatter.SamplingRate);
        }

        public new LibraryGridViewModel ViewModel
        {
            get { return (LibraryGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
