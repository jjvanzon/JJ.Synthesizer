using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Presentation.Svg.Models.Styling;
using JJ.Framework.Presentation.Svg.Enums;
using JJ.Framework.Presentation.Svg.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Positioners;
using JJ.Framework.Presentation.Svg.Gestures;

namespace JJ.Presentation.Synthesizer.Converters
{
    internal class EntitiesToDiagramConverter
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

        private EntityPositionManager _entityPositionMananger;

        // TODO: In the fugure drag gesture and drop gesture should be two different gesture types.
        private DragDropGesture _dragGesture;
        private DragDropGesture _dropGesture;

        static EntitiesToDiagramConverter()
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
                //Visible = false,
                // Temporary (2015-04-02) for debugging.
                Visible = true,
                Color = ColorHelper.White,
                Width = 15
            };

            _invisibleBackStyle = new BackStyle
            {
                // TODO: Reactivate code line after debugging
                //Visible = false 
                // Temporary (2015-04-02) for debugging.
                Visible = true,
                Color = ColorHelper.GetColor(64, 40, 128, 192),
            };

            _invisibleLineStyle = new LineStyle
            {
                // TODO: Reactivate code line after debugging
                // Visible = false 
                // Temporary (2015-04-02) for debugging.
                Visible = true,
                Color = ColorHelper.GetColor(128, 40, 128, 192),
                Width = 2,
                DashStyleEnum = DashStyleEnum.Dotted
            };
        }

        public EntitiesToDiagramConverter(IEntityPositionRepository entityPositionRepository)
        {
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _entityPositionMananger = new EntityPositionManager(entityPositionRepository);
        }

        public Result Execute(Operator rootOperator)
        {
            if (rootOperator == null) throw new NullException(() => rootOperator);

            var diagram = new Diagram();
            _dragGesture = new DragDropGesture();
            _dropGesture = new DragDropGesture();

            Rectangle rectangle = ConvertToRectanglesAndLinesRecursive(rootOperator, diagram);

            return new Result(diagram, _dragGesture, _dropGesture);
        }

        private Rectangle ConvertToRectanglesAndLinesRecursive(Operator op, Diagram diagram)
        {
            Rectangle rectangle1 = ConvertToRectangleWithRelatedEntities(op, diagram);

            foreach (Inlet inlet in op.Inlets)
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

        private Rectangle ConvertToRectangleWithRelatedEntities(Operator op, Diagram diagram)
        {
            Rectangle rectangle = ConvertToRectangle(op);
            rectangle.Diagram = diagram;

            Point point = ConvertToPoint(op);
            point.Diagram = diagram;
            point.Parent = rectangle;

            Label label = ConvertToLabel(op);
            label.Diagram = diagram;
            label.Parent = rectangle;

            OperatorRegionsPositioner.Result positionerResult = OperatorRegionsPositioner.Execute(rectangle, op.Inlets.Count, op.Outlets.Count);

            IEnumerable<Rectangle> rectangles = positionerResult.InletRectangles
                                                                .Union(positionerResult.OutletRectangles)
                                                                /*.Union(positionerResult.NameRectangle)*/;
            foreach (Rectangle rectangle2 in rectangles)
            {
                rectangle2.BackStyle = _invisibleBackStyle;
                rectangle2.LineStyle = _invisibleLineStyle;
                rectangle2.Diagram = diagram;
                rectangle2.Parent = rectangle;
            }

            //positionerResult.NameRectangle.Gestures.Add(new MoveGesture());
            rectangle.Gestures.Add(new MoveGesture());
            positionerResult.OutletRectangles.ForEach(x => x.Gestures.Add(_dragGesture));
            positionerResult.InletRectangles.ForEach(x => x.Gestures.Add(_dropGesture));

            return rectangle;
        }

        private Rectangle ConvertToRectangle(Operator op)
        {
            EntityPosition entityPosition = _entityPositionMananger.GetOrCreateOperatorPosition(op);

            var rectangle = new Rectangle
            {
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT,
                BackStyle = _defaultBackStyle,
                LineStyle = _defaultLineStyle,
                // EntityPosition is the center instead of the top-left corner.
                X = entityPosition.X - DEFAULT_WIDTH / 2f,
                Y = entityPosition.Y - DEFAULT_HEIGHT / 2f
            };

            return rectangle;
        }

        private Label ConvertToLabel(Operator op)
        {
            var label = new Label
            {
                Text = op.Name,
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT,
                TextStyle = _defaultTextStyle
            };

            return label;
        }

        private Point ConvertToPoint(Operator op)
        {
            EntityPosition entityPosition = _entityPositionMananger.GetOrCreateOperatorPosition(op);

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
