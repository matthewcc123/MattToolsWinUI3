using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Settings
{
    public class UserSetting
    {
        public AppSetting App {  get; set; }
        public RossumSetting Rossum { get; set; }

        public UserSetting() 
        { 
            App = new AppSetting(); 
            Rossum = new RossumSetting();
        }
    }
}
