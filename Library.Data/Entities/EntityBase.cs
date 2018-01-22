namespace Library.Data.Entities
{
    public class EntityBase<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}