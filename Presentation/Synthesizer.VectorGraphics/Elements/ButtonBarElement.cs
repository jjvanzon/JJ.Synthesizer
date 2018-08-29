using System;
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
        public event EventHandler AddClicked;
        public event EventHandler AddToInstrumentClicked;
        public event EventHandler BrowseClicked;
        public event EventHandler CloneClicked;
        public event EventHandler CloseClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler RenameClicked;
        public event EventHandler ExpandClicked;
        public event EventHandler NewClicked;
        public event EventHandler PlayClicked;
        public event EventHandler RedoClicked;
        public event EventHandler RefreshClicked;
        public event EventHandler SaveClicked;
        public event EventHandler TreeStructureClicked;
        public event EventHandler UndoClicked;

        private readonly PictureButtonElement _pictureButtonAdd;
        private readonly PictureButtonElement _pictureButtonAddToInstrument;
        private readonly PictureButtonElement _pictureButtonBrowse;
        private readonly PictureButtonElement _pictureButtonClone;
        private readonly PictureButtonElement _pictureButtonClose;
        private readonly PictureButtonElement _pictureButtonDelete;
        private readonly PictureButtonElement _pictureButtonExpand;
        private readonly PictureButtonElement _pictureButtonNew;
        private readonly PictureButtonElement _pictureButtonPlay;
        private readonly PictureButtonElement _pictureButtonRedo;
        private readonly PictureButtonElement _pictureButtonRefresh;
        private readonly PictureButtonElement _pictureButtonRename;
        private readonly PictureButtonElement _pictureButtonSave;
        private readonly PictureButtonElement _pictureButtonTreeStructure;
        private readonly PictureButtonElement _pictureButtonUndo;

        private readonly PictureButtonElement[] _pictureButtonsInReverseOrder;

        public ButtonBarElement(
            Element parent,
            ToolTipElement toolTipElement,
            UnderlyingPictureWrapper underlyingPictureWrapper)
            : base(parent)
        {
            _pictureButtonAdd = new PictureButtonElement(this, underlyingPictureWrapper.PictureAdd, CommonResourceFormatter.Add, toolTipElement);
            _pictureButtonAddToInstrument = new PictureButtonElement(this, underlyingPictureWrapper.PictureAddToInstrument, ResourceFormatter.AddToInstrument, toolTipElement);
            _pictureButtonBrowse = new PictureButtonElement(this, underlyingPictureWrapper.PictureBrowse, ResourceFormatter.DocumentList, toolTipElement);
            _pictureButtonClone = new PictureButtonElement(this, underlyingPictureWrapper.PictureClone, CommonResourceFormatter.Clone, toolTipElement);
            _pictureButtonClose = new PictureButtonElement(this, underlyingPictureWrapper.PictureClose, CommonResourceFormatter.Close, toolTipElement);
            _pictureButtonDelete = new PictureButtonElement(this, underlyingPictureWrapper.PictureDelete, CommonResourceFormatter.Delete, toolTipElement);
            _pictureButtonExpand = new PictureButtonElement(this, underlyingPictureWrapper.PictureExpand, CommonResourceFormatter.Open, toolTipElement);
            _pictureButtonNew = new PictureButtonElement(this, underlyingPictureWrapper.PictureNew, CommonResourceFormatter.New, toolTipElement);
            _pictureButtonPlay = new PictureButtonElement(this, underlyingPictureWrapper.PicturePlay, ResourceFormatter.Play, toolTipElement);
            _pictureButtonRedo = new PictureButtonElement(this, underlyingPictureWrapper.PictureRedo, CommonResourceFormatter.Redo, toolTipElement);
            _pictureButtonRefresh = new PictureButtonElement(this, underlyingPictureWrapper.PictureRefresh, CommonResourceFormatter.Refresh, toolTipElement);
            _pictureButtonRename = new PictureButtonElement(this, underlyingPictureWrapper.PictureRename, CommonResourceFormatter.Rename_WithName(ResourceFormatter.Document), toolTipElement);
            _pictureButtonSave = new PictureButtonElement(this, underlyingPictureWrapper.PictureSave, CommonResourceFormatter.Save, toolTipElement);
            _pictureButtonTreeStructure = new PictureButtonElement(this, underlyingPictureWrapper.PictureTreeStructure, CommonResourceFormatter.TreeStructure, toolTipElement);
            _pictureButtonUndo = new PictureButtonElement(this, underlyingPictureWrapper.PictureUndo, CommonResourceFormatter.Undo, toolTipElement);

            _pictureButtonsInReverseOrder = new[]
                {
                    _pictureButtonTreeStructure,

                    _pictureButtonExpand,
                    _pictureButtonPlay,
                    _pictureButtonAddToInstrument,

                    _pictureButtonRename,

                    _pictureButtonNew,
                    _pictureButtonAdd,
                    _pictureButtonDelete,
                    _pictureButtonClone,

                    _pictureButtonBrowse,
                    _pictureButtonSave,
                    _pictureButtonUndo,
                    _pictureButtonRedo,
                    _pictureButtonRefresh,
                    _pictureButtonClose,
                }.Reverse()
                 .ToArray();

            _pictureButtonAdd.MouseDown += _pictureButtonAdd_MouseDown;
			_pictureButtonAddToInstrument.MouseDown += _pictureButtonAddToInstrument_MouseDown;
            _pictureButtonBrowse.MouseDown += _pictureButtonBrowse_MouseDown;
            _pictureButtonClone.MouseDown += _pictureButtonClone_MouseDown;
            _pictureButtonClose.MouseDown += _pictureButtonClose_MouseDown;
            _pictureButtonDelete.MouseDown += _pictureButtonDelete_MouseDown;
            _pictureButtonExpand.MouseDown += _pictureButtonExpand_MouseDown;
            _pictureButtonNew.MouseDown += _pictureButtonNew_MouseDown;
            _pictureButtonPlay.MouseDown += _pictureButtonPlay_MouseDown;
            _pictureButtonRedo.MouseDown += _pictureButtonRedo_MouseDown;
            _pictureButtonRefresh.MouseDown += _pictureButtonRefresh_MouseDown;
            _pictureButtonRename.MouseDown += _pictureButtonRename_MouseDown;
            _pictureButtonSave.MouseDown += _pictureButtonSave_MouseDown;
            _pictureButtonTreeStructure.MouseDown += _pictureButtonTreeStructure_MouseDown;
            _pictureButtonUndo.MouseDown += _pictureButtonUndo_MouseDown;

            Position.Height = StyleHelper.ROW_HEIGHT;

            // Magic Defaults
            _pictureButtonAdd.Visible = false;
            _pictureButtonAddToInstrument.Visible = false;
            _pictureButtonBrowse.Visible = false;
            _pictureButtonClone.Visible = false;
            _pictureButtonClose.Visible = true;
            _pictureButtonDelete.Visible = false;
            _pictureButtonExpand.Visible = false;
            _pictureButtonNew.Visible = false;
            _pictureButtonPlay.Visible = false;
            _pictureButtonRedo.Visible = false;
            _pictureButtonRefresh.Visible = false;
            _pictureButtonRename.Visible = false;
            _pictureButtonSave.Visible = false;
            _pictureButtonTreeStructure.Visible = false;
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

        public bool CloneButtonVisible
        {
            get => _pictureButtonClone.Visible;
            set
            {
                _pictureButtonClone.Visible = value;
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

        public bool BrowseButtonVisible
        {
            get => _pictureButtonBrowse.Visible;
            set
            {
                _pictureButtonBrowse.Visible = value;
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

        public bool RenameButtonVisible
        {
            get => _pictureButtonRename.Visible;
            set
            {
                _pictureButtonRename.Visible = value;
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

        public bool TreeStructureButtonVisible
        {
            get => _pictureButtonTreeStructure.Visible;
            set
            {
                _pictureButtonTreeStructure.Visible = value;
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
            int count = 0;
            if (AddButtonVisible) count++;
            if (AddToInstrumentButtonVisible) count++;
            if (BrowseButtonVisible) count++;
            if (CloneButtonVisible) count++;
            if (CloseButtonVisible) count++;
            if (NewButtonVisible) count++;
            if (ExpandButtonVisible) count++;
            if (PlayButtonVisible) count++;
            if (RedoButtonVisible) count++;
            if (RefreshButtonVisible) count++;
            if (RenameButtonVisible) count++;
            if (DeleteButtonVisible) count++;
            if (SaveButtonVisible) count++;
            if (TreeStructureButtonVisible) count++;
            if (UndoButtonVisible) count++;
            return count;
        }

        // Events

        private void _pictureButtonAdd_MouseDown(object sender, EventArgs e) => AddClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonAddToInstrument_MouseDown(object sender, EventArgs e) => AddToInstrumentClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonBrowse_MouseDown(object sender, EventArgs e) => BrowseClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonClone_MouseDown(object sender, EventArgs e) => CloneClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonClose_MouseDown(object sender, EventArgs e) => CloseClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonDelete_MouseDown(object sender, EventArgs e) => DeleteClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonExpand_MouseDown(object sender, EventArgs e) => ExpandClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonNew_MouseDown(object sender, EventArgs e) => NewClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonPlay_MouseDown(object sender, EventArgs e) => PlayClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonRedo_MouseDown(object sender, EventArgs e) => RedoClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonRefresh_MouseDown(object sender, EventArgs e) => RefreshClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonRename_MouseDown(object sender, EventArgs e) => RenameClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonSave_MouseDown(object sender, EventArgs e) => SaveClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonTreeStructure_MouseDown(object sender, EventArgs e) => TreeStructureClicked?.Invoke(sender, EventArgs.Empty);
        private void _pictureButtonUndo_MouseDown(object sender, EventArgs e) => UndoClicked?.Invoke(sender, EventArgs.Empty);
    }
}