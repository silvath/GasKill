using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GassKillCoreWinForms
{
    public class Killer
    {
        private const string DISABLE = "_Disable";

        public static List<BankPlugin> GetPlugins()
        {
            List<BankPlugin> plugins = new List<BankPlugin>();
            string path = GetFolderProgramFiles32();
            //Banco do Brasil
            BankPlugin plugin = BankPlugin.GetBankPluginBB();
            if ((ExistFolder(string.Format("{0}{1}", path, plugin.FolderName))) || (ExistFolder(string.Format("{0}{1}{2}", path, plugin.FolderName, DISABLE))))
                plugins.Add(plugin);
            return (plugins);
        }

        private static string GetFolderProgramFiles32()
        {
            return (@"C:\Program Files (x86)\");
        }

        private static bool ExistFolder(string path) 
        {
            return (System.IO.Directory.Exists(path));
        }

        public static bool IsPluginEnabled(BankPlugin plugin) 
        {
            string path = GetFolderProgramFiles32();
            return ((ExistFolder(string.Format("{0}{1}", path, plugin.FolderName))));
        }

        public static string Enable(BankPlugin plugin) 
        {
            if (IsPluginEnabled(plugin))
                return(string.Empty);
            string path = GetFolderProgramFiles32();
            return(Update(true, plugin.ServiceImageName, string.Format("{0}{1}{2}", path, plugin.FolderName, DISABLE), string.Format("{0}{1}", path, plugin.FolderName))); 
        }

        public static string Disable(BankPlugin plugin)
        {
            if (!IsPluginEnabled(plugin))
                return(string.Empty);
            string path = GetFolderProgramFiles32();
            return(Update(false, plugin.ServiceImageName, string.Format("{0}{1}", path, plugin.FolderName), string.Format("{0}{1}{2}", path, plugin.FolderName, DISABLE))); 
        }

        private static string Update(bool enable, string processKill, string pathFrom, string pathTo) 
        {
            //Kill
            if (!string.IsNullOrEmpty(processKill))
            {
                try
                {
                    Process.EnterDebugMode();
                    foreach(Process process in GetProcess(processKill))
                        process.Kill();
                }catch (Exception e){
                    return (string.Format("Não foi possível matar o processo. {0}", e.ToString()));
                }finally {
                    Process.LeaveDebugMode();
                }
            }
            //Rename
            string message = string.Empty;
            int max = 30;
            for(int i = 0; i < max;i++)
            {
                try
                {
                    message = string.Empty;
                    Directory.Move(pathFrom, pathTo);
                    break;
                }catch (Exception e){
                    message = string.Format("Não foi possível renomiar. {0}", e.ToString());
                }
            }
            if (!string.IsNullOrEmpty(message))
                return (message);
            //Start Process
            if (!enable)
                return(string.Empty);
            ServiceController controller = new ServiceController("GbpSv");
            if(controller != null)
                controller.Start();
            return (string.Empty);
        }

        private static List<Process> GetProcess(string processName) 
        {
            List<Process> processes = new List<Process>();
            foreach (Process process in Process.GetProcesses())
                if (process.ProcessName.ToLower() == processName.ToLower())
                    processes.Add(process);
            return (processes);
        }
    }
}
