using System.Windows.Forms;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Canonical;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Properties;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class DocumentGridUserControl : GridUserControlBase
	{
		private DataGridViewColumn _playColumn;

		public DocumentGridUserControl()
		{
			InitializeComponent();

			IDPropertyName = nameof(IDAndName.ID);
			Title = ResourceFormatter.Documents;
			ColumnTitlesVisible = false;
			PlayButtonVisible = true;

			KeyDown += base_KeyDown;
			CellClick += base_CellClick;
		}

		protected override object GetDataSource() => ViewModel?.List;

		protected override void AddColumns()
		{
			AddHiddenColumn(nameof(IDAndName.ID));
			_playColumn = AddImageColumn(Resources.PlayIconThinner);
			AddAutoSizeColumn(nameof(IDAndName.Name), CommonResourceFormatter.Name);
		}

		public new DocumentGridViewModel ViewModel
		{
			// ReSharper disable once MemberCanBePrivate.Global
			get => (DocumentGridViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		private void base_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Space:
					Play();
					e.Handled = true;
					break;
			}
		}

		private void base_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (ViewModel == null) return;

			if (e.RowIndex == -1)
			{
				return;
			}

			if (e.ColumnIndex == _playColumn.Index)
			{
				Play();
			}
		}
	}
}
