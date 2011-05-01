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
            XcoQueue<Eier.Ei> unbemalteEier = new XcoQueue<Ei>();
            space.Add(unbemalteEier, "UnbemalteEier");
            unbemalteEier.AddNotificationForEntryEnqueued(this.unbemalteEierNotify);
            
            XcoQueue<Eier.Ei> bemalteEier = new XcoQueue<Ei>();
            space.Add(bemalteEier, "BemalteEier");
            bemalteEier.AddNotificationForEntryEnqueued(this.bemalteEierNotify);
            
            XcoQueue<Eier.SchokoHase> schokoHasen = new XcoQueue<SchokoHase>();
            space.Add(unbemalteEier, "SchokoHasen");

            space_uri = new Uri("xco://127.0.0.1:8000");
        }


        private delegate void testDelegate(XcoQueue<Ei> source, Ei entry);
        private void test(XcoQueue<Ei> source, Ei entry)
        {
            textBox3.AppendText("Ei " + entry._Id + " produced");
        }
        private void unbemalteEierNotify(XcoQueue<Ei> source, Ei entry)
        {
            this.Invoke(new testDelegate(test), source, entry);
        }



        private void bemalteEierNotify(XcoQueue<Ei> source, Ei entry)
        {
            textBox3.AppendText("Ei " + entry._Id + " " + entry._Farbe + " bemalt");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce + " + textBox1.Text + " Eier");
         
            startJob(new Henne("Henne_id", this.space_uri, System.Convert.ToInt32(textBox1.Text)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.AppendText("Produce + " + textBox2.Text + " Hasen");
            startJob(new ChocolatierHase("SchokoHase_id",  this.space_uri, System.Convert.ToInt32(textBox2.Text)));
        }

        private void startJob(ProduktionsTiere t)
        {
            ThreadStart ts = new ThreadStart(t.produce);
            Thread th = new Thread(ts);
            th.Start();
        }

    }






    
    public class ProduktionsTiere
    {
        protected int count;
        protected XcoSpace space;
        protected Uri remote_space_uri;
        protected string id;

        public ProduktionsTiere(string id, Uri space_uri, int count)
        {

            this.space = new XcoSpace(0);
            this.count = count;
            this.remote_space_uri = space_uri;
        }

        public virtual void produce() { }
    }


    public class Henne : ProduktionsTiere
    {
        public Henne(string id, Uri space_uri, int count) : base(id, space_uri, count) { }

        public override void produce()
        {
            XcoQueue<Ei> q = this.space.Get<XcoQueue<Ei>>("UnbemalteEier", remote_space_uri);

            for (int i = 0; i < this.count; i++)
            {
                q.Enqueue(new Ei(id));
            }
        }
    }

    public class ChocolatierHase : ProduktionsTiere
    {
        public ChocolatierHase(string id, Uri space_uri, int count) : base(id, space_uri, count) { }

        public override void produce()
        {
            XcoQueue<SchokoHase> q = this.space.Get<XcoQueue<SchokoHase>>("SchokoHasen", remote_space_uri);

            for (int i = 0; i < this.count; i++)
            {
                q.Enqueue(new SchokoHase(id));
            }
        }
    }

}
