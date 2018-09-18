using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public class MainViewModel
	{
		// General

		public string TitleBar { get; set; }
		public MonitoringBarViewModel MonitoringBar { get; set; }

		public IList<string> ValidationMessages { get; set; }
		public IList<string> WarningMessages { get; set; }
		/// <summary> It is suggested you show this as a modal window. </summary>
		public IList<string> PopupMessages { get; set; }
		public DocumentOrPatchNotFoundPopupViewModel DocumentOrPatchNotFound { get; set; }
		public bool Successful { get; set; }
		public bool PropertiesPanelVisible { get; set; }
		public bool DetailsOrGridPanelVisible { get; set; }
		public bool CurveDetailsPanelVisible { get; set; }
		public bool MustClose { get; set; }

		// The Document List

		public DocumentGridViewModel DocumentGrid { get; set; }
		/// <summary> Only for creating a new document. </summary>
		public DocumentDetailsViewModel DocumentDetails { get; set; }
		/// <summary> It is suggested you show this as a modal window. </summary>
		public DocumentCannotDeleteViewModel DocumentCannotDelete { get; set; }
		/// <summary> It is suggested you show this as a modal window. </summary>
		public DocumentDeleteViewModel DocumentDelete { get; set; }
		/// <summary> It is suggested you show this as a modal window. </summary>
		public DocumentDeletedViewModel DocumentDeleted { get; set; }

		// The Open Document

		public DocumentViewModel Document { get; set; }
	}
}
