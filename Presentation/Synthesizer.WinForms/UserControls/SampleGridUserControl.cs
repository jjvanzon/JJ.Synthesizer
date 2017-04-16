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

            IDPropertyName = nameof(SampleListItemViewModel.ID);
            Title = ResourceFormatter.Samples;
            ColumnTitlesVisible = true;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddHiddenColumn(nameof(SampleListItemViewModel.ID));
            AddAutoSizeColumn(nameof(SampleListItemViewModel.Name), CommonResourceFormatter.Name);
            AddColumn(nameof(SampleListItemViewModel.SampleDataType), ResourceFormatter.SampleDataType);
            AddColumn(nameof(SampleListItemViewModel.SpeakerSetup), ResourceFormatter.SpeakerSetup);
            AddColumn(nameof(SampleListItemViewModel.SamplingRate), ResourceFormatter.SamplingRate);
            AddColumn(nameof(SampleListItemViewModel.IsActiveText), ResourceFormatter.IsActive);
            AddColumnWithWidth(nameof(SampleListItemViewModel.UsedIn), ResourceFormatter.UsedIn, 180);
        }

        // Binding

        public new SampleGridViewModel ViewModel
        {
            get => (SampleGridViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
