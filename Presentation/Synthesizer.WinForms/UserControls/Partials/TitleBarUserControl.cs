using System;
using System.Drawing;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class TitleBarUserControl : UserControl
    {
        public event EventHandler CloseClicked;
        public event EventHandler RemoveClicked;
        public event EventHandler AddClicked;

        public TitleBarUserControl()
        {
            InitializeComponent();
        }

        private void TitleBarUserControl_Load(object sender, EventArgs e)
        {
            PositionControls();
        }

        // Properties

        public override string Text
        {
            get => labelTitle.Text;
            set { labelTitle.Text = value; }
        }

        public bool AddButtonVisible
        {
            get => pictureBoxAdd.Visible;
            set
            {
                if (pictureBoxAdd.Visible == value) return;

                pictureBoxAdd.Visible = value;

                PositionControls();
            }
        }

        public bool RemoveButtonVisible
        {
            get => pictureBoxRemove.Visible;
            set
            {
                if (pictureBoxRemove.Visible == value) return;

                pictureBoxRemove.Visible = value;

                PositionControls();
            }
        }

        public bool CloseButtonVisible
        {
            get => pictureBoxClose.Visible;
            set
            {
                if (pictureBoxClose.Visible == value) return;

                pictureBoxClose.Visible = value;

                PositionControls();
            }
        }

        // Positioning

        // I am fed up with WinForms, so I am doing it myself.
        // If I use a TableLayoutPanel, the spacing of my buttons
        // is screwed up when I set the Form's AutoScaleMode to 'Font' (the default)
        // and then change the font.

        // I also want to make some buttons visible or invisible,
        // which you cannot do with the TableLayoutPanel.
        
        private const int BUTTON_SPACING = 5;
        private const int BUTTON_SIZE = 16;

        private void PositionControls()
        {
            
            int x = Width;

            x -= BUTTON_SPACING;
            x -= BUTTON_SIZE;

            if (CloseButtonVisible)
            {
                pictureBoxClose.Location = new Point(x, BUTTON_SPACING);
                pictureBoxClose.Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                x -= BUTTON_SPACING;
                x -= BUTTON_SIZE;
            }

            if (RemoveButtonVisible)
            {
                pictureBoxRemove.Location = new Point(x, BUTTON_SPACING);
                pictureBoxRemove.Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                x -= BUTTON_SPACING;
                x -= BUTTON_SIZE;
            }

            if (AddButtonVisible)
            {
                pictureBoxAdd.Location = new Point(x, BUTTON_SPACING);
                pictureBoxAdd.Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                x -= BUTTON_SPACING;
                x -= BUTTON_SIZE;
            }

            labelTitle.Location = new Point(0, 0);
            labelTitle.Size = new Size(x, Height);
        }

        private void TitleBarUserControl_Resize(object sender, EventArgs e)
        {
            PositionControls();
        }

        // Events

        private void pictureBoxAdd_MouseDown(object sender, MouseEventArgs e)
        {
            AddClicked?.Invoke(sender, EventArgs.Empty);
        }

        private void pictureBoxRemove_MouseDown(object sender, MouseEventArgs e)
        {
            RemoveClicked?.Invoke(sender, EventArgs.Empty);
        }

        private void pictureBoxClose_MouseDown(object sender, MouseEventArgs e)
        {
            CloseClicked?.Invoke(sender, EventArgs.Empty);
        }
    }
}
