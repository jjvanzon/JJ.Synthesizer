using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Drawing;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.WinForms.Properties;
using Point = System.Drawing.Point;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class ButtonBarUserControl : UserControl
    {
        private readonly ButtonBarElement _buttonBarElement;

        public ButtonBarUserControl()
        {
            InitializeComponent();

            var diagram = new Diagram();
            diagram.Background.Style.BackStyle.Color = BackColor.ToVectorGrahics();

            ITextMeasurer textMeasurer = new TextMeasurer(diagramControl.CreateGraphics());

            _buttonBarElement = new ButtonBarElement(
                diagram.Background,
                textMeasurer,
                Resources.AddIcon,
                Resources.PianoIconUniformLongerKeys,
                Resources.CloseIcon,
                Resources.RemoveIcon,
                Resources.OpenWindowIcon,
                Resources.NewIcon,
                Resources.PlayIcon,
                Resources.RedoIcon,
                Resources.RefreshIcon,
                Resources.SaveIcon,
                Resources.UndoIcon);

            diagramControl.Location = new Point(0, 0);
            diagramControl.Diagram = diagram;
        }

        [DefaultValue(false)]
        public bool AddButtonVisible
        {
            get => _buttonBarElement.AddButtonVisible;
            set
            {
                _buttonBarElement.AddButtonVisible = value;
                PositionControls();
            }
        }

        public bool AddToInstrumentButtonVisible
        {
            get => _buttonBarElement.AddToInstrumentButtonVisible;
            set
            {
                _buttonBarElement.AddToInstrumentButtonVisible = value;
                PositionControls();
            }
        }

        public bool CloseButtonVisible
        {
            get => _buttonBarElement.CloseButtonVisible;
            set
            {
                _buttonBarElement.CloseButtonVisible = value;
                PositionControls();
            }
        }

        [DefaultValue(false)]
        public bool NewButtonVisible
        {
            get => _buttonBarElement.NewButtonVisible;
            set
            {
                _buttonBarElement.NewButtonVisible = value;
                PositionControls();
            }
        }

        public bool ExpandButtonVisible
        {
            get => _buttonBarElement.ExpandButtonVisible;
            set
            {
                _buttonBarElement.ExpandButtonVisible = value;
                PositionControls();
            }
        }

        public bool PlayButtonVisible
        {
            get => _buttonBarElement.PlayButtonVisible;
            set
            {
                _buttonBarElement.PlayButtonVisible = value;
                PositionControls();
            }
        }

        [DefaultValue(false)]
        public bool RedoButtonVisible
        {
            get => _buttonBarElement.RedoButtonVisible;
            set
            {
                _buttonBarElement.RedoButtonVisible = value;
                PositionControls();
            }
        }

        public bool RefreshButtonVisible
        {
            get => _buttonBarElement.RefreshButtonVisible;
            set
            {
                _buttonBarElement.RefreshButtonVisible = value;
                PositionControls();
            }
        }

        public bool DeleteButtonVisible
        {
            get => _buttonBarElement.DeleteButtonVisible;
            set
            {
                _buttonBarElement.DeleteButtonVisible = value;
                PositionControls();
            }
        }

        public bool SaveButtonVisible
        {
            get => _buttonBarElement.SaveButtonVisible;
            set
            {
                _buttonBarElement.SaveButtonVisible = value;
                PositionControls();
            }
        }

        [DefaultValue(false)]
        public bool UndoButtonVisible
        {
            get => _buttonBarElement.UndoButtonVisible;
            set
            {
                _buttonBarElement.UndoButtonVisible = value;
                PositionControls();
            }
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

        // Positioning

        private void PositionControls()
        {
            diagramControl.Size = new Size(Width, Height);

            if (_buttonBarElement != null)
            {
                _buttonBarElement.PositionElements();
                Width = (int)_buttonBarElement.Position.WidthInPixels;
                Height = (int)_buttonBarElement.Position.HeightInPixels;
            }
        }

        private void ButtonBarUserControl_Resize(object sender, EventArgs e) => PositionControls();
        private void ButtonBarUserControl_Load(object sender, EventArgs e) => PositionControls();

        // Events

        public event EventHandler AddClicked
        {
            add => _buttonBarElement.AddClicked += value;
            remove => _buttonBarElement.AddClicked -= value;
        }

        public event EventHandler AddToInstrumentClicked
        {
            add => _buttonBarElement.AddToInstrumentClicked += value;
            remove => _buttonBarElement.AddToInstrumentClicked -= value;
        }

        public event EventHandler CloseClicked
        {
            add => _buttonBarElement.CloseClicked += value;
            remove => _buttonBarElement.CloseClicked -= value;
        }

        public event EventHandler NewClicked
        {
            add => _buttonBarElement.NewClicked += value;
            remove => _buttonBarElement.NewClicked -= value;
        }

        public event EventHandler ExpandClicked
        {
            add => _buttonBarElement.ExpandClicked += value;
            remove => _buttonBarElement.ExpandClicked -= value;
        }

        public event EventHandler PlayClicked
        {
            add => _buttonBarElement.PlayClicked += value;
            remove => _buttonBarElement.PlayClicked -= value;
        }

        public event EventHandler RedoClicked
        {
            add => _buttonBarElement.RedoClicked += value;
            remove => _buttonBarElement.RedoClicked -= value;
        }

        public event EventHandler RefreshClicked
        {
            add => _buttonBarElement.RefreshClicked += value;
            remove => _buttonBarElement.RefreshClicked -= value;
        }

        public event EventHandler DeleteClicked
        {
            add => _buttonBarElement.DeleteClicked += value;
            remove => _buttonBarElement.DeleteClicked -= value;
        }

        public event EventHandler SaveClicked
        {
            add => _buttonBarElement.SaveClicked += value;
            remove => _buttonBarElement.SaveClicked -= value;
        }

        public event EventHandler UndoClicked
        {
            add => _buttonBarElement.UndoClicked += value;
            remove => _buttonBarElement.UndoClicked -= value;
        }
    }
}