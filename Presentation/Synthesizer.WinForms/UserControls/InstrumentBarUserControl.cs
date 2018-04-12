using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Drawing;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Properties;
using Point = System.Drawing.Point;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	public partial class InstrumentBarUserControl : UserControl
	{
		public event EventHandler HeightChanged
		{
			add => _instrumentBarElement.HeightChanged += value;
			remove => _instrumentBarElement.HeightChanged -= value;
		}

		public event EventHandler ExpandRequested;
		public event EventHandler PlayRequested;

		public event EventHandler<EventArgs<int>> ExpandPatchRequested;
		public event EventHandler<EventArgs<int>> MovePatchBackwardRequested;
		public event EventHandler<EventArgs<int>> MovePatchForwardRequested;
		public event EventHandler<EventArgs<int>> PlayPatchRequested;
		public event EventHandler<EventArgs<int>> DeletePatchRequested;

		public event EventHandler<EventArgs<int>> ExpandMidiMappingGroupRequested;
		public event EventHandler<EventArgs<int>> MoveMidiMappingGroupBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveMidiMappingGroupForwardRequested;
		public event EventHandler<EventArgs<int>> DeleteMidiMappingGroupRequested;

		private readonly InstrumentBarElement _instrumentBarElement;

		public InstrumentBarUserControl()
		{
			InitializeComponent();

			var diagram = new Diagram();
			diagram.Background.Style.BackStyle.Color = SystemColors.Control.ToArgb();

			_instrumentBarElement = new InstrumentBarElement(
				diagram.Background,
				Resources.RemoveIcon,
				Resources.OpenWindowIcon,
				Resources.LessThanIcon,
				Resources.GreaterThanIcon,
				Resources.PlayIcon,
				new TextMeasurer(diagramControl.CreateGraphics()));

			_instrumentBarElement.ExpandRequested += (sender, e) => ExpandRequested(sender, e);
			_instrumentBarElement.PlayRequested += (sender, e) => PlayRequested(sender, e);

			_instrumentBarElement.ExpandPatchRequested += (sender, e) => ExpandPatchRequested(sender, e);
			_instrumentBarElement.MovePatchBackwardRequested += (sender, e) => MovePatchBackwardRequested(sender, e);
			_instrumentBarElement.MovePatchForwardRequested += (sender, e) => MovePatchForwardRequested(sender, e);
			_instrumentBarElement.PlayPatchRequested += (sender, e) => PlayPatchRequested(sender, e);
			_instrumentBarElement.DeletePatchRequested += (sender, e) => DeletePatchRequested(sender, e);

			_instrumentBarElement.ExpandMidiMappingGroupRequested += (sender, e) => ExpandMidiMappingGroupRequested(sender, e);
			_instrumentBarElement.MoveMidiMappingGroupBackwardRequested += (sender, e) => MoveMidiMappingGroupBackwardRequested(sender, e);
			_instrumentBarElement.MoveMidiMappingGroupForwardRequested += (sender, e) => MoveMidiMappingGroupForwardRequested(sender, e);
			_instrumentBarElement.DeleteMidiMappingGroupRequested += (sender, e) => DeleteMidiMappingGroupRequested(sender, e);

			diagramControl.Location = new Point(0, 0);
			diagramControl.Diagram = diagram;
		}

		public InstrumentBarViewModel ViewModel
		{
			get => _instrumentBarElement.ViewModel;
			set
			{
				_instrumentBarElement.ViewModel = value;
				diagramControl.Refresh();
			}
		}

		public void PositionControls()
		{
			diagramControl.Size = new Size(Width, Height);

			if (_instrumentBarElement != null)
			{
				_instrumentBarElement.Position.WidthInPixels = Width;
				_instrumentBarElement.PositionElements();
				Height = (int)_instrumentBarElement.Position.HeightInPixels;
			}
		}

		private void InstrumentBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
		private void InstrumentBarUserControl_Load(object sender, EventArgs e) => PositionControls();
	}
}
