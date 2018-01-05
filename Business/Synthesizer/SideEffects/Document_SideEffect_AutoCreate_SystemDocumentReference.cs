using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Document_SideEffect_AutoCreate_SystemDocumentReference : ISideEffect
	{
		private readonly Document _document;
		private readonly DocumentFacade _documentFacade;

		public Document_SideEffect_AutoCreate_SystemDocumentReference(Document document, RepositoryWrapper repositories)
		{
			_document = document ?? throw new NullException(() => document);
			_documentFacade = new DocumentFacade(repositories);
		}

		public void Execute()
		{
			Document systemDocument = _documentFacade.GetSystemDocument();

			_documentFacade.CreateDocumentReference(_document, systemDocument);
		}
	}
}