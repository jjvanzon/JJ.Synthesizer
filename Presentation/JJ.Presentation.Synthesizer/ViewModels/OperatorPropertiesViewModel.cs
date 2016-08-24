using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel : OperatorPropertiesViewModelBase
    {
        /// <summary> not editable </summary>
        public IDAndName OperatorType { get; set; }
    }
}
