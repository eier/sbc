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

            XcoQueue<Ei> unbemalteEier = new XcoQueue<Ei>(1000);
            space.Add(unbemalteEier, "UnbemalteEier");
            unbemalteEier.AddNotificationForEntryEnqueued(this.unbemalteEierNotify);

            XcoQueue<Ei> bemalteEier = new XcoQueue<Ei>(1000);
            space.Add(bemalteEier, "BemalteEier");
            bemalteEier.AddNotificationForEntryEnqueued(this.bemalteEierNotify);

            XcoQueue<SchokoHase> schokoHasen = new XcoQueue<SchokoHase>(1000);
            space.Add(schokoHasen, "SchokoHasen");
            schokoHasen.AddNotificationForEntryEnqueued(this.schokoHasenNotify);

            XcoQueue<Nest> nester = new XcoQueue<Nest>(1000);
            space.Add(nester, "Nester");
            nester.AddNotificationForEntryEnqueued(this.nesterNotify);

            XcoQueue<Nest> ausgeliefert = new XcoQueue<Nest>(1000);
            space.Add(ausgeliefert, "Ausgeliefert");
            ausgeliefert.AddNotificationForEntryEnqueued(this.ausgeliefertNotify);

            space_uri = new Uri("xco://127.0.0.1:8000");
        }



        private void unbemalteEierNotify(XcoQueue<Ei> source, Ei entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " produziert" + Environment.NewLine); }));
        }

        private void bemalteEierNotify(XcoQueue<Ei> source, Ei entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " bemalt" + Environment.NewLine); })); 
        }

        private void schokoHasenNotify(XcoQueue<SchokoHase> source, SchokoHase entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " produziert" + Environment.NewLine); }));
        }

        private void nesterNotify(XcoQueue<Nest> source, Nest entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " zusammengestellt" + Environment.NewLine); }));
        }

        private void ausgeliefertNotify(XcoQueue<Nest> source, Nest entry)
        {
            this.Invoke(new Action(() => { textBox3.AppendText(entry.ToString() + " ausgeliefert" + Environment.NewLine); }));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce " + textBox1.Text + " Eier" + Environment.NewLine);
            startJob(new Henne((string) comboBox1.SelectedItem, this.space_uri, System.Convert.ToInt32(textBox1.Text)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce " + textBox2.Text + " Hasen" + Environment.NewLine);
            startJob(new ChocolatierHase((string) comboBox2.SelectedItem, this.space_uri, System.Convert.ToInt32(textBox2.Text)));
        }

        private void startJob(Tiere t)
        {
            ThreadStart ts = new ThreadStart(t.work);
            Thread th = new Thread(ts);
            th.Start();
        }
    }
}
