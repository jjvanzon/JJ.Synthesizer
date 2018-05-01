using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;
using JJ.Framework.Drawing;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using KeyEventArgs = JJ.Framework.VectorGraphics.EventArg.KeyEventArgs;
using Rectangle = JJ.Framework.VectorGraphics.Models.Elements.Rectangle;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class CurveDetailsUserControl : DetailsOrPropertiesUserControlBase
	{
		public event EventHandler<EventArgs<int>> ChangeSelectedNodeTypeRequested;
		public event EventHandler<EventArgs<int>> CreateNodeRequested;
		public event EventHandler<EventArgs<int>> ExpandCurveRequested;
		public event EventHandler<MoveNodeEventArgs> NodeMoving;
		public event EventHandler<MoveNodeEventArgs> NodeMoved;
		public event EventHandler<EventArgs<int>> SelectCurveRequested;
		public event EventHandler<NodeEventArgs> SelectNodeRequested;
		public event EventHandler<NodeEventArgs> ExpandNodeRequested;

		/// <summary> Only create after SetCurveFacade is called. </summary>
		private CurveDetailsViewModelToDiagramConverter _converter;

		private readonly TextMeasurer _textMeasurer;

		public CurveDetailsUserControl()
		{
			InitializeComponent();

			TitleBarBackColor = SystemColors.Window;
			TitleLabelVisible = false;

			// Make sure the base's button bar is in front of the diagramControl.
			diagramControl.SendToBack();

			_textMeasurer = new TextMeasurer(diagramControl.CreateGraphics());
		}

		private void CurveDetailsUserControl_Load(object sender, EventArgs e)
		{
			diagramControl.Diagram = _converter.Result.Diagram;
		}

		/// <summary>
		/// It is exceptional to pass a business logic object to a UI element,
		/// but the Curve editors use the calculation of the business layer,
		/// in order to plot the curve exactly as used in the sound calculations.
		/// </summary>
		public void SetCurveFacade(CurveFacade curveFacade)
		{
			_converter = new CurveDetailsViewModelToDiagramConverter(
				SystemInformation.DoubleClickTime,
				SystemInformation.DoubleClickSize.Width,
				_textMeasurer,
				curveFacade);

			_converter.Result.BackgroundClickGesture.Click += BackgroundClickGesture_Click;
			_converter.Result.BackgroundDoubleClickGesture.DoubleClick += BackgroundDoubleClickGesture_DoubleClick;
			_converter.Result.ChangeNodeTypeGesture.ChangeNodeTypeRequested += ChangeNodeTypeGesture_ChangeNodeTypeRequested;
			_converter.Result.KeyDownGesture.KeyDown += Diagram_KeyDown;
			_converter.Result.DeleteGesture.DeleteSelectionRequested += DeleteGesture_DeleteSelectionRequested;
			_converter.Result.MoveNodeGesture.Moved += MoveNodeGesture_Moved;
			_converter.Result.MoveNodeGesture.Moving += MoveNodeGesture_Moving;
			_converter.Result.NodeToolTipGesture.ToolTipTextRequested += NodeToolTipGesture_ToolTipTextRequested;
			_converter.Result.SelectNodeGesture.SelectRequested += SelectGesture_Selected;
			_converter.Result.ExpandNodeKeyboardGesture.ExpandRequested += ExpandNodeKeyboardGesture_ExpandRequested;
			_converter.Result.ExpandNodeMouseGesture.ExpandRequested += ExpandNodeMouseGesture_ExpandRequested;
		}

		// Gui

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Details_WithName(ResourceFormatter.Curve);
		}

		protected override void PositionControls()
		{
			base.PositionControls();

			diagramControl.Left = 0;
			diagramControl.Top = 0;
			diagramControl.Width = Width;
			diagramControl.Height = Height;
		}

		// Binding

		public new CurveDetailsViewModel ViewModel
		{
			// ReSharper disable once MemberCanBePrivate.Global
			get => (CurveDetailsViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.Curve.ID;

		protected override void ApplyViewModelToControls()
		{
			AssertConverter();

			_converter.Execute(ViewModel);

			diagramControl.Refresh();
		}

		// Events

		private void CurveDetailsUserControl_Resize(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				ApplyViewModelToControls();
			}
		}

		private void CurveDetailsUserControl_Paint(object sender, PaintEventArgs e)
		{
			if (ViewModel != null)
			{
				ApplyViewModelToControls();
			}
		}

		private void CurveDetailsUserControl_AddClicked(object sender, EventArgs e)
		{
			CreateNode();

		}

		private void DeleteGesture_DeleteSelectionRequested(object sender, EventArgs e) => Delete();

		// ReSharper disable once RedundantNameQualifier
		private void Diagram_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case KeyCodeEnum.Insert:
					CreateNode();
					break;

				// NOTE: Delete action handled by DeleteGesture_DeleteSelectionRequested.
			}
		}

		private void SelectGesture_Selected(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;
			AssertConverter();

			int nodeID = (int)e.Element.Tag;

			SelectNodeRequested(this, new NodeEventArgs(ViewModel.Curve.ID, nodeID));

			_converter.Result.ExpandNodeKeyboardGesture.SelectedEntityID = nodeID;
		}

		private void MoveNodeGesture_Moving(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;
			if (NodeMoving == null) return;
			AssertConverter();

			int nodeID = (int)e.Element.Tag;

			var rectangle = (Rectangle)e.Element;

			float x = rectangle.Position.AbsoluteX + rectangle.Position.Width / 2;
			float y = rectangle.Position.AbsoluteY + rectangle.Position.Height / 2;

			var e2 = new MoveNodeEventArgs(ViewModel.Curve.ID, nodeID, x, y);

			NodeMoving(this, e2);

			ApplyViewModelToControls();

			// TODO: This kind of seems to belong in the ApplyViewModelToControls().
			// Refresh ToolTip Text
			NodeViewModel nodeViewModel = ViewModel.Nodes[nodeID];
			_converter.Result.NodeToolTipGesture.ChangeToolTipText(nodeViewModel.Caption);
		}

		private void MoveNodeGesture_Moved(object sender, ElementEventArgs e)
		{
			// TODO: Lots of code repetition betwen Moved and Moving events.
			if (ViewModel == null) return;
			if (NodeMoved == null) return;
			AssertConverter();

			int nodeID = (int)e.Element.Tag;

			var rectangle = (Rectangle)e.Element;

			float x = rectangle.Position.AbsoluteX + rectangle.Position.Width / 2;
			float y = rectangle.Position.AbsoluteY + rectangle.Position.Height / 2;

			var e2 = new MoveNodeEventArgs(ViewModel.Curve.ID, nodeID, x, y);

			NodeMoved(this, e2);

			ApplyViewModelToControls();

			// TODO: This kind of seems to belong in the ApplyViewModelToControls().
			// Refresh ToolTip Text
			NodeViewModel nodeViewModel = ViewModel.Nodes[nodeID];
			_converter.Result.NodeToolTipGesture.ChangeToolTipText(nodeViewModel.Caption);
		}

		private void BackgroundClickGesture_Click(object sender, ElementEventArgs e)
		{
			if (ViewModel == null) return;
			SelectCurveRequested(sender, new EventArgs<int>(ViewModel.Curve.ID));
		}

		private void BackgroundDoubleClickGesture_DoubleClick(object sender, EventArgs e)
		{
			if (ViewModel == null) return;
			ExpandCurveRequested(sender, new EventArgs<int>(ViewModel.Curve.ID));
		}

		private void ChangeNodeTypeGesture_ChangeNodeTypeRequested(object sender, EventArgs e)
		{
			if (ViewModel == null) return;
			ChangeSelectedNodeTypeRequested(this, new EventArgs<int>(ViewModel.Curve.ID));
		}

		private void ExpandNodeKeyboardGesture_ExpandRequested(object sender, IDEventArgs e)
		{
			if (ViewModel == null) return;
			ExpandNodeRequested(this, new NodeEventArgs(ViewModel.Curve.ID, e.ID));
		}

		private void ExpandNodeMouseGesture_ExpandRequested(object sender, IDEventArgs e)
		{
			if (ViewModel == null) return;
			ExpandNodeRequested(this, new NodeEventArgs(ViewModel.Curve.ID, e.ID));
		}

		// TODO: This logic should be in the presenter.
		private void NodeToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
		{
			if (ViewModel == null) return;

			int nodeID = (int)e.Element.Tag;

			NodeViewModel nodeViewModel = ViewModel.Nodes[nodeID];

			e.ToolTipText = nodeViewModel.Caption;
		}

		private void CreateNode()
		{
			if (ViewModel == null) return;
			CreateNodeRequested(this, new EventArgs<int>(ViewModel.Curve.ID));
		}

		private void AssertConverter()
		{
			if (_converter == null)
			{
				throw new Exception($"{nameof(_converter)} is null. Call {nameof(SetCurveFacade)} first.");
			}
		}
	}
}
