using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Persistence.Synthesizer;
using JJ.Framework.Reflection;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Presentation.Svg.Models.Styling;
using JJ.Framework.Presentation.Svg.Enums;
using JJ.Framework.Presentation.Svg.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.Converters
{
    internal class EntitiesToDiagramConverter
    {
        private const float DEFAULT_WIDTH = 250;
        private const float DEFAULT_HEIGHT = 60;

        private static Font _defaultFont;
        private static TextStyle _defaultTextStyle;
        private static BackStyle _defaultBackStyle;
        private static LineStyle _defaultLineStyle;
        private static PointStyle _invisiblePointStyle;

        private EntityPositionManager _entityPositionMananger;

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
                Size = 13
            };

            _defaultTextStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
                Font = _defaultFont
            };

            _invisiblePointStyle = new PointStyle
            {
                Visible = false
            };
        }

        public EntitiesToDiagramConverter(IEntityPositionRepository entityPositionRepository)
        {
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _entityPositionMananger = new EntityPositionManager(entityPositionRepository);
        }

        public Diagram Execute(Operator rootOperator)
        {
            if (rootOperator == null) throw new NullException(() => rootOperator);

            var diagram = new Diagram();

            Rectangle rectangle = ConvertToRectanglesAndLinesRecursive(rootOperator, diagram);

            return diagram;
        }

        private Rectangle ConvertToRectanglesAndLinesRecursive(Operator op, Diagram diagram)
        {
            Rectangle rectangle1 = ConvertToRectangleWithRelatedEntities(op, diagram);

            foreach (Outlet outlet in op.Outlets)
            {
                foreach (Inlet inlet in outlet.ConnectedInlets)
                {
                    // Recursive call
                    Rectangle rectangle2 = ConvertToRectanglesAndLinesRecursive(inlet.Operator, diagram);

                    Line line = CreateLine();
                    line.Diagram = diagram;
                    line.PointA = GetPointFromRectangle(rectangle1);
                    line.PointB = GetPointFromRectangle(rectangle2);
                }
            }

            return rectangle1;
        }

        private Rectangle ConvertToRectangleWithRelatedEntities(Operator op, Diagram diagram)
        {
            Rectangle rectangle = ConvertToRectangle(op);
            rectangle.Diagram = diagram;

            Label label = ConvertToLabel(op);
            label.Diagram = diagram;
            label.Parent = rectangle;

            Point point = ConvertToPoint(op);
            point.Diagram = diagram;
            point.Parent = rectangle;

            return rectangle;
        }

        private Rectangle ConvertToRectangle(Operator op)
        {
            EntityPosition entityPosition = _entityPositionMananger.GetOperatorPosition(op);

            var rectangle = new Rectangle
            {
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT,
                BackStyle = _defaultBackStyle,
                LineStyle = _defaultLineStyle,
                // EntityPosition is the center.
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
            EntityPosition entityPosition = _entityPositionMananger.GetOperatorPosition(op);

            var point = new Point
            {
                X = entityPosition.X,
                Y = entityPosition.Y,
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

        private Point GetPointFromRectangle(Rectangle rectangle)
        {
            return rectangle.Children.OfType<Point>().Single();
        }
    }
}
