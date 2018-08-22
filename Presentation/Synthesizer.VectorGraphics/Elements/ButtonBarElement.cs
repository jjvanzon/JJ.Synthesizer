using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Collections;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public class ButtonBarElement : ElementBase
    {
        public event EventHandler AddClicked;
        public event EventHandler AddToInstrumentClicked;
        public event EventHandler CloseClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler ExpandClicked;
        public event EventHandler NewClicked;
        public event EventHandler PlayClicked;
        public event EventHandler RedoClicked;
        public event EventHandler RefreshClicked;
        public event EventHandler SaveClicked;
        public event EventHandler UndoClicked;

        private const float HEIGHT = StyleHelper.ROW_HEIGHT;

        private readonly PictureButtonElement _pictureButtonAdd;
        private readonly PictureButtonElement _pictureButtonAddToInstrument;
        private readonly PictureButtonElement _pictureButtonClose;
        private readonly PictureButtonElement _pictureButtonDelete;
        private readonly PictureButtonElement _pictureButtonExpand;
        private readonly PictureButtonElement _pictureButtonNew;
        private readonly PictureButtonElement _pictureButtonPlay;
        private readonly PictureButtonElement _pictureButtonRedo;
        private readonly PictureButtonElement _pictureButtonRefresh;
        private readonly PictureButtonElement _pictureButtonSave;
        private readonly PictureButtonElement _pictureButtonUndo;

        public ButtonBarElement(
            Element parent,
            ITextMeasurer textMeasurer,
            object underlyingPictureAdd,
            object underlyingPictureAddToInstrument,
            object underlyingPictureClose,
            object underlyingPictureDelete,
            object underlyingPictureExpand,
            object underlyingPictureNew,
            object underlyingPicturePlay,
            object underlyingPictureRedo,
            object underlyingPictureRefresh,
            object underlyingPictureSave,
            object underlyingPictureUndo)
            : this(
                parent,
                new ToolTipElement(
                    parent?.Diagram?.Background,
                    StyleHelper.ToolTipBackStyle,
                    StyleHelper.ToolTipLineStyle,
                    StyleHelper.ToolTipTextStyle,
                    textMeasurer),
                underlyingPictureAdd,
                underlyingPictureAddToInstrument,
                underlyingPictureClose,
                underlyingPictureDelete,
                underlyingPictureExpand,
                underlyingPictureNew,
                underlyingPicturePlay,
                underlyingPictureRedo,
                underlyingPictureRefresh,
                underlyingPictureSave,
                underlyingPictureUndo) { }

        public ButtonBarElement(
            Element parent,
            ToolTipElement toolTipElement,
            object underlyingPictureAdd,
            object underlyingPictureAddToInstrument,
            object underlyingPictureClose,
            object underlyingPictureDelete,
            object underlyingPictureExpand,
            object underlyingPictureNew,
            object underlyingPicturePlay,
            object underlyingPictureRedo,
            object underlyingPictureRefresh,
            object underlyingPictureSave,
            object underlyingPictureUndo)
            : base(parent)
        {
            _pictureButtonAdd = new PictureButtonElement(this, underlyingPictureAdd, CommonResourceFormatter.Add, toolTipElement);
            _pictureButtonAddToInstrument = new PictureButtonElement(this, underlyingPictureAddToInstrument, ResourceFormatter.AddToInstrument, toolTipElement);
            _pictureButtonClose = new PictureButtonElement(this, underlyingPictureClose, CommonResourceFormatter.Close, toolTipElement);
            _pictureButtonDelete = new PictureButtonElement(this, underlyingPictureDelete, CommonResourceFormatter.Delete, toolTipElement);
            _pictureButtonExpand = new PictureButtonElement(this, underlyingPictureExpand, CommonResourceFormatter.Open, toolTipElement);
            _pictureButtonNew = new PictureButtonElement(this, underlyingPictureNew, CommonResourceFormatter.New, toolTipElement);
            _pictureButtonPlay = new PictureButtonElement(this, underlyingPicturePlay, ResourceFormatter.Play, toolTipElement);
            _pictureButtonRedo = new PictureButtonElement(this, underlyingPictureRedo, CommonResourceFormatter.Redo, toolTipElement);
            _pictureButtonRefresh = new PictureButtonElement(this, underlyingPictureRefresh, CommonResourceFormatter.Refresh, toolTipElement);
            _pictureButtonSave = new PictureButtonElement(this, underlyingPictureSave, CommonResourceFormatter.Save, toolTipElement);
            _pictureButtonUndo = new PictureButtonElement(this, underlyingPictureUndo, CommonResourceFormatter.Undo, toolTipElement);

			_pictureButtonAdd.MouseDown += _pictureButtonAdd_MouseDown;
			_pictureButtonAddToInstrument.MouseDown += _pictureButtonAddToInstrument_MouseDown;
            _pictureButtonClose.MouseDown += _pictureButtonClose_MouseDown;
            _pictureButtonDelete.MouseDown += _pictureButtonDelete_MouseDown;
            _pictureButtonExpand.MouseDown += _pictureButtonExpand_MouseDown;
            _pictureButtonNew.MouseDown += _pictureButtonNew_MouseDown;
            _pictureButtonPlay.MouseDown += _pictureButtonPlay_MouseDown;
            _pictureButtonRedo.MouseDown += _pictureButtonRedo_MouseDown;
            _pictureButtonRefresh.MouseDown += _pictureButtonRefresh_MouseDown;
            _pictureButtonSave.MouseDown += _pictureButtonSave_MouseDown;
            _pictureButtonUndo.MouseDown += _pictureButtonUndo_MouseDown;

            // Magic Defaults
            _pictureButtonAdd.Visible = false;
            _pictureButtonAddToInstrument.Visible = false;
            _pictureButtonClose.Visible = true;
            _pictureButtonDelete.Visible = false;
            _pictureButtonExpand.Visible = false;
            _pictureButtonNew.Visible = false;
            _pictureButtonPlay.Visible = false;
            _pictureButtonRedo.Visible = false;
            _pictureButtonRefresh.Visible = false;
            _pictureButtonSave.Visible = false;
            _pictureButtonUndo.Visible = false;
        }

        public bool AddButtonVisible
        {
            get => _pictureButtonAdd.Visible;
            set
            {
                _pictureButtonAdd.Visible = value;
                PositionElements();
            }
        }

        public bool AddToInstrumentButtonVisible
        {
            get => _pictureButtonAddToInstrument.Visible;
            set
            {
                _pictureButtonAddToInstrument.Visible = value;
                PositionElements();
            }
        }

        public bool CloseButtonVisible
        {
            get => _pictureButtonClose.Visible;
            set
            {
                _pictureButtonClose.Visible = value;
                PositionElements();
            }
        }

        public bool DeleteButtonVisible
        {
            get => _pictureButtonDelete.Visible;
            set
            {
                _pictureButtonDelete.Visible = value;
                PositionElements();
            }
        }

        public bool ExpandButtonVisible
        {
            get => _pictureButtonExpand.Visible;
            set
            {
                _pictureButtonExpand.Visible = value;
                PositionElements();
            }
        }

        public bool NewButtonVisible
        {
            get => _pictureButtonNew.Visible;
            set
            {
                _pictureButtonNew.Visible = value;
                PositionElements();
            }
        }

        public bool PlayButtonVisible
        {
            get => _pictureButtonPlay.Visible;
            set
            {
                _pictureButtonPlay.Visible = value;
                PositionElements();
            }
        }

        public bool RedoButtonVisible
        {
            get => _pictureButtonRedo.Visible;
            set
            {
                _pictureButtonRedo.Visible = value;
                PositionElements();
            }
        }

        public bool RefreshButtonVisible
        {
            get => _pictureButtonRefresh.Visible;
            set
            {
                _pictureButtonRefresh.Visible = value;
                PositionElements();
            }
        }

        public bool SaveButtonVisible
        {
            get => _pictureButtonSave.Visible;
            set
            {
                _pictureButtonSave.Visible = value;
                PositionElements();
            }
        }

        public bool UndoButtonVisible
        {
            get => _pictureButtonUndo.Visible;
            set
            {
                _pictureButtonUndo.Visible = value;
                PositionElements();
            }
        }

        public void PositionElements()
        {
            int visibleButtonCount = GetVisibleButtonCount();

            Position.Width = visibleButtonCount * StyleHelper.PICTURE_BUTTON_SIZE;

            Position.Height = HEIGHT;

            float x = Position.Width;

            x -= StyleHelper.PICTURE_BUTTON_SIZE;

            var pictureButtonsInReverseOrder = new[]
            {
                _pictureButtonClose,
                _pictureButtonDelete,
                _pictureButtonAdd,
                _pictureButtonNew,
                _pictureButtonExpand,
                _pictureButtonRefresh,
                _pictureButtonSave,
                _pictureButtonAddToInstrument,
                _pictureButtonPlay,
                _pictureButtonRedo,
                _pictureButtonUndo
            };

            foreach (PictureButtonElement pictureButton in pictureButtonsInReverseOrder)
            {
                if (pictureButton.Visible)
                {
                    pictureButton.Position.X = x;
                    pictureButton.Position.Y = 0;
                    pictureButton.Position.Width = StyleHelper.PICTURE_BUTTON_SIZE;
                    pictureButton.Position.Height = StyleHelper.PICTURE_BUTTON_SIZE;

                    x -= StyleHelper.PICTURE_BUTTON_SIZE;
                }
            }

            pictureButtonsInReverseOrder.ForEach(e => e.PositionElements());
        }

        // Helpers

        private int GetVisibleButtonCount()
        {
            int count = 0;
            if (AddButtonVisible) count++;
            if (AddToInstrumentButtonVisible) count++;
            if (CloseButtonVisible) count++;
            if (NewButtonVisible) count++;
            if (ExpandButtonVisible) count++;
            if (PlayButtonVisible) count++;
            if (RedoButtonVisible) count++;
            if (RefreshButtonVisible) count++;
            if (DeleteButtonVisible) count++;
            if (SaveButtonVisible) count++;
            if (UndoButtonVisible) count++;
            return count;
        }

        private void _pictureButtonAdd_MouseDown(object sender, EventArgs e) => AddClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonAddToInstrument_MouseDown(object sender, EventArgs e) => AddToInstrumentClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonClose_MouseDown(object sender, EventArgs e) => CloseClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonDelete_MouseDown(object sender, EventArgs e) => DeleteClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonExpand_MouseDown(object sender, EventArgs e) => ExpandClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonNew_MouseDown(object sender, EventArgs e) => NewClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonPlay_MouseDown(object sender, EventArgs e) => PlayClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonRedo_MouseDown(object sender, EventArgs e) => RedoClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonRefresh_MouseDown(object sender, EventArgs e) => RefreshClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonSave_MouseDown(object sender, EventArgs e) => SaveClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonUndo_MouseDown(object sender, EventArgs e) => UndoClicked?.Invoke(sender, EventArgs.Empty);
    }
}