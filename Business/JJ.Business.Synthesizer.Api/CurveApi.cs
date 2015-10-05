using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Api
{
    public class CurveApi
    {
        private static CurveManager _curveManager = CreateCurveManager();

        private static CurveManager CreateCurveManager()
        {
            return new CurveManager(RepositoryHelper.CurveRepositories);
        }

        /// <param name="values">When a value is null, a node will not be created at that point in time.</param>
        public static Curve Create(double timeSpan, params double?[] values)
        {
            return _curveManager.Create(timeSpan, values);
        }
    }
}
