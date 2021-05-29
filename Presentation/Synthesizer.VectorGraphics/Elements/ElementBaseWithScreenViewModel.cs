using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public abstract class ElementBaseWithScreenViewModel : ElementBase
    {
        private ScreenViewModelBase _viewModel;
        private int _refreshCounter = -1;

        public ElementBaseWithScreenViewModel(Element parent) : base(parent) { }

        /// <summary> nullable </summary>
        protected ScreenViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                bool mustApplyViewModel = value != null &&
                                          (value != _viewModel ||
                                           value.RefreshID != _refreshCounter);
                _viewModel = value;

                if (_viewModel == null)
                {
                    return;
                }

                _refreshCounter = _viewModel.RefreshID;

                if (mustApplyViewModel)
                {
                    ApplyViewModelToElements();
                    PositionElements();
                }
            }
        }

        protected abstract void ApplyViewModelToElements();
        public abstract void PositionElements();
    }
}