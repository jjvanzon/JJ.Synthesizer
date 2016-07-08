using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class CurrentPatchesUserControl : UserControlBase
    {
        private const int BUTTON_COUNT = 3;

        public event EventHandler<Int32EventArgs> RemoveRequested;
        public event EventHandler CloseRequested;
        public event EventHandler PreviewAutoPatchRequested;
        public event EventHandler PreviewAutoPatchPolyphonicRequested;

        public CurrentPatchesUserControl()
        {
            InitializeComponent();
        }

        // Binding

        private new CurrentPatchesViewModel ViewModel => (CurrentPatchesViewModel)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            buttonPreviewAutoPatchPolyphonic.Visible = ViewModel.CanPreviewAutoPatchPolyphonic;

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

            PositionControls(ViewModel.CanPreviewAutoPatchPolyphonic);
        }

        private void PositionControls(bool buttonPreviewAutoPatchPolyphonicVisible)
        {
            flowLayoutPanel.Top = 0;
            flowLayoutPanel.Left = 0;
            flowLayoutPanel.Height = Height;

            int buttonCount;
            if (buttonPreviewAutoPatchPolyphonicVisible)
            {
                buttonCount = 3;
            }
            else
            {
                buttonCount = 2;
            }

            int x = Width - StyleHelper.DefaultMargin - (StyleHelper.IconButtonSize + StyleHelper.DefaultSpacing) * buttonCount;
            if (x < 1)
            {
                x = 1;
            }
            flowLayoutPanel.Width = x;

            x += StyleHelper.DefaultSpacing;

            buttonPreviewAutoPatch.Top = 0;
            buttonPreviewAutoPatch.Left = x;
            buttonPreviewAutoPatch.Width = StyleHelper.IconButtonSize;
            buttonPreviewAutoPatch.Height = StyleHelper.IconButtonSize;

            x += StyleHelper.IconButtonSize;
            x += StyleHelper.DefaultSpacing;

            if (buttonPreviewAutoPatchPolyphonicVisible)
            {
                buttonPreviewAutoPatchPolyphonic.Top = 0;
                buttonPreviewAutoPatchPolyphonic.Left = x;
                buttonPreviewAutoPatchPolyphonic.Width = StyleHelper.IconButtonSize;
                buttonPreviewAutoPatchPolyphonic.Height = StyleHelper.IconButtonSize;

                x += StyleHelper.IconButtonSize;
                x += StyleHelper.DefaultSpacing;
            }

            buttonClose.Top = 0;
            buttonClose.Left = x;
            buttonClose.Width = StyleHelper.IconButtonSize;
            buttonClose.Height = StyleHelper.IconButtonSize;
        }

        private void CurrentPatchesUserControl_SizeChanged(object sender, EventArgs e)
        {
            bool buttonPreviewAutoPatchPolyphonicVisible = false;
            if (ViewModel != null)
            {
                buttonPreviewAutoPatchPolyphonicVisible = ViewModel.CanPreviewAutoPatchPolyphonic;
            }

            PositionControls(buttonPreviewAutoPatchPolyphonicVisible);
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
                CloseRequested(sender, EventArgs.Empty);
            }
        }

        private void buttonPreviewAutoPatch_Click(object sender, EventArgs e)
        {
            if (PreviewAutoPatchRequested != null)
            {
                PreviewAutoPatchRequested(sender, EventArgs.Empty);
            }
        }

        private void buttonPreviewAutoPatchPolyphonic_Click(object sender, EventArgs e)
        {
            if (PreviewAutoPatchPolyphonicRequested != null)
            {
                PreviewAutoPatchPolyphonicRequested(sender, EventArgs.Empty);
            }
        }
    }
}
