using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Sbc11WorkflowService.Communication
{
    /// <summary>
    /// Produkt is the base class for all products like Ei, SchokoHase, ...
    /// </summary>
    public class Produkt
    {
        public string Id { get; set; }
        public string Produzent { get; set; }
    }

    public class Ei : Produkt
    {
        public override string ToString()
        {
            return string.Format("Ei {0} {1}", Id, Farbe);
        }

        public string Farbe { get; set; }
        public string Maler { get; set; }
    }

    public class SchokoHase : Produkt
    {
        public override string ToString()
        {
            return string.Format("SchokoHase {0}", Id);
        }
    }

    public class Nest : Produkt
    {
        public Ei[] Eier { get; set; }
        public SchokoHase SchokoHase { get; set; }

        public override string ToString()
        {
            return string.Format("Nest {0} mit ({1}) und {2}", Id, Eier.ToDetailedString(), SchokoHase);
        }
    }
}
