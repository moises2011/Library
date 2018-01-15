﻿namespace Library.Data.Entities
{
    public class Book : EntityBase
    {
        public string Name { get; set; }

        public long Price { get; set; }

        public int Amount { get; set; }
    }
}