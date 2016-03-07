using System;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Business.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentCannotDeleteUserControl : DocumentCannotDeleteUserControl_NotDesignable
    {
        public event EventHandler CloseRequested;

        public DocumentCannotDeleteUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        // Actions

        public void Show(DocumentCannotDeleteViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            ViewModel = viewModel;
            ApplyViewModelToControls();

            base.Show();
        }

        // Gui

        private void SetTitles()
        {
            labelMessagesTitle.Text = CommonTitles.Messages + ":";
            buttonOK.Text = CommonTitles.OK;
        }

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

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class DocumentCannotDeleteUserControl_NotDesignable : UserControlBase<DocumentCannotDeleteViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
