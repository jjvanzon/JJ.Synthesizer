using System;
using System.Drawing;
using System.Windows.Forms;
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

		private readonly MidiMappingDetailsViewModelToDiagramConverter _converter;
		private readonly MidiMappingDetailsViewModelToDiagramConverterResult _converterResult;

		// Constructors

		public MidiMappingDetailsUserControl()
		{
			InitializeComponent();

			_converter = new MidiMappingDetailsViewModelToDiagramConverter(SystemInformation.DoubleClickTime, SystemInformation.DoubleClickSize.Width);
			_converterResult = _converter.Result;

			BindVectorGraphicsEvents();
		}

		private void MidiMappingDetailsUserControl_Load(object sender, EventArgs e)
		{
			TitleBarBackColor = SystemColors.Window;
			TitleLabelVisible = false;

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

		private void BindVectorGraphicsEvents()
		{
		//	_converterResult.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
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

		//private void SelectOperatorGesture_OperatorSelected(object sender, ElementEventArgs e)
		//{
		//	if (ViewModel == null) return;

		//	int midiMappingElementID = VectorGraphicsTagHelper.GetMidiMappingElementID(e.Element.Tag);

		//	SelectElementRequested(this, new EventArgs<(int, int)>((ViewModel.MidiMapping.ID, midiMappingElementID)));

		//	//_converterResult.ExpandElementeyboardGesture.SelectedOperatorID = ViewModel.SelectedElement?.ID;
		//}
	}
}
