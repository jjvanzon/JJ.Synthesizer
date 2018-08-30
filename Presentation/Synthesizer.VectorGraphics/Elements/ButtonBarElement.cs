using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Collections;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public sealed class ButtonBarElement : ElementBase
    {
        public PictureButtonElement PictureButtonAdd { get; }
        public PictureButtonElement PictureButtonAddToInstrument { get; }
        public PictureButtonElement PictureButtonBrowse { get; }
        public PictureButtonElement PictureButtonClone { get; }
        public PictureButtonElement PictureButtonClose { get; }
        public PictureButtonElement PictureButtonDelete { get; }
        public PictureButtonElement PictureButtonExpand { get; }
        public PictureButtonElement PictureButtonNew { get; }
        public PictureButtonElement PictureButtonPlay { get; }
        public PictureButtonElement PictureButtonRedo { get; }
        public PictureButtonElement PictureButtonRefresh { get; }
        public PictureButtonElement PictureButtonRename { get; }
        public PictureButtonElement PictureButtonSave { get; }
        public PictureButtonElement PictureButtonTreeStructure { get; }
        public PictureButtonElement PictureButtonUndo { get; }

        private readonly PictureButtonElement[] _pictureButtonsInReverseOrder;

        public ButtonBarElement(Element parent, ToolTipElement toolTipElement, UnderlyingPictureWrapper underlyingPictureWrapper)
            : base(parent)
        {
            PictureButtonAdd = new PictureButtonElement(this, underlyingPictureWrapper.PictureAdd, CommonResourceFormatter.Add, toolTipElement);

            PictureButtonAddToInstrument = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureAddToInstrument,
                ResourceFormatter.AddToInstrument,
                toolTipElement);

            PictureButtonBrowse = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureBrowse,
                ResourceFormatter.DocumentList,
                toolTipElement);

            PictureButtonClone = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureClone,
                CommonResourceFormatter.Clone,
                toolTipElement);

            PictureButtonClose = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureClose,
                CommonResourceFormatter.Close,
                toolTipElement);

            PictureButtonDelete = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureDelete,
                CommonResourceFormatter.Delete,
                toolTipElement);

            PictureButtonExpand = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureExpand,
                CommonResourceFormatter.Open,
                toolTipElement);

            PictureButtonNew = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureNew,
                CommonResourceFormatter.New,
                toolTipElement);

            PictureButtonPlay = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PicturePlay,
                ResourceFormatter.Play,
                toolTipElement);

            PictureButtonRedo = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureRedo,
                CommonResourceFormatter.Redo,
                toolTipElement);

            PictureButtonRefresh = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureRefresh,
                CommonResourceFormatter.Refresh,
                toolTipElement);

            PictureButtonRename = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureRename,
                CommonResourceFormatter.Rename_WithName(ResourceFormatter.Document),
                toolTipElement);

            PictureButtonSave = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureSave,
                CommonResourceFormatter.Save,
                toolTipElement);

            PictureButtonTreeStructure = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureTreeStructure,
                CommonResourceFormatter.TreeStructure,
                toolTipElement);

            PictureButtonUndo = new PictureButtonElement(
                this,
                underlyingPictureWrapper.PictureUndo,
                CommonResourceFormatter.Undo,
                toolTipElement);

            _pictureButtonsInReverseOrder = new[]
                {
                    PictureButtonTreeStructure,
                    PictureButtonExpand,
                    PictureButtonPlay,
                    PictureButtonAddToInstrument,
                    PictureButtonRename,
                    PictureButtonNew,
                    PictureButtonAdd,
                    PictureButtonDelete,
                    PictureButtonClone,
                    PictureButtonBrowse,
                    PictureButtonSave,
                    PictureButtonUndo,
                    PictureButtonRedo,
                    PictureButtonRefresh,
                    PictureButtonClose
                }.Reverse()
                 .ToArray();

            Position.Height = StyleHelper.ROW_HEIGHT;

            Children.OfType<PictureButtonElement>().ForEach(x => x.Visible = false);
        }

        // Positioning

        public void PositionElements()
        {
            int visibleButtonCount = GetVisibleButtonCount();

            Position.Width = GetWidth(visibleButtonCount);

            float x = Position.Width;

            x -= StyleHelper.SPACING_SMALL;
            x -= StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;

            foreach (PictureButtonElement pictureButton in _pictureButtonsInReverseOrder)
            {
                if (pictureButton.Visible)
                {
                    pictureButton.Position.X = x;

                    x -= StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;
                    x -= StyleHelper.SPACING_LARGE;
                }
            }

            _pictureButtonsInReverseOrder.ForEach(e => e.PositionElements());
        }

        public static float GetWidth(int buttonCount)
            => buttonCount * StyleHelper.PICTURE_BUTTON_PICTURE_SIZE +
               (buttonCount - 1) * StyleHelper.SPACING_LARGE +
               StyleHelper.SPACING_SMALL;

        private int GetVisibleButtonCount()
        {
            var count = 0;
            if (PictureButtonAdd.Visible) count++;
            if (PictureButtonAddToInstrument.Visible) count++;
            if (PictureButtonBrowse.Visible) count++;
            if (PictureButtonClone.Visible) count++;
            if (PictureButtonClose.Visible) count++;
            if (PictureButtonNew.Visible) count++;
            if (PictureButtonExpand.Visible) count++;
            if (PictureButtonPlay.Visible) count++;
            if (PictureButtonRedo.Visible) count++;
            if (PictureButtonRefresh.Visible) count++;
            if (PictureButtonRename.Visible) count++;
            if (PictureButtonDelete.Visible) count++;
            if (PictureButtonSave.Visible) count++;
            if (PictureButtonTreeStructure.Visible) count++;
            if (PictureButtonUndo.Visible) count++;
            return count;
        }
    }
}