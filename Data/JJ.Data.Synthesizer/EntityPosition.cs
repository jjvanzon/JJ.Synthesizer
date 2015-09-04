namespace JJ.Data.Synthesizer
{
    public class EntityPosition
    {
        /// <summary>
        /// Data store generated, auto-incremented ID, unlike other entities.
        /// </summary>
        public virtual int ID { get; set; }
        public virtual string EntityTypeName { get; set; }
        public virtual int EntityID { get; set; }
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
    }
}
