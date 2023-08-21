using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.Services.Interfaces
{
    public interface IAccessService:IDisposable
    {
        Task<bool> CheckUserRole(long userId, string role);
    }
}
