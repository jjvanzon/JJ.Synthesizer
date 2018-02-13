using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public abstract class ElementWithViewModelBase : ElementBase
	{
		private ViewModelBase _viewModel;
		private int _refreshCounter = -1;

		/// <summary> nullable </summary>
		public ViewModelBase ViewModel
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
				}
			}
		}

		protected abstract void ApplyViewModelToElements();
	}
}