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
using JJ.Presentation.Synthesizer.Svg.Gestures;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    public class ViewModelToDiagramConverter
    {
        public class Result
        {
            public Result(
                Diagram diagram, 
                MoveGesture moveGesture, 
                DragGesture dragGesture, 
                DropGesture dropGesture, 
                LineGesture lineGesture,
                SelectOperatorGesture selectOperatorGesture,
                DeleteOperatorGesture deleteOperatorGesture)
            {
                Diagram = diagram;
                MoveGesture = moveGesture;
                DragGesture = dragGesture;
                DropGesture = dropGesture;
                LineGesture = lineGesture;
                SelectOperatorGesture = selectOperatorGesture;
                DeleteOperatorGesture = deleteOperatorGesture;
            }

            public Diagram Diagram { get; private set; }
            public MoveGesture MoveGesture { get; private set; }
            public DragGesture DragGesture { get; private set; }
            public DropGesture DropGesture { get; private set; }
            public LineGesture LineGesture { get; private set; }
            public SelectOperatorGesture SelectOperatorGesture { get; private set; }
            public DeleteOperatorGesture DeleteOperatorGesture { get; private set; }
        }

        private class OperatorSvgElements
        {
            public Rectangle Rectangle { get; set; }
            public IList<Rectangle> InletRectangles { get; set; }
            public IList<Rectangle> OutletRectangles { get; set; }
            public IList<Point> InletPoints { get; set; }
            public IList<Point> OutletPoints { get; set; }
        }

        private MoveGesture _moveGesture;
        private DragGesture _dragGesture;
        private DropGesture _dropGesture;
        private LineGesture _lineGesture;
        private SelectOperatorGesture _selectOperatorGesture;
        private DeleteOperatorGesture _deleteOperatorGesture;
        private Dictionary<OperatorViewModel, OperatorSvgElements> _dictionary;

        static ViewModelToDiagramConverter()
        {
            InitializeStyling();
        }

        /// <param name="mustShowInvisibleElements">for debugging</param>
        public ViewModelToDiagramConverter(bool mustShowInvisibleElements = false)
        {
            if (mustShowInvisibleElements)
            {
                MakeHiddenStylesVisible();
            }
        }

        public Result Execute(PatchViewModel patchViewModel)
        {
            if (patchViewModel == null) throw new NullException(() => patchViewModel);

            _dictionary = new Dictionary<OperatorViewModel, OperatorSvgElements>();

            var diagram = new Diagram();

            _moveGesture = new MoveGesture();
            _dragGesture = new DragGesture();
            _dropGesture = new DropGesture(_dragGesture);
            _lineGesture = new LineGesture(diagram, _lightLineStyle, lineZIndex: -1);
            _selectOperatorGesture = new SelectOperatorGesture();
            _deleteOperatorGesture = new DeleteOperatorGesture();

            diagram.Canvas.Gestures.Add(_deleteOperatorGesture);

            foreach (OperatorViewModel operatorViewModel in patchViewModel.Operators)
            {
                OperatorSvgElements rectangle = ConvertToRectangles_WithRelatedObject_Recursive(operatorViewModel, diagram);
            }

            return new Result(diagram, _moveGesture, _dragGesture, _dropGesture, _lineGesture, _selectOperatorGesture, _deleteOperatorGesture);
        }

        private OperatorSvgElements ConvertToRectangles_WithRelatedObject_Recursive(OperatorViewModel operatorViewModel, Diagram diagram)
        {
            OperatorSvgElements operatorSvgElements1;
            if (_dictionary.TryGetValue(operatorViewModel, out operatorSvgElements1))
            {
                return operatorSvgElements1;
            }

            operatorSvgElements1 = ConvertToRectangle_WithRelatedObjects(operatorViewModel, diagram);

            _dictionary.Add(operatorViewModel, operatorSvgElements1);

            for (int i = 0; i < operatorViewModel.Inlets.Count; i++)
            {
                InletViewModel inlet = operatorViewModel.Inlets[i];

                if (inlet.InputOutlet != null)
                {
                    // Recursive call
                    OperatorSvgElements operatorSvgElements2 = ConvertToRectangles_WithRelatedObject_Recursive(inlet.InputOutlet.Operator, diagram);

                    Line line = CreateLine();
                    line.Diagram = diagram;
                    line.PointA = operatorSvgElements1.InletPoints[i];

                    // TODO: This does not work for multiple outlets.
                    if (operatorSvgElements2.OutletPoints.Count > 0)
                    {
                        line.PointB = operatorSvgElements2.OutletPoints[0];
                    }
                }
            }

            return operatorSvgElements1;
        }

        private OperatorSvgElements ConvertToRectangle_WithRelatedObjects(OperatorViewModel operatorViewModel, Diagram diagram)
        {
            Rectangle rectangle = ConvertToRectangle(operatorViewModel);
            rectangle.Diagram = diagram;

            Label label = ConvertToLabel(operatorViewModel);
            label.Diagram = diagram;
            label.Parent = rectangle;

            // Add invisible elements to diagram
            OperatorElementsPositioner.Result positionerResult = OperatorElementsPositioner.Execute(
                rectangle, 
                operatorViewModel.Inlets.Count, 
                operatorViewModel.Outlets.Count);

            IEnumerable<Rectangle> inletAndOutletRectangles = Enumerable.Union(positionerResult.InletRectangles,
                                                                               positionerResult.OutletRectangles);

            foreach (Rectangle inletOrOutletRectangle in inletAndOutletRectangles)
            {
                inletOrOutletRectangle.Diagram = diagram;
                inletOrOutletRectangle.Parent = rectangle;
                inletOrOutletRectangle.BackStyle = _invisibleBackStyle;
                inletOrOutletRectangle.LineStyle = _invisibleLineStyle;
            }

            IEnumerable<Point> inletAndOutletPoints = Enumerable.Union(positionerResult.InletPoints,
                                                                       positionerResult.OutletPoints);

            foreach (Point inletOrOutletPoint in inletAndOutletPoints)
            {
                inletOrOutletPoint.Diagram = diagram;
                inletOrOutletPoint.Parent = rectangle;
                inletOrOutletPoint.PointStyle = _pointStyle;
            }

            // Tags
            rectangle.Tag = operatorViewModel.ID;

            for (int i = 0; i < operatorViewModel.Inlets.Count; i++)
            {
                InletViewModel inletViewModel = operatorViewModel.Inlets[i];
                Element inletElement = positionerResult.InletRectangles[i];
                inletElement.Tag = inletViewModel.ID;
            }

            for (int i = 0; i < operatorViewModel.Outlets.Count; i++)
            {
                OutletViewModel outletViewModel = operatorViewModel.Outlets[i];
                Element outletElement = positionerResult.OutletRectangles[i];
                outletElement.Tag = outletViewModel.ID;
            }

            // Gestures
            rectangle.Gestures.Add(_moveGesture);
            rectangle.Gestures.Add(_selectOperatorGesture);

            foreach (Element outletElement in positionerResult.OutletRectangles)
            {
                outletElement.MustBubble = false;
                outletElement.Gestures.Add(_dragGesture);
                outletElement.Gestures.Add(_lineGesture);
            }

            foreach (Element inletElement in positionerResult.InletRectangles)
            {
                inletElement.Gestures.Add(_dropGesture);
            }

            // Return result
            return new OperatorSvgElements
            {
                Rectangle = rectangle,
                InletRectangles = positionerResult.InletRectangles,
                OutletRectangles = positionerResult.OutletRectangles,
                InletPoints = positionerResult.InletPoints,
                OutletPoints = positionerResult.OutletPoints
            };
        }

        private Rectangle ConvertToRectangle(OperatorViewModel operatorViewModel)
        {
            var rectangle = new Rectangle
            {
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT,
                X = operatorViewModel.CenterX - DEFAULT_WIDTH / 2f,
                Y = operatorViewModel.CenterY - DEFAULT_HEIGHT / 2f
            };

            if (operatorViewModel.IsSelected)
            {
                rectangle.BackStyle = _selectedBackStyle;
                rectangle.LineStyle = _selectedLineStyle;
            }
            else
            {
                rectangle.BackStyle = _backStyle;
                rectangle.LineStyle = _lineStyle;
            }

            return rectangle;
        }

        private Label ConvertToLabel(OperatorViewModel operatorViewModel)
        {
            var label = new Label
            {
                Text = operatorViewModel.Name,
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT,
                TextStyle = _textStyle
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
                LineStyle = _lineStyle,
                ZIndex = -1
            };

            return line;
        }

        // Styling

        private const float DEFAULT_WIDTH = 125;
        private const float DEFAULT_HEIGHT = 60;

        private static Font _defaultFont;
        private static TextStyle _textStyle;
        private static BackStyle _backStyle;
        private static LineStyle _lineStyle;
        private static PointStyle _pointStyle;
        private static LineStyle _lightLineStyle;
        private static PointStyle _invisiblePointStyle;
        private static BackStyle _invisibleBackStyle;
        private static LineStyle _invisibleLineStyle;
        private static LineStyle _selectedLineStyle;
        private static BackStyle _selectedBackStyle;

        private static void InitializeStyling()
        {
            _pointStyle = new PointStyle
            {
                Color = ColorHelper.GetColor(45, 45, 45),
                Width = 8
            };

            _backStyle = new BackStyle
            {
                Color = ColorHelper.GetColor(220, 220, 220)
            };

            _selectedBackStyle = new BackStyle
            {
                Color = ColorHelper.GetColor(122, 189, 254)
            };

            _lineStyle = new LineStyle
            {
                Width = 2,
                Color = ColorHelper.GetColor(45, 45, 45)
            };

            _selectedLineStyle = new LineStyle
            {
                Width = 2,
                Color = ColorHelper.GetColor(0, 0, 0)
            };

            _lightLineStyle = new LineStyle
            {
                Width = 3,
                Color = ColorHelper.GetColor(128, 45, 45, 45),
                DashStyleEnum = DashStyleEnum.Dotted
            };

            _defaultFont = new Font
            {
                Bold = true,
                Name = "Verdana",
                Size = 13,
            };

            _textStyle = new TextStyle
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
        }

        private void MakeHiddenStylesVisible()
        {
            _invisiblePointStyle.Visible = true;
            _invisiblePointStyle.Color = ColorHelper.GetColor(128, 40, 128, 192);
            _invisiblePointStyle.Width = 10;

            _invisibleBackStyle.Visible = true;
            _invisibleBackStyle.Color = ColorHelper.GetColor(64, 40, 128, 192);

            _invisibleLineStyle.Visible = true;
            _invisibleLineStyle.Color = ColorHelper.GetColor(128, 40, 128, 192);
            _invisibleLineStyle.Width = 2;
            _invisibleLineStyle.DashStyleEnum = DashStyleEnum.Dotted;
        }
    }
}
