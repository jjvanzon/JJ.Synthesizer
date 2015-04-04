using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Svg.Enums;
using JJ.Framework.Presentation.Svg.Helpers;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Presentation.Svg.Models.Styling;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.Svg.Positioners;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    public class ViewModelToDiagramConverter
    {
        public class Result
        {
            public Result(Diagram diagram, DragDropGesture dragGesture, DragDropGesture dropGesture)
            {
                Diagram = diagram;
                DragGesture = dragGesture;
                DropGesture = dropGesture;
            }

            public Diagram Diagram { get; private set; }
            public DragDropGesture DragGesture { get; private set; }
            public DragDropGesture DropGesture { get; private set; }
        }

        private const float DEFAULT_WIDTH = 125;
        private const float DEFAULT_HEIGHT = 60;

        private static Font _defaultFont;
        private static TextStyle _defaultTextStyle;
        private static BackStyle _defaultBackStyle;
        private static LineStyle _defaultLineStyle;
        private static PointStyle _invisiblePointStyle;
        private static BackStyle _invisibleBackStyle;
        private static LineStyle _invisibleLineStyle;

        // TODO: In the fugure drag gesture and drop gesture should be two different gesture types.
        private DragDropGesture _dragGesture;
        private DragDropGesture _dropGesture;

        private Dictionary<OperatorViewModel, Rectangle> _dictionary;

        static ViewModelToDiagramConverter()
        {
            _defaultBackStyle = new BackStyle
            {
                Visible = true,
                Color = ColorHelper.GetColor(220, 220, 220)
            };

            _defaultLineStyle = new LineStyle
            {
                Width = 2,
                Color = ColorHelper.GetColor(45, 45, 45)
            };

            _defaultFont = new Font
            {
                Bold = true,
                Name = "Verdana",
                Size = 13,
            };

            _defaultTextStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
                Font = _defaultFont,
                Color = ColorHelper.GetColor(20, 20, 20)
            };

            _invisiblePointStyle = new PointStyle
            {
                Visible = false,
            };

            _invisibleBackStyle = new BackStyle
            {
                Visible = false 
            };

            _invisibleLineStyle = new LineStyle
            {
                 Visible = false 
            };

            // Temporary (2015-04-02) for debugging: Show invisible elements.
            _invisiblePointStyle.Visible = true;
            _invisiblePointStyle.Color = ColorHelper.White;
            _invisiblePointStyle.Width = 15;

            _invisibleBackStyle.Visible = true;
            _invisibleBackStyle.Color = ColorHelper.GetColor(64, 40, 128, 192);

            _invisibleLineStyle.Visible = true;
            _invisibleLineStyle.Color = ColorHelper.GetColor(128, 40, 128, 192);
            _invisibleLineStyle.Width = 2;
            _invisibleLineStyle.DashStyleEnum = DashStyleEnum.Dotted;
        }

        public Result Execute(PatchViewModel patchViewModel)
        {
            if (patchViewModel == null) throw new NullException(() => patchViewModel);

            _dictionary = new Dictionary<OperatorViewModel, Rectangle>();

            var diagram = new Diagram();
            _dragGesture = new DragDropGesture();
            _dropGesture = new DragDropGesture();

            foreach (OperatorViewModel operatorViewModel in patchViewModel.Operators)
            {
                Rectangle rectangle = ConvertToRectanglesAndLinesRecursive(operatorViewModel, diagram);
            }

            return new Result(diagram, _dragGesture, _dropGesture);
        }

        private Rectangle ConvertToRectanglesAndLinesRecursive(OperatorViewModel operatorViewModel, Diagram diagram)
        {
            Rectangle rectangle1;
            if (_dictionary.TryGetValue(operatorViewModel, out rectangle1))
            {
                return rectangle1;
            }

            rectangle1 = ConvertToRectangleWithRelatedEntities(operatorViewModel, diagram);

            _dictionary.Add(operatorViewModel, rectangle1);

            foreach (InletViewModel inlet in operatorViewModel.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    // Recursive call
                    Rectangle rectangle2 = ConvertToRectanglesAndLinesRecursive(inlet.InputOutlet.Operator, diagram);

                    Line line = CreateLine();
                    line.Diagram = diagram;
                    line.PointA = GetCenterPointFromRectangle(rectangle1);
                    line.PointB = GetCenterPointFromRectangle(rectangle2);
                }
            }

            return rectangle1;
        }

        private Rectangle ConvertToRectangleWithRelatedEntities(OperatorViewModel operatorViewModel, Diagram diagram)
        {
            Rectangle rectangle = ConvertToRectangle(operatorViewModel);
            rectangle.Diagram = diagram;

            Point point = ConvertToPoint(operatorViewModel);
            point.Diagram = diagram;
            point.Parent = rectangle;

            Label label = ConvertToLabel(operatorViewModel);
            label.Diagram = diagram;
            label.Parent = rectangle;

            OperatorRegionsPositioner.Result positionerResult = OperatorRegionsPositioner.Execute(rectangle, operatorViewModel.Inlets.Count, operatorViewModel.Outlets.Count);

            IEnumerable<Rectangle> rectangles = Enumerable.Union(positionerResult.InletRectangles,
                                                                 positionerResult.OutletRectangles);
            foreach (Rectangle rectangle2 in rectangles)
            {
                rectangle2.Diagram = diagram;
                rectangle2.Parent = rectangle;
                rectangle2.BackStyle = _invisibleBackStyle;
                rectangle2.LineStyle = _invisibleLineStyle;
                rectangle2.Bubble = false;
            }

            rectangle.Gestures.Add(new MoveGesture());

            positionerResult.OutletRectangles.ForEach(x => x.Gestures.Add(_dragGesture));

            positionerResult.InletRectangles.ForEach(x => x.Gestures.Add(_dropGesture));

            return rectangle;
        }

        private Rectangle ConvertToRectangle(OperatorViewModel operatorViewModel)
        {
            var rectangle = new Rectangle
            {
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT,
                BackStyle = _defaultBackStyle,
                LineStyle = _defaultLineStyle,
                X = operatorViewModel.CenterX - DEFAULT_WIDTH / 2f,
                Y = operatorViewModel.CenterY - DEFAULT_HEIGHT / 2f
            };

            return rectangle;
        }

        private Label ConvertToLabel(OperatorViewModel operatorViewModel)
        {
            var label = new Label
            {
                Text = operatorViewModel.Name,
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT,
                TextStyle = _defaultTextStyle
            };

            return label;
        }

        private Point ConvertToPoint(OperatorViewModel operatorViewModel)
        {
            var point = new Point
            {
                X = DEFAULT_WIDTH / 2f,
                Y = DEFAULT_HEIGHT / 2f,
                PointStyle = _invisiblePointStyle,
            };

            return point;
        }

        private Line CreateLine()
        {
            var line = new Line
            {
                LineStyle = _defaultLineStyle,
                ZIndex = -1
            };

            return line;
        }

        private Point GetCenterPointFromRectangle(Rectangle rectangle)
        {
            return rectangle.Children.OfType<Point>().Single();
        }
    }
}
