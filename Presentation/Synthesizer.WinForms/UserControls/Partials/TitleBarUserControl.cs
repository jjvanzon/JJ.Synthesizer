using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Drawing;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using Point = System.Drawing.Point;

// ReSharper disable VirtualMemberCallInConstructor

// ReSharper disable UnusedMember.Global

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class TitleBarUserControl : UserControl
    {
        public TitleBarElement TitleBarElement { get; }

        public TitleBarUserControl()
        {
            InitializeComponent();

            var diagram = new Diagram();
            diagram.Background.Style.BackStyle.Color = BackColor.ToVectorGrahics();

            ITextMeasurer textMeasurer = new TextMeasurer(diagramControl.CreateGraphics());

            TitleBarElement = new TitleBarElement(
                diagram.Background,
                textMeasurer,
                UnderlyingPictureHelper.UnderlyingPictureWrapper
            );

            diagramControl.Location = new Point(0, 0);
            diagramControl.Diagram = diagram;
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;

                if (diagramControl.Diagram != null)
                {
                    diagramControl.Diagram.Background.Style.BackStyle.Color = value.ToVectorGrahics();
                }
            }
        }

        public int ButtonBarWidth => (int)TitleBarElement.ButtonBarElement.Position.Width;

        // Positioning

        private void PositionControls()
        {
            diagramControl.Width = Width;

            if (TitleBarElement != null)
            {
                TitleBarElement.Position.Width = Width;
                TitleBarElement?.PositionElements();
                Height = (int)TitleBarElement.Position.Height;
            }

            diagramControl.Height = Height;
            diagramControl.Refresh();
        }

        private void TitleBarUserControl_Load(object sender, EventArgs e) => PositionControls();
        private void TitleBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
    }
}