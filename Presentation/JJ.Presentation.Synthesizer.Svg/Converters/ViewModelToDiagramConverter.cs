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
                DeleteOperatorGesture deleteOperatorGesture,
                ToolTipGesture operatorToolTipGesture,
                ToolTipGesture inletToolTipGesture,
                ToolTipGesture outletToolTipGesture)
            {
                Diagram = diagram;
                MoveGesture = moveGesture;
                DragGesture = dragGesture;
                DropGesture = dropGesture;
                LineGesture = lineGesture;
                SelectOperatorGesture = selectOperatorGesture;
                DeleteOperatorGesture = deleteOperatorGesture;
                DeleteOperatorGesture = deleteOperatorGesture;
                OperatorToolTipGesture = operatorToolTipGesture;
                InletToolTipGesture = inletToolTipGesture;
                OutletToolTipGesture = outletToolTipGesture;
            }

            public Diagram Diagram { get; private set; }
            public MoveGesture MoveGesture { get; private set; }
            public DragGesture DragGesture { get; private set; }
            public DropGesture DropGesture { get; private set; }
            public LineGesture LineGesture { get; private set; }
            public SelectOperatorGesture SelectOperatorGesture { get; private set; }
            public DeleteOperatorGesture DeleteOperatorGesture { get; private set; }
            public ToolTipGesture OperatorToolTipGesture { get; private set; }
            public ToolTipGesture InletToolTipGesture { get; private set; }
            public ToolTipGesture OutletToolTipGesture { get; private set; }
        }

        private class OperatorSvgElements
        {
            public Rectangle Rectangle { get; set; }
            public IList<Point> InletPoints { get; set; }
            public IList<Point> OutletPoints { get; set; }
            public IList<Rectangle> InletRectangles { get; set; }
            public IList<Rectangle> OutletRectangles { get; set; }
        }

        private const float DEFAULT_WIDTH = 85; // 125;
        private const float DEFAULT_HEIGHT = 40; // 60;

        private Result _result;
        private Dictionary<OperatorViewModel, OperatorSvgElements> _convertedOperatorDictionary;

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

        public Result Execute(PatchViewModel sourcePatchViewModel)
        {
            var destDiagram = new Diagram();
            var moveGesture = new MoveGesture();
            var dragGesture = new DragGesture();
            var dropGesture = new DropGesture(dragGesture);
            var lineGesture = new LineGesture(destDiagram, _lineStyleLight, lineZIndex: -1);
            var selectOperatorGesture = new SelectOperatorGesture();
            var deleteOperatorGesture = new DeleteOperatorGesture();

            // TODO: Give tool tips their own styles.
            var operatorToolTipGesture = new ToolTipGesture(destDiagram, _backStyle, _lineStyle, _textStyle, zIndex: 2);
            var inletToolTipGesture = new ToolTipGesture(destDiagram, _backStyle, _lineStyle, _textStyle, zIndex: 2);
            var outletToolTipGesture = new ToolTipGesture(destDiagram, _backStyle, _lineStyle, _textStyle, zIndex: 2);

            _result = new Result(destDiagram, moveGesture, dragGesture, dropGesture, lineGesture, selectOperatorGesture, deleteOperatorGesture, operatorToolTipGesture, inletToolTipGesture, outletToolTipGesture);

            _convertedOperatorDictionary = new Dictionary<OperatorViewModel, OperatorSvgElements>();

            return Execute(sourcePatchViewModel, _result);
        }
        
        public Result Execute(PatchViewModel sourcePatchViewModel, Result result)
        {
            if (sourcePatchViewModel == null) throw new NullException(() => sourcePatchViewModel);
            if (result == null) throw new NullException(() => result);

            _result = result;

            DeleteOperatorGesture deleteOperatorGesture = _result.Diagram.Canvas.Gestures.OfType<DeleteOperatorGesture>().SingleOrDefault();
            if (deleteOperatorGesture == null)
            {
                _result.Diagram.Canvas.Gestures.Add(_result.DeleteOperatorGesture);
            }

            foreach (OperatorViewModel sourceOperatorViewModel in sourcePatchViewModel.Operators)
            {
                OperatorSvgElements destRectangle = ConvertToRectangles_WithRelatedObject_Recursive(sourceOperatorViewModel, result.Diagram);
            }

            return _result;
        }

        private OperatorSvgElements ConvertToRectangles_WithRelatedObject_Recursive(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            OperatorSvgElements operatorSvgElements1;
            if (_convertedOperatorDictionary.TryGetValue(sourceOperatorViewModel, out operatorSvgElements1))
            {
                return operatorSvgElements1;
            }

            operatorSvgElements1 = ConvertToRectangle_WithRelatedObjects(sourceOperatorViewModel, destDiagram);

            _convertedOperatorDictionary.Add(sourceOperatorViewModel, operatorSvgElements1);

            // Go recursive and tie operators together with lines.
            for (int i = 0; i < sourceOperatorViewModel.Inlets.Count; i++)
            {
                InletViewModel inlet = sourceOperatorViewModel.Inlets[i];

                if (inlet.InputOutlet != null)
                {
                    // Recursive call
                    OperatorSvgElements operatorSvgElements2 = ConvertToRectangles_WithRelatedObject_Recursive(inlet.InputOutlet.Operator, destDiagram);

                    Line line = destDiagram.Canvas.Children
                                                  .OfType<Line>()
                                                  .Where(x => Equals(x.Tag, inlet.ID))
                                                  .FirstOrDefault();
                    if (line == null)
                    {
                        line = CreateLine();
                        line.Tag = inlet.ID;
                        line.Diagram = destDiagram;
                    }

                    line.PointA = operatorSvgElements1.InletPoints[i];

                    if (operatorSvgElements2.OutletPoints.Count > 0) // TODO: This does not work for multiple outlets.
                    {
                        line.PointB = operatorSvgElements2.OutletPoints[0];
                    }
                }
            }

            return operatorSvgElements1;
        }

        private OperatorSvgElements ConvertToRectangle_WithRelatedObjects(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            Rectangle destOperatorRectangle = ConvertToOperatorRectangle(sourceOperatorViewModel, destDiagram);
            Label destLabel = ConvertToOperatorLabel(sourceOperatorViewModel, destOperatorRectangle);
            IList<Rectangle> destInletRectangles = ConvertToInletRectangles(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destInletPoints = ConvertToInletPoints(sourceOperatorViewModel, destOperatorRectangle);
            IList<Rectangle> destOutletRectangles = ConvertToOutletRectangles(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destOutletPoints = ConvertToOutletPoints(sourceOperatorViewModel, destOperatorRectangle);

            IList<Element> childrenToDelete = destOperatorRectangle.Children
                                                                   .Except(destLabel)
                                                                   .Except(destInletRectangles)
                                                                   .Except(destInletPoints)
                                                                   .Except(destOutletRectangles)
                                                                   .Except(destOutletPoints)
                                                                   .ToArray();
            foreach (Element childToDelete in childrenToDelete)
            {
                childToDelete.Children.Clear();
                childToDelete.Parent = null;
                childToDelete.Diagram = null;
            }

            // Return result
            return new OperatorSvgElements
            {
                Rectangle = destOperatorRectangle,
                InletRectangles = destInletRectangles,
                InletPoints = destInletPoints,
                OutletRectangles = destOutletRectangles,
                OutletPoints = destOutletPoints
            };
        }

        private Rectangle ConvertToOperatorRectangle(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            Rectangle destOperatorRectangle = destDiagram.Elements
                                                         .OfType<Rectangle>()
                                                         .Where(x => Equals(x.Tag, sourceOperatorViewModel.ID))
                                                         .SingleOrDefault();
            if (destOperatorRectangle == null)
            {
                destOperatorRectangle = new Rectangle();
                destOperatorRectangle.Diagram = destDiagram;
                destOperatorRectangle.Tag = sourceOperatorViewModel.ID;
            }

            destOperatorRectangle.Width = DEFAULT_WIDTH;
            destOperatorRectangle.Height = DEFAULT_HEIGHT;
            destOperatorRectangle.X = sourceOperatorViewModel.CenterX - DEFAULT_WIDTH / 2f;
            destOperatorRectangle.Y = sourceOperatorViewModel.CenterY - DEFAULT_HEIGHT / 2f;
            destOperatorRectangle.Gestures.Add(_result.MoveGesture);
            destOperatorRectangle.Gestures.Add(_result.SelectOperatorGesture);
            //destRectangle.Gestures.Add(_operatorToolTipGesture);

            if (sourceOperatorViewModel.IsSelected)
            {
                destOperatorRectangle.BackStyle = _backStyleSelected;
                destOperatorRectangle.LineStyle = _lineStyleSelected;
            }
            else
            {
                destOperatorRectangle.BackStyle = _backStyle;
                destOperatorRectangle.LineStyle = _lineStyle;
            }

            return destOperatorRectangle;
        }

        private Label ConvertToOperatorLabel(OperatorViewModel sourceOperatorViewModel, Rectangle destRectangle)
        {
            Label destLabel = destRectangle.Children.OfType<Label>().FirstOrDefault();
            if (destLabel == null)
            {
                destLabel = new Label();
                destLabel.Diagram = destRectangle.Diagram;
            }

            destLabel.Text = sourceOperatorViewModel.Name;
            destLabel.Width = DEFAULT_WIDTH;
            destLabel.Height = DEFAULT_HEIGHT;
            destLabel.TextStyle = _textStyle;
            destLabel.Parent = destRectangle;

            return destLabel;
        }

        private IList<Rectangle> ConvertToInletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel.Inlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destInletRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float rowHeight = destOperatorRectangle.Height / 3;
            float heightOverflow = _pointStyle.Width / 2;
            float inletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Inlets.Count;
            float x = 0;

            foreach (InletViewModel sourceInletViewModel in sourceOperatorViewModel.Inlets)
            {
                Rectangle destInletRectangle = ConvertToInletRectangle(sourceInletViewModel, destOperatorRectangle);

                destInletRectangle.X = x;
                destInletRectangle.Y = -heightOverflow;
                destInletRectangle.Width = inletWidth;
                destInletRectangle.Height = rowHeight + heightOverflow;

                destInletRectangles.Add(destInletRectangle);

                x += inletWidth;
            }

            return destInletRectangles;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Rectangle ConvertToInletRectangle(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
        {
            // Convert to Inlet Rectangle
            Rectangle destInletRectangle = destOperatorRectangle.Children
                                                                .OfType<Rectangle>()
                                                                .Where(x => Equals(x.Tag, sourceInletViewModel.ID))
                                                                .FirstOrDefault();
            if (destInletRectangle == null)
            {
                destInletRectangle = new Rectangle();
                destInletRectangle.Diagram = destOperatorRectangle.Diagram;
                destInletRectangle.Tag = sourceInletViewModel.ID;
            }

            destInletRectangle.BackStyle = _backStyleInvisible;
            destInletRectangle.LineStyle = _lineStyleInvisible;
            destInletRectangle.Parent = destOperatorRectangle;
            destInletRectangle.Gestures.Add(_result.DropGesture);
            //destInletRectangle.Gestures.Add(_inletToolTipGesture);
            //destInletRectangle.MustBubble = false; // The is only done to make the tooltip work, so if the tooltip uses another region, it is not necessary anymore.
            return destInletRectangle;
        }

        private IList<Point> ConvertToInletPoints(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel.Inlets.Count == 0)
            {
                return new Point[0];
            }

            IList<Point> destInletPoints = new List<Point>(sourceOperatorViewModel.Inlets.Count);

            float inletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Inlets.Count;
            float x = 0;
            foreach (InletViewModel sourceInletViewModel in sourceOperatorViewModel.Inlets)
            {
                Point destInletPoint = ConvertToInletPoint(sourceInletViewModel, destOperatorRectangle);

                destInletPoint.X = x + inletWidth / 2f;
                destInletPoint.Y = 0;

                destInletPoints.Add(destInletPoint);

                x += inletWidth;
            }

            return destInletPoints;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Point ConvertToInletPoint(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
        {
            Point destInletPoint = destOperatorRectangle.Children
                                                        .OfType<Point>()
                                                        .Where(x => Equals(x.Tag, sourceInletViewModel.ID))
                                                        .FirstOrDefault();
            if (destInletPoint == null)
            {
                destInletPoint = new Point();
                destInletPoint.Diagram = destOperatorRectangle.Diagram;
                destInletPoint.Tag = sourceInletViewModel.ID;
            }

            destInletPoint.PointStyle = _pointStyle;
            destInletPoint.Parent = destOperatorRectangle;

            return destInletPoint;
        }

        private IList<Rectangle> ConvertToOutletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel.Outlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destOutletRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float outletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Outlets.Count;
            float rowHeight = destOperatorRectangle.Height / 3;
            float heightOverflow = _pointStyle.Width / 2;
            float x = 0;
            float y = 2f * rowHeight;

            foreach (OutletViewModel sourceOutletViewModel in sourceOperatorViewModel.Outlets)
            {
                Rectangle destOutletRectangle = ConvertToOutletRectangle(sourceOutletViewModel, destOperatorRectangle);

                destOutletRectangle.X = x;
                destOutletRectangle.Y = y;
                destOutletRectangle.Width = outletWidth;
                destOutletRectangle.Height = rowHeight + heightOverflow;

                destOutletRectangles.Add(destOutletRectangle);

                x += outletWidth;
            }

            return destOutletRectangles;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Rectangle ConvertToOutletRectangle(OutletViewModel sourceOutletViewModel, Rectangle destOperatorRectangle)
        {
            Rectangle destOutletRectangle = destOperatorRectangle.Children
                                                                 .OfType<Rectangle>()
                                                                 .Where(x => Equals(x.Tag, sourceOutletViewModel.ID)) // TODO: This is ambiguous. If inlets and outlets have ID clashes you are in trouble.
                                                                 .FirstOrDefault();
            if (destOutletRectangle == null)
            {
                destOutletRectangle = new Rectangle();
                destOutletRectangle.Diagram = destOperatorRectangle.Diagram;
                destOutletRectangle.Tag = sourceOutletViewModel.ID;
            }

            destOutletRectangle.BackStyle = _backStyleInvisible;
            destOutletRectangle.LineStyle = _lineStyleInvisible;
            destOutletRectangle.MustBubble = false;
            destOutletRectangle.Gestures.Add(_result.DragGesture);
            destOutletRectangle.Gestures.Add(_result.LineGesture);
            //destOutletRectangle.Gestures.Add(_outletToolTipGesture);
            destOutletRectangle.Parent = destOperatorRectangle;

            return destOutletRectangle;
        }

        private IList<Point> ConvertToOutletPoints(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel.Outlets.Count == 0)
            {
                return new Point[0];
            }

            IList<Point> destOutletPoints = new List<Point>(sourceOperatorViewModel.Outlets.Count);

            float outletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Outlets.Count;
            float x = 0;

            foreach (OutletViewModel sourceOutletViewModel in sourceOperatorViewModel.Outlets)
            {
                Point destOutletPoint = ConvertToOutletPoint(sourceOutletViewModel, destOperatorRectangle);

                destOutletPoint.X = x + outletWidth / 2f;
                destOutletPoint.Y = destOperatorRectangle.Height;

                destOutletPoints.Add(destOutletPoint);

                x += outletWidth;
            }

            return destOutletPoints;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Point ConvertToOutletPoint(OutletViewModel sourceOutletViewModel, Rectangle destOperatorRectangle)
        {
            Point destOutletPoint = destOperatorRectangle.Children
                                                         .OfType<Point>()
                                                         .Where(x => Equals(x.Tag, sourceOutletViewModel.ID)) // TODO: This is ambiguous. If inlets and outlets have ID clashes you are in trouble.
                                                         .FirstOrDefault();
            if (destOutletPoint == null)
            {
                destOutletPoint = new Point();
                destOutletPoint.Diagram = destOperatorRectangle.Diagram;
                destOutletPoint.Tag = sourceOutletViewModel.ID;
            }

            destOutletPoint.PointStyle = _pointStyle;
            destOutletPoint.Parent = destOperatorRectangle;

            return destOutletPoint;
        }

        private Line CreateLine()
        {
            var destLine = new Line
            {
                LineStyle = _lineStyle,
                ZIndex = -1
            };

            return destLine;
        }

        // Styling

        private static Font _defaultFont;
        private static TextStyle _textStyle;
        private static BackStyle _backStyle;
        private static LineStyle _lineStyle;
        private static PointStyle _pointStyle;
        private static LineStyle _lineStyleLight;
        private static PointStyle _pointStyleInvisible;
        private static BackStyle _backStyleInvisible;
        private static LineStyle _lineStyleInvisible;
        private static LineStyle _lineStyleSelected;
        private static BackStyle _backStyleSelected;

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

            _backStyleSelected = new BackStyle
            {
                Color = ColorHelper.GetColor(122, 189, 254)
            };

            _lineStyle = new LineStyle
            {
                Width = 2,
                Color = ColorHelper.GetColor(45, 45, 45)
            };

            _lineStyleSelected = new LineStyle
            {
                Width = 2,
                Color = ColorHelper.GetColor(0, 0, 0)
            };

            _lineStyleLight = new LineStyle
            {
                Width = 3,
                Color = ColorHelper.GetColor(128, 45, 45, 45),
                DashStyleEnum = DashStyleEnum.Dotted
            };

            _defaultFont = new Font
            {
                Bold = true,
                Name = "Microsoft Sans Serif",
                Size = 11,
            };

            _textStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
                Font = _defaultFont,
                Color = ColorHelper.GetColor(20, 20, 20)
            };

            _pointStyleInvisible = new PointStyle
            {
                Visible = false,
            };

            _backStyleInvisible = new BackStyle
            {
                Visible = false
            };

            _lineStyleInvisible = new LineStyle
            {
                Visible = false
            };
        }

        private void MakeHiddenStylesVisible()
        {
            _pointStyleInvisible.Visible = true;
            _pointStyleInvisible.Color = ColorHelper.GetColor(128, 40, 128, 192);
            _pointStyleInvisible.Width = 10;

            _backStyleInvisible.Visible = true;
            _backStyleInvisible.Color = ColorHelper.GetColor(64, 40, 128, 192);

            _lineStyleInvisible.Visible = true;
            _lineStyleInvisible.Color = ColorHelper.GetColor(128, 40, 128, 192);
            _lineStyleInvisible.Width = 2;
            _lineStyleInvisible.DashStyleEnum = DashStyleEnum.Dotted;
        }
    }
}
