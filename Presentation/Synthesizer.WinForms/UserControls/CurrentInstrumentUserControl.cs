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
	public partial class CurrentInstrumentUserControl : UserControl
	{
		public event EventHandler ExpandRequested;
		public event EventHandler PlayRequested;

		public event EventHandler<EventArgs<int>> ExpandPatchRequested;
		public event EventHandler<EventArgs<int>> MovePatchBackwardRequested;
		public event EventHandler<EventArgs<int>> MovePatchForwardRequested;
		public event EventHandler<EventArgs<int>> PlayPatchRequested;
		public event EventHandler<EventArgs<int>> DeletePatchRequested;

		public event EventHandler<EventArgs<int>> ExpandMidiMappingElementRequested;
		public event EventHandler<EventArgs<int>> MoveMidiMappingElementBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveMidiMappingElementForwardRequested;
		public event EventHandler<EventArgs<int>> PlayMidiMappingElementRequested;
		public event EventHandler<EventArgs<int>> DeleteMidiMappingElementRequested;

		private readonly CurrentInstrumentElement _currentInstrumentElement;

		public CurrentInstrumentUserControl()
		{
			InitializeComponent();

			var diagram = new Diagram();
			diagram.Background.Style.BackStyle.Color = SystemColors.Control.ToArgb();

			_currentInstrumentElement = new CurrentInstrumentElement(
				diagram.Background,
				Resources.RemoveIcon,
				Resources.OpenWindowIcon,
				Resources.LessThanIcon,
				Resources.GreaterThanIcon,
				Resources.PlayIcon,
				new TextMeasurer(diagramControl.CreateGraphics()));

			_currentInstrumentElement.ExpandRequested += (sender, e) => ExpandRequested(sender, e);
			_currentInstrumentElement.PlayRequested += (sender, e) => PlayRequested(sender, e);

			_currentInstrumentElement.ExpandPatchRequested += (sender, e) => ExpandPatchRequested(sender, e);
			_currentInstrumentElement.MovePatchBackwardRequested += (sender, e) => MovePatchBackwardRequested(sender, e);
			_currentInstrumentElement.MovePatchForwardRequested += (sender, e) => MovePatchForwardRequested(sender, e);
			_currentInstrumentElement.PlayPatchRequested += (sender, e) => PlayPatchRequested(sender, e);
			_currentInstrumentElement.DeletePatchRequested += (sender, e) => DeletePatchRequested(sender, e);

			_currentInstrumentElement.ExpandMidiMappingElementRequested += (sender, e) => ExpandMidiMappingElementRequested(sender, e);
			_currentInstrumentElement.MoveMidiMappingElementBackwardRequested += (sender, e) => MoveMidiMappingElementBackwardRequested(sender, e);
			_currentInstrumentElement.MoveMidiMappingElementForwardRequested += (sender, e) => MoveMidiMappingElementForwardRequested(sender, e);
			_currentInstrumentElement.PlayMidiMappingElementRequested += (sender, e) => PlayMidiMappingElementRequested(sender, e);
			_currentInstrumentElement.DeleteMidiMappingElementRequested += (sender, e) => DeleteMidiMappingElementRequested(sender, e);

			diagramControl.Location = new Point(0, 0);
			diagramControl.Diagram = diagram;

			PositionControls();
		}

		private void CurrentInstrumentUserControl2_SizeChanged(object sender, EventArgs e) => PositionControls();

		public CurrentInstrumentViewModel ViewModel
		{
			get => _currentInstrumentElement.ViewModel;
			set
			{
				_currentInstrumentElement.ViewModel = value;
				diagramControl.Refresh();
			}
		}

		private void PositionControls()
		{
			diagramControl.Size = new Size(Width, Height);

			if (_currentInstrumentElement != null)
			{
				_currentInstrumentElement.Position.WidthInPixels = Width;
				_currentInstrumentElement.Position.HeightInPixels = Height;

				_currentInstrumentElement.PositionElements();
			}
		}
	}
}
