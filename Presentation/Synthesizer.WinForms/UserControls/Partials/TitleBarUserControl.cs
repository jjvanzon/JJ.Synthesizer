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
        private readonly TitleBarElement _titleBarElement;

        public TitleBarUserControl()
        {
            InitializeComponent();

            var diagram = new Diagram();
            diagram.Background.Style.BackStyle.Color = BackColor.ToVectorGraphics();

            ITextMeasurer textMeasurer = new TextMeasurer(diagramControl.CreateGraphics());

            _titleBarElement = new TitleBarElement(
                diagram.Background,
                textMeasurer,
                UnderlyingPictureHelper.UnderlyingPictureWrapper
            );

            diagramControl.Location = new Point(0, 0);
            diagramControl.Diagram = diagram;
        }

        public override string Text
        {
            get => _titleBarElement.Text;
            set => _titleBarElement.Text = value;
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;

                if (diagramControl.Diagram != null)
                {
                    diagramControl.Diagram.Background.Style.BackStyle.Color = value.ToVectorGraphics();
                }
            }
        }

        public bool AddButtonVisible
        {
            get => _titleBarElement.AddButtonVisible;
            set
            {
                _titleBarElement.AddButtonVisible = value;
                PositionControls();
            }
        }

        public bool AddToInstrumentButtonVisible
        {
            get => _titleBarElement.AddToInstrumentButtonVisible;
            set
            {
                _titleBarElement.AddToInstrumentButtonVisible = value;
                PositionControls();
            }
        }

        public bool BrowseButtonVisible
        {
            get => _titleBarElement.BrowseButtonVisible;
            set
            {
                _titleBarElement.BrowseButtonVisible = value;
                PositionControls();
            }
        }

        public bool CloneButtonVisible
        {
            get => _titleBarElement.CloneButtonVisible;
            set
            {
                _titleBarElement.CloneButtonVisible = value;
                PositionControls();
            }
        }

        public bool CloseButtonVisible
        {
            get => _titleBarElement.CloseButtonVisible;
            set
            {
                _titleBarElement.CloseButtonVisible = value;
                PositionControls();
            }
        }

        public bool DeleteButtonVisible
        {
            get => _titleBarElement.DeleteButtonVisible;
            set
            {
                _titleBarElement.DeleteButtonVisible = value;
                PositionControls();
            }
        }

        public bool ExpandButtonVisible
        {
            get => _titleBarElement.ExpandButtonVisible;
            set
            {
                _titleBarElement.ExpandButtonVisible = value;
                PositionControls();
            }
        }

        [DefaultValue(false)]
        public bool NewButtonVisible
        {
            get => _titleBarElement.NewButtonVisible;
            set
            {
                _titleBarElement.NewButtonVisible = value;
                PositionControls();
            }
        }

        public bool PlayButtonVisible
        {
            get => _titleBarElement.PlayButtonVisible;
            set
            {
                _titleBarElement.PlayButtonVisible = value;
                PositionControls();
            }
        }

        public bool RedoButtonVisible
        {
            get => _titleBarElement.RedoButtonVisible;
            set
            {
                _titleBarElement.RedoButtonVisible = value;
                PositionControls();
            }
        }

        public bool RefreshButtonVisible
        {
            get => _titleBarElement.RefreshButtonVisible;
            set
            {
                _titleBarElement.RefreshButtonVisible = value;
                PositionControls();
            }
        }

        public bool RenameButtonVisible
        {
            get => _titleBarElement.RenameButtonVisible;
            set
            {
                _titleBarElement.RenameButtonVisible = value;
                PositionControls();
            }
        }

        public bool SaveButtonVisible
        {
            get => _titleBarElement.SaveButtonVisible;
            set
            {
                _titleBarElement.SaveButtonVisible = value;
                PositionControls();
            }
        }

        public bool TreeStructureButtonVisible
        {
            get => _titleBarElement.TreeStructureButtonVisible;
            set
            {
                _titleBarElement.TreeStructureButtonVisible = value;
                PositionControls();
            }
        }

        public bool UndoButtonVisible
        {
            get => _titleBarElement.UndoButtonVisible;
            set
            {
                _titleBarElement.UndoButtonVisible = value;
                PositionControls();
            }
        }

        public int ButtonBarWidth => (int)_titleBarElement.ButtonBarWidth;

        // Positioning

        private void PositionControls()
        {
            diagramControl.Width = Width;

            if (_titleBarElement != null)
            {
                _titleBarElement.Position.Width = Width;
                _titleBarElement?.PositionElements();
                Height = (int)_titleBarElement.Position.Height;
            }

            diagramControl.Height = Height;
            diagramControl.Refresh();
        }

        private void TitleBarUserControl_Load(object sender, EventArgs e) => PositionControls();
        private void TitleBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();

        // Events

        public event EventHandler AddClicked
        {
            add => _titleBarElement.AddClicked += value;
            remove => _titleBarElement.AddClicked -= value;
        }

        public event EventHandler AddToInstrumentClicked
        {
            add => _titleBarElement.AddToInstrumentClicked += value;
            remove => _titleBarElement.AddToInstrumentClicked -= value;
        }

        public event EventHandler BrowseClicked
        {
            add => _titleBarElement.BrowseClicked += value;
            remove => _titleBarElement.BrowseClicked -= value;
        }

        public event EventHandler CloneClicked
        {
            add => _titleBarElement.CloneClicked += value;
            remove => _titleBarElement.CloneClicked -= value;
        }

        public event EventHandler CloseClicked
        {
            add => _titleBarElement.CloseClicked += value;
            remove => _titleBarElement.CloseClicked -= value;
        }

        public event EventHandler DeleteClicked
        {
            add => _titleBarElement.DeleteClicked += value;
            remove => _titleBarElement.DeleteClicked -= value;
        }

        public event EventHandler ExpandClicked
        {
            add => _titleBarElement.ExpandClicked += value;
            remove => _titleBarElement.ExpandClicked -= value;
        }

        public event EventHandler NewClicked
        {
            add => _titleBarElement.NewClicked += value;
            remove => _titleBarElement.NewClicked -= value;
        }

        public event EventHandler PlayClicked
        {
            add => _titleBarElement.PlayClicked += value;
            remove => _titleBarElement.PlayClicked -= value;
        }

        public event EventHandler RedoClicked
        {
            add => _titleBarElement.RedoClicked += value;
            remove => _titleBarElement.RedoClicked -= value;
        }

        public event EventHandler RefreshClicked
        {
            add => _titleBarElement.RefreshClicked += value;
            remove => _titleBarElement.RefreshClicked -= value;
        }

        public event EventHandler RenameClicked
        {
            add => _titleBarElement.RenameClicked += value;
            remove => _titleBarElement.RenameClicked -= value;
        }

        public event EventHandler SaveClicked
        {
            add => _titleBarElement.SaveClicked += value;
            remove => _titleBarElement.SaveClicked -= value;
        }

        public event EventHandler TreeStructureClicked
        {
            add => _titleBarElement.TreeStructureClicked += value;
            remove => _titleBarElement.TreeStructureClicked -= value;
        }

        public event EventHandler UndoClicked
        {
            add => _titleBarElement.UndoClicked += value;
            remove => _titleBarElement.UndoClicked -= value;
        }
    }
}