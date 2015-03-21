using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GassKillCore
{
    public class Killer
    {
        private const string DISABLE = "_Disable";

        public List<BankPlugin> GetPlugins() 
        {
            List<BankPlugin> plugins = new List<BankPlugin>();
            string path = GetFolderProgramFiles32();
            //Banco do Brasil
            BankPlugin plugin = BankPlugin.GetBankPluginBB();
            if ((this.ExistFolder(string.Format("{0}{1}", path, plugin.FolderName))) || (this.ExistFolder(string.Format("{0}{1}{2}", path, plugin.FolderName, DISABLE))))
                plugins.Add(plugin);
            return (plugins);
        }

        private string GetFolderProgramFiles32() 
        {
            return (@"C:\Program Files (x86)\");
        }

        private bool ExistFolder(string path) 
        {
            ApplicationData.Current.
            return (false);
        }
    }
}
