//using System;
//using JJ.Framework.Exceptions;

//namespace JJ.Framework.Presentation.VectorGraphics.Models.Elements
//{
//    public class CurvePosition : ElementPosition
//    {
//        private readonly Curve _curve;

//        internal CurvePosition(Curve curve)
//            : base(curve)
//        {
//            _curve = curve;
//        }

//        public override float X
//        {
//            get
//            {
//                if (_curve.PointA == null) return 0f;
//                if (_curve.PointB == null) return 0f;

//                return Math.Min(_curve.PointA.Position.X, _curve.PointB.Position.X);
//            }
//            set
//            {
//                if (_curve.PointA == null) return;
//                if (_curve.PointB == null) return;

//                float dx = _curve.PointB.Position.X - _curve.PointA.Position.X;
//                _curve.PointA.Position.X = value;
//                _curve.PointB.Position.X = _curve.PointA.Position.X + dx;
//            }
//        }

//        public override float Y
//        {
//            get
//            {
//                if (_curve.PointA == null) return 0f;
//                if (_curve.PointB == null) return 0f;

//                return Math.Min(_curve.PointA.Position.Y, _curve.PointB.Position.Y);
//            }
//            set
//            {
//                if (_curve.PointA == null) return;
//                if (_curve.PointB == null) return;

//                float dy = _curve.PointB.Position.Y - _curve.PointA.Position.Y;
//                _curve.PointA.Position.Y = value;
//                _curve.PointB.Position.Y = _curve.PointA.Position.Y + dy;
//            }
//        }

//        public override float Width
//        {
//            get => 0;
//            set => throw new NotSupportedException();
//        }

//        public override float Height
//        {
//            get { return 0; }
//            set { throw new NotSupportedException(); }
//        }
//    }
//}
