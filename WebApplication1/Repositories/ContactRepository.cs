using WebApplication1.Context;
using WebApplication1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication1.Repositories
{
    public class ContactRepository : DbRepository<Contact>
    {
        public ContactRepository(AppDBContext db) : base(db) { }
        public override IQueryable<Contact> Items => base.Items.Include(x => x.Category);

    }
}
