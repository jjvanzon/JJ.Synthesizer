using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
// ReSharper disable PossibleNullReferenceException
#pragma warning disable IDE0022 // Use expression body for methods

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class PatchDetailsUserControl : DetailsOrPropertiesUserControlBase
	{
		private const bool DEFAULT_MUST_EXECUTE_MOVE_ACTION_WHILE_DRAGGING = false;

		private static readonly bool _mustExecuteOperatorMoveActionWhileDragging = GetMustExecuteOperatorMoveActionWhileDragging();

		public event EventHandler<EventArgs<int>> DeleteOperatorRequested;
		public event EventHandler<MoveOperatorEventArgs> MoveOperatorRequested;
		public event EventHandler<ChangeInputOutletEventArgs> ChangeInputOutletRequested;
		public event EventHandler<PatchAndOperatorEventArgs> SelectOperatorRequested;
		public event EventHandler<EventArgs<int>> ExpandOperatorRequested;
		public event EventHandler<EventArgs<int>> ExpandPatchRequested;
		public event EventHandler<EventArgs<int>> SelectPatchRequested;

		private PatchViewModelToDiagramConverter _converter;
		private PatchViewModelToDiagramConverterResult _converterResult;

		// Constructors

		public PatchDetailsUserControl()
		{
			InitializeComponent();

			ExpandButtonVisible = true;
		}

		// Gui

		protected override void SetTitles() => TitleBarText = CommonResourceFormatter.Details_WithName(ResourceFormatter.Patch);

		protected override void PositionControls()
		{
			base.PositionControls();

			diagramControl.Left = 0;
			diagramControl.Top = TitleBarHeight + 1;
			diagramControl.Width = Width;
			diagramControl.Height = Height - TitleBarHeight;
		}

		// Binding

		public new PatchDetailsViewModel ViewModel
		{
			get => (PatchDetailsViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.Entity.ID;

		protected override void ApplyViewModelToControls()
		{
			SaveButtonVisible = ViewModel.CanSave;

			UnbindVectorGraphicsEvents();

			if (_converterResult == null)
			{
				_converter = new PatchViewModelToDiagramConverter(
					SystemInformation.DoubleClickTime,
					SystemInformation.DoubleClickSize.Width);

				_converterResult = _converter.Execute(ViewModel.Entity);
			}
			else
			{
				_converterResult = _converter.Execute(ViewModel.Entity, _converterResult);
			}

			BindVectorGraphicsEvents();

			diagramControl.Diagram = _converterResult.Diagram;
		}

		private void BindVectorGraphicsEvents()
		{
			_converterResult.SelectOperatorGesture.SelectRequested += SelectOperatorGesture_SelectRequested;
			_converterResult.MoveGesture.Moving += MoveGesture_Moving;
			_converterResult.MoveGesture.Moved += MoveGesture_Moved;
			_converterResult.DropLineGesture.Dropped += DropLineGesture_Dropped;
			_converterResult.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;
			_converterResult.ExpandOperatorMouseGesture.ExpandOperatorRequested += ExpandOperatorMouseGesture_ExpandOperatorRequested;
			_converterResult.ExpandOperatorKeyboardGesture.ExpandOperatorRequested += ExpandOperatorKeyboardGesture_ExpandOperatorRequested;
			_converterResult.ExpandPatchGesture.DoubleClick += ExpandPatchGesture_DoubleClick;
			_converterResult.InletToolTipGesture.ToolTipTextRequested += InletToolTipGesture_ToolTipTextRequested;
			_converterResult.OutletToolTipGesture.ToolTipTextRequested += OutletToolTipGesture_ToolTipTextRequested;
			_converterResult.SelectPatchGesture.Click += SelectPatchGesture_Click;
		}

		private void UnbindVectorGraphicsEvents()
		{
			// ReSharper disable once InvertIf
			if (_converterResult != null)
			{
				_converterResult.SelectOperatorGesture.SelectRequested -= SelectOperatorGesture_SelectRequested;
				_converterResult.MoveGesture.Moving -= MoveGesture_Moving;
				_converterResult.MoveGesture.Moved -= MoveGesture_Moved;
				_converterResult.DropLineGesture.Dropped -= DropLineGesture_Dropped;
				_converterResult.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;
				_converterResult.ExpandOperatorMouseGesture.ExpandOperatorRequested -= ExpandOperatorMouseGesture_ExpandOperatorRequested;
				_converterResult.ExpandOperatorKeyboardGesture.ExpandOperatorRequested -= ExpandOperatorKeyboardGesture_ExpandOperatorRequested;
				_converterResult.ExpandPatchGesture.DoubleClick -= ExpandPatchGesture_DoubleClick;
				_converterResult.InletToolTipGesture.ToolTipTextRequested -= InletToolTipGesture_ToolTipTextRequested;
				_converterResult.OutletToolTipGesture.ToolTipTextRequested -= OutletToolTipGesture_ToolTipTextRequested;
				_converterResult.SelectPatchGesture.Click -= SelectPatchGesture_Click;
			}
		}

		// Events

		private void DropLineGesture_Dropped(object sender, DroppedEventArgs e)
		{
			if (ViewModel == null) return;

			int inletID =  VectorGraphicsTagHelper.GetInletID(e.DroppedOnElement.Tag);
			int outletID = VectorGraphicsTagHelper.GetOutletID(e.DraggedElement.Tag);

			ChangeInputOutletRequested(this, new ChangeInputOutletEventArgs(
				ViewModel.Entity.ID,
				inletID,
				outletID));
		}

		private void MoveGesture_Moving(object sender, ElementEventArgs e)
		{
			if (_mustExecuteOperatorMoveActionWhileDragging)
			{
				DoMoveOperator(e);
			}
		}

		private void MoveGesture_Moved(object sender, ElementEventArgs e)
		{
			if (!_mustExecuteOperatorMoveActionWhileDragging)
			{
				DoMoveOperator(e);
			}
		}

		private void DoMoveOperator(ElementEventArgs e)
		{
			int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

			float centerX = e.Element.Position.AbsoluteX + e.Element.Position.Width / 2f;
			float centerY = e.Element.Position.AbsoluteY + e.Element.Position.Height / 2f;

			MoveOperator(operatorID, centerX, centerY);
		}

		private void MoveOperator(int operatorID, float centerX, float centerY)
		{
			if (ViewModel == null) return;

			MoveOperatorRequested(this, new MoveOperatorEventArgs(
				ViewModel.Entity.ID,
				operatorID,
				centerX,
				centerY));
		}

		private void SelectOperatorGesture_SelectRequested(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;

			int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

			SelectOperatorRequested(this, new PatchAndOperatorEventArgs(ViewModel.Entity.ID, operatorID));

			_converterResult.ExpandOperatorKeyboardGesture.SelectedOperatorID = ViewModel.SelectedOperator?.ID;
		}

		private void DeleteOperatorGesture_DeleteRequested(object sender, EventArgs e)
		{
			if (ViewModel == null) return;
			DeleteOperatorRequested(this, new EventArgs<int>(ViewModel.Entity.ID));

			_converterResult.ExpandOperatorKeyboardGesture.SelectedOperatorID = ViewModel.SelectedOperator?.ID;
		}

		private void ExpandOperatorMouseGesture_ExpandOperatorRequested(object sender, IDEventArgs e)
		{
			ExpandOperatorRequested(this, new EventArgs<int>(e.ID));
		}

		private void ExpandOperatorKeyboardGesture_ExpandOperatorRequested(object sender, IDEventArgs e)
		{
			ExpandOperatorRequested(this, new EventArgs<int>(e.ID));
		}

		private void ExpandPatchGesture_DoubleClick(object sender, EventArgs e)
		{
			ExpandPatchRequested(this, new EventArgs<int>(ViewModel.Entity.ID));
		}

		// TODO: Lower priority: You might want to use the presenter for the the following 3 things.

		private void InletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
		{
			if (ViewModel == null) return;

			int inletID = VectorGraphicsTagHelper.GetInletID(e.Element.Tag);

			InletViewModel inletViewModel = ViewModel.Entity.OperatorDictionary.Values
																			   .SelectMany(x => x.Inlets)
																			   .Single(x => x.ID == inletID);
			e.ToolTipText = inletViewModel.Caption;
		}

		private void OutletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
		{
			if (ViewModel == null) return;

			int id = VectorGraphicsTagHelper.GetOutletID(e.Element.Tag);

			OutletViewModel outletViewModel = ViewModel.Entity.OperatorDictionary.Values.SelectMany(x => x.Outlets)
																						.Single(x => x.ID == id);
			e.ToolTipText = outletViewModel.Caption;
		}

		private void SelectPatchGesture_Click(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;
			SelectPatchRequested(sender, new EventArgs<int>(ViewModel.Entity.ID));
		}

		// Helpers

		private static bool GetMustExecuteOperatorMoveActionWhileDragging()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
			{
				return CustomConfigurationManager.GetSection<ConfigurationSection>().ExecuteOperatorMoveActionWhileDragging;
			}

			return DEFAULT_MUST_EXECUTE_MOVE_ACTION_WHILE_DRAGGING;
		}
	}
}
