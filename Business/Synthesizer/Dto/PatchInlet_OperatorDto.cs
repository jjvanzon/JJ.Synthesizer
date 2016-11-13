using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PatchInlet_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PatchInlet);

        public PatchInlet_OperatorDto()
            : base(new OperatorDtoBase[0])
        { }

        public double DefaultValue { get; set; }
        public int ListIndex { get; set; }
        public string Name { get; set; }
        public DimensionEnum DimensionEnum { get; set; }
    }
}
