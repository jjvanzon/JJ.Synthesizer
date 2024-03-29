﻿using System;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public sealed class TitleBarElement : ElementBase
    {
        private readonly Label _titleLabel;
        private readonly ButtonBarElement _buttonBarElement;

        public TitleBarElement(
            Element parent,
            ITextMeasurer textMeasurer,
            UnderlyingPictureWrapper underlyingPictureWrapper)
            : base(parent)
        {
            var toolTipElement = new ToolTipElement(
                parent?.Diagram?.Background,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle,
                textMeasurer);

            _titleLabel = new Label(this)
            {
                TextStyle = StyleHelper.TitleTextStyle
            };

            _titleLabel.Position.Height = StyleHelper.ROW_HEIGHT;

            _buttonBarElement = new ButtonBarElement(this, toolTipElement, underlyingPictureWrapper);

            Position.Height = StyleHelper.ROW_HEIGHT;
        }

        public string Text
        {
            get => _titleLabel.Text;
            set => _titleLabel.Text = value;
        }

        public bool AddButtonVisible
        {
            get => _buttonBarElement.AddButtonVisible;
            set
            {
                _buttonBarElement.AddButtonVisible = value;
                PositionElements();
            }
        }

        public bool AddToInstrumentButtonVisible
        {
            get => _buttonBarElement.AddToInstrumentButtonVisible;
            set
            {
                _buttonBarElement.AddToInstrumentButtonVisible = value;
                PositionElements();
            }
        }

        public bool BrowseButtonVisible
        {
            get => _buttonBarElement.BrowseButtonVisible;
            set
            {
                _buttonBarElement.BrowseButtonVisible = value;
                PositionElements();
            }
        }

        public bool CloneButtonVisible
        {
            get => _buttonBarElement.CloneButtonVisible;
            set
            {
                _buttonBarElement.CloneButtonVisible = value;
                PositionElements();
            }
        }

        public bool CloseButtonVisible
        {
            get => _buttonBarElement.CloseButtonVisible;
            set
            {
                _buttonBarElement.CloseButtonVisible = value;
                PositionElements();
            }
        }

        public bool DeleteButtonVisible
        {
            get => _buttonBarElement.DeleteButtonVisible;
            set
            {
                _buttonBarElement.DeleteButtonVisible = value;
                PositionElements();
            }
        }

        public bool ExpandButtonVisible
        {
            get => _buttonBarElement.ExpandButtonVisible;
            set
            {
                _buttonBarElement.ExpandButtonVisible = value;
                PositionElements();
            }
        }

        public bool NewButtonVisible
        {
            get => _buttonBarElement.NewButtonVisible;
            set
            {
                _buttonBarElement.NewButtonVisible = value;
                PositionElements();
            }
        }

        public bool PlayButtonVisible
        {
            get => _buttonBarElement.PlayButtonVisible;
            set
            {
                _buttonBarElement.PlayButtonVisible = value;
                PositionElements();
            }
        }

        public bool RedoButtonVisible
        {
            get => _buttonBarElement.RedoButtonVisible;
            set
            {
                _buttonBarElement.RedoButtonVisible = value;
                PositionElements();
            }
        }

        public bool RefreshButtonVisible
        {
            get => _buttonBarElement.RefreshButtonVisible;
            set
            {
                _buttonBarElement.RefreshButtonVisible = value;
                PositionElements();
            }
        }

        public bool RenameButtonVisible
        {
            get => _buttonBarElement.RenameButtonVisible;
            set
            {
                _buttonBarElement.RenameButtonVisible = value;
                PositionElements();
            }
        }

        public bool SaveButtonVisible
        {
            get => _buttonBarElement.SaveButtonVisible;
            set
            {
                _buttonBarElement.SaveButtonVisible = value;
                PositionElements();
            }
        }

        public bool TreeStructureButtonVisible
        {
            get => _buttonBarElement.TreeStructureButtonVisible;
            set
            {
                _buttonBarElement.TreeStructureButtonVisible = value;
                PositionElements();
            }
        }

        public bool UndoButtonVisible
        {
            get => _buttonBarElement.UndoButtonVisible;
            set
            {
                _buttonBarElement.UndoButtonVisible = value;
                PositionElements();
            }
        }

        // Positioning

        public float ButtonBarWidth => _buttonBarElement.Position.Width;

        public void PositionElements()
        {
            _buttonBarElement.PositionElements();

            _titleLabel.Position.Width = Position.Width - _buttonBarElement.Position.Width;
            _buttonBarElement.Position.X = Position.Width - _buttonBarElement.Position.Width;
        }

        // Events

        public event EventHandler AddClicked
        {
            add => _buttonBarElement.AddClicked += value;
            remove => _buttonBarElement.AddClicked -= value;
        }

        public event EventHandler AddToInstrumentClicked
        {
            add => _buttonBarElement.AddToInstrumentClicked += value;
            remove => _buttonBarElement.AddToInstrumentClicked -= value;
        }

        public event EventHandler BrowseClicked
        {
            add => _buttonBarElement.BrowseClicked += value;
            remove => _buttonBarElement.BrowseClicked -= value;
        }

        public event EventHandler CloneClicked
        {
            add => _buttonBarElement.CloneClicked += value;
            remove => _buttonBarElement.CloneClicked -= value;
        }

        public event EventHandler CloseClicked
        {
            add => _buttonBarElement.CloseClicked += value;
            remove => _buttonBarElement.CloseClicked -= value;
        }

        public event EventHandler DeleteClicked
        {
            add => _buttonBarElement.DeleteClicked += value;
            remove => _buttonBarElement.DeleteClicked -= value;
        }

        public event EventHandler NewClicked
        {
            add => _buttonBarElement.NewClicked += value;
            remove => _buttonBarElement.NewClicked -= value;
        }

        public event EventHandler ExpandClicked
        {
            add => _buttonBarElement.ExpandClicked += value;
            remove => _buttonBarElement.ExpandClicked -= value;
        }

        public event EventHandler PlayClicked
        {
            add => _buttonBarElement.PlayClicked += value;
            remove => _buttonBarElement.PlayClicked -= value;
        }

        public event EventHandler RedoClicked
        {
            add => _buttonBarElement.RedoClicked += value;
            remove => _buttonBarElement.RedoClicked -= value;
        }

        public event EventHandler RefreshClicked
        {
            add => _buttonBarElement.RefreshClicked += value;
            remove => _buttonBarElement.RefreshClicked -= value;
        }

        public event EventHandler RenameClicked
        {
            add => _buttonBarElement.RenameClicked += value;
            remove => _buttonBarElement.RenameClicked -= value;
        }

        public event EventHandler SaveClicked
        {
            add => _buttonBarElement.SaveClicked += value;
            remove => _buttonBarElement.SaveClicked -= value;
        }

        public event EventHandler TreeStructureClicked
        {
            add => _buttonBarElement.TreeStructureClicked += value;
            remove => _buttonBarElement.TreeStructureClicked -= value;
        }

        public event EventHandler UndoClicked
        {
            add => _buttonBarElement.UndoClicked += value;
            remove => _buttonBarElement.UndoClicked -= value;
        }
    }
}