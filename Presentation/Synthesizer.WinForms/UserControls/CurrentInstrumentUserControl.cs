using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurrentInstrumentUserControl : UserControlBase
    {
        private readonly IList<CurrentInstrumentItemUserControl> _itemControls = new List<CurrentInstrumentItemUserControl>();

        public event EventHandler<EventArgs<int>> RemoveRequested;
        public event EventHandler ExpandRequested;
        public event EventHandler PlayRequested;

        public CurrentInstrumentUserControl()
        {
            InitializeComponent();
        }

        private void CurrentInstrumentUserControl_Load(object sender, EventArgs e)
        {
            SetTitles();
        }

        public new CurrentInstrumentViewModel ViewModel
        {
            get => (CurrentInstrumentViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        private void SetTitles()
        {
            toolTip.SetToolTip(buttonExpand, CommonResourceFormatter.Open);
            toolTip.SetToolTip(buttonPlay, ResourceFormatter.Play);
        }

        protected override void ApplyViewModelToControls()
        {
            // Update
            int minCount = Math.Min(_itemControls.Count, ViewModel.List.Count);
            for (int i = 0; i < minCount; i++)
            {
                IDAndName itemViewModel = ViewModel.List[i];
                CurrentInstrumentItemUserControl itemUserControl = _itemControls[i];
                itemUserControl.ViewModel = itemViewModel;
            }

            // Insert
            for (int i = _itemControls.Count; i < ViewModel.List.Count; i++)
            {
                IDAndName itemViewModel = ViewModel.List[i];
                var itemControl = new CurrentInstrumentItemUserControl
                {
                    Margin = new Padding(0),
                    ViewModel = itemViewModel
                };
                itemControl.RemoveRequested += ItemControl_RemoveRequested;

                _itemControls.Add(itemControl);
                Controls.Add(itemControl);
            }

            // Delete
            for (int i = _itemControls.Count - 1; i >= ViewModel.List.Count; i--)
            {
                CurrentInstrumentItemUserControl itemControl = _itemControls[i];
                itemControl.RemoveRequested -= ItemControl_RemoveRequested;

                _itemControls.RemoveAt(i);
                Controls.Remove(itemControl);
            }

            PositionControls();
        }

        private void PositionControls()
        {
            int x = Width;

            x -= StyleHelper.IconButtonSize;

            buttonExpand.Top = 0;
            buttonExpand.Left = x;
            buttonExpand.Width = StyleHelper.IconButtonSize;
            buttonExpand.Height = StyleHelper.IconButtonSize;

            x -= StyleHelper.DefaultSpacing;
            x -= StyleHelper.IconButtonSize;

            buttonPlay.Top = 0;
            buttonPlay.Left = x;
            buttonPlay.Width = StyleHelper.IconButtonSize;
            buttonPlay.Height = StyleHelper.IconButtonSize;

            foreach (CurrentInstrumentItemUserControl itemControl in _itemControls.Reverse())
            {
                x -= StyleHelper.DefaultSpacing;
                x -= StyleHelper.DefaultSpacing;
                x -= itemControl.Width;

                itemControl.Top = 0;
                itemControl.Left = x;
                itemControl.Height = Height;
            }
        }

        private void Base_SizeChanged(object sender, EventArgs e) => PositionControls();
        private void ItemControl_RemoveRequested(object sender, EventArgs<int> e) => RemoveRequested?.Invoke(sender, e);
        private void buttonExpand_Click(object sender, EventArgs e) => ExpandRequested?.Invoke(sender, EventArgs.Empty);
        private void buttonPlay_Click(object sender, EventArgs e) => PlayRequested?.Invoke(sender, EventArgs.Empty);
    }
}
