﻿//using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
//using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
//using JJ.Presentation.Synthesizer.ViewModels.Items;
//using System.Collections.Generic;
//using System.Linq;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
//{
//    internal class OperatorToolTipRectangleConverter
//    {
//        private Dictionary<int, Rectangle> _destOperatorToolTipRectangleDictionary = new Dictionary<int, Rectangle>();

//        private ToolTipGesture _operatorToolTipGesture;

//        public OperatorToolTipRectangleConverter(ToolTipGesture operatorToolTipGesture)
//        {
//            if (operatorToolTipGesture == null) throw new NullException(() => operatorToolTipGesture);

//            _operatorToolTipGesture = operatorToolTipGesture;
//        }

//        public Rectangle ConvertToOperatorToolTipRectangle(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
//        {
//            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
//            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

//            Rectangle destOperatorToolTipRectangle = TryGetRectangle(destOperatorRectangle, sourceOperatorViewModel.ID);
//            if (destOperatorToolTipRectangle == null)
//            {
//                destOperatorToolTipRectangle = new Rectangle
//                {
//                    Diagram = destOperatorRectangle.Diagram,
//                    Parent = destOperatorRectangle,
//                    Tag = VectorGraphicsTagHelper.GetOperatorTag(sourceOperatorViewModel.ID)
//                };

//                _destOperatorToolTipRectangleDictionary.Add(sourceOperatorViewModel.ID, destOperatorToolTipRectangle);
//            }

//            destOperatorToolTipRectangle.X = 0;
//            destOperatorToolTipRectangle.Width = destOperatorRectangle.Width;
            
//            // TODO: This might not quite work for number operators, since they have a different height.
//            destOperatorToolTipRectangle.Y = PositionHelper.CalculateY(StyleHelper.OPERATOR_HEIGHT, rowCount: 4, rowIndexFrom: 1);
//            destOperatorToolTipRectangle.Height = PositionHelper.CalculateHeight(StyleHelper.OPERATOR_HEIGHT, rowCount: 4, rowIndexFrom: 1, rowIndexTill: 2);
//            destOperatorToolTipRectangle.BackStyle = StyleHelper.BackStyleInvisible;
//            destOperatorToolTipRectangle.LineStyle = StyleHelper.BorderStyleInvisible;

//            if (_operatorToolTipGesture != null)
//            {
//                destOperatorToolTipRectangle.Gestures.Add(_operatorToolTipGesture);
//            }

//            return destOperatorToolTipRectangle;
//        }

//        private Rectangle TryGetRectangle(Element destParent, int id)
//        {
//            Rectangle destOperatorToolTipRectangle;
//            if (!_destOperatorToolTipRectangleDictionary.TryGetValue(id, out destOperatorToolTipRectangle))
//            {
//                destOperatorToolTipRectangle = destParent.Children
//                                                         .OfType<Rectangle>()
//                                                         .Where(x => VectorGraphicsTagHelper.TryGetOperatorID(x.Tag) == id)
//                                                         .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

//                if (destOperatorToolTipRectangle != null)
//                {
//                    _destOperatorToolTipRectangleDictionary.Add(id, destOperatorToolTipRectangle);
//                }
//            }

//            return destOperatorToolTipRectangle;
//        }
//    }
//}
