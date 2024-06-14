using WebApplication1.Entities.Base;

namespace WebApplication1.Entities
{
    public class Contact : Entity
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? CategoryId { get; set; } // внешний ключ
        public virtual Category? Category { get; set; }//навигационное свойство
    }
}
