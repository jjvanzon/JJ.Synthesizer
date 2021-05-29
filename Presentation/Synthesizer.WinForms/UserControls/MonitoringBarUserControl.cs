using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Drawing;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.ViewModels;
using Point = System.Drawing.Point;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    public partial class MonitoringBarUserControl : UserControl
    {
        public event EventHandler HeightChanged
        {
            add => _monitoringBarElement.HeightChanged += value;
            remove => _monitoringBarElement.HeightChanged -= value;
        }

        private readonly MonitoringBarElement _monitoringBarElement;

        public MonitoringBarUserControl()
        {
            InitializeComponent();

            var diagram = new Diagram();
            diagram.Background.Style.BackStyle.Color = SystemColors.Control.ToArgb();

            ITextMeasurer textMeasurer = new TextMeasurer(diagramControl.CreateGraphics());

            _monitoringBarElement = new MonitoringBarElement(diagram.Background, textMeasurer);

            diagramControl.Location = new Point(0, 0);
            diagramControl.Diagram = diagram;
        }

        public void PositionControls()
        {
            diagramControl.Size = new Size(Width, Height);

            if (_monitoringBarElement != null)
            {
                _monitoringBarElement.Position.WidthInPixels = Width;
                _monitoringBarElement.PositionElements();
                Height = (int)_monitoringBarElement.Position.HeightInPixels;
            }
        }

        public MonitoringBarViewModel ViewModel
        {
            // ReSharper disable once UnusedMember.Global
            get => _monitoringBarElement.ViewModel;
            set
            {
                _monitoringBarElement.ViewModel = value;
                diagramControl.Refresh();
            }
        }

        private void MonitoringBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
        private void MonitoringBarUserControl_Load(object sender, EventArgs e) => PositionControls();
    }
}