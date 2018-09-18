using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Drawing;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using Point = System.Drawing.Point;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    public partial class TopBarUserControl : UserControl
    {
        public TopBarElement TopBarElement { get; }

        public TopBarUserControl()
        {
            InitializeComponent();

            var diagram = new Diagram();
            diagram.Background.Style.BackStyle.Color = SystemColors.Control.ToArgb();

            TopBarElement = new TopBarElement(
                diagram.Background,
                UnderlyingPictureHelper.UnderlyingPictureWrapper,
                new TextMeasurer(diagramControl.CreateGraphics()));

            diagramControl.Location = new Point(0, 0);
            diagramControl.Diagram = diagram;
        }

        public void PositionControls()
        {
            diagramControl.Size = new Size(Width, Height);

            if (TopBarElement != null)
            {
                TopBarElement.Position.WidthInPixels = Width;
                TopBarElement.PositionElements();
                Height = (int)TopBarElement.Position.HeightInPixels;
            }
        }

        private void TopBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
        private void TopBarUserControl_Load(object sender, EventArgs e) => PositionControls();
    }
}