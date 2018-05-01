using System;
using System.Drawing;
using System.Windows.Forms;
// ReSharper disable LocalizableElement
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
	public partial class FromTillUserControl : UserControl
	{
		public FromTillUserControl() => InitializeComponent();

		public string Mask
		{
			get => fromMaskedTextBox.Mask;
			set
			{
				fromMaskedTextBox.Mask = value;
				tillMaskedTextBox.Mask = value;
			}
		}

		public string From
		{
			get => fromMaskedTextBox.Text;
			set => fromMaskedTextBox.Text = value;
		}

		public string Till
		{
			get => tillMaskedTextBox.Text;
			set => tillMaskedTextBox.Text = value;
		}

		[Obsolete("Use From and Till instead.", true)]
		public override string Text
		{
			get => throw new NotSupportedException("Use From and Till instead.");
			set => throw new NotSupportedException("Use From and Till instead.");
		}

		private void FromTillUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
		private void FromTillUserControl_Load(object sender, EventArgs e) => PositionControls();

		private void PositionControls()
		{
			int textBoxWidth = (Width - labelDash.Width) / 2;
			if (textBoxWidth < 1) textBoxWidth = 1;

			int height = fromMaskedTextBox.Height;

			int x = 0;

			fromMaskedTextBox.Location = new Point(x, 0);
			fromMaskedTextBox.Size = new Size(textBoxWidth, height);

			x += textBoxWidth;

			labelDash.Location = new Point(x, 0);
			labelDash.Height = height;

			x += labelDash.Width;

			tillMaskedTextBox.Location = new Point(x, 0);
			tillMaskedTextBox.Size = new Size(textBoxWidth, height);

			Height = height;
		}
	}
}