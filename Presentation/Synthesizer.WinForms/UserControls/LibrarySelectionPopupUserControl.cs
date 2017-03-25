using System;
using System.Windows.Forms;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class LibrarySelectionPopupUserControl : UserControl
    {
        public event EventHandler<EventArgs<int?>> OKRequested;

        public event EventHandler CancelRequested
        {
            add
            {
                buttonCancel.Click += value;
            }
            remove
            {
                buttonCancel.Click -= value;
            }
        }

        public LibrarySelectionPopupUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        private void SetTitles()
        {
            buttonOK.Text = CommonResourceFormatter.OK;
            buttonCancel.Text = CommonResourceFormatter.Cancel;
        }

        public LibrarySelectionPopupViewModel ViewModel
        {
            get { return librarySelectionGridUserControl.ViewModel; }
            set { librarySelectionGridUserControl.ViewModel = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            int? id = librarySelectionGridUserControl.TryGetSelectedID();
            OKRequested?.Invoke(sender, new EventArgs<int?>(id));
        }
    }
}
