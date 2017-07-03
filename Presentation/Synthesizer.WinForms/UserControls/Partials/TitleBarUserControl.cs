using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class TitleBarUserControl : UserControl
    {
        public TitleBarUserControl()
        {
            InitializeComponent();

            _titleLabelVisible = labelTitle.Visible;
        }

        public event EventHandler AddClicked
        {
            add => buttonBarUserControl.AddClicked += value;
            remove => buttonBarUserControl.AddClicked -= value;
        }

        public event EventHandler CloseClicked
        {
            add => buttonBarUserControl.CloseClicked += value;
            remove => buttonBarUserControl.CloseClicked -= value;
        }

        public event EventHandler NewClicked
        {
            add => buttonBarUserControl.NewClicked += value;
            remove => buttonBarUserControl.NewClicked -= value;
        }

        public event EventHandler OpenClicked
        {
            add => buttonBarUserControl.OpenClicked += value;
            remove => buttonBarUserControl.OpenClicked -= value;
        }

        public event EventHandler PlayClicked
        {
            add => buttonBarUserControl.PlayClicked += value;
            remove => buttonBarUserControl.PlayClicked -= value;
        }

        public event EventHandler RefreshClicked
        {
            add => buttonBarUserControl.RefreshClicked += value;
            remove => buttonBarUserControl.RefreshClicked -= value;
        }

        public event EventHandler RemoveClicked
        {
            add => buttonBarUserControl.RemoveClicked += value;
            remove => buttonBarUserControl.RemoveClicked -= value;
        }

        public event EventHandler SaveClicked
        {
            add => buttonBarUserControl.SaveClicked += value;
            remove => buttonBarUserControl.SaveClicked -= value;
        }

        public override string Text
        {
            get => labelTitle.Text;
            set => labelTitle.Text = value;
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                labelTitle.BackColor = value;
                buttonBarUserControl.BackColor = value;
            }
        }

        public bool AddButtonVisible
        {
            get => buttonBarUserControl.AddButtonVisible;
            set
            {
                buttonBarUserControl.AddButtonVisible = value;
                PositionControls();
            }
        }

        public bool CloseButtonVisible
        {
            get => buttonBarUserControl.CloseButtonVisible;
            set
            {
                buttonBarUserControl.CloseButtonVisible = value;
                PositionControls();
            }
        }

        [DefaultValue(false)]
        public bool NewButtonVisible
        {
            get => buttonBarUserControl.NewButtonVisible;
            set
            {
                buttonBarUserControl.NewButtonVisible = value;
                PositionControls();
            }
        }

        public bool OpenButtonVisible
        {
            get => buttonBarUserControl.OpenButtonVisible;
            set
            {
                buttonBarUserControl.OpenButtonVisible = value;
                PositionControls();
            }
        }

        public bool PlayButtonVisible
        {
            get => buttonBarUserControl.PlayButtonVisible;
            set
            {
                buttonBarUserControl.PlayButtonVisible = value;
                PositionControls();
            }
        }

        public bool RefreshButtonVisible
        {
            get => buttonBarUserControl.RefreshButtonVisible;
            set
            {
                buttonBarUserControl.RefreshButtonVisible = value;
                PositionControls();
            }
        }

        public bool RemoveButtonVisible
        {
            get => buttonBarUserControl.RemoveButtonVisible;
            set
            {
                buttonBarUserControl.RemoveButtonVisible = value;
                PositionControls();
            }
        }

        public bool SaveButtonVisible
        {
            get => buttonBarUserControl.SaveButtonVisible;
            set
            {
                buttonBarUserControl.SaveButtonVisible = value;
                PositionControls();
            }
        }

        private bool _titleLabelVisible;
        public bool TitleLabelVisible
        {
            get => _titleLabelVisible;
            set
            {
                _titleLabelVisible = value;
                labelTitle.Visible = value;
                PositionControls();
            }
        }

        public int ButtonBarWidth => buttonBarUserControl.Width;

        private void PositionControls()
        {
            Height = buttonBarUserControl.Height;

            if (_titleLabelVisible)
            {
                labelTitle.Top = 0;
                labelTitle.Left = 0;
                labelTitle.Height = Height;
                labelTitle.Width = Width - buttonBarUserControl.Width;
            }

            buttonBarUserControl.Top = 0;
            buttonBarUserControl.Left = Width - buttonBarUserControl.Width;
        }

        private void TitleBarUserControl_Load(object sender, EventArgs e) => PositionControls();
        private void TitleBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
    }
}