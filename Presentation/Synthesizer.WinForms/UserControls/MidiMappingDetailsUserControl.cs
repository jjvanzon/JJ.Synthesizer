using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class MidiMappingDetailsUserControl : DetailsOrPropertiesUserControlBase
	{
		public event EventHandler<EventArgs<(int midiMappingID, int midiMappingElementID)>> SelectElementRequested;
		public event EventHandler<EventArgs<(int midiMappingID, int midiMappingElementID, float x, float y)>> MoveElementRequested;

		private readonly MidiMappingDetailsViewModelToDiagramConverter _converter;

		// Constructors

		public MidiMappingDetailsUserControl()
		{
			InitializeComponent();

			_converter = new MidiMappingDetailsViewModelToDiagramConverter(SystemInformation.DoubleClickTime, SystemInformation.DoubleClickSize.Width);
			_converter.Result.MoveGesture.Moved += MoveGesture_Moved;
			_converter.Result.SelectElementGesture.SelectRequested += SelectElementGesture_SelectRequested;
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

		private void MoveGesture_Moved(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingElementID = (int)e.Element.Tag;

			// TODO: Looks complicated for no apparent reason. Maybe put helper methods in the vector graphics API?

			float centerX = e.Element.Position.AbsoluteX + e.Element.Position.Width / 2f;
			float centerY = e.Element.Position.AbsoluteY + e.Element.Position.Height / 2f;

			MoveElementRequested(this, new EventArgs<(int, int, float, float)>((ViewModel.MidiMapping.ID, midiMappingElementID, centerX, centerY)));
		}

		private void SelectElementGesture_SelectRequested(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int midiMappingElementID = (int)e.Element.Tag;

			SelectElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMapping.ID, midiMappingElementID)));

			//_converterResult.ExpandElementeyboardGesture.SelectedOperatorID = ViewModel.SelectedElement?.ID;
		}
	}
}
