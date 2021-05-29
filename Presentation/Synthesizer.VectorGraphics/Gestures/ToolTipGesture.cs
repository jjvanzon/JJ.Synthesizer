using System;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class ToolTipGesture : GestureBase
    {
        public event EventHandler<ToolTipTextEventArgs> ToolTipTextRequested;

        private readonly ToolTipElement _toolTipElement;
        private readonly string _fixedToolTipText;
        private readonly ToolTipPositioningEnum _preferredSideToShowToolTip;
        private readonly MouseLeaveGesture _mouseLeaveGesture;

        private Element _previousElement;

        /// <param name="fixedToolTipText">
        /// If you do not set it to a fixed text here,
        /// use the ToolTipTextRequested event and/or the ShowToolTipText method to change it on the fly.
        /// </param>
        public ToolTipGesture(
            ToolTipElement toolTipElement,
            string fixedToolTipText = null,
            ToolTipPositioningEnum preferredSideToShowToolTip = ToolTipPositioningEnum.TopRight)
        {
            _toolTipElement = toolTipElement ?? throw new ArgumentNullException(nameof(toolTipElement));
            _fixedToolTipText = fixedToolTipText;
            _preferredSideToShowToolTip = preferredSideToShowToolTip;

            _mouseLeaveGesture = new MouseLeaveGesture();
            _mouseLeaveGesture.MouseLeave += _mouseLeaveGesture_MouseLeave;
        }

        ~ToolTipGesture()
        {
            if (_mouseLeaveGesture != null)
            {
                _mouseLeaveGesture.MouseLeave -= _mouseLeaveGesture_MouseLeave;
            }
        }

        protected override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            // TODO: Is e.Element not nullable?
            if (e.Element == null)
            {
                return;
            }

            if (e.Element == _previousElement)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_fixedToolTipText))
            {
                ShowToolTipText(e.Element, _fixedToolTipText);
            }
            else
            {
                var e2 = new ToolTipTextEventArgs(e.Element);
                ToolTipTextRequested?.Invoke(sender, e2);

                if (!string.IsNullOrEmpty(e2.ToolTipText))
                {
                    ShowToolTipText(e.Element, e2.ToolTipText);
                }
            }

            _previousElement = e.Element;
        }

        private void _mouseLeaveGesture_MouseLeave(object sender, MouseEventArgs e) => HideToolTip(e.Element);

        /// <summary> Only shows the new tool tip text if the tool tip was already visible. </summary>
        public void ChangeToolTipText(string text)
        {
            if (_previousElement == null)
            {
                return;
            }

            ShowToolTipText(_previousElement, text);
        }

        /// <summary> Sets the tool tip text, positions the tooltip and shows it on the provided element. </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public void ShowToolTipText(Element element, string text)
        {
            if (element == null) throw new NullException(() => element);

            HideToolTip(element);

            if (string.IsNullOrEmpty(text))
            {
                // Keep it hidden, in case of no text.
                return;
            }

            _toolTipElement.SetText(text);

            _toolTipElement.Parent = element;

            SetToolTipElementRelativePosition();

            // Add _mouseLeaveGesture.
            if (!element.Gestures.Contains(_mouseLeaveGesture))
            {
                element.Gestures.Add(_mouseLeaveGesture);
            }

            _toolTipElement.Visible = true;
        }

        private void HideToolTip(Element parentElement)
        {
            _toolTipElement.Visible = false;

            parentElement?.Gestures.Remove(_mouseLeaveGesture);

            _previousElement?.Gestures.Remove(_mouseLeaveGesture);
            _previousElement = null;
        }

        // Positioning

        private void SetToolTipElementRelativePosition()
        {
            // Set X and Y

            switch (_preferredSideToShowToolTip)
            {
                case ToolTipPositioningEnum.TopRight:
                    PositionRightAligned(_toolTipElement);
                    PositionOnTop(_toolTipElement);
                    break;

                case ToolTipPositioningEnum.BottomRight:
                    PositionRightAligned(_toolTipElement);
                    PositionOnBottom(_toolTipElement);
                    break;

                case ToolTipPositioningEnum.TopLeft:
                    PositionLeftAligned(_toolTipElement);
                    PositionOnTop(_toolTipElement);
                    break;

                case ToolTipPositioningEnum.BottomLeft:
                    PositionLeftAligned(_toolTipElement);
                    PositionOnBottom(_toolTipElement);
                    break;

                case ToolTipPositioningEnum.CenterLeft:
                    PositionToTheLeft(_toolTipElement);
                    PositionInCenterY(_toolTipElement);
                    break;

                case ToolTipPositioningEnum.CenterRight:
                    PositionToTheRight(_toolTipElement);
                    PositionInCenterY(_toolTipElement);
                    break;

                default:
                    throw new ValueNotSupportedException(_preferredSideToShowToolTip);
            }

            // Correct position if out of diagram bounds.
            bool rightBoundIsExceeded = RightBoundIsExceeded(_toolTipElement);
            if (rightBoundIsExceeded)
            {
                switch (_preferredSideToShowToolTip)
                {
                    case ToolTipPositioningEnum.CenterRight:
                    case ToolTipPositioningEnum.CenterLeft:
                        PositionToTheLeft(_toolTipElement);
                        break;

                    default:
                        PositionLeftAligned(_toolTipElement);
                        break;
                }
            }

            bool leftBoundIsExceeded = LeftBoundIsExceeded(_toolTipElement);
            if (leftBoundIsExceeded)
            {
                switch (_preferredSideToShowToolTip)
                {
                    case ToolTipPositioningEnum.CenterRight:
                    case ToolTipPositioningEnum.CenterLeft:
                        PositionToTheRight(_toolTipElement);
                        break;

                    default:
                        PositionRightAligned(_toolTipElement);
                        break;
                }
            }

            bool topBoundIsExceeded = TopBoundIsExceeded(_toolTipElement);
            if (topBoundIsExceeded)
            {
                PositionOnBottom(_toolTipElement);
            }

            bool bottomBoundIsExceeded = BottomBoundIsExceeded(_toolTipElement);
            if (bottomBoundIsExceeded)
            {
                PositionOnTop(_toolTipElement);
            }
        }

        private bool BottomBoundIsExceeded(Element toolTipElement)
        {
            bool bottomBoundIsExceeded;
            if (!toolTipElement.Diagram.Position.YAxisIsFlipped)
            {
                bottomBoundIsExceeded = toolTipElement.Position.AbsoluteBottom > toolTipElement.Diagram.Position.ScaledBottom;
            }
            else
            {
                bottomBoundIsExceeded = toolTipElement.Position.AbsoluteBottom < toolTipElement.Diagram.Position.ScaledBottom;
            }

            return bottomBoundIsExceeded;
        }

        private bool TopBoundIsExceeded(Element toolTipElement)
        {
            bool topBoundIsExceeded;
            if (!toolTipElement.Diagram.Position.YAxisIsFlipped)
            {
                topBoundIsExceeded = toolTipElement.Position.AbsoluteY < toolTipElement.Diagram.Position.ScaledY;
            }
            else
            {
                topBoundIsExceeded = toolTipElement.Position.AbsoluteY > toolTipElement.Diagram.Position.ScaledY;
            }

            return topBoundIsExceeded;
        }

        private bool RightBoundIsExceeded(Element toolTipElement)
        {
            bool rightBoundIsExceeded;
            if (!toolTipElement.Diagram.Position.XAxisIsFlipped)
            {
                rightBoundIsExceeded = toolTipElement.Position.AbsoluteRight > toolTipElement.Diagram.Position.ScaledRight;
            }
            else
            {
                rightBoundIsExceeded = toolTipElement.Position.AbsoluteRight < toolTipElement.Diagram.Position.ScaledRight;
            }

            return rightBoundIsExceeded;
        }

        private bool LeftBoundIsExceeded(Element element)
        {
            bool leftBoundIsExceeded;
            if (!element.Diagram.Position.XAxisIsFlipped)
            {
                leftBoundIsExceeded = element.Position.AbsoluteX < element.Diagram.Position.ScaledX;
            }
            else
            {
                leftBoundIsExceeded = element.Position.AbsoluteX > element.Diagram.Position.ScaledX;
            }

            return leftBoundIsExceeded;
        }

        // Note it is an assumption that the tool tip height will be similar to the mouse arrow height.

        private static void PositionToTheLeft(Element toolTipElement) => toolTipElement.Position.Right = 0;

        private static void PositionToTheRight(Element toolTipElement) => toolTipElement.Position.X = toolTipElement.Parent.Position.Width;

        private static void PositionLeftAligned(Element toolTipElement) => toolTipElement.Position.X = toolTipElement.Parent.Position.Width / 2f - toolTipElement.Position.Width;

        private static void PositionRightAligned(Element toolTipElement) => toolTipElement.Position.X = toolTipElement.Parent.Position.Width / 2f;

        private static void PositionOnTop(Element toolTipElement) => toolTipElement.Position.Y = -toolTipElement.Position.Height;

        private static void PositionOnBottom(Element toolTipElement) => toolTipElement.Position.Y = toolTipElement.Position.Height + toolTipElement.Position.Height;

        private static void PositionInCenterY(Element toolTipElement) => toolTipElement.Position.CenterY = toolTipElement.Parent.Position.Height / 2f;
    }
}