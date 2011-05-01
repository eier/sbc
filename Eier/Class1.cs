using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Eier
{

    [Serializable]
    public class Ei : ISerializable
    {

        public string _Id;
        public string _Farbe;
        public string Farbe
        {
            set { this._Farbe = value; }
            get { return this._Farbe; }
        }

        public Ei(string i)
        {
            this._Id = i;
            this._Farbe = "unbemalt";
        }
        public Ei(SerializationInfo info, StreamingContext ctx)
        {
            this._Farbe = info.GetString("_Farbe");
            this._Id = info.GetString("_Id");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("_Farbe", this._Farbe);
            info.AddValue("_Id", this._Id);
        }

        public override string ToString()
        {
            return "Ei " + this._Id + " " + this._Farbe;
        }

    }


    [Serializable]
    public class SchokoHase
    {

        public string _Id;

        public SchokoHase(string i)
        {
            this._Id = i;
        }
        public SchokoHase(SerializationInfo info, StreamingContext ctx)
        {
            this._Id = info.GetString("_Id");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("_Id", this._Id);
        }

        public override string ToString()
        {
            return "SchokoHase " + this._Id;
        }

    }

}
