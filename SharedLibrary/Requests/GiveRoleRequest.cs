using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Requests
{
    public class GiveRoleRequest
    {
        public int roleId {  get; set; }
        public int userId { get; set; }
    }
}
