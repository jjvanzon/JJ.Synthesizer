using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Presentation.Synthesizer.Svg;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using System.ComponentModel;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.WinForms;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

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
        private ViewModelToDiagramConverterResult _svg;
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
                if (value == null) throw new NullException(() => value);
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

        // TODO: This field is not used, but I think it might need to be used when finishing up work to the vector graphics system?
        private bool _applyViewModelIsBusy;

        private void ApplyViewModel()
        {
            try
            {
                _applyViewModelIsBusy = true;

                if (_svg == null || _alwaysRecreateDiagram)
                {
                    UnbindSvgEvents();

                    _converter = new ViewModelToDiagramConverter(_mustShowInvisibleElements, _toolTipFeatureEnabled);
                    _svg = _converter.Execute(_viewModel.Entity);

                    _svg.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
                    _svg.MoveGesture.Moved += MoveGesture_Moved;
                    _svg.DropGesture.Dropped += DropGesture_Dropped;
                    _svg.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;

                    if (_toolTipFeatureEnabled)
                    {
                        _svg.OperatorToolTipGesture.ToolTipTextRequested += OperatorToolTipGesture_ShowToolTipRequested;
                        _svg.InletToolTipGesture.ToolTipTextRequested += InletToolTipGesture_ToolTipTextRequested;
                        _svg.OutletToolTipGesture.ToolTipTextRequested += OutletToolTipGesture_ToolTipTextRequested;
                    }

                    //_svg.LineGesture.Dropped += DropGesture_Dropped;
                }
                else
                {
                    _svg = _converter.Execute(_viewModel.Entity, _svg);
                }

                diagramControl1.Diagram = _svg.Diagram;

                ApplyOperatorToolboxItemsViewModel(_viewModel.OperatorToolboxItems);
            }
            finally
            {
                _applyViewModelIsBusy = false;
            }
        }

        private void UnbindSvgEvents()
        {
            if (_svg != null)
            {
                _svg.SelectOperatorGesture.OperatorSelected -= SelectOperatorGesture_OperatorSelected;
                _svg.MoveGesture.Moved -= MoveGesture_Moved;
                _svg.DropGesture.Dropped -= DropGesture_Dropped;
                _svg.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;

                if (_toolTipFeatureEnabled)
                {
                    _svg.OperatorToolTipGesture.ToolTipTextRequested -= OperatorToolTipGesture_ShowToolTipRequested;
                    _svg.InletToolTipGesture.ToolTipTextRequested -= InletToolTipGesture_ToolTipTextRequested;
                    _svg.OutletToolTipGesture.ToolTipTextRequested -= OutletToolTipGesture_ToolTipTextRequested;
                }

                //_svg.LineGesture.Dropped -= DropGesture_Dropped;
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
            int inletID =  SvgTagHelper.GetInletID(e.DroppedOnElement.Tag);
            int outletID = SvgTagHelper.GetOutletID(e.DraggedElement.Tag);

            ChangeInputOutlet(inletID, outletID);
        }

        private void MoveGesture_Moved(object sender, MoveEventArgs e)
        {
            int operatorIndexNumber = SvgTagHelper.GetOperatorID(e.Element.Tag);

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
            int operatorID = SvgTagHelper.GetOperatorID(e.Element.Tag);

            SelectOperator(operatorID);

            // TODO: Low Priority: Program double click gesture in Vector Graphics system,
            // and respond to double click instead.
            ShowOperatorProperties(operatorID);
        }

        private void DeleteOperatorGesture_DeleteRequested(object sender, EventArgs e)
        {
            DeleteOperator();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        // TODO: Lower priority: You might want to use the presenter for the the following 3 things.

        private void OperatorToolTipGesture_ShowToolTipRequested(object sender, ToolTipTextEventArgs e)
        {
            int operatorID = SvgTagHelper.GetOperatorID(e.Element.Tag);

            e.ToolTipText = _viewModel.Entity.Operators.Where(x => x.ID == operatorID).Single().Caption;
        }

        private void InletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            int inletID = SvgTagHelper.GetInletID(e.Element.Tag);

            InletViewModel inketViewModel = _viewModel.Entity.Operators.SelectMany(x => x.Inlets)
                                                                       .Where(x => x.ID == inletID)
                                                                       .Single();
            e.ToolTipText = inketViewModel.Name;
        }

        private void OutletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            int id = SvgTagHelper.GetOutletID(e.Element.Tag);

            OutletViewModel outletViewModel = _viewModel.Entity.Operators.SelectMany(x => x.Outlets)
                                                                         .Where(x => x.ID == id)
                                                                         .Single();
            e.ToolTipText = outletViewModel.Name;
        }
    }
}
