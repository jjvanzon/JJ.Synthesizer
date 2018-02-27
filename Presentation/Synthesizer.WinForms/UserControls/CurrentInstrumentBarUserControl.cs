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
	public partial class CurrentInstrumentBarUserControl : UserControl
	{
		public event EventHandler ExpandRequested;
		public event EventHandler PlayRequested;

		public event EventHandler<EventArgs<int>> ExpandPatchRequested;
		public event EventHandler<EventArgs<int>> MovePatchBackwardRequested;
		public event EventHandler<EventArgs<int>> MovePatchForwardRequested;
		public event EventHandler<EventArgs<int>> PlayPatchRequested;
		public event EventHandler<EventArgs<int>> DeletePatchRequested;

		public event EventHandler<EventArgs<int>> ExpandMidiMappingRequested;
		public event EventHandler<EventArgs<int>> MoveMidiMappingBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveMidiMappingForwardRequested;
		public event EventHandler<EventArgs<int>> DeleteMidiMappingRequested;

		private readonly CurrentInstrumentBarElement _currentInstrumentBarElement;

		public CurrentInstrumentBarUserControl()
		{
			InitializeComponent();

			var diagram = new Diagram();
			diagram.Background.Style.BackStyle.Color = SystemColors.Control.ToArgb();

			_currentInstrumentBarElement = new CurrentInstrumentBarElement(
				diagram.Background,
				Resources.RemoveIcon,
				Resources.OpenWindowIcon,
				Resources.LessThanIcon,
				Resources.GreaterThanIcon,
				Resources.PlayIcon,
				new TextMeasurer(diagramControl.CreateGraphics()));

			_currentInstrumentBarElement.ExpandRequested += (sender, e) => ExpandRequested(sender, e);
			_currentInstrumentBarElement.PlayRequested += (sender, e) => PlayRequested(sender, e);

			_currentInstrumentBarElement.ExpandPatchRequested += (sender, e) => ExpandPatchRequested(sender, e);
			_currentInstrumentBarElement.MovePatchBackwardRequested += (sender, e) => MovePatchBackwardRequested(sender, e);
			_currentInstrumentBarElement.MovePatchForwardRequested += (sender, e) => MovePatchForwardRequested(sender, e);
			_currentInstrumentBarElement.PlayPatchRequested += (sender, e) => PlayPatchRequested(sender, e);
			_currentInstrumentBarElement.DeletePatchRequested += (sender, e) => DeletePatchRequested(sender, e);

			_currentInstrumentBarElement.ExpandMidiMappingRequested += (sender, e) => ExpandMidiMappingRequested(sender, e);
			_currentInstrumentBarElement.MoveMidiMappingBackwardRequested += (sender, e) => MoveMidiMappingBackwardRequested(sender, e);
			_currentInstrumentBarElement.MoveMidiMappingForwardRequested += (sender, e) => MoveMidiMappingForwardRequested(sender, e);
			_currentInstrumentBarElement.DeleteMidiMappingRequested += (sender, e) => DeleteMidiMappingRequested(sender, e);

			diagramControl.Location = new Point(0, 0);
			diagramControl.Diagram = diagram;

			PositionControls();
		}

		private void CurrentInstrumentBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();

		public CurrentInstrumentBarViewModel ViewModel
		{
			get => _currentInstrumentBarElement.ViewModel;
			set
			{
				_currentInstrumentBarElement.ViewModel = value;
				diagramControl.Refresh();
			}
		}

		public void PositionControls()
		{
			diagramControl.Size = new Size(Width, Height);

			if (_currentInstrumentBarElement != null)
			{
				_currentInstrumentBarElement.Position.WidthInPixels = Width;

				_currentInstrumentBarElement.PositionElements();

				Height = (int)_currentInstrumentBarElement.Position.HeightInPixels;
			}
		}
	}
}
