using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public sealed class TopButtonBarElement : ElementBaseWithScreenViewModel
    {
        public ButtonBarElement ButtonBarElement { get; }

        public TopButtonBarElement(
            Element parent,
            ToolTipElement toolTipElement,
            UnderlyingPictureWrapper underlyingPictureWrapper)
            : base(parent)
        {
            ButtonBarElement = new ButtonBarElement(this,toolTipElement, underlyingPictureWrapper)
            {
                AddButtonVisible = false,
                BrowseButtonVisible = true,
                CloseButtonVisible = false,
                RedoButtonVisible = true,
                RefreshButtonVisible = true,
                RenameButtonVisible = true,
                SaveButtonVisible = true,
                TreeStructureButtonVisible = true,
                UndoButtonVisible = true
            };

            // DIRTY: Low priorty: Coincidentally this is the max number of visible buttons in any given situation (currently).
            const int maxVisibleButtonCount = 10;
            float width = ButtonBarElement.GetWidth(maxVisibleButtonCount);
            const float height = StyleHelper.ROW_HEIGHT;

            ButtonBarElement.Position.Width = width;
            ButtonBarElement.Position.Height = height;
            Position.Width = width;
            Position.Height = height;
        }

        public new TopButtonBarViewModel ViewModel
        {
            get => (TopButtonBarViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToElements()
        {
            ButtonBarElement.Visible = ViewModel.Visible;
            ButtonBarElement.AddToInstrumentButtonVisible = ViewModel.CanAddToInstrument;
            ButtonBarElement.NewButtonVisible = ViewModel.CanCreate;
            ButtonBarElement.ExpandButtonVisible = ViewModel.CanOpenExternally;
            ButtonBarElement.PlayButtonVisible = ViewModel.CanPlay;
            ButtonBarElement.DeleteButtonVisible = ViewModel.CanDelete;
        }

        public override void PositionElements() => ButtonBarElement.PositionElements();
    }
}