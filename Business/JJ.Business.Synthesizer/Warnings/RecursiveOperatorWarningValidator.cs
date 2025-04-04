﻿using JJ.Business.Synthesizer.Names;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class RecursiveOperatorWarningValidator : ValidatorBase<Operator>
    {
        private ISet<object> _alreadyDone;

        public RecursiveOperatorWarningValidator(Operator obj, ISet<object> alreadyDone = null)
            : base(obj, postponeExecute: true)
        {
            _alreadyDone = alreadyDone ?? new HashSet<object>();

            Execute();
        }

        protected override void Execute()
        {
            if (_alreadyDone.Contains(Object)) return;
            _alreadyDone.Add(Object);

            Execute(new VersatileOperatorWarningValidator(Object));
            
            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.Input != null)
                {
                    Execute(new RecursiveOperatorWarningValidator(inlet.Input.Operator, _alreadyDone));
                }
            }
        }
    }
}
