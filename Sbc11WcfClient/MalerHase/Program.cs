using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sbc11WcfClient.Common;
using System.Threading;
using System.Windows.Threading;
using System.ServiceModel;

namespace MalerHase
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MalerHase : Tier
    {
        static void Main(string[] args)
        {
            MalerHase mh = new MalerHase(args[0], args[1]);
            mh.Work();

            Console.WriteLine("Press any key to end...");
            Console.ReadLine();

            mh.Stop();
            Console.ReadLine();
        }

        protected string _farbe;

        public MalerHase(string id, string farbe)
            : base(id)
        {
            this._farbe = farbe;
        }

        protected override void DoWork()
        {
            _client.LookForUnbemaltesEi();
        }

        public override void ReturnUnbemaltesEi(Sbc11WcfClient.Common.OsterFabrikService.Ei ei)
        {
            ei.Maler = _id;
            ei.Farbe = _farbe;
            
            Console.WriteLine("Ei bemalt");

            new Thread(new ThreadStart(() =>
                {
                    _client.AddBemaltesEi(ei);
                    Work();
                })).Start();
        }

        public override void Unload()
        {
            base.Unload();
            Environment.Exit(0);
        }
    }
}
