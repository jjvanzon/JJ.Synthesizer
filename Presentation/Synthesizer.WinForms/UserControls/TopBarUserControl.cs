using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Drawing;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using Point = System.Drawing.Point;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    public partial class TopBarUserControl : UserControl
    {
        private readonly TopBarElement _topBarElement;

        public TopBarUserControl()
        {
            InitializeComponent();

            var diagram = new Diagram();
            diagram.Background.Style.BackStyle.Color = SystemColors.Control.ToArgb();

            _topBarElement = new TopBarElement(
                diagram.Background,
                UnderlyingPictureHelper.UnderlyingPictureWrapper,
                new TextMeasurer(diagramControl.CreateGraphics()));

            diagramControl.Location = new Point(0, 0);
            diagramControl.Diagram = diagram;
        }

        public TopButtonBarViewModel TopButtonBarViewModel
        {
            get => _topBarElement.TopButtonBarViewModel;
            set
            {
                _topBarElement.TopButtonBarViewModel = value;
                diagramControl.Refresh();
            }
        }

        public InstrumentBarViewModel InstrumentBarViewModel
        {
            get => _topBarElement.InstrumentBarViewModel;
            set
            {
                _topBarElement.InstrumentBarViewModel = value;
                diagramControl.Refresh();
            }
        }

        public void PositionControls()
        {
            diagramControl.Size = new Size(Width, Height);

            if (_topBarElement != null)
            {
                _topBarElement.Position.WidthInPixels = Width;
                _topBarElement.PositionElements();
                Height = (int)_topBarElement.Position.HeightInPixels;
            }
        }

        private void TopBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
        private void TopBarUserControl_Load(object sender, EventArgs e) => PositionControls();

        // Events

        public event EventHandler HeightChanged
        {
            add => _topBarElement.HeightChanged += value;
            remove => _topBarElement.HeightChanged -= value;
        }

        public event EventHandler InstrumentBar_ExpandRequested
        {
            add => _topBarElement.InstrumentBar_ExpandRequested += value;
            remove => _topBarElement.InstrumentBar_ExpandRequested -= value;
        }

        public event EventHandler InstrumentBar_PlayRequested
        {
            add => _topBarElement.InstrumentBar_PlayRequested += value;
            remove => _topBarElement.InstrumentBar_PlayRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_ExpandPatchRequested
        {
            add => _topBarElement.InstrumentBar_ExpandPatchRequested += value;
            remove => _topBarElement.InstrumentBar_ExpandPatchRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MovePatchBackwardRequested
        {
            add => _topBarElement.InstrumentBar_MovePatchBackwardRequested += value;
            remove => _topBarElement.InstrumentBar_MovePatchBackwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MovePatchForwardRequested
        {
            add => _topBarElement.InstrumentBar_MovePatchForwardRequested += value;
            remove => _topBarElement.InstrumentBar_MovePatchForwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_PlayPatchRequested
        {
            add => _topBarElement.InstrumentBar_PlayPatchRequested += value;
            remove => _topBarElement.InstrumentBar_PlayPatchRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_DeletePatchRequested
        {
            add => _topBarElement.InstrumentBar_DeletePatchRequested += value;
            remove => _topBarElement.InstrumentBar_DeletePatchRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_ExpandMidiMappingGroupRequested
        {
            add => _topBarElement.InstrumentBar_ExpandMidiMappingGroupRequested += value;
            remove => _topBarElement.InstrumentBar_ExpandMidiMappingGroupRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MoveMidiMappingGroupBackwardRequested
        {
            add => _topBarElement.InstrumentBar_MoveMidiMappingGroupBackwardRequested += value;
            remove => _topBarElement.InstrumentBar_MoveMidiMappingGroupBackwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MoveMidiMappingGroupForwardRequested
        {
            add => _topBarElement.InstrumentBar_MoveMidiMappingGroupForwardRequested += value;
            remove => _topBarElement.InstrumentBar_MoveMidiMappingGroupForwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_DeleteMidiMappingGroupRequested
        {
            add => _topBarElement.InstrumentBar_DeleteMidiMappingGroupRequested += value;
            remove => _topBarElement.InstrumentBar_DeleteMidiMappingGroupRequested -= value;
        }

        public event EventHandler TopButtonBar_AddToInstrumentRequested
        {
            add => _topBarElement.TopButtonBar_AddToInstrumentRequested += value;
            remove => _topBarElement.TopButtonBar_AddToInstrumentRequested -= value;
        }

        public event EventHandler TopButtonBar_CloseRequested
        {
            add => _topBarElement.TopButtonBar_CloseRequested += value;
            remove => _topBarElement.TopButtonBar_CloseRequested -= value;
        }

        public event EventHandler TopButtonBar_DeleteRequested
        {
            add => _topBarElement.TopButtonBar_DeleteRequested += value;
            remove => _topBarElement.TopButtonBar_DeleteRequested -= value;
        }

        public event EventHandler TopButtonBar_DocumentGridShowRequested
        {
            add => _topBarElement.TopButtonBar_DocumentGridShowRequested += value;
            remove => _topBarElement.TopButtonBar_DocumentGridShowRequested -= value;
        }

        public event EventHandler TopButtonBar_DocumentPropertiesShowRequested
        {
            add => _topBarElement.TopButtonBar_DocumentPropertiesShowRequested += value;
            remove => _topBarElement.TopButtonBar_DocumentPropertiesShowRequested -= value;
        }

        public event EventHandler TopButtonBar_DocumentTreeShowOrCloseRequested
        {
            add => _topBarElement.TopButtonBar_DocumentTreeShowOrCloseRequested += value;
            remove => _topBarElement.TopButtonBar_DocumentTreeShowOrCloseRequested -= value;
        }

        public event EventHandler TopButtonBar_NewRequested
        {
            add => _topBarElement.TopButtonBar_NewRequested += value;
            remove => _topBarElement.TopButtonBar_NewRequested -= value;
        }

        public event EventHandler TopButtonBar_OpenItemExternallyRequested
        {
            add => _topBarElement.TopButtonBar_OpenItemExternallyRequested += value;
            remove => _topBarElement.TopButtonBar_OpenItemExternallyRequested -= value;
        }

        public event EventHandler TopButtonBar_PlayRequested
        {
            add => _topBarElement.TopButtonBar_PlayRequested += value;
            remove => _topBarElement.TopButtonBar_PlayRequested -= value;
        }

        public event EventHandler TopButtonBar_RefreshRequested
        {
            add => _topBarElement.TopButtonBar_RefreshRequested += value;
            remove => _topBarElement.TopButtonBar_RefreshRequested -= value;
        }

        public event EventHandler TopButtonBar_RedoRequested
        {
            add => _topBarElement.TopButtonBar_RedoRequested += value;
            remove => _topBarElement.TopButtonBar_RedoRequested -= value;
        }

        public event EventHandler TopButtonBar_SaveRequested
        {
            add => _topBarElement.TopButtonBar_SaveRequested += value;
            remove => _topBarElement.TopButtonBar_SaveRequested -= value;
        }

        public event EventHandler TopButtonBar_UndoRequested
        {
            add => _topBarElement.TopButtonBar_UndoRequested += value;
            remove => _topBarElement.TopButtonBar_UndoRequested -= value;
        }
    }
}