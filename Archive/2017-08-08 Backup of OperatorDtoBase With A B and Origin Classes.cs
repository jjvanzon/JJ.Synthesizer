//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal abstract class OperatorDtoBase_WithABAndOrigin : OperatorDtoBase_WithAAndB
//    {
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_WithABAndZeroOrigin : OperatorDtoBase_WithAAndB
//    {
//        private static readonly InputDto _origin = InputDtoFactory.CreateInputDto(0);

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, _origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//            }
//        }
//    }

//    // -----

//    internal abstract class OperatorDtoBase_VarA_VarB_VarOrigin : OperatorDtoBase
//    {
//        public InputDto A { get; set; }
//        public InputDto B { get; set; }
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_VarA_VarB_ZeroOrigin : OperatorDtoBase_WithAAndB
//    {
//        private static readonly InputDto _origin = InputDtoFactory.CreateInputDto(0);

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, _origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_VarA_VarB_ConstOrigin : OperatorDtoBase_WithAAndB
//    {
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_VarA_ConstB_VarOrigin : OperatorDtoBase
//    {
//        public InputDto A { get; set; }
//        public InputDto B { get; set; }
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_VarA_ConstB_ZeroOrigin : OperatorDtoBase_WithAAndB
//    {
//        private static readonly InputDto _origin = InputDtoFactory.CreateInputDto(0);

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, _origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_VarA_ConstB_ConstOrigin : OperatorDtoBase_WithAAndB
//    {
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_ConstA_VarB_VarOrigin : OperatorDtoBase
//    {
//        public InputDto A { get; set; }
//        public InputDto B { get; set; }
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_ConstA_VarB_ZeroOrigin : OperatorDtoBase_WithAAndB
//    {
//        private static readonly InputDto _origin = InputDtoFactory.CreateInputDto(0);

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, _origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_ConstA_VarB_ConstOrigin : OperatorDtoBase_WithAAndB
//    {
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_ConstA_ConstB_VarOrigin : OperatorDtoBase_WithAAndB
//    {
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_ConstA_ConstB_ZeroOrigin : OperatorDtoBase_WithAAndB
//    {
//        private static readonly InputDto _origin = InputDtoFactory.CreateInputDto(0);

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, _origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//            }
//        }
//    }

//    internal abstract class OperatorDtoBase_ConstA_ConstB_ConstOrigin : OperatorDtoBase_WithAAndB
//    {
//        public InputDto Origin { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { A, B, Origin };
//            set
//            {
//                var array = value.ToArray();
//                A = array[0];
//                B = array[1];
//                Origin = array[2];
//            }
//        }
//    }
//}
