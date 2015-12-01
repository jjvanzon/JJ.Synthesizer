using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurrentPatchesUserControl : UserControl
    {
        public event EventHandler<Int32EventArgs> RemoveRequested;
        public event EventHandler CloseRequested;

        private CurrentPatchesViewModel _viewModel;

        public CurrentPatchesUserControl()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CurrentPatchesViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModel();
            }
        }

        private void ApplyViewModel()
        {
            IList<CurrentPatchItemUserControl> itemUserControls = flowLayoutPanel.Controls.OfType<CurrentPatchItemUserControl>().ToArray();

            if (itemUserControls.Count != flowLayoutPanel.Controls.Count)
            {
                throw new NotEqualException(() => itemUserControls.Count, () => flowLayoutPanel.Controls.Count);
            }

            // Update
            int minCount = new int[] { itemUserControls.Count, ViewModel.List.Count }.Min();
            for (int i = 0; i < minCount; i++)
            {
                CurrentPatchItemViewModel itemViewModel = ViewModel.List[i];
                CurrentPatchItemUserControl itemUserControl = itemUserControls[i];
                itemUserControl.ViewModel = itemViewModel;
            }

            // Insert
            for (int i = itemUserControls.Count; i < ViewModel.List.Count; i++)
            {
                CurrentPatchItemViewModel itemViewModel = ViewModel.List[i];
                var itemUserControl = new CurrentPatchItemUserControl
                {
                    Margin = new Padding(0)
                };
                itemUserControl.RemoveRequested += ItemUserControl_RemoveRequested;
                flowLayoutPanel.Controls.Add(itemUserControl);
                itemUserControl.ViewModel = itemViewModel;
            }

            // Delete
            for (int i = itemUserControls.Count - 1; i >= ViewModel.List.Count; i--)
            {
                CurrentPatchItemUserControl itemUserControl = itemUserControls[i];
                itemUserControl.RemoveRequested -= ItemUserControl_RemoveRequested;
                flowLayoutPanel.Controls.RemoveAt(i);
            }

            PositionControls();
        }

        private void PositionControls()
        {
            flowLayoutPanel.Top = 0;
            flowLayoutPanel.Left = 0;
            flowLayoutPanel.Height = Height;

            int flowLayoutPanelWidth = Width - buttonClose.Width - StyleHelper.DefaultSpacing;
            if (flowLayoutPanelWidth < 1) flowLayoutPanelWidth = 1;
            flowLayoutPanel.Width = Width;

            buttonClose.Top = 0;
            buttonClose.Left = flowLayoutPanelWidth + StyleHelper.DefaultSpacing;
        }

        private void CurrentPatchesUserControl_SizeChanged(object sender, EventArgs e)
        {
            PositionControls();
        }

        private void ItemUserControl_RemoveRequested(object sender, Int32EventArgs e)
        {
            if (RemoveRequested != null)
            {
                RemoveRequested(sender, e);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (CloseRequested != null)
            {
                CloseRequested(sender, e);
            }
        }
    }
}
