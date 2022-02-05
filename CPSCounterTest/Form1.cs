using CPSCounter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPSCounterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CPSUpdater updater = new CPSUpdater();
            CPSClient client = new CPSClient();
            updater.AddHandler(client);
            updater.StartUpdater();
            client.LoadCounter(new Listener());
        }
    }
    public class Listener : Counter
    {
        public override void Initialize()
        {
            Console.WriteLine("Включен");
        }
        public override void OnClick(int cps)
        {
            Console.WriteLine("CPS: " + cps);
        }
        public override void OnClickRemoved(int cps)
        {
            Console.WriteLine("1 CPS убран теперь CPS: " + cps);
        }
    }
}
