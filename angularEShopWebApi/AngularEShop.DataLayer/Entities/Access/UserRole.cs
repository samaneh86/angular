using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Access
{
    public class UserRole:BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
