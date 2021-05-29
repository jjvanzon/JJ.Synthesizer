namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto_PositionReader : IOperatorDto_WithDimension
    {
        /// <summary>
        /// Dimension transformations will be moved
        /// from the point in the graph where a user puts it,
        /// to here, so that you can do math with it.
        /// </summary>
        InputDto Position { get; set; }
    }
}
