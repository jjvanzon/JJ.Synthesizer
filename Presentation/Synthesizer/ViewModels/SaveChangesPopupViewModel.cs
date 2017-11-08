namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SaveChangesPopupViewModel : ViewModelBase
    {
        internal int? DocumentIDToOpenAfterConfirmation { get; set; }
        internal bool MustGoToDocumentCreateAfterConfirmation { get; set; }
    }
}
