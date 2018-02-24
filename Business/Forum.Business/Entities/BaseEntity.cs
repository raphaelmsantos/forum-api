using Forum.Business.Interfaces.Models;
using System;

namespace Forum.Business.Entities
{
    public class BaseEntity : IActivable
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}
