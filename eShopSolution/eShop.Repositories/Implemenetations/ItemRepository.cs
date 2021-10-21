using eShop.Entities;
using eShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Repositories.Implemenetations
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DbContext db) : base(db)
        {

        }
    }
}
