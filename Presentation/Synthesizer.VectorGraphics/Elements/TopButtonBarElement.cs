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
            ButtonBarElement = new ButtonBarElement(this,toolTipElement, underlyingPictureWrapper);
            ButtonBarElement.PictureButtonBrowse.Visible = true;
            ButtonBarElement.PictureButtonClone.Visible = true;
            ButtonBarElement.PictureButtonRedo.Visible = true;
            ButtonBarElement.PictureButtonRefresh.Visible = true;
            ButtonBarElement.PictureButtonRename.Visible = true;
            ButtonBarElement.PictureButtonSave.Visible = true;
            ButtonBarElement.PictureButtonTreeStructure.Visible = true;
            ButtonBarElement.PictureButtonUndo.Visible = true;

            // DIRTY: Low priority: Coincidentally this is the max number of visible buttons in any given situation (currently).
            const int maxVisibleButtonCount = 11;
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
            ButtonBarElement.PictureButtonAddToInstrument.Visible = ViewModel.CanAddToInstrument;
            ButtonBarElement.PictureButtonNew.Visible = ViewModel.CanCreate;
            ButtonBarElement.PictureButtonClone.Visible = ViewModel.CanClone;
            ButtonBarElement.PictureButtonDelete.Visible = ViewModel.CanDelete;
            ButtonBarElement.PictureButtonExpand.Visible = ViewModel.CanOpenExternally;
            ButtonBarElement.PictureButtonPlay.Visible = ViewModel.CanPlay;
        }

        public override void PositionElements() => ButtonBarElement.PositionElements();
    }
}