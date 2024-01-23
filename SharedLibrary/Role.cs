using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public class Role : BaseClass
    {
        public string Name { get; set; }

        public bool BanPermission { get; set; }
        
        public bool CreateRolePermission { get; set; }

        public bool PremiumPermission { get; set; }
    }
}
