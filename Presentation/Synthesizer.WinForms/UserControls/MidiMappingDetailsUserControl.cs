using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Drawing;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class MidiMappingDetailsUserControl : DetailsOrPropertiesUserControlBase
	{
		public event EventHandler<EventArgs<(int midiMappingID, int midiMappingElementID)>> SelectElementRequested;
		public event EventHandler<EventArgs<(int midiMappingID, int midiMappingElementID, float x, float y)>> MoveElementRequested;
		public event EventHandler<EventArgs<(int midiMappingID, int midiMappingElementID)>> ExpandElementRequested;

		private readonly MidiMappingDetailsViewModelToDiagramConverter _converter;

		// Constructors

		public MidiMappingDetailsUserControl()
		{
			InitializeComponent();

			var textMeasurer = new TextMeasurer(diagramControl.CreateGraphics());

			_converter = new MidiMappingDetailsViewModelToDiagramConverter(
				textMeasurer,
				SystemInformation.DoubleClickTime,
				SystemInformation.DoubleClickSize.Width);

			_converter.Result.DeleteElementGesture.DeleteSelectionRequested += DeleteElementGesture_DeleteSelectionRequested;
			_converter.Result.ExpandElementKeyboardGesture.ExpandRequested += ExpandElementKeyboardGesture_ExpandRequested;
			_converter.Result.ExpandElementMouseGesture.ExpandRequested += ExpandElementMouseGesture_ExpandRequested;
			_converter.Result.SelectElementGesture.SelectRequested += SelectElementGesture_SelectRequested;
			_converter.Result.MoveGesture.Moved += MoveGesture_Moved;
		}

		private void MidiMappingDetailsUserControl_Load(object sender, EventArgs e)
		{
			TitleBarBackColor = SystemColors.Window;
			TitleLabelVisible = false;

			AddToInstrumentButtonVisible = false;
			PlayButtonVisible = false;
			NewButtonVisible = true;

			diagramControl.Left = 0;
			diagramControl.Top = 0;
			diagramControl.Diagram = _converter.Result.Diagram;

			// Make sure the base's button bar is in front of the diagramControl.
			diagramControl.SendToBack();
		}

		// Gui

		protected override void PositionControls()
		{
			base.PositionControls();

			diagramControl.Width = Width;
			diagramControl.Height = Height;
		}

		// Binding

		public new MidiMappingDetailsViewModel ViewModel
		{
			get => (MidiMappingDetailsViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.MidiMapping.ID;

		protected override void ApplyViewModelToControls()
		{
			_converter.Execute(ViewModel);

			diagramControl.Refresh();
		}

		// Events

		private void MidiMappingDetailsUserControl_Resize(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				ApplyViewModelToControls();
			}
		}

		private void MidiMappingDetailsUserControl_Paint(object sender, PaintEventArgs e)
		{
			if (ViewModel != null)
			{
				ApplyViewModelToControls();
			}
		}

		private void DeleteElementGesture_DeleteSelectionRequested(object sender, EventArgs e) => Delete();

		private void ExpandElementKeyboardGesture_ExpandRequested(object sender, IDEventArgs e)
		{
			if (ViewModel == null) return;
			ExpandElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMapping.ID, e.ID)));
		}

		private void ExpandElementMouseGesture_ExpandRequested(object sender, IDEventArgs e)
		{
			if (ViewModel == null) return;
			ExpandElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMapping.ID, e.ID)));
		}

		private void MoveGesture_Moved(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingElementID = (int)e.Element.Tag;

			MoveElementRequested(
				this,
				new EventArgs<(int, int, float, float)>(
					(
					ViewModel.MidiMapping.ID,
					midiMappingElementID,
					e.Element.Position.AbsoluteCenterX,
					e.Element.Position.AbsoluteCenterY)));
		}

		private void SelectElementGesture_SelectRequested(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingElementID = (int)e.Element.Tag;

			SelectElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMapping.ID, midiMappingElementID)));

			_converter.Result.ExpandElementKeyboardGesture.SelectedEntityID = midiMappingElementID;
		}
	}
}