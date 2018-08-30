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
		public event EventHandler<EventArgs<(int midiMappingGroupID, int midiMappingID)>> SelectMidiMappingRequested;
		public event EventHandler<EventArgs<(int midiMappingGroupID, int midiMappingID, float x, float y)>> MoveMidiMappingRequested;
		public event EventHandler<EventArgs<int>> ExpandMidiMappingRequested;

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

			_converter.Result.DeleteMidiMappingGesture.DeleteSelectionRequested += DeleteMidiMappingGesture_DeleteSelectionRequested;
			_converter.Result.ExpandMidiMappingKeyboardGesture.ExpandRequested += ExpandMidiMappingKeyboardGesture_ExpandRequested;
			_converter.Result.ExpandMidiMappingMouseGesture.ExpandRequested += ExpandMidiMappingMouseGesture_ExpandRequested;
			_converter.Result.SelectMidiMappingGesture.SelectRequested += SelectMidiMappingGesture_SelectRequested;
			_converter.Result.MoveGesture.Moved += MoveGesture_Moved;
		}

		private void MidiMappingGroupDetailsUserControl_Load(object sender, EventArgs e)
		{
            TitleBarUserControl.BackColor = SystemColors.Window;
            TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonAdd.Visible = true;
            TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonAddToInstrument.Visible = true;
		    TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonNew.Visible = true;

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
			// ReSharper disable once MemberCanBePrivate.Global
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

		private void DeleteMidiMappingGesture_DeleteSelectionRequested(object sender, EventArgs e) => Delete();

		private void ExpandMidiMappingKeyboardGesture_ExpandRequested(object sender, IDEventArgs e)
		{
			if (ViewModel == null) return;
			ExpandMidiMappingRequested(this, new EventArgs<int>(e.ID));
		}

		private void ExpandMidiMappingMouseGesture_ExpandRequested(object sender, IDEventArgs e)
		{
			if (ViewModel == null) return;
			ExpandMidiMappingRequested(this, new EventArgs<int>(e.ID));
		}

		private void MoveGesture_Moved(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingID = (int)e.Element.Tag;

			MoveMidiMappingRequested(
				this,
				new EventArgs<(int, int, float, float)>(
					(
					ViewModel.MidiMappingGroup.ID,
					midiMappingID,
					e.Element.Position.AbsoluteCenterX,
					e.Element.Position.AbsoluteCenterY)));
		}

		private void SelectMidiMappingGesture_SelectRequested(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingID = (int)e.Element.Tag;

			SelectMidiMappingRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMappingGroup.ID, midiMappingID)));

			_converter.Result.ExpandMidiMappingKeyboardGesture.SelectedEntityID = midiMappingID;
		}
	}
}