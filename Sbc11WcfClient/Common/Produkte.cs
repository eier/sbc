using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sbc11WcfClient.Common.OsterFabrikService
{
    /// <summary>
    /// Produkt is the base class for all products like Ei, SchokoHase, ...
    /// </summary>
    public partial class Produkt
    {

        public Produkt(string id, string produzent)
        {
            Id = id;
            Produzent = produzent;
        }
    }

    public partial class Ei : Produkt
    {
        public Ei(string id, string produzent)
            : base(id, produzent)
        {
            Farbe = null;
            Maler = "Kein Maler";
        }

        public override string ToString()
        {
            return string.Format("Ei {0} {1}", Id, Farbe);
        }
    }

    public partial class SchokoHase : Produkt
    {
        public SchokoHase(string id, string produzent)
            : base(id, produzent)
        {

        }

        public override string ToString()
        {
            return string.Format("SchokoHase {0}", Id);
        }
    }

    public partial class Nest : Produkt
    {
        public Nest(string id, string produzent, Ei[] eier, SchokoHase schokoHase)
            : base(id, produzent)
        {
            Eier = eier;
            SchokoHase = schokoHase;
        }

        public override string ToString()
        {
            return string.Format("Nest {0} mit ({1}) und {2}", Id, Eier.ToDetailedString(), SchokoHase);
        }
    }
}
