using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurrentInstrumentUserControl : UserControlBase
    {
        public event EventHandler<EventArgs<int>> RemoveRequested;
        public event EventHandler CloseRequested;
        public event EventHandler ShowAutoPatchRequested;

        public CurrentInstrumentUserControl()
        {
            InitializeComponent();
        }

        // Binding

        public new CurrentInstrumentViewModel ViewModel
        {
            get => (CurrentInstrumentViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            IList<CurrentInstrumentItemUserControl> itemUserControls = flowLayoutPanel.Controls.OfType<CurrentInstrumentItemUserControl>().ToArray();

            if (itemUserControls.Count != flowLayoutPanel.Controls.Count)
            {
                throw new NotEqualException(() => itemUserControls.Count, () => flowLayoutPanel.Controls.Count);
            }

            // Update
            int minCount = new[] { itemUserControls.Count, ViewModel.List.Count }.Min();
            for (int i = 0; i < minCount; i++)
            {
                IDAndName itemViewModel = ViewModel.List[i];
                CurrentInstrumentItemUserControl itemUserControl = itemUserControls[i];
                itemUserControl.ViewModel = itemViewModel;
            }

            // Insert
            for (int i = itemUserControls.Count; i < ViewModel.List.Count; i++)
            {
                IDAndName itemViewModel = ViewModel.List[i];
                var itemUserControl = new CurrentInstrumentItemUserControl
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
                CurrentInstrumentItemUserControl itemUserControl = itemUserControls[i];
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

            const int buttonCount = 2;

            int x = Width - StyleHelper.DefaultMargin - (StyleHelper.IconButtonSize + StyleHelper.DefaultSpacing) * buttonCount;
            if (x < 1)
            {
                x = 1;
            }
            flowLayoutPanel.Width = x;

            x += StyleHelper.DefaultSpacing;

            buttonShowAutoPatch.Top = 0;
            buttonShowAutoPatch.Left = x;
            buttonShowAutoPatch.Width = StyleHelper.IconButtonSize;
            buttonShowAutoPatch.Height = StyleHelper.IconButtonSize;

            x += StyleHelper.IconButtonSize;
            x += StyleHelper.DefaultSpacing;

            buttonClose.Top = 0;
            buttonClose.Left = x;
            buttonClose.Width = StyleHelper.IconButtonSize;
            buttonClose.Height = StyleHelper.IconButtonSize;
        }

        private void CurrentInstrumentUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();

        private void ItemUserControl_RemoveRequested(object sender, EventArgs<int> e) => RemoveRequested?.Invoke(sender, e);

        private void buttonClose_Click(object sender, EventArgs e) => CloseRequested?.Invoke(sender, EventArgs.Empty);

        private void buttonShowAutoPatch_Click(object sender, EventArgs e) => ShowAutoPatchRequested?.Invoke(sender, EventArgs.Empty);
    }
}
