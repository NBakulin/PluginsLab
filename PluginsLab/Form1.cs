using Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{

    public partial class Form1 : Form, IMainApp
    {
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        public Bitmap Image
        {
            get
            {
                return (Bitmap)pictureBox1.Image;
            }
            set
            {
                pictureBox1.Image = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
            FindPlugins();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void FindPlugins()
        {
            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;

            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("Interface.IPlugin");

                        if (iface != null)
                        {
                            Interface.IPlugin plugin = (Interface.IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }

        void CreatePluginsMenu()
        {
            foreach (string name in plugins.Keys)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(name);
                item.Click += OnPluginClick;
                toolStrip1.Items.Add(item);
            }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            Interface.IPlugin plugin = (Interface.IPlugin)plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform(this);
        }
    }
}
