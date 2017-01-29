using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using CanonicalModel = JJ.Data.Canonical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    /// <summary>
    /// The MessageBoxes are shown 'async',
    /// because the MessageBox is a response to an action,
    /// around which an _actionIsBusy flag is maintained.
    /// The response to the dialog result would make another action go off,
    /// but the _actionIsBusy boolean is true,
    /// so the presenter action would not go off.
    ///
    /// (See _actionIsBusy summary for why that flag is maintained.)
    ///
    /// Because the ApplyViewModel is performed, while the persenter action is not.
    /// you end up seeing the same MessageBox being shown over and over again.
    /// </summary>
    internal static class MessageBoxHelper
    {
        private static readonly object _dummySender = new object();

        public static event EventHandler<EventArgs<int>> DocumentDeleteConfirmed;
        public static event EventHandler DocumentDeleteCanceled;
        public static event EventHandler DocumentDeletedOK;
        public static event EventHandler PopupMessagesOK;

        public static void ShowDocumentConfirmDelete(Form parentForm, DocumentDeleteViewModel viewModel)
        {
            if (parentForm == null) throw new NullException(() => parentForm);
            if (viewModel == null) throw new NullException(() => viewModel);

            // Class summary explains why this is 'async'.
            parentForm.BeginInvoke(new Action(() =>
            {
                string message = CommonMessageFormatter.AreYouSureYouWishToDeleteWithName(PropertyDisplayNames.Document, viewModel.Document.Name);

                DialogResult dialogResult = MessageBox.Show(message, Titles.ApplicationName, MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        if (DocumentDeleteConfirmed != null)
                        {
                            var e = new EventArgs<int>(viewModel.Document.ID);
                            DocumentDeleteConfirmed(_dummySender, e);
                        }
                        break;

                    case DialogResult.No:

                        if (DocumentDeleteCanceled != null)
                        {
                            DocumentDeleteCanceled(_dummySender, EventArgs.Empty);
                        }
                        break;

                    default:
                        throw new ValueNotSupportedException(dialogResult);
                }
            }));
        }

        public static void ShowDocumentIsDeleted(Form parentForm)
        {
            if (parentForm == null) throw new NullException(() => parentForm);

            // Class summary explains why this is 'async'.
            parentForm.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(CommonMessageFormatter.ObjectIsDeleted(PropertyDisplayNames.Document));

                if (DocumentDeletedOK != null)
                {
                    DocumentDeletedOK(_dummySender, EventArgs.Empty);
                }
            }));
        }

        public static void ShowPopupMessages(Form parentForm, IList<CanonicalModel.Message> popupMessages)
        {
            if (parentForm == null) throw new NullException(() => parentForm);
            if (popupMessages == null) throw new NullException(() => popupMessages);

            // Class summary explains why this is 'async'.
            parentForm.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(string.Join(Environment.NewLine, popupMessages.Select(x => x.Text)));

                if (PopupMessagesOK != null)
                {
                    PopupMessagesOK(_dummySender, EventArgs.Empty);
                }
            }));
        }

        public static void ShowMessageBox(Form parentForm, string text)
        {
            if (parentForm == null) throw new NullException(() => parentForm);

            // Class summary explains why this is 'async'.
            parentForm.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(text);
            }));
        }
    }
}
