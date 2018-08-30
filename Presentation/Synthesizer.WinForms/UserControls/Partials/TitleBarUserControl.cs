using System;
using System.ComponentModel;
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

        public override string Text
        {
            get => TitleBarElement.Text;
            set => TitleBarElement.Text = value;
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

        public bool AddButtonVisible
        {
            get => TitleBarElement.AddButtonVisible;
            set
            {
                TitleBarElement.AddButtonVisible = value;
                PositionControls();
            }
        }

        public bool AddToInstrumentButtonVisible
        {
            get => TitleBarElement.AddToInstrumentButtonVisible;
            set
            {
                TitleBarElement.AddToInstrumentButtonVisible = value;
                PositionControls();
            }
        }

        public bool BrowseButtonVisible
        {
            get => TitleBarElement.BrowseButtonVisible;
            set
            {
                TitleBarElement.BrowseButtonVisible = value;
                PositionControls();
            }
        }

        public bool CloneButtonVisible
        {
            get => TitleBarElement.CloneButtonVisible;
            set
            {
                TitleBarElement.CloneButtonVisible = value;
                PositionControls();
            }
        }

        public bool CloseButtonVisible
        {
            get => TitleBarElement.CloseButtonVisible;
            set
            {
                TitleBarElement.CloseButtonVisible = value;
                PositionControls();
            }
        }

        public bool DeleteButtonVisible
        {
            get => TitleBarElement.DeleteButtonVisible;
            set
            {
                TitleBarElement.DeleteButtonVisible = value;
                PositionControls();
            }
        }

        public bool ExpandButtonVisible
        {
            get => TitleBarElement.ExpandButtonVisible;
            set
            {
                TitleBarElement.ExpandButtonVisible = value;
                PositionControls();
            }
        }

        [DefaultValue(false)]
        public bool NewButtonVisible
        {
            get => TitleBarElement.NewButtonVisible;
            set
            {
                TitleBarElement.NewButtonVisible = value;
                PositionControls();
            }
        }

        public bool PlayButtonVisible
        {
            get => TitleBarElement.PlayButtonVisible;
            set
            {
                TitleBarElement.PlayButtonVisible = value;
                PositionControls();
            }
        }

        public bool RedoButtonVisible
        {
            get => TitleBarElement.RedoButtonVisible;
            set
            {
                TitleBarElement.RedoButtonVisible = value;
                PositionControls();
            }
        }

        public bool RefreshButtonVisible
        {
            get => TitleBarElement.RefreshButtonVisible;
            set
            {
                TitleBarElement.RefreshButtonVisible = value;
                PositionControls();
            }
        }

        public bool RenameButtonVisible
        {
            get => TitleBarElement.RenameButtonVisible;
            set
            {
                TitleBarElement.RenameButtonVisible = value;
                PositionControls();
            }
        }

        public bool SaveButtonVisible
        {
            get => TitleBarElement.SaveButtonVisible;
            set
            {
                TitleBarElement.SaveButtonVisible = value;
                PositionControls();
            }
        }

        public bool TreeStructureButtonVisible
        {
            get => TitleBarElement.TreeStructureButtonVisible;
            set
            {
                TitleBarElement.TreeStructureButtonVisible = value;
                PositionControls();
            }
        }

        public bool UndoButtonVisible
        {
            get => TitleBarElement.UndoButtonVisible;
            set
            {
                TitleBarElement.UndoButtonVisible = value;
                PositionControls();
            }
        }

        public int ButtonBarWidth => (int)TitleBarElement.ButtonBarWidth;

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