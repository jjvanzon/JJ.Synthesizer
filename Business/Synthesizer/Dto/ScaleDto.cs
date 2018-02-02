using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
	public class ScaleDto
	{
		public int ID { get; set; }
		public IList<double> Frequencies { get; set; }
		public string Name { get; set; }
	}
}