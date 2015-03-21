using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GassKillCore
{
    public class BankPlugin
    {
        public string Bank { set; get; }
        public string FolderName { set; get; }
        public string ServiceName { set; get; }
        public string ServiceImageName { set; get; }

        public static BankPlugin GetBankPluginBB()
        {
            return (new BankPlugin() { Bank = "Banco do Brasil", FolderName = "GbPlugin", ServiceName = "Gbp Service", ServiceImageName = "gbpsv.exe" });
        }
    }
}
