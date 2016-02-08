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
    internal partial class DocumentCannotDeleteUserControl : UserControl
    {
        public event EventHandler CloseRequested;

        private DocumentCannotDeleteViewModel _viewModel;

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

            _viewModel = viewModel;
            ApplyViewModel();

            base.Show();
        }

        // Gui

        private void SetTitles()
        {
            labelMessagesTitle.Text = CommonTitles.Messages + ":";
            buttonOK.Text = CommonTitles.OK;
        }

        private void ApplyViewModel()
        {
            if (_viewModel == null)
            {
                labelCannotDeleteObject.Text = null;
                labelMessageList.Text = null;
                return;
            }

            labelCannotDeleteObject.Text = CommonMessageFormatter.CannotDeleteObjectWithName(PropertyDisplayNames.Document, _viewModel.Document.Name);

            string formattedMessages = MessageHelper.FormatMessages(_viewModel.ValidationMessages);
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
