using System;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    internal class TopButtonBarElement : ElementBaseWithScreenViewModel
    {
        private readonly ButtonBarElement _buttonBarElement;

        public TopButtonBarElement(
            Element parent,
            ToolTipElement toolTipElement,
            UnderlyingPictureWrapper underlyingPictureWrapper)
            : base(parent)
        {
            _buttonBarElement = new ButtonBarElement(this,toolTipElement, underlyingPictureWrapper)
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

            _buttonBarElement.Position.Width = _buttonBarElement.MaxWidth;
            _buttonBarElement.Position.Height = StyleHelper.ROW_HEIGHT;
        }

        public new TopButtonBarViewModel ViewModel
        {
            get => (TopButtonBarViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToElements()
        {
            _buttonBarElement.AddToInstrumentButtonVisible = ViewModel.CanAddToInstrument;
            _buttonBarElement.NewButtonVisible = ViewModel.CanCreate;
            _buttonBarElement.ExpandButtonVisible = ViewModel.CanOpenExternally;
            _buttonBarElement.PlayButtonVisible = ViewModel.CanPlay;
            _buttonBarElement.DeleteButtonVisible = ViewModel.CanDelete;
        }

        public float MaxWidth => _buttonBarElement.MaxWidth;

        public override void PositionElements() => _buttonBarElement.PositionElements();

        public event EventHandler AddToInstrumentRequested
        {
            add => _buttonBarElement.AddToInstrumentClicked += value;
            remove => _buttonBarElement.AddToInstrumentClicked -= value;
        }

        public event EventHandler CloseRequested
        {
            add => _buttonBarElement.CloseClicked += value;
            remove => _buttonBarElement.CloseClicked -= value;
        }

        public event EventHandler DeleteRequested
        {
            add => _buttonBarElement.DeleteClicked += value;
            remove => _buttonBarElement.DeleteClicked -= value;
        }

        public event EventHandler DocumentGridShowRequested
        {
            add => _buttonBarElement.BrowseClicked += value;
            remove => _buttonBarElement.BrowseClicked -= value;
        }

        public event EventHandler DocumentPropertiesShowRequested
        {
            add => _buttonBarElement.RenameClicked += value;
            remove => _buttonBarElement.RenameClicked -= value;
        }

        public event EventHandler DocumentTreeShowOrCloseRequested
        {
            add => _buttonBarElement.TreeStructureClicked += value;
            remove => _buttonBarElement.TreeStructureClicked -= value;
        }

        public event EventHandler NewRequested
        {
            add => _buttonBarElement.NewClicked += value;
            remove => _buttonBarElement.NewClicked -= value;
        }

        public event EventHandler OpenItemExternallyRequested
        {
            add => _buttonBarElement.ExpandClicked += value;
            remove => _buttonBarElement.ExpandClicked -= value;
        }

        public event EventHandler PlayRequested
        {
            add => _buttonBarElement.PlayClicked += value;
            remove => _buttonBarElement.PlayClicked -= value;
        }

        public event EventHandler RefreshRequested
        {
            add => _buttonBarElement.RefreshClicked += value;
            remove => _buttonBarElement.RefreshClicked -= value;
        }

        public event EventHandler RedoRequested
        {
            add => _buttonBarElement.RedoClicked += value;
            remove => _buttonBarElement.RedoClicked -= value;
        }

        public event EventHandler SaveRequested
        {
            add => _buttonBarElement.SaveClicked += value;
            remove => _buttonBarElement.SaveClicked -= value;
        }

        public event EventHandler UndoRequested
        {
            add => _buttonBarElement.UndoClicked += value;
            remove => _buttonBarElement.UndoClicked -= value;
        }
    }
}