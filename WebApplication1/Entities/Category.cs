using WebApplication1.Entities.Base;

namespace WebApplication1.Entities
{
    public class Category : Entity
    {
        //public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
