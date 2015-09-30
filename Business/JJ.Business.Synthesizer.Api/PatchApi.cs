using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Data.Synthesizer;
using JJ.Framework.Data;

namespace JJ.Business.Synthesizer.Api
{
    public class PatchApi
    {
        private PatchManager _patchManager;

        public Patch Patch { get; private set; }

        public PatchApi()
        {
            _patchManager = new PatchManager(RepositoryHelper.PatchRepositories);
            _patchManager.Create();
        }

        public OperatorWrapper_Sine Sine(Outlet volume = null, Outlet pitch = null, Outlet origin = null, Outlet phaseStart = null)
        {
            return _patchManager.Sine(volume, pitch, origin, phaseStart);
        }

        public OperatorWrapper_Number Number(double number = 0)
        {
            return _patchManager.Number(number);
        }
    }
}
