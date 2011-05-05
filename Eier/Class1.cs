using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


using XcoSpaces;
using XcoSpaces.Collections;
using XcoSpaces.Exceptions;
using Eier;


namespace Eier
{

    // SQueue wraps the XcoQueue. SQueue contains to XcoQueue, one for the real data, the other one for Notifications which are triggered automatically by Enqueue and Dequeue functions.
    // Transaction is used to make data movement and notification atomic. The queue for nofitications has the same name as the real queue, but with "Notify" appended.
    // To use notifications, use AddNotification on the this.count member, to get informed about data movement of the real queue. Either the integer 1 oder -1 is appended to the Notification queue,
    // whenever Enqueue or Dequeue is called. A counter must be maintained externally to keep track of the occupancy.

    public class SQueue<T>
    {
        public XcoQueue<T> queue;
        public XcoQueue<int> count;
        XcoSpace space;

        // Constructor to build a new XcoQueue in local space.
        public SQueue(XcoSpace space, string name)
        {
             this.space = space;
             this.queue = new XcoQueue<T>(1000);
             this.count = new XcoQueue<int>(1000);
             this.space.Add(this.queue, name);
             this.space.Add(this.count, name + "Notify");
        }

        // Constructor using container discovery.
        public SQueue(XcoSpace space, string name, Uri remote_space_uri)
        {
            this.space = space;
            this.queue = space.Get<XcoQueue<T>>(name, remote_space_uri);
            this.count = space.Get<XcoQueue<int>>(name + "Notify", remote_space_uri);
        }


        public void Enqueue(T entry)
        {
            // Check if a transaction is already running, if so, dont open a new one and dont commit !
            if (space.CurrentTransaction != null)
            {
                this.queue.Enqueue(entry, true);
                this.count.Enqueue(1, true);
            }
            else
            {
                using (XcoTransaction tx = space.BeginTransaction())
                {
                    this.queue.Enqueue(entry, true);
                    this.count.Enqueue(1, true);
                    tx.Commit();
                }
            }
        }

        public T Dequeue()
        {
            T entry;
            if (space.CurrentTransaction != null)
            {
                entry = this.queue.Dequeue(1000);
                this.count.Enqueue(-1, true);
            }
            else
            {
                using (XcoTransaction tx = space.BeginTransaction())
                {
                    entry = this.queue.Dequeue(1000);
                    this.count.Enqueue(-1, true);
                    tx.Commit();
                }
            }
            return entry;
        }

    }


    // Produkt is the base class for all products like Ei, SchokoHase, ...
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



    // Tiere is the base class for all participants.
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

        // work() is called by the main-function or thread.
        // Build space and dio real_work();
        // In early development phase, the cause of some exceptions couldn't be identified, so a while loop keeps things alive.
        public virtual void work()
        {
            using (this.space = new XcoSpace(0))
            {

                while (true)
                {
                    try
                    {
                        real_work();
                        return;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
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
            SQueue<Ei> q = new SQueue<Ei>(this.space, "UnbemalteEier", remote_space_uri);

            for (int i = 0; i < this.count; i++)
            {
                q.Enqueue(new Ei(id + "_" + i.ToString(), id));
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
            SQueue<SchokoHase> q = new SQueue<SchokoHase>(this.space, "SchokoHasen", remote_space_uri);

            for (int i = 0; i < this.count; i++)
            {
                q.Enqueue(new SchokoHase(id + "_" + i.ToString(), id));
            }
        }
    }


    public class LogistikHase : Tiere
    {
        public LogistikHase(string id, Uri space_uri)
            : base(id, space_uri)
        {
        }

        public override void real_work()
        {
            SQueue<Nest> eingang = new SQueue<Nest>(this.space, "Nester", remote_space_uri);
            SQueue<Nest> ausgang = new SQueue<Nest>(this.space, "Ausgeliefert", remote_space_uri);

            while (true)
            {
                try
                {
                    using (XcoTransaction tx = space.BeginTransaction())
                    {

                        Nest n = eingang.Dequeue();
                        ausgang.Enqueue(n);
                        tx.Commit();
                    }
                }
                catch (Exception e)
                {
                }
                // Dequeue operations have a timeout to allow the following lines to quit the programm nicely.
                if (Console.KeyAvailable)
                    return;

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
            SQueue<Ei> unbemalt = new SQueue<Ei>(this.space, "UnbemalteEier", remote_space_uri);
            SQueue<Ei> bemalt = new SQueue<Ei>(this.space, "BemalteEier", remote_space_uri);

            while (true)            
            {
                try
                {
                    using (XcoTransaction tx = space.BeginTransaction())
                    {

                        Ei e = unbemalt.Dequeue();
                        Console.WriteLine("done");
                        e.Maler = this.id;
                        e.Farbe = this.farbe;
                        bemalt.Enqueue(e);
                        tx.Commit();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Timeout");                   
                }
                if (Console.KeyAvailable)
                    return;
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
            SQueue<SchokoHase> hasen = new SQueue<SchokoHase>(this.space, "SchokoHasen", remote_space_uri);
            SQueue<Ei> bemalt = new SQueue<Ei>(this.space, "BemalteEier", remote_space_uri);
            SQueue<Nest> nester = new SQueue<Nest>(this.space, "Nester", remote_space_uri);

            int count = 0;

            while (true)
            {
                try
                {
                    using (XcoTransaction tx = space.BeginTransaction())
                    {
                        Ei e1 = bemalt.Dequeue();
                        Ei e2 = bemalt.Dequeue();
                        SchokoHase sh = hasen.Dequeue();
                        nester.Enqueue(new Nest(id + count.ToString(), id, e1, e2, sh));
                        count = count + 1;
                        tx.Commit();
                        Console.WriteLine("done");
                    }
                }
                catch (Exception e)
                {
                }
                if (Console.KeyAvailable)
                    return;
            }
        }
    }

}
