using eShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.API.Helper.Interfaces
{
    public interface IUserAccessor
    {
        User GetUser();
    }
}
