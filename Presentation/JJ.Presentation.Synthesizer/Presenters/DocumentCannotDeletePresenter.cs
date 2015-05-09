using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentCannotDeletePresenter
    {
        public DocumentCannotDeleteViewModel Show(Document document, IList<Message> messages)
        {
            if (document == null) throw new NullException(() => document);

            DocumentCannotDeleteViewModel viewModel = document.ToCannotDeleteViewModel(messages);
            return viewModel;
        }

        public DocumentCannotDeleteViewModel OK(DocumentCannotDeleteViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.Visible = false;
            return viewModel;
        }
    }
}
