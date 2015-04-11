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

namespace JJ.Presentation.Synthesizer.Svg
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
            public Rectangle OperatorRectangle { get; set; }
            public IList<Point> InletPoints { get; set; }
            public IList<Point> OutletPoints { get; set; }
            public IList<Rectangle> InletRectangles { get; set; }
            public IList<Rectangle> OutletRectangles { get; set; }
        }

        private const float DEFAULT_WIDTH = 85; // 125;
        private const float DEFAULT_HEIGHT = 40; // 60;

        private Result _result;
        private Dictionary<OperatorViewModel, OperatorSvgElements> _convertedOperatorDictionary;
        private IList<Line> _convertedLines;

        // Constructors

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
        
        // General

        public Result Execute(PatchViewModel sourcePatchViewModel, Result result = null)
        {
            if (sourcePatchViewModel == null) throw new NullException(() => sourcePatchViewModel);

            if (result == null)
            {
                var destDiagram = new Diagram();
                var moveGesture = new MoveGesture();
                var dragGesture = new DragGesture();
                var dropGesture = new DropGesture(dragGesture);
                var lineGesture = new LineGesture(destDiagram, _lineStyleDashed, lineZIndex: -1);
                var selectOperatorGesture = new SelectOperatorGesture();
                var deleteOperatorGesture = new DeleteOperatorGesture();

                // TODO: Give tool tips their own styles.
                ToolTipGesture operatorToolTipGesture = null; //= new ToolTipGesture(destDiagram, _backStyle, _lineStyle, _textStyle, zIndex: 2);
                ToolTipGesture inletToolTipGesture = null; //= new ToolTipGesture(destDiagram, _backStyle, _lineStyle, _textStyle, zIndex: 2);
                ToolTipGesture outletToolTipGesture = null; //= new ToolTipGesture(destDiagram, _backStyle, _lineStyle, _textStyle, zIndex: 2);

                result = new Result(destDiagram, moveGesture, dragGesture, dropGesture, lineGesture, selectOperatorGesture, deleteOperatorGesture, operatorToolTipGesture, inletToolTipGesture, outletToolTipGesture);
            }

            _convertedOperatorDictionary = new Dictionary<OperatorViewModel, OperatorSvgElements>();
            _convertedLines = new List<Line>();

            IList<Rectangle> destExistingOperatorRectangles = result.Diagram.Canvas.Children
                                                                                   .OfType<Rectangle>()
                                                                                   .Where(x => TagHelper.IsOperatorTag(x.Tag))
                                                                                   .ToArray();
            IList<Line> destExistingLines = result.Diagram.Canvas.Children
                                                                 .OfType<Line>()
                                                                 .Where(x => TagHelper.IsInletTag(x.Tag))
                                                                 .ToArray();
            _result = result;
            _result.Diagram.Canvas.Gestures.Clear();
            _result.Diagram.Canvas.Gestures.Add(_result.DeleteOperatorGesture);

            foreach (OperatorViewModel sourceOperatorViewModel in sourcePatchViewModel.Operators)
            {
                ConvertToRectangles_WithRelatedObject_Recursive(sourceOperatorViewModel, result.Diagram);
            }

            // Delete operator rectangles + descendants
            IList<Rectangle> destConvertedOperatorRectangles = _convertedOperatorDictionary.Select(x => x.Value.OperatorRectangle).ToArray();
            IList<Rectangle> destOperatorRectanglesToDelete = destExistingOperatorRectangles.Except(destConvertedOperatorRectangles).ToArray();

            foreach (Rectangle destOperatorRectangleToDelete in destOperatorRectanglesToDelete)
            {
                IList<Element> destDescendantElements = destOperatorRectanglesToDelete.SelectMany(x => x.Children)
                                                                                      .UnionRecursive(x => x.Children)
                                                                                      .ToArray();
                foreach (Element destDescendantElement in destDescendantElements)
                {
                    destDescendantElement.Parent = null;
                    destDescendantElement.Diagram = null;
                }

                destOperatorRectangleToDelete.Parent = null;
                destOperatorRectangleToDelete.Diagram = null;
            }

            // Delete lines
            IList<Line> destLinesToDelete = destExistingLines.Except(_convertedLines).ToArray();
            foreach (Line destLineToDelete in destLinesToDelete)
            {
                destLineToDelete.Parent = null;
                destLineToDelete.Diagram = null;
            }

            _convertedOperatorDictionary = null;
            _convertedLines = null;

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
                InletViewModel inletViewModel = sourceOperatorViewModel.Inlets[i];

                if (inletViewModel.InputOutlet != null)
                {
                    // Recursive call
                    OperatorSvgElements operatorSvgElements2 = ConvertToRectangles_WithRelatedObject_Recursive(inletViewModel.InputOutlet.Operator, destDiagram);

                    Line destLine = TryGetInletLine(inletViewModel.ID);
                    if (destLine == null)
                    {
                        destLine = CreateLine();
                        destLine.Tag = TagHelper.GetInletTag(inletViewModel.ID);
                        destLine.Diagram = destDiagram;
                        _inletLineDictionary.Add(inletViewModel.ID, destLine);
                    }

                    destLine.PointA = operatorSvgElements1.InletPoints[i];

                    _convertedLines.Add(destLine);

                    if (operatorSvgElements2.OutletPoints.Count > 0) // TODO: This does not work for multiple outlets.
                    {
                        destLine.PointB = operatorSvgElements2.OutletPoints[0];
                    }
                }
            }

            return operatorSvgElements1;
        }

        private Dictionary<int, Line> _inletLineDictionary = new Dictionary<int, Line>();

        private Line TryGetInletLine(int inletID)
        {
            Line line;
            if (!_inletLineDictionary.TryGetValue(inletID, out line))
            {
                line = _result.Diagram.Canvas.Children
                                             .OfType<Line>()
                                             .Where(x => TagHelper.TryGetInletID(x.Tag) == inletID)
                                             .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (line != null)
                {
                    _inletLineDictionary.Add(inletID, line);
                }
            }

            return line;
        }

        private Line CreateLine()
        {
            var destLine = new Line
            {
                LineStyle = _lineStyleThin,
                ZIndex = -1
            };

            return destLine;
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
                OperatorRectangle = destOperatorRectangle,
                InletRectangles = destInletRectangles,
                InletPoints = destInletPoints,
                OutletRectangles = destOutletRectangles,
                OutletPoints = destOutletPoints
            };
        }

        // Operator Rectangles

        private Rectangle ConvertToOperatorRectangle(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            Rectangle destOperatorRectangle = TryGetOperatorRectangle(sourceOperatorViewModel.ID);
            if (destOperatorRectangle == null)
            {
                destOperatorRectangle = new Rectangle();
                destOperatorRectangle.Diagram = destDiagram;
                destOperatorRectangle.Tag = TagHelper.GetOperatorTag(sourceOperatorViewModel.ID);

                _destOperatorRectangleDictionary.Add(sourceOperatorViewModel.ID, destOperatorRectangle);
            }

            destOperatorRectangle.Width = DEFAULT_WIDTH;
            destOperatorRectangle.Height = DEFAULT_HEIGHT;
            destOperatorRectangle.X = sourceOperatorViewModel.CenterX - DEFAULT_WIDTH / 2f;
            destOperatorRectangle.Y = sourceOperatorViewModel.CenterY - DEFAULT_HEIGHT / 2f;

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

            destOperatorRectangle.Gestures.Clear();
            destOperatorRectangle.Gestures.Add(_result.MoveGesture);
            destOperatorRectangle.Gestures.Add(_result.SelectOperatorGesture);

            return destOperatorRectangle;
        }

        private Dictionary<int, Rectangle> _destOperatorRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetOperatorRectangle(int operatorID)
        {
            Rectangle destRectangle;
            if (!_destOperatorRectangleDictionary.TryGetValue(operatorID, out destRectangle))
            {
                destRectangle = _result.Diagram.Elements
                                               .OfType<Rectangle>()
                                               .Where(x => TagHelper.TryGetOperatorID(x.Tag) == operatorID)
                                               .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destOperatorRectangleDictionary.Add(operatorID, destRectangle);
                }
            }

            return destRectangle;
        }

        // Operator Labels

        private Label ConvertToOperatorLabel(OperatorViewModel sourceOperatorViewModel, Rectangle destRectangle)
        {
            Label destLabel = destRectangle.Children.OfType<Label>().FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.
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

        // Inlet Rectangles

        private IList<Rectangle> ConvertToInletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel.Inlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destInletRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float rowHeight = destOperatorRectangle.Height / 4;
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
            Rectangle destInletRectangle = TryGetInletRectangle(destOperatorRectangle, sourceInletViewModel.ID);
            if (destInletRectangle == null)
            {
                destInletRectangle = new Rectangle();
                destInletRectangle.Diagram = destOperatorRectangle.Diagram;
                destInletRectangle.Tag = TagHelper.GetInletTag(sourceInletViewModel.ID);

                _destInletRectangleDictionary.Add(sourceInletViewModel.ID, destInletRectangle);
            }

            destInletRectangle.BackStyle = _backStyleInvisible;
            destInletRectangle.LineStyle = _lineStyleInvisible;
            destInletRectangle.Parent = destOperatorRectangle;

            destInletRectangle.Gestures.Clear();
            destInletRectangle.Gestures.Add(_result.DropGesture);
            //destInletRectangle.Gestures.Add(_inletToolTipGesture);
            //destInletRectangle.MustBubble = false; // The is only done to make the tooltip work, so if the tooltip uses another region, it is not necessary anymore.

            return destInletRectangle;
        }

        private Dictionary<int, Rectangle> _destInletRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetInletRectangle(Element destParent, int inletID)
        {
            Rectangle destRectangle;
            if (!_destInletRectangleDictionary.TryGetValue(inletID, out destRectangle))
            {
                destRectangle = destParent.Children
                                          .OfType<Rectangle>()
                                          .Where(x => TagHelper.TryGetInletID(x.Tag) == inletID)
                                          .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destInletRectangleDictionary.Add(inletID, destRectangle);
                }
            }

            return destRectangle;
        }

        // Inlet Points

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
            Point destInletPoint = TryGetInletPoint(destOperatorRectangle, sourceInletViewModel.ID);
            if (destInletPoint == null)
            {
                destInletPoint = new Point();
                destInletPoint.Diagram = destOperatorRectangle.Diagram;
                destInletPoint.Tag = TagHelper.GetInletTag(sourceInletViewModel.ID);

                _destInletPointDictionary.Add(sourceInletViewModel.ID, destInletPoint);
            }

            destInletPoint.PointStyle = _pointStyle;
            destInletPoint.Parent = destOperatorRectangle;

            return destInletPoint;
        }

        private Dictionary<int, Point> _destInletPointDictionary = new Dictionary<int, Point>();

        private Point TryGetInletPoint(Element destParent, int inletID)
        {
            Point destPoint;
            if (!_destInletPointDictionary.TryGetValue(inletID, out destPoint))
            {
                destPoint = destParent.Children
                                      .OfType<Point>()
                                      .Where(x => TagHelper.TryGetInletID(x.Tag) == inletID)
                                      .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destPoint != null)
                {
                    _destInletPointDictionary.Add(inletID, destPoint);
                }
            }

            return destPoint;
        }

        // Outlet Rectangles

        private IList<Rectangle> ConvertToOutletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel.Outlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destOutletRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float outletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Outlets.Count;
            float rowHeight = destOperatorRectangle.Height / 4;
            float heightOverflow = _pointStyle.Width / 2;
            float x = 0;
            float y = rowHeight * 3;

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
            Rectangle destOutletRectangle = TryGetOutletRectangle(destOperatorRectangle, sourceOutletViewModel.ID);
            if (destOutletRectangle == null)
            {
                destOutletRectangle = new Rectangle();
                destOutletRectangle.Diagram = destOperatorRectangle.Diagram;
                destOutletRectangle.Tag = TagHelper.GetOutletTag(sourceOutletViewModel.ID);

                _destOutletRectangleDictionary.Add(sourceOutletViewModel.ID, destOutletRectangle);
            }

            destOutletRectangle.BackStyle = _backStyleInvisible;
            destOutletRectangle.LineStyle = _lineStyleInvisible;

            destOutletRectangle.Gestures.Clear();
            destOutletRectangle.Gestures.Add(_result.DragGesture);
            destOutletRectangle.Gestures.Add(_result.LineGesture);
            //destOutletRectangle.Gestures.Add(_outletToolTipGesture);
            destOutletRectangle.MustBubble = false;

            destOutletRectangle.Parent = destOperatorRectangle;

            return destOutletRectangle;
        }

        private Dictionary<int, Rectangle> _destOutletRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetOutletRectangle(Element destParent, int outletID)
        {
            Rectangle destRectangle;
            if (!_destOutletRectangleDictionary.TryGetValue(outletID, out destRectangle))
            {
                destRectangle = destParent.Children
                                          .OfType<Rectangle>()
                                          .Where(x => TagHelper.TryGetOutletID(x.Tag) == outletID)
                                          .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destOutletRectangleDictionary.Add(outletID, destRectangle);
                }
            }

            return destRectangle;
        }

        // Outlet Points

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
            Point destOutletPoint = TryGetOutletPoint(destOperatorRectangle, sourceOutletViewModel.ID);
            if (destOutletPoint == null)
            {
                destOutletPoint = new Point();
                destOutletPoint.Diagram = destOperatorRectangle.Diagram;
                destOutletPoint.Tag = TagHelper.GetOutletTag(sourceOutletViewModel.ID);

                _destOutletPointDictionary.Add(sourceOutletViewModel.ID, destOutletPoint);
            }

            destOutletPoint.PointStyle = _pointStyle;
            destOutletPoint.Parent = destOperatorRectangle;

            return destOutletPoint;
        }

        private Dictionary<int, Point> _destOutletPointDictionary = new Dictionary<int, Point>();

        private Point TryGetOutletPoint(Element destParent, int outletID)
        {
            Point destPoint;
            if (!_destOutletPointDictionary.TryGetValue(outletID, out destPoint))
            {
                destPoint = destParent.Children
                                      .OfType<Point>()
                                      .Where(x => TagHelper.TryGetOutletID(x.Tag) == outletID)
                                      .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destPoint != null)
                {
                    _destOutletPointDictionary.Add(outletID, destPoint);
                }
            }

            return destPoint;
        }

        // Styling

        private static Font _defaultFont;
        private static TextStyle _textStyle;
        private static BackStyle _backStyle;
        private static BackStyle _backStyleSelected;
        private static BackStyle _backStyleInvisible;
        private static LineStyle _lineStyle;
        private static LineStyle _lineStyleDashed;
        private static LineStyle _lineStyleSelected;
        private static LineStyle _lineStyleInvisible;
        private static LineStyle _lineStyleThin;
        private static PointStyle _pointStyle;
        private static PointStyle _pointStyleInvisible;

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

            _lineStyleThin = new LineStyle
            {
                Width = 1,
                Color = ColorHelper.GetColor(45, 45, 45)
            };

            _lineStyleSelected = new LineStyle
            {
                Width = 2,
                Color = ColorHelper.GetColor(0, 0, 0)
            };

            _lineStyleDashed = new LineStyle
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
