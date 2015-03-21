using GassKillCoreWinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GassKillWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshPlugins();
        }

        private void RefreshPlugins() 
        {
            this.listView1.Items.Clear();
            List<BankPlugin> plugins = Killer.GetPlugins();
            foreach (BankPlugin plugin in plugins) 
            {
                ListViewItem item = new ListViewItem();
                item.Text = string.Format("{0}", plugin.Bank);
                item.SubItems.Add(Killer.IsPluginEnabled(plugin) ? "Yes" : "No");
                this.listView1.Items.Add(item);
            }
        }

        private BankPlugin GetSelection() 
        {
            if ((this.listView1.SelectedItems == null) || (this.listView1.SelectedItems.Count == 0))
                return (null);
            List<BankPlugin> plugins = Killer.GetPlugins();
            return(plugins.Find(p => p.Bank == this.listView1.SelectedItems[0].Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Enable
            BankPlugin plugin = this.GetSelection();
            if (plugin == null)
                return;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            string message = Killer.Enable(plugin);
            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);
            this.RefreshPlugins();
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Disable
            BankPlugin plugin = this.GetSelection();
            if (plugin == null)
                return;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            string message = Killer.Disable(plugin);
            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);
            this.RefreshPlugins();
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}
