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
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Configuration;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchDetailsUserControl : UserControl
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

        private PatchDetailsViewModel _viewModel;
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PatchDetailsViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectDetails(PropertyDisplayNames.Patch);
            buttonPlay.Text = Titles.Play;
        }

        private void ApplyViewModel()
        {
            if (_viewModel == null)
            {
                return;
            }

            UnbindVectorGraphicsEvents();

            if (_vectorGraphics == null || _alwaysRecreateDiagram)
            {
                _converter = new PatchViewModelToDiagramConverter(
                    SystemInformation.DoubleClickTime,
                    SystemInformation.DoubleClickSize.Width);

                _vectorGraphics = _converter.Execute(_viewModel.Entity);
            }
            else
            {
                _vectorGraphics = _converter.Execute(_viewModel.Entity, _vectorGraphics);
            }

            BindVectorGraphicsEvents();

            diagramControl1.Diagram = _vectorGraphics.Diagram;

            ApplyOperatorToolboxItemsViewModel(_viewModel.OperatorToolboxItems);
        }

        private void BindVectorGraphicsEvents()
        {
            _vectorGraphics.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
            _vectorGraphics.MoveGesture.Moved += MoveGesture_Moved;
            _vectorGraphics.DropLineGesture.Dropped += DropLineGesture_Dropped;
            _vectorGraphics.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;
            _vectorGraphics.DoubleClickOperatorGesture.DoubleClick += DoubleClickOperatorGesture_DoubleClick;

            if (_vectorGraphics.OperatorToolTipGesture != null)
            {
                _vectorGraphics.OperatorToolTipGesture.ToolTipTextRequested += OperatorToolTipGesture_ShowToolTipRequested;
            }
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
                _vectorGraphics.MoveGesture.Moved -= MoveGesture_Moved;
                _vectorGraphics.DropLineGesture.Dropped -= DropLineGesture_Dropped;
                _vectorGraphics.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;
                _vectorGraphics.DoubleClickOperatorGesture.DoubleClick -= DoubleClickOperatorGesture_DoubleClick;

                if (_vectorGraphics.OperatorToolTipGesture != null)
                {
                    _vectorGraphics.OperatorToolTipGesture.ToolTipTextRequested -= OperatorToolTipGesture_ShowToolTipRequested;
                }
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

        private void LoseFocus()
        {
            if (LoseFocusRequested != null)
            {
                LoseFocusRequested(this, EventArgs.Empty);
            }
        }

        private void CreateOperator(int operatorTypeID)
        {
            if (CreateOperatorRequested != null)
            {
                var e = new CreateOperatorEventArgs(operatorTypeID);
                CreateOperatorRequested(this, e);
            }
        }

        private void MoveOperator(int operatorID, float centerX, float centerY)
        {
            if (MoveOperatorRequested != null)
            {
                var e = new MoveEntityEventArgs(operatorID, centerX, centerY);
                MoveOperatorRequested(this, e);
            }
        }

        private void ChangeInputOutlet(int inletID, int inputOutletID)
        {
            if (ChangeInputOutletRequested != null)
            {
                var e = new ChangeInputOutletEventArgs(inletID, inputOutletID);
                ChangeInputOutletRequested(this, e);
            }
        }

        private void SelectOperator(int operatorID)
        {
            if (SelectOperatorRequested != null)
            {
                var e = new Int32EventArgs(operatorID);
                SelectOperatorRequested(this, e);
            }
        }

        private void DeleteOperator()
        {
            if (DeleteOperatorRequested != null)
            {
                DeleteOperatorRequested(this, EventArgs.Empty);
            }
        }

        private void Play()
        {
            if (PlayRequested != null)
            {
                PlayRequested(this, EventArgs.Empty);
            }
        }

        private void ShowOperatorProperties(int operatorID)
        {
            if (OperatorPropertiesRequested != null)
            {
                var e = new Int32EventArgs(operatorID);
                OperatorPropertiesRequested(this, e);
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

            ChangeInputOutlet(inletID, outletID);
        }

        private void MoveGesture_Moved(object sender, ElementEventArgs e)
        {
            int operatorIndexNumber = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            float centerX = e.Element.AbsoluteX + e.Element.Width / 2f;
            float centerY = e.Element.AbsoluteY + e.Element.Height / 2f;

            MoveOperator(operatorIndexNumber, centerX, centerY);
        }

        private void toolStripLabel_Click(object sender, EventArgs e)
        {
            ToolStripItem control = (ToolStripItem)sender;
            int operatorTypeID = (int)control.Tag;

            CreateOperator(operatorTypeID);
        }

        private void SelectOperatorGesture_OperatorSelected(object sender, ElementEventArgs e)
        {
            int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);
            SelectOperator(operatorID);
        }

        private void DeleteOperatorGesture_DeleteRequested(object sender, EventArgs e)
        {
            DeleteOperator();
        }

        private void DoubleClickOperatorGesture_DoubleClick(object sender, ElementEventArgs e)
        {
            int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);
            ShowOperatorProperties(operatorID);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        // TODO: Lower priority: You might want to use the presenter for the the following 3 things.

        private void OperatorToolTipGesture_ShowToolTipRequested(object sender, ToolTipTextEventArgs e)
        {
            if (_viewModel == null) return;

            int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            e.ToolTipText = _viewModel.Entity.Operators.Where(x => x.ID == operatorID).Single().Caption;
        }

        private void InletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            if (_viewModel == null) return;

            int inletID = VectorGraphicsTagHelper.GetInletID(e.Element.Tag);

            InletViewModel inketViewModel = _viewModel.Entity.Operators.SelectMany(x => x.Inlets)
                                                                       .Where(x => x.ID == inletID)
                                                                       .Single();
            e.ToolTipText = inketViewModel.Name;
        }

        private void OutletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            if (_viewModel == null) return;

            int id = VectorGraphicsTagHelper.GetOutletID(e.Element.Tag);

            OutletViewModel outletViewModel = _viewModel.Entity.Operators.SelectMany(x => x.Outlets)
                                                                         .Where(x => x.ID == id)
                                                                         .Single();
            e.ToolTipText = outletViewModel.Name;
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void PatchDetailsUserControl_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible)
            {
                LoseFocus();
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
}
