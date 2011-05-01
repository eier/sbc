using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

        public 
        public Nest(string i, string p, Ei ei1, Ei ei2, SchokoHase sh)
            : base(i, p)
        {
            this.ei1 = ei1;
            this.ei2 = ei2;
            this.sh = sh; 
        }

        public override string ToString()
        {
            return "Nest " + this._Id;
        }
    }



}
