using WebApplication1.Interfaces;

namespace WebApplication1.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
