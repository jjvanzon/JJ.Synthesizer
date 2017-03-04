using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Configuration;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchDetailsUserControl : DetailsOrPropertiesUserControlBase
    {
        private const bool DEFAULT_MUST_EXECUTE_MOVE_ACTION_WHILE_DRAGGING = false;

        private static readonly Size _defaultToolStripLabelSize = new Size(86, 22);
        private static readonly bool _mustExecuteOperatorMoveActionWhileDragging = GetMustExecuteOperatorMoveActionWhileDragging();

        public event EventHandler<EventArgs<int>> DeleteOperatorRequested;
        public event EventHandler<CreateOperatorEventArgs> CreateOperatorRequested;
        public event EventHandler<MoveOperatorEventArgs> MoveOperatorRequested;
        public event EventHandler<ChangeInputOutletEventArgs> ChangeInputOutletRequested;
        public event EventHandler<SelectOperatorEventArgs> SelectOperatorRequested;
        public event EventHandler<EventArgs<int>> PlayRequested;
        public event EventHandler<EventArgs<int>> ShowOperatorPropertiesRequested;
        public event EventHandler<EventArgs<int>> ShowPatchPropertiesRequested;

        private PatchViewModelToDiagramConverter _converter;
        private PatchViewModelToDiagramConverterResult _converterResult;
        private bool _operatorToolboxItemsViewModelIsApplied; // Dirty way to only apply it once.

        // Constructors

        public PatchDetailsUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            TitleBarText = CommonResourceFormatter.ObjectDetails(ResourceFormatter.Patch);
            buttonPlay.Text = ResourceFormatter.Play;
        }

        protected override void PositionControls()
        {
            base.PositionControls();

            tableLayoutPanelToolboxAndPatch.Left = 0;
            tableLayoutPanelToolboxAndPatch.Top = TitleBarHeight;
            tableLayoutPanelToolboxAndPatch.Width = Width;
            tableLayoutPanelToolboxAndPatch.Height = Height - TitleBarHeight;
        }

        // Binding

        public new PatchDetailsViewModel ViewModel
        {
            get { return (PatchDetailsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override int GetID()
        {
            return ViewModel.Entity.ID;
        }

        protected override void ApplyViewModelToControls()
        {
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

            diagramControl1.Diagram = _converterResult.Diagram;

            ApplyOperatorToolboxItemsViewModel(ViewModel.OperatorToolboxItems);
        }

        private void BindVectorGraphicsEvents()
        {
            _converterResult.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
            _converterResult.MoveGesture.Moving += MoveGesture_Moving;
            _converterResult.MoveGesture.Moved += MoveGesture_Moved;
            _converterResult.DropLineGesture.Dropped += DropLineGesture_Dropped;
            _converterResult.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;
            _converterResult.ShowOperatorPropertiesMouseGesture.ShowOperatorPropertiesRequested += ShowOperatorPropertiesMouseGesture_ShowOperatorPropertiesRequested;
            _converterResult.ShowOperatorPropertiesKeyboardGesture.ShowOperatorPropertiesRequested += ShowOperatorPropertiesKeyboardGesture_ShowOperatorPropertiesRequested;
            _converterResult.ShowPatchPropertiesGesture.ShowPatchPropertiesRequested += ShowPatchPropertiesGesture_ShowPatchPropertiesRequested;
            _converterResult.InletToolTipGesture.ToolTipTextRequested += InletToolTipGesture_ToolTipTextRequested;
            _converterResult.OutletToolTipGesture.ToolTipTextRequested += OutletToolTipGesture_ToolTipTextRequested;
        }

        private void ShowPatchPropertiesGesture_ShowPatchPropertiesRequested(object sender, EventArgs e)
        {
            ShowPatchPropertiesRequested?.Invoke(this, new EventArgs<int>(ViewModel.Entity.ID));
        }

        private void UnbindVectorGraphicsEvents()
        {
            // ReSharper disable once InvertIf
            if (_converterResult != null)
            {
                _converterResult.SelectOperatorGesture.OperatorSelected -= SelectOperatorGesture_OperatorSelected;
                _converterResult.MoveGesture.Moving -= MoveGesture_Moving;
                _converterResult.MoveGesture.Moved -= MoveGesture_Moved;
                _converterResult.DropLineGesture.Dropped -= DropLineGesture_Dropped;
                _converterResult.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;
                _converterResult.ShowOperatorPropertiesMouseGesture.ShowOperatorPropertiesRequested -= ShowOperatorPropertiesMouseGesture_ShowOperatorPropertiesRequested;
                _converterResult.ShowOperatorPropertiesKeyboardGesture.ShowOperatorPropertiesRequested -= ShowOperatorPropertiesKeyboardGesture_ShowOperatorPropertiesRequested;
                _converterResult.ShowPatchPropertiesGesture.ShowPatchPropertiesRequested -= ShowPatchPropertiesGesture_ShowPatchPropertiesRequested;
                _converterResult.InletToolTipGesture.ToolTipTextRequested -= InletToolTipGesture_ToolTipTextRequested;
                _converterResult.OutletToolTipGesture.ToolTipTextRequested -= OutletToolTipGesture_ToolTipTextRequested;
            }
        }

        private void ApplyOperatorToolboxItemsViewModel(IList<Data.Canonical.IDAndName> operatorTypeToolboxItems)
        {
            if (_operatorToolboxItemsViewModelIsApplied)
            {
                return;
            }
            _operatorToolboxItemsViewModelIsApplied = true;

            int i = 1;

            foreach (Data.Canonical.IDAndName idAndName in operatorTypeToolboxItems)
            {
                ToolStripItem toolStripItem = new ToolStripButton
                {
                    Name = "toolStripButton" + i,
                    Size = _defaultToolStripLabelSize,
                    Text = idAndName.Name,
                    DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text,
                    Tag = idAndName.ID
                };

                // TODO: Clean up the event handlers too somewhere.
                toolStripItem.Click += toolStripLabel_Click;

                toolStrip1.Items.Add(toolStripItem);

                i++;
            }
        }

        // Events

        private void DropLineGesture_Dropped(object sender, DroppedEventArgs e)
        {
            if (ViewModel == null) return;

            int inletID =  VectorGraphicsTagHelper.GetInletID(e.DroppedOnElement.Tag);
            int outletID = VectorGraphicsTagHelper.GetOutletID(e.DraggedElement.Tag);

            ChangeInputOutletRequested?.Invoke(this, new ChangeInputOutletEventArgs(
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

            MoveOperatorRequested?.Invoke(this, new MoveOperatorEventArgs(
                ViewModel.Entity.ID,
                operatorID,
                centerX,
                centerY));
        }

        private void toolStripLabel_Click(object sender, EventArgs e)
        {
            if (ViewModel == null) return;

            var control = (ToolStripItem)sender;
            int operatorTypeID = (int)control.Tag;

            CreateOperatorRequested?.Invoke(this, new CreateOperatorEventArgs(ViewModel.Entity.ID, operatorTypeID));
        }

        private void SelectOperatorGesture_OperatorSelected(object sender, ElementEventArgs e)
        {
            if (ViewModel == null) return;

            int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            SelectOperatorRequested?.Invoke(this, new SelectOperatorEventArgs(ViewModel.Entity.ID, operatorID));

            _converterResult.ShowOperatorPropertiesKeyboardGesture.SelectedOperatorID = operatorID;
        }

        private void DeleteOperatorGesture_DeleteRequested(object sender, EventArgs e)
        {
            if (ViewModel == null) return;
            DeleteOperatorRequested?.Invoke(this, new EventArgs<int>(ViewModel.Entity.ID));
        }

        private void ShowOperatorPropertiesMouseGesture_ShowOperatorPropertiesRequested(object sender, IDEventArgs e)
        {
            ShowOperatorPropertiesRequested?.Invoke(this, new EventArgs<int>(e.ID));
        }

        private void ShowOperatorPropertiesKeyboardGesture_ShowOperatorPropertiesRequested(object sender, IDEventArgs e)
        {
            ShowOperatorPropertiesRequested?.Invoke(this, new EventArgs<int>(e.ID));
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (ViewModel == null) return;
            PlayRequested?.Invoke(this, new EventArgs<int>(ViewModel.Entity.ID));
        }

        // TODO: Lower priority: You might want to use the presenter for the the following 3 things.

        private void InletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            if (ViewModel == null) return;

            int inletID = VectorGraphicsTagHelper.GetInletID(e.Element.Tag);

            InletViewModel inletViewModel = ViewModel.Entity.OperatorDictionary.Values.SelectMany(x => x.Inlets)
                                                                               .Where(x => x.ID == inletID)
                                                                               .Single();
            e.ToolTipText = inletViewModel.Caption;
        }

        private void OutletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            if (ViewModel == null) return;

            int id = VectorGraphicsTagHelper.GetOutletID(e.Element.Tag);

            OutletViewModel outletViewModel = ViewModel.Entity.OperatorDictionary.Values.SelectMany(x => x.Outlets)
                                                                                        .Where(x => x.ID == id)
                                                                                        .Single();
            e.ToolTipText = outletViewModel.Caption;
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
