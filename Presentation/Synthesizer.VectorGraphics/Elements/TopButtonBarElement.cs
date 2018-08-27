using System;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public class TopButtonBarElement : ElementBaseWithScreenViewModel
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

            ButtonBarElement.Position.Width = ButtonBarElement.MaxWidth;
            ButtonBarElement.Position.Height = StyleHelper.ROW_HEIGHT;
        }

        public new TopButtonBarViewModel ViewModel
        {
            get => (TopButtonBarViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToElements()
        {
            ButtonBarElement.AddToInstrumentButtonVisible = ViewModel.CanAddToInstrument;
            ButtonBarElement.NewButtonVisible = ViewModel.CanCreate;
            ButtonBarElement.ExpandButtonVisible = ViewModel.CanOpenExternally;
            ButtonBarElement.PlayButtonVisible = ViewModel.CanPlay;
            ButtonBarElement.DeleteButtonVisible = ViewModel.CanDelete;
        }

        public float MaxWidth => ButtonBarElement.MaxWidth;

        public override void PositionElements() => ButtonBarElement.PositionElements();
    }
}