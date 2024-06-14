using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Entities;


namespace WebApplication1.Repositories
{
    public class ContactRepository : DbRepository<Contact>
    {
        public ContactRepository(AppDBContext db) : base(db) { }
        public override IQueryable<Contact> Items => base.Items.Include(x => x.Category);
       
    }
}
