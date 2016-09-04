//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using JJ.Framework.Business;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.SideEffects
//{
//    internal class Curve_SideEffect_SetDefaults_Basic : ISideEffect
//    {
//        private readonly Curve _curve;
//        private readonly IDimensionRepository _dimensionRepository;

//        public Curve_SideEffect_SetDefaults_Basic(Curve curve, IDimensionRepository dimensionRepository)
//        {
//            if (curve == null) throw new NullException(() => curve);
//            if (dimensionRepository == null) throw new NullException(() => dimensionRepository);

//            _curve = curve;
//            _dimensionRepository = dimensionRepository;
//        }

//        public void Execute()
//        {
//            // Used to set defaults for now obsolete properties.
//            // TODO: Refactor this class away?
//        }
//    }
//}
