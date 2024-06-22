using WebApplication1.Entities.Base;
using WebApplication1.Entities.Enums;

namespace WebApplication1.Entities
{
    public class Contact : Entity
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool UnDeleteAble { get; set; } = false;
        public PriorityType PriorityType { get; set; } = PriorityType.notSelected;
        public string Mobile { get; set; }
        public int? CategoryId { get; set; } // внешний ключ
        public virtual Category? Category { get; set; }//навигационное свойство
        public string? PhotoPath { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
