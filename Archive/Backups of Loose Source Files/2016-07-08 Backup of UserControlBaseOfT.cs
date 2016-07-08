//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Forms;
//using JJ.Presentation.Synthesizer.ViewModels;

//namespace JJ.Presentation.Synthesizer.WinForms.UserControls
//{
//    internal abstract class UserControlBase<TViewModel> : UserControlBase
//        where TViewModel : ViewModelBase
//    {
//        /// <summary> nullable </summary>
//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public new TViewModel ViewModel
//        {
//            get
//            {
//                return (TViewModel)base.ViewModel;
//            }
//            set
//            {
//                base.ViewModel = value;
//            }
//        }
//    }
//}
