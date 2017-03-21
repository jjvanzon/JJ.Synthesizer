using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class SampleGridUserControl : GridUserControlBase
    {
        public SampleGridUserControl()
        {
            InitializeComponent();

            IDPropertyName = PropertyNames.ID;
            Title = ResourceFormatter.Samples;
            ColumnTitlesVisible = true;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddColumn(nameof(SampleListItemViewModel.ID), CommonResourceFormatter.ID, visible: false);
            AddColumn(nameof(SampleListItemViewModel.Name), CommonResourceFormatter.Name, autoSize: true);
            AddColumn(nameof(SampleListItemViewModel.SampleDataType), ResourceFormatter.SampleDataType);
            AddColumn(nameof(SampleListItemViewModel.SpeakerSetup), ResourceFormatter.SpeakerSetup);
            AddColumn(nameof(SampleListItemViewModel.SamplingRate), ResourceFormatter.SamplingRate);
            AddColumn(nameof(SampleListItemViewModel.IsActiveText), ResourceFormatter.IsActive);
            AddColumn(nameof(SampleListItemViewModel.UsedIn), ResourceFormatter.UsedIn, widthInPixels: 180);
        }

        // Binding

        public new SampleGridViewModel ViewModel
        {
            get { return (SampleGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
