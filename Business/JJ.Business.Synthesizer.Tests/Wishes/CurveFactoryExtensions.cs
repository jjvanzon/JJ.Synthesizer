using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public static class CurveFactoryExtensions
    {
        /// <inheritdoc cref="CreateCurveDocs" />
        public static Curve CreateCurve(this CurveFactory curveFactory, params (double time, double value)[] nodeTuples)
            => CreateCurve(curveFactory, (IList<(double x, double y)>)nodeTuples);

        /// <inheritdoc cref="CreateCurveDocs" />
        public static Curve CreateCurve(this CurveFactory curveFactory, IList<(double time, double value)> nodeTuples)
        {
            if (nodeTuples == null) throw new NullException(() => nodeTuples);

            var nodeInfos = nodeTuples.Select(x => new NodeInfo(x.Item1, x.Item2)).ToArray();
            
            Curve curve = curveFactory.CreateCurve(nodeInfos);

            return curve;
        }

        /// <summary>
        /// Shorthand that takes tuples like (0, 0), (0.1, 0.2), (0.2, 1.0)
        /// and creates a curve from them.
        /// </summary>
        /// <param name="curveFactory">The factory used to create the <see cref="Curve"/>.</param>
        /// <param name="nodeTuples">
        /// A list of tuples representing the nodes,
        /// where each tuple contains the x and y coordinates of a node.
        /// </param>
        /// <returns>A <see cref="Curve"/> object populated with the specified nodes.</returns>
        [UsedImplicitly]
        private static Curve CreateCurveDocs(this CurveFactory curveFactory, object nodeTuples) 
            => throw new NotSupportedException();
    }
}