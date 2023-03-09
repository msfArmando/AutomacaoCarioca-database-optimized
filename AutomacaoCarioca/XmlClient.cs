using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoCarioca
{
    public class XmlClient
    {
        public static string StaticIdClient { get; set; }
        public string IdClient { get; set; }
        public string NameClient { get; set; }
        public string InscricaoClient { get; set; }
        public string IdRefLogin { get; set; }
        public XmlClient()
        {
            StaticIdClient = Guid.NewGuid().ToString();
            IdClient = StaticIdClient;
            IdRefLogin = XmlLogin.StaticIdLogin;
        }
    }
}
