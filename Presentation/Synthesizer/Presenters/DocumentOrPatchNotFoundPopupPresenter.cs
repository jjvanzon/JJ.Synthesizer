using System;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.ResourceStrings;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class DocumentOrPatchNotFoundPopupPresenter : PresenterBase<DocumentOrPatchNotFoundPopupViewModel>
	{
		private readonly IDocumentRepository _documentRepository;

		public DocumentOrPatchNotFoundPopupPresenter(IDocumentRepository documentRepository) => _documentRepository = documentRepository ?? throw new NullException(() => documentRepository);

	    /// <see cref="PresenterBase{DocumentOrPatchNotFoundPopupViewModel}.ExecuteNonPersistedAction"/>
		public void Show(DocumentOrPatchNotFoundPopupViewModel userInput, string documentName, string patchName = null) => ExecuteNonPersistedAction(
	        userInput,
	        () =>
	        {
	            // GetEntities
	            Document document = _documentRepository.TryGetByName(documentName);
	            string canonicalPatchName = NameHelper.ToCanonical(patchName);
	            Patch patch = document?.Patches
	                                  .Where(x => string.Equals(NameHelper.ToCanonical(x.Name), canonicalPatchName))
	                                  .SingleWithClearException(new { canonicalPatchName });
	            // ToViewModel
	            string message;
	            if (document == null)
	            {
	                message = CommonResourceFormatter.NotFound_WithType_AndName(ResourceFormatter.Document, documentName);
	            }
	            else if (patch == null)
	            {
	                message = CommonResourceFormatter.NotFound_WithType_AndName(ResourceFormatter.Patch, patchName);
	            }
	            else
	            {
	                throw new Exception($"Either {nameof(document)} or {nameof(patch)} should have been null.");
	            }

	            userInput.NotFoundMessage = message;
	            userInput.Visible = true;
	        });

	    public void OK(DocumentOrPatchNotFoundPopupViewModel userInput) => ExecuteNonPersistedAction(userInput, () =>
	    {
	        userInput.MustCloseMainView = true;
	        userInput.Visible = false;
	    });
	}
}