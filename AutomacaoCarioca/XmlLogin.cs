using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoCarioca
{
    public class XmlLogin
    {
        public static string StaticIdLogin { get; set; }
        public string IdLogin { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public XmlLogin(string user, string password)
        {
            StaticIdLogin = Guid.NewGuid().ToString();
            IdLogin = StaticIdLogin;
            User = user;
            Password = password;
        }
    }
}
