using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenAuthenticationWebAPI.Models
{
    public class UserMasterRepository
    {
        SECURITY_DBEntities _db = new SECURITY_DBEntities();

        public UserMaster validateUser(string username, string password)
        {
            return _db.UserMasters.FirstOrDefault(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.UserPassword == password);
        }
    }
}