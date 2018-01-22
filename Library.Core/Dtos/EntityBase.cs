namespace Library.Core.Dtos
{
    public class EntityBase<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}