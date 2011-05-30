using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Sbc11WcfClient.Common;
using System.ServiceModel;
using Sbc11WcfClient.Common.OsterFabrikService;

namespace Sbc11WcfClient
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Reentrant)]
    public partial class Form1 : Form, Sbc11WcfClient.Common.OsterFabrikService.IOsterFabrikServiceCallback
    {
        private OsterFabrikServiceClient _client;

        public Form1()
        {
            InitializeComponent();

            InstanceContext ic = new InstanceContext(this);
            _client = new OsterFabrikServiceClient(ic);

            _client.RegisterForBemalteEierNotifications();
            _client.RegisterForLogistikNotifications();
            _client.RegisterForNesterNotifications();
            _client.RegisterForSchokoHasenNotifications();
            _client.RegisterForUnbemalteEierNotifications();
        }

        // Build new Tier and use thread to call work() which calls real_work().
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce " + textBox1.Text + " Eier" + Environment.NewLine);
            startJob(new Henne((string)comboBox1.SelectedItem, int.Parse(textBox1.Text)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce " + textBox2.Text + " Hasen" + Environment.NewLine);
            startJob(new ChocolatierHase((string)comboBox2.SelectedItem, int.Parse(textBox2.Text)));
        }

        // Wrapper for ThreadStart madness.
        private void startJob(Tier t)
        {
            ThreadStart ts = new ThreadStart(t.Work);
            Thread th = new Thread(ts);
            th.Start();
        }


        public void NotifyUnbemaltesEiChanged(int currentCount)
        {
            lblUnbemalteEier.Text = currentCount.ToString();
        }

        public void NotifyBemaltesEiChanged(int currentCount)
        {
            lblBemalteEier.Text = currentCount.ToString();
        }

        public void NotifySchokoHaseChanged(int currentCount)
        {
            lblSchokoHasen.Text = currentCount.ToString();
        }

        public void NotifyNestChanged(int currentcount)
        {
            lblNester.Text = currentcount.ToString();
        }

        public void NotifyLogistikChanged(int currentlyDelivered)
        {
            lblAusgeliefert.Text = currentlyDelivered.ToString();
        }

        public void ReturnUnbemaltesEi(Common.OsterFabrikService.Ei ei)
        {
            throw new NotImplementedException();
        }

        public void ReturnBemaltesEi(Common.OsterFabrikService.Ei ei)
        {
            throw new NotImplementedException();
        }

        public void ReturnSchokoHase(Common.OsterFabrikService.SchokoHase hase)
        {
            throw new NotImplementedException();
        }

        public void ReturnNest(Common.OsterFabrikService.Nest nest)
        {
            throw new NotImplementedException();
        }
    }
}
