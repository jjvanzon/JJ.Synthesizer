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
	public partial class CurrentInstrumentUserControl2 : UserControl
	{
		public event EventHandler ExpandRequested;
		public event EventHandler<EventArgs<int>> ExpandItemRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler PlayRequested;
		public event EventHandler<EventArgs<int>> PlayItemRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;

		private readonly Diagram _diagram;
		private readonly CurrentInstrumentElement _currentInstrumentElement;

		public CurrentInstrumentUserControl2()
		{
			InitializeComponent();

			_diagram = new Diagram();

			_currentInstrumentElement = new CurrentInstrumentElement(
				_diagram,
				Resources.RemoveIcon,
				Resources.OpenWindowIcon,
				Resources.LessThanIcon,
				Resources.GreaterThanIcon,
				Resources.PlayIcon,
				new TextMeasurer(diagramControl.CreateGraphics()))
			{
				Parent = _diagram.Background
			};

			_currentInstrumentElement.ExpandRequested += (sender, e) => ExpandRequested(sender, e);
			_currentInstrumentElement.ExpandItemRequested += (sender, e) => ExpandItemRequested(sender, e);
			_currentInstrumentElement.MoveBackwardRequested += (sender, e) => MoveBackwardRequested(sender, e);
			_currentInstrumentElement.MoveForwardRequested += (sender, e) => MoveForwardRequested(sender, e);
			_currentInstrumentElement.PlayRequested += (sender, e) => PlayRequested(sender, e);
			_currentInstrumentElement.PlayItemRequested += (sender, e) => PlayItemRequested(sender, e);
			_currentInstrumentElement.DeleteRequested += (sender, e) => DeleteRequested(sender, e);

			diagramControl.Location = new Point(0, 0);
			diagramControl.Diagram = _diagram;

			PositionControls();
		}

		private void CurrentInstrumentUserControl2_SizeChanged(object sender, EventArgs e) => PositionControls();

		public CurrentInstrumentViewModel ViewModel
		{
			get => _currentInstrumentElement.ViewModel;
			set => _currentInstrumentElement.ViewModel = value;
		}

		private void PositionControls()
		{
			diagramControl.Size = new Size(Width, Height);

			// TODO: This seems a hack.
			if (_currentInstrumentElement != null)
			{
				_currentInstrumentElement.Position.WidthInPixels = Width;
				_currentInstrumentElement.Position.HeightInPixels = Height;

				_currentInstrumentElement.PositionElements();
			}
		}
	}
}
