namespace JJ.Business.SynthesizerPrototype.Dto
{
	public class MathPropertiesDto
	{
		public bool IsVar { get; set; }
		public bool IsConst { get; set; }
		public bool IsConstZero { get; set; }
		public bool IsConstOne { get; set; }
		public bool IsConstNonZero { get; set; }
		public bool IsConstSpecialValue { get; set; }
		public double ConstValue { get; set; }
	}
}
