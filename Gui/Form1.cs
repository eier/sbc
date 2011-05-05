using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


using XcoSpaces;
using XcoSpaces.Collections;
using XcoSpaces.Exceptions;
using Eier;

namespace Gui
{
    public partial class Form1 : Form
    {
        public XcoSpace space;
        public Uri space_uri;

        public Form1()
        {
            InitializeComponent();

            space = new XcoSpace(8000);

            // Example of builing a new XcoQueue with the SQueue wrapper.
            // To notifications are added, the first for debugging purpose, the second for keeping track of the occupancy.

            SQueue<Ei> unbemalteEier = new SQueue<Ei>(space, "UnbemalteEier");
            unbemalteEier.queue.AddNotificationForEntryEnqueued(this.unbemalteEierCB);
            unbemalteEier.count.AddNotificationForEntryEnqueued(this.unbemalteEierNotifyCB);

            SQueue<Ei> bemalteEier = new SQueue<Ei>(space, "BemalteEier");
            bemalteEier.queue.AddNotificationForEntryEnqueued(this.bemalteEierCB);
            bemalteEier.count.AddNotificationForEntryEnqueued(this.bemalteEierNotifyCB);

            SQueue<SchokoHase> schokoHasen = new SQueue<SchokoHase>(space, "SchokoHasen");
            schokoHasen.queue.AddNotificationForEntryEnqueued(this.schokoHasenCB);
            schokoHasen.count.AddNotificationForEntryEnqueued(this.schokoHasenNotifyCB);

            SQueue<Nest> nester = new SQueue<Nest>(space, "Nester");
            nester.queue.AddNotificationForEntryEnqueued(this.nesterCB);
            nester.count.AddNotificationForEntryEnqueued(this.nesterNotifyCB);

            SQueue<Nest> geliefert = new SQueue<Nest>(space, "Ausgeliefert");
            geliefert.queue.AddNotificationForEntryEnqueued(this.ausgeliefertCB);
            geliefert.count.AddNotificationForEntryEnqueued(this.ausgeliefertNotifyCB);

            space_uri = new Uri("xco://127.0.0.1:8000");
        }


        // FIrst notification for debugging.
        private void unbemalteEierCB(XcoQueue<Ei> source, Ei entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " produziert" + Environment.NewLine); }));
        }
        // Second to keep track of element count.
        private void unbemalteEierNotifyCB(XcoQueue<int> source, int entry)
        {
            this.Invoke(new Action(() => { label1.Text = (System.Convert.ToInt32(label1.Text) + entry).ToString(); }));
        }


        private void bemalteEierCB(XcoQueue<Ei> source, Ei entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " bemalt" + Environment.NewLine); }));
        }
        private void bemalteEierNotifyCB(XcoQueue<int> source, int entry)
        {
            this.Invoke(new Action(() => { label2.Text = (System.Convert.ToInt32(label2.Text) + entry).ToString(); }));
        }


        private void schokoHasenCB(XcoQueue<SchokoHase> source, SchokoHase entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " produziert" + Environment.NewLine); }));
        }
        private void schokoHasenNotifyCB(XcoQueue<int> source, int entry)
        {
            this.Invoke(new Action(() => { label3.Text = (System.Convert.ToInt32(label3.Text) + entry).ToString(); }));
        }


        private void nesterCB(XcoQueue<Nest> source, Nest entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " zusammengestellt" + Environment.NewLine); }));
        }
        private void nesterNotifyCB(XcoQueue<int> source, int entry)
        {
            this.Invoke(new Action(() => { label4.Text = (System.Convert.ToInt32(label4.Text) + entry).ToString(); }));
        }


        private void ausgeliefertCB(XcoQueue<Nest> source, Nest entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " ausgeliefert" + Environment.NewLine); }));
        }
        private void ausgeliefertNotifyCB(XcoQueue<int> source, int entry)
        {
            this.Invoke(new Action(() => { label5.Text = (System.Convert.ToInt32(label5.Text) + entry).ToString(); }));
        }


        // Build new Tier and use thread to call work() which calls real_work().
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce " + textBox1.Text + " Eier" + Environment.NewLine);
            startJob(new Henne((string)comboBox1.SelectedItem, this.space_uri, System.Convert.ToInt32(textBox1.Text)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce " + textBox2.Text + " Hasen" + Environment.NewLine);
            startJob(new ChocolatierHase((string)comboBox2.SelectedItem, this.space_uri, System.Convert.ToInt32(textBox2.Text)));
        }

        // Wrapper for ThreadStart madness.
        private void startJob(Tiere t)
        {
            ThreadStart ts = new ThreadStart(t.work);
            Thread th = new Thread(ts);
            th.Start();
        }

    }
}
