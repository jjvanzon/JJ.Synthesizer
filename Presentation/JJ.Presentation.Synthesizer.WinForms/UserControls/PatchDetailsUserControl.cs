using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Configuration;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Configuration;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchDetailsUserControl : PatchDetailsUserControl_NotDesignable
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;
        public event EventHandler DeleteOperatorRequested;
        public event EventHandler<CreateOperatorEventArgs> CreateOperatorRequested;
        public event EventHandler<MoveEntityEventArgs> MoveOperatorRequested;
        public event EventHandler<ChangeInputOutletEventArgs> ChangeInputOutletRequested;
        public event EventHandler<Int32EventArgs> SelectOperatorRequested;
        public event EventHandler PlayRequested;
        public event EventHandler<Int32EventArgs> OperatorPropertiesRequested;
        public event EventHandler SelectedOperatorPropertiesRequested;

        private PatchViewModelToDiagramConverter _converter;
        private PatchViewModelToDiagramConverterResult _vectorGraphics;
        private static bool _alwaysRecreateDiagram = GetAlwaysRecreateDiagram();

        // Constructors

        public PatchDetailsUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectDetails(PropertyDisplayNames.Patch);
            buttonPlay.Text = Titles.Play;
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null)
            {
                return;
            }

            UnbindVectorGraphicsEvents();

            if (_vectorGraphics == null || _alwaysRecreateDiagram)
            {
                _converter = new PatchViewModelToDiagramConverter(
                    SystemInformation.DoubleClickTime,
                    SystemInformation.DoubleClickSize.Width);

                _vectorGraphics = _converter.Execute(ViewModel.Entity);
            }
            else
            {
                _vectorGraphics = _converter.Execute(ViewModel.Entity, _vectorGraphics);
            }

            BindVectorGraphicsEvents();

            diagramControl1.Diagram = _vectorGraphics.Diagram;

            ApplyOperatorToolboxItemsViewModel(ViewModel.OperatorToolboxItems);
        }

        private void BindVectorGraphicsEvents()
        {
            _vectorGraphics.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
            _vectorGraphics.MoveGesture.Moving += MoveGesture_Moving;
            _vectorGraphics.MoveGesture.Moved += MoveGesture_Moved;
            _vectorGraphics.DropLineGesture.Dropped += DropLineGesture_Dropped;
            _vectorGraphics.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;
            _vectorGraphics.DoubleClickOperatorGesture.DoubleClick += DoubleClickOperatorGesture_DoubleClick;
            _vectorGraphics.OperatorEnterKeyGesture.EnterKeyPressed += OperatorEnterKeyGesture_EnterKeyPressed;

            if (_vectorGraphics.InletToolTipGesture != null)
            {
                _vectorGraphics.InletToolTipGesture.ToolTipTextRequested += InletToolTipGesture_ToolTipTextRequested;
            }
            if (_vectorGraphics.OutletToolTipGesture != null)
            {
                _vectorGraphics.OutletToolTipGesture.ToolTipTextRequested += OutletToolTipGesture_ToolTipTextRequested;
            }
        }

        private void UnbindVectorGraphicsEvents()
        {
            if (_vectorGraphics != null)
            {
                _vectorGraphics.SelectOperatorGesture.OperatorSelected -= SelectOperatorGesture_OperatorSelected;
                _vectorGraphics.MoveGesture.Moving -= MoveGesture_Moving;
                _vectorGraphics.MoveGesture.Moved -= MoveGesture_Moved;
                _vectorGraphics.DropLineGesture.Dropped -= DropLineGesture_Dropped;
                _vectorGraphics.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;
                _vectorGraphics.DoubleClickOperatorGesture.DoubleClick -= DoubleClickOperatorGesture_DoubleClick;
                _vectorGraphics.OperatorEnterKeyGesture.EnterKeyPressed -= OperatorEnterKeyGesture_EnterKeyPressed;
                
                if (_vectorGraphics.InletToolTipGesture != null)
                {
                    _vectorGraphics.InletToolTipGesture.ToolTipTextRequested -= InletToolTipGesture_ToolTipTextRequested;
                }
                if (_vectorGraphics.OutletToolTipGesture != null)
                {
                    _vectorGraphics.OutletToolTipGesture.ToolTipTextRequested -= OutletToolTipGesture_ToolTipTextRequested;
                }
            }
        }

        private static Size _defaultToolStripLabelSize = new Size(86, 22);

        private bool _operatorToolboxItemsViewModelIsApplied = false; // Dirty way to only apply it once.

        private void ApplyOperatorToolboxItemsViewModel(IList<OperatorTypeViewModel> operatorTypeToolboxItems)
        {
            if (_operatorToolboxItemsViewModelIsApplied)
            {
                return;
            }
            _operatorToolboxItemsViewModelIsApplied = true;

            int i = 1;

            foreach (OperatorTypeViewModel operatorTypeToolboxItem in operatorTypeToolboxItems)
            {
                ToolStripItem toolStripItem = new ToolStripButton
                {
                    Name = "toolStripButton" + i,
                    Size = _defaultToolStripLabelSize,
                    Text = operatorTypeToolboxItem.DisplayName,
                    DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text,
                    Tag = operatorTypeToolboxItem.ID
                };

                // TODO: Clean up the event handlers too somewhere.
                toolStripItem.Click += toolStripLabel_Click;

                toolStrip1.Items.Add(toolStripItem);

                i++;
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void DropLineGesture_Dropped(object sender, DroppedEventArgs e)
        {
            int inletID =  VectorGraphicsTagHelper.GetInletID(e.DroppedOnElement.Tag);
            int outletID = VectorGraphicsTagHelper.GetOutletID(e.DraggedElement.Tag);

            if (ChangeInputOutletRequested != null)
            {
                var e2 = new ChangeInputOutletEventArgs(inletID, outletID);
                ChangeInputOutletRequested(this, e2);
            }
        }

        private void MoveGesture_Moving(object sender, ElementEventArgs e)
        {
            //int operatorIndexNumber = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            //float centerX = e.Element.AbsoluteX + e.Element.Width / 2f;
            //float centerY = e.Element.AbsoluteY + e.Element.Height / 2f;

            //MoveOperator(operatorIndexNumber, centerX, centerY);

            //throw new NotImplementedException();
        }

        private void MoveGesture_Moved(object sender, ElementEventArgs e)
        {
            int operatorIndexNumber = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            float centerX = e.Element.Scaling.AbsoluteX + e.Element.Width / 2f;
            float centerY = e.Element.Scaling.AbsoluteY + e.Element.Height / 2f;

            MoveOperator(operatorIndexNumber, centerX, centerY);
        }

        private void MoveOperator(int operatorID, float centerX, float centerY)
        {
            if (MoveOperatorRequested != null)
            {
                var e = new MoveEntityEventArgs(operatorID, centerX, centerY);
                MoveOperatorRequested(this, e);
            }
        }

        private void toolStripLabel_Click(object sender, EventArgs e)
        {
            ToolStripItem control = (ToolStripItem)sender;
            int operatorTypeID = (int)control.Tag;

            if (CreateOperatorRequested != null)
            {
                var e2 = new CreateOperatorEventArgs(operatorTypeID);
                CreateOperatorRequested(this, e2);
            }
        }

        private void SelectOperatorGesture_OperatorSelected(object sender, ElementEventArgs e)
        {
            int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            if (SelectOperatorRequested != null)
            {
                var e2 = new Int32EventArgs(operatorID);
                SelectOperatorRequested(this, e2);
            }
        }

        private void DeleteOperatorGesture_DeleteRequested(object sender, EventArgs e)
        {
            if (DeleteOperatorRequested != null)
            {
                DeleteOperatorRequested(this, EventArgs.Empty);
            }
        }

        private void DoubleClickOperatorGesture_DoubleClick(object sender, ElementEventArgs e)
        {
            int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            if (OperatorPropertiesRequested != null)
            {
                var e2 = new Int32EventArgs(operatorID);
                OperatorPropertiesRequested(this, e2);
            }
        }

        private void OperatorEnterKeyGesture_EnterKeyPressed(object sender, EventArgs e)
        {
            if (SelectedOperatorPropertiesRequested != null)
            {
                SelectedOperatorPropertiesRequested(this, EventArgs.Empty);
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (PlayRequested != null)
            {
                PlayRequested(this, EventArgs.Empty);
            }
        }

        // TODO: Lower priority: You might want to use the presenter for the the following 3 things.

        private void InletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            if (ViewModel == null) return;

            int inletID = VectorGraphicsTagHelper.GetInletID(e.Element.Tag);
            
            InletViewModel inletViewModel = ViewModel.Entity.Operators.SelectMany(x => x.Inlets)
                                                                       .Where(x => x.ID == inletID)
                                                                       .Single();
            e.ToolTipText = inletViewModel.Caption;
        }

        private void OutletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            if (ViewModel == null) return;

            int id = VectorGraphicsTagHelper.GetOutletID(e.Element.Tag);

            OutletViewModel outletViewModel = ViewModel.Entity.Operators.SelectMany(x => x.Outlets)
                                                                         .Where(x => x.ID == id)
                                                                         .Single();
            e.ToolTipText = outletViewModel.Caption;
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void PatchDetailsUserControl_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible)
            {
                if (LoseFocusRequested != null)
                {
                    LoseFocusRequested(this, EventArgs.Empty);
                }
            }
        }

        private const bool DEFAULT_ALWAYS_RECREATE_DIAGRAM = false;

        private static bool GetAlwaysRecreateDiagram()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                return CustomConfigurationManager.GetSection<ConfigurationSection>().AlwaysRecreateDiagram;
            }
            return DEFAULT_ALWAYS_RECREATE_DIAGRAM;
        }
    }

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class PatchDetailsUserControl_NotDesignable : UserControlBase<PatchDetailsViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
