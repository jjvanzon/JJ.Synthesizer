//using System.Linq;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Canonical;
//using JJ.Framework.Resources;
//using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

//namespace JJ.Presentation.Synthesizer.WinForms.UserControls
//{
//	internal partial class ScaleGridUserControl : GridUserControlBase
//	{
//		public ScaleGridUserControl()
//		{
//			InitializeComponent();

//			IDPropertyName = nameof(IDAndName.ID);
//			Title = ResourceFormatter.Scales;
//			ColumnTitlesVisible = false;
//		}

//		protected override object GetDataSource() => ViewModel.Dictionary.Values.ToArray();

//		protected override void AddColumns()
//		{
//			AddHiddenColumn(nameof(IDAndName.ID));
//			AddAutoSizeColumn(nameof(IDAndName.Name), CommonResourceFormatter.Name);
//		}

//		public new ScaleGridViewModel ViewModel
//		{
//			get => (ScaleGridViewModel)base.ViewModel;
//			set => base.ViewModel = value;
//		}
//	}
//}
