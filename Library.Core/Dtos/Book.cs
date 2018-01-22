namespace Library.Core.Dtos
{
    public class Book : EntityBase<long>
    {
        public string Name { get; set; }

        public long Price { get; set; }

        public int Amount { get; set; }
    }
}
