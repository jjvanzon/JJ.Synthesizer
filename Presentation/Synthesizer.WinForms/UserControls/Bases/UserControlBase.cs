using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
	internal class UserControlBase : UserControl
	{
		private ScreenViewModelBase _viewModel;
		private int _refreshCounter = -1;

		/// <summary> nullable </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ScreenViewModelBase ViewModel
		{
			get => _viewModel;
			protected set
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
					ApplyViewModelToControls();
				}
			}
		}

		/// <summary> does nothing </summary>
		protected virtual void ApplyViewModelToControls()
		{ }
	}
}
