﻿//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
//using JJ.Framework.Presentation.Resources;
//using JJ.Presentation.Synthesizer.ViewModels.Items;

//namespace JJ.Presentation.Synthesizer.WinForms.UserControls
//{
//    internal partial class CurveGridUserControl : GridUserControlBase
//    {
//        public CurveGridUserControl()
//        {
//            InitializeComponent();

//            Title = ResourceFormatter.Curves;
//            IDPropertyName = nameof(CurveListItemViewModel.ID);
//            ColumnTitlesVisible = true;
//        }

//        protected override object GetDataSource() => ViewModel?.List;

//        protected override void AddColumns()
//        {
//            AddHiddenColumn(nameof(CurveListItemViewModel.ID));
//            AddAutoSizeColumn(nameof(CurveListItemViewModel.Name), CommonResourceFormatter.Name);
//            AddColumnWithWidth(nameof(CurveListItemViewModel.UsedIn), ResourceFormatter.UsedIn, 180);
//        }

//        public new CurveGridViewModel ViewModel
//        {
//            get => (CurveGridViewModel)base.ViewModel;
//            set => base.ViewModel = value;
//        }
//    }
//}
