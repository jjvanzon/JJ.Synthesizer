using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using System.ComponentModel;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Framework.Presentation.WinForms.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchDetailsUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;
        public event EventHandler DeleteOperatorRequested;
        public event EventHandler<CreateOperatorEventArgs> CreateOperatorRequested;
        public event EventHandler<MoveOperatorEventArgs> MoveOperatorRequested;
        public event EventHandler<ChangeInputOutletEventArgs> ChangeInputOutletRequested;
        public event EventHandler<Int32EventArgs> SelectOperatorRequested;
        public event EventHandler PlayRequested;
        public event EventHandler<Int32EventArgs> OperatorPropertiesRequested;

        private PatchDetailsViewModel _viewModel;
        private ViewModelToDiagramConverter _converter;
        private ViewModelToDiagramConverterResult _vectorGraphics;
        private static bool _alwaysRecreateDiagram;
        private static bool _mustShowInvisibleElements;
        private static bool _toolTipFeatureEnabled;

        // Constructors

        static PatchDetailsUserControl()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                ConfigurationSection config = CustomConfigurationManager.GetSection<ConfigurationSection>();
                _alwaysRecreateDiagram = config.Testing.AlwaysRecreateDiagram;
                _mustShowInvisibleElements = config.Testing.MustShowInvisibleElements;
                _toolTipFeatureEnabled = config.Testing.ToolTipsFeatureEnabled;
            }
        }

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

            if (_vectorGraphics == null || _alwaysRecreateDiagram)
            {
                UnbindVectorGraphicsEvents();

                _converter = new ViewModelToDiagramConverter( 
                    SystemInformation.DoubleClickTime,
                    SystemInformation.DoubleClickSize.Width,
                    _mustShowInvisibleElements, 
                    _toolTipFeatureEnabled);

                _vectorGraphics = _converter.Execute(_viewModel.Entity);

                _vectorGraphics.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
                _vectorGraphics.MoveGesture.Moved += MoveGesture_Moved;
                _vectorGraphics.DropGesture.Dropped += DropGesture_Dropped;
                _vectorGraphics.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;
                _vectorGraphics.DoubleClickOperatorGesture.DoubleClick += DoubleClickOperatorGesture_DoubleClick;

                if (_toolTipFeatureEnabled)
                {
                    _vectorGraphics.OperatorToolTipGesture.ToolTipTextRequested += OperatorToolTipGesture_ShowToolTipRequested;
                    _vectorGraphics.InletToolTipGesture.ToolTipTextRequested += InletToolTipGesture_ToolTipTextRequested;
                    _vectorGraphics.OutletToolTipGesture.ToolTipTextRequested += OutletToolTipGesture_ToolTipTextRequested;
                }

                //_vectorGraphics.LineGesture.Dropped += DropGesture_Dropped;
            }
            else
            {
                _vectorGraphics = _converter.Execute(_viewModel.Entity, _vectorGraphics);
            }

            diagramControl1.Diagram = _vectorGraphics.Diagram;

            ApplyOperatorToolboxItemsViewModel(_viewModel.OperatorToolboxItems);
        }

        private void UnbindVectorGraphicsEvents()
        {
            if (_vectorGraphics != null)
            {
                _vectorGraphics.SelectOperatorGesture.OperatorSelected -= SelectOperatorGesture_OperatorSelected;
                _vectorGraphics.MoveGesture.Moved -= MoveGesture_Moved;
                _vectorGraphics.DropGesture.Dropped -= DropGesture_Dropped;
                _vectorGraphics.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;

                if (_toolTipFeatureEnabled)
                {
                    _vectorGraphics.OperatorToolTipGesture.ToolTipTextRequested -= OperatorToolTipGesture_ShowToolTipRequested;
                    _vectorGraphics.InletToolTipGesture.ToolTipTextRequested -= InletToolTipGesture_ToolTipTextRequested;
                    _vectorGraphics.OutletToolTipGesture.ToolTipTextRequested -= OutletToolTipGesture_ToolTipTextRequested;
                }

                //_vectorGraphics.LineGesture.Dropped -= DropGesture_Dropped;
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

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
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
                var e = new MoveOperatorEventArgs(operatorID, centerX, centerY);
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
            Close();
        }

        private void DropGesture_Dropped(object sender, DroppedEventArgs e)
        {
            int inletID =  VectorGraphicsTagHelper.GetInletID(e.DroppedOnElement.Tag);
            int outletID = VectorGraphicsTagHelper.GetOutletID(e.DraggedElement.Tag);

            ChangeInputOutlet(inletID, outletID);
        }

        private void MoveGesture_Moved(object sender, ElementEventArgs e)
        {
            int operatorIndexNumber = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);

            float centerX = e.Element.X + e.Element.Width / 2f;
            float centerY = e.Element.Y + e.Element.Height / 2f;

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

            // In Progress: Program double click gesture in Vector Graphics system,
            // and respond to double click instead.
            ShowOperatorProperties(operatorID);
        }

        private void DeleteOperatorGesture_DeleteRequested(object sender, EventArgs e)
        {
            DeleteOperator();
        }

        private void DoubleClickOperatorGesture_DoubleClick(object sender, ElementEventArgs e)
        {
            // TODO: This event handler does not work yet, because the whole diagram is regenerated all the time,
            // making the second click be on a completely new element.

            //int operatorID = VectorGraphicsTagHelper.GetOperatorID(e.Element.Tag);
            //ShowOperatorProperties(operatorID);
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
    }
}
