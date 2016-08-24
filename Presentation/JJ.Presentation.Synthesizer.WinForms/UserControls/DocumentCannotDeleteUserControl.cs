using System;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Business.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentCannotDeleteUserControl : UserControlBase
    {
        public event EventHandler CloseRequested;

        public DocumentCannotDeleteUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        // Actions

        public new void Show()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);

            ApplyViewModelToControls();

            base.Show();
        }

        // Gui

        private void SetTitles()
        {
            labelMessagesTitle.Text = CommonTitles.Messages + ":";
            buttonOK.Text = CommonTitles.OK;
        }

        // Binding

        private new DocumentCannotDeleteViewModel ViewModel => (DocumentCannotDeleteViewModel)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null)
            {
                labelCannotDeleteObject.Text = null;
                labelMessageList.Text = null;
                return;
            }

            labelCannotDeleteObject.Text = CommonMessageFormatter.CannotDeleteObjectWithName(PropertyDisplayNames.Document, ViewModel.Document.Name);

            string formattedMessages = MessageHelper.FormatMessages(ViewModel.ValidationMessages);
            labelMessageList.Text = formattedMessages;
        }

        // Events

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }
    }
}
