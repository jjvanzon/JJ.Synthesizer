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
	internal partial class MidiMappingGroupDetailsUserControl : DetailsOrPropertiesUserControlBase
	{
		public event EventHandler<EventArgs<(int midiMappingGroupID, int midiMappingElementID)>> SelectElementRequested;
		public event EventHandler<EventArgs<(int midiMappingGroupID, int midiMappingElementID, float x, float y)>> MoveElementRequested;
		public event EventHandler<EventArgs<(int midiMappingGroupID, int midiMappingElementID)>> ExpandElementRequested;

		private readonly MidiMappingGroupDetails_ViewModelToDiagramConverter _converter;

		// Constructors

		public MidiMappingGroupDetailsUserControl()
		{
			InitializeComponent();

			var textMeasurer = new TextMeasurer(diagramControl.CreateGraphics());

			_converter = new MidiMappingGroupDetails_ViewModelToDiagramConverter(
				textMeasurer,
				SystemInformation.DoubleClickTime,
				SystemInformation.DoubleClickSize.Width);

			_converter.Result.DeleteElementGesture.DeleteSelectionRequested += DeleteElementGesture_DeleteSelectionRequested;
			_converter.Result.ExpandElementKeyboardGesture.ExpandRequested += ExpandElementKeyboardGesture_ExpandRequested;
			_converter.Result.ExpandElementMouseGesture.ExpandRequested += ExpandElementMouseGesture_ExpandRequested;
			_converter.Result.SelectElementGesture.SelectRequested += SelectElementGesture_SelectRequested;
			_converter.Result.MoveGesture.Moved += MoveGesture_Moved;
		}

		private void MidiMappingGroupDetailsUserControl_Load(object sender, EventArgs e)
		{
			TitleBarBackColor = SystemColors.Window;
			TitleLabelVisible = false;

			AddToInstrumentButtonVisible = true;
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

		public new MidiMappingGroupDetailsViewModel ViewModel
		{
			get => (MidiMappingGroupDetailsViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.MidiMappingGroup.ID;

		protected override void ApplyViewModelToControls()
		{
			_converter.Execute(ViewModel);

			diagramControl.Refresh();
		}

		// Events

		private void MidiMappingGroupDetailsUserControl_Resize(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				ApplyViewModelToControls();
			}
		}

		private void MidiMappingGroupDetailsUserControl_Paint(object sender, PaintEventArgs e)
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
			ExpandElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMappingGroup.ID, e.ID)));
		}

		private void ExpandElementMouseGesture_ExpandRequested(object sender, IDEventArgs e)
		{
			if (ViewModel == null) return;
			ExpandElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMappingGroup.ID, e.ID)));
		}

		private void MoveGesture_Moved(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingElementID = (int)e.Element.Tag;

			MoveElementRequested(
				this,
				new EventArgs<(int, int, float, float)>(
					(
					ViewModel.MidiMappingGroup.ID,
					midiMappingElementID,
					e.Element.Position.AbsoluteCenterX,
					e.Element.Position.AbsoluteCenterY)));
		}

		private void SelectElementGesture_SelectRequested(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingElementID = (int)e.Element.Tag;

			SelectElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMappingGroup.ID, midiMappingElementID)));

			_converter.Result.ExpandElementKeyboardGesture.SelectedEntityID = midiMappingElementID;
		}
	}
}