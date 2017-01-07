using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Business.Synthesizer.Dtos
{
    public class UsedInDto<TEntity>
    {
        public TEntity Entity { get; set; }
        public IList<IDAndName> UsedInIDAndNames { get; set; }
    }
}
