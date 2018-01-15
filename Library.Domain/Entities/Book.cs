namespace Library.Domain.Entities
{
    public class Book : EntityBase
    {
        public string Name { get; set; }

        public long Price { get; set; }

        public int Amount { get; set; }
    }
}
