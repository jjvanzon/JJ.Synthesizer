using System;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public sealed class TitleBarElement : ElementBase
    {
        private readonly Label _titleLabel;

        public ButtonBarElement ButtonBarElement { get; }

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

            ButtonBarElement = new ButtonBarElement(this, toolTipElement, underlyingPictureWrapper);

            Position.Height = StyleHelper.ROW_HEIGHT;
        }

        public string Text
        {
            get => _titleLabel.Text;
            set => _titleLabel.Text = value;
        }

        public bool AddButtonVisible
        {
            get => ButtonBarElement.AddButtonVisible;
            set
            {
                ButtonBarElement.AddButtonVisible = value;
                PositionElements();
            }
        }

        public bool AddToInstrumentButtonVisible
        {
            get => ButtonBarElement.AddToInstrumentButtonVisible;
            set
            {
                ButtonBarElement.AddToInstrumentButtonVisible = value;
                PositionElements();
            }
        }

        public bool BrowseButtonVisible
        {
            get => ButtonBarElement.BrowseButtonVisible;
            set
            {
                ButtonBarElement.BrowseButtonVisible = value;
                PositionElements();
            }
        }

        public bool CloneButtonVisible
        {
            get => ButtonBarElement.CloneButtonVisible;
            set
            {
                ButtonBarElement.CloneButtonVisible = value;
                PositionElements();
            }
        }

        public bool CloseButtonVisible
        {
            get => ButtonBarElement.CloseButtonVisible;
            set
            {
                ButtonBarElement.CloseButtonVisible = value;
                PositionElements();
            }
        }

        public bool DeleteButtonVisible
        {
            get => ButtonBarElement.DeleteButtonVisible;
            set
            {
                ButtonBarElement.DeleteButtonVisible = value;
                PositionElements();
            }
        }

        public bool ExpandButtonVisible
        {
            get => ButtonBarElement.ExpandButtonVisible;
            set
            {
                ButtonBarElement.ExpandButtonVisible = value;
                PositionElements();
            }
        }

        public bool NewButtonVisible
        {
            get => ButtonBarElement.NewButtonVisible;
            set
            {
                ButtonBarElement.NewButtonVisible = value;
                PositionElements();
            }
        }

        public bool PlayButtonVisible
        {
            get => ButtonBarElement.PlayButtonVisible;
            set
            {
                ButtonBarElement.PlayButtonVisible = value;
                PositionElements();
            }
        }

        public bool RedoButtonVisible
        {
            get => ButtonBarElement.RedoButtonVisible;
            set
            {
                ButtonBarElement.RedoButtonVisible = value;
                PositionElements();
            }
        }

        public bool RefreshButtonVisible
        {
            get => ButtonBarElement.RefreshButtonVisible;
            set
            {
                ButtonBarElement.RefreshButtonVisible = value;
                PositionElements();
            }
        }

        public bool RenameButtonVisible
        {
            get => ButtonBarElement.RenameButtonVisible;
            set
            {
                ButtonBarElement.RenameButtonVisible = value;
                PositionElements();
            }
        }

        public bool SaveButtonVisible
        {
            get => ButtonBarElement.SaveButtonVisible;
            set
            {
                ButtonBarElement.SaveButtonVisible = value;
                PositionElements();
            }
        }

        public bool TreeStructureButtonVisible
        {
            get => ButtonBarElement.TreeStructureButtonVisible;
            set
            {
                ButtonBarElement.TreeStructureButtonVisible = value;
                PositionElements();
            }
        }

        public bool UndoButtonVisible
        {
            get => ButtonBarElement.UndoButtonVisible;
            set
            {
                ButtonBarElement.UndoButtonVisible = value;
                PositionElements();
            }
        }

        // Positioning

        public float ButtonBarWidth => ButtonBarElement.Position.Width;

        public void PositionElements()
        {
            ButtonBarElement.PositionElements();

            _titleLabel.Position.Width = Position.Width - ButtonBarElement.Position.Width;
            ButtonBarElement.Position.X = Position.Width - ButtonBarElement.Position.Width;
        }
    }
}