using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


using XcoSpaces;
using XcoSpaces.Collections;
using XcoSpaces.Exceptions;
using Eier;


namespace Eier
{
    [Serializable]
    public class Produkt
    {

        public Produkt(string i, string p)
        {
            _Id = i;
            _Produzent = p;
        }

        public string _Id;
        public string Id
        {
            set { this._Id = value; }
            get { return this._Id; }
        }

        public string _Produzent;
        public string Produzent
        {
            set { this._Produzent = value; }
            get { return this._Produzent; }
        }
    }

    [Serializable]
    public class Ei : Produkt
    {
        public Ei(string i, string p) : base(i,p)
        {
            this._Farbe = "unbemalt";
            this._Maler = "Kein Maler";
        }

        public override string ToString()
        {
            return "Ei " + this._Id + " " + this._Farbe;
        }

        public string _Farbe;
        public string Farbe
        {
            set { this._Farbe = value; }
            get { return this._Farbe; }
        }

        public string _Maler;
        public string Maler
        {
            set { this._Maler = value; }
            get { return this._Maler; }
        }
    }


    [Serializable]
    public class SchokoHase : Produkt
    {
        public SchokoHase(string i, string p) : base(i,p)
        {
            
        }

        public override string ToString()
        {
            return "SchokoHase " + this._Id;
        }
    }

    [Serializable]
    public class Nest : Produkt
    {

        public Ei e1;
        public Ei e2;
        public SchokoHase sh;

        public Nest(string i, string p, Ei ei1, Ei ei2, SchokoHase sh)
            : base(i, p)
        {
            this.e1 = ei1;
            this.e2 = ei2;
            this.sh = sh; 
        }

        public override string ToString()
        {
            return "Nest " + this._Id + " mit " + e1.ToString() + " " + e2.ToString() + " " + sh.ToString();
        }
    }


    public class Tiere
    {
        protected XcoSpace space;
        protected Uri remote_space_uri;
        protected string id;

        public Tiere(string id, Uri space_uri)
        {
            this.id = id;
            this.remote_space_uri = space_uri;
        }

        public virtual void work()
        {
            using (this.space = new XcoSpace(0))
            {
                real_work();
            }
        }

        public virtual void real_work()
        {
        }
    }


    public class Henne : Tiere
    {
        protected int count;

        public Henne(string id, Uri space_uri, int count)
            : base(id, space_uri)
        {
            this.count = count;
        }

        public override void real_work()
        {
            XcoQueue<Ei> q = this.space.Get<XcoQueue<Ei>>("UnbemalteEier", remote_space_uri);

            for (int i = 0; i < this.count; i++)
            {
                q.Enqueue(new Ei(id + "_" + i.ToString(), id), true);
            }
        }
    }

    public class ChocolatierHase : Tiere
    {
        protected int count;

        public ChocolatierHase(string id, Uri space_uri, int count)
            : base(id, space_uri)
        {
            this.count = count;
        }

        public override void real_work()
        {
            XcoQueue<SchokoHase> q = this.space.Get<XcoQueue<SchokoHase>>("SchokoHasen", remote_space_uri);

            for (int i = 0; i < this.count; i++)
            {
                q.Enqueue(new SchokoHase(id + "_" + i.ToString(), id), true);
            }
        }
    }


    public class LogistikHase : Tiere
    {
        protected int count;

        public LogistikHase(string id, Uri space_uri, int count)
            : base(id, space_uri)
        {
            this.count = count;
        }

        public override void real_work()
        {
            XcoQueue<Nest> eingang = this.space.Get<XcoQueue<Nest>>("Nester", remote_space_uri);
            XcoQueue<Nest> ausgang = this.space.Get<XcoQueue<Nest>>("Ausgeliefert", remote_space_uri);

            while (this.count > 0)
            {
                Nest n = eingang.Dequeue(true);
                ausgang.Enqueue(n, true);
                this.count = this.count - 1;
            }
        }
    }

    public class MalerHase : Tiere
    {

        protected string farbe;

        public MalerHase(string id, Uri space_uri, string farbe)
            : base(id, space_uri)
        {
            this.farbe = farbe;
        }

        public override void real_work()
        {
            XcoQueue<Ei> unbemalt = space.Get<XcoQueue<Ei>>("UnbemalteEier", remote_space_uri);
            XcoQueue<Ei> bemalt = space.Get<XcoQueue<Ei>>("BemalteEier", remote_space_uri);

            while (true)            
            {
                Console.WriteLine("Dequeue");
                try
                {
                    Ei e = unbemalt.Dequeue(1000);
                    Console.WriteLine("done");
                    e.Maler = this.id;
                    e.Farbe = this.farbe;
                    bemalt.Enqueue(e, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Timeout");
                   
                }
            }
        }
    }


    public class NestHase : Tiere
    {

        public NestHase(string id, Uri space_uri)
            : base(id, space_uri)
        {

        }

        public override void real_work()
        {
            XcoQueue<SchokoHase> schoko = space.Get<XcoQueue<SchokoHase>>("SchokoHasen", remote_space_uri);
            XcoQueue<Ei> bemalt = space.Get<XcoQueue<Ei>>("BemalteEier", remote_space_uri);
            XcoQueue<Nest> nester = space.Get<XcoQueue<Nest>>("Nester", remote_space_uri);
            int count = 0;

            while (true)
            {
                Console.WriteLine("Begin");

                //using(XcoTransaction tx = space.BeginTransaction())
                //{
                    Ei e1 = bemalt.Dequeue(true);
                    Ei e2 = bemalt.Dequeue(true);
                    SchokoHase sh = schoko.Dequeue(true);
                    //tx.Commit();
                    Console.WriteLine("done");
                    nester.Enqueue(new Nest(id + count.ToString(),id, e1,e2,sh), true);
                //}
                count = count + 1;
            }
        }
    }

}
