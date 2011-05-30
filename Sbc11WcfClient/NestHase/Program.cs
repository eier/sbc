using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sbc11WcfClient.Common;
using Sbc11WcfClient.Common.OsterFabrikService;
using System.Threading;

namespace NestHase
{
    public class NestHase : Tier
    {
        static void Main(string[] args)
        {
            NestHase nh = new NestHase(args[0]);
            nh.Work();

            Console.WriteLine("Press any key to end...");
            Console.ReadLine();

            nh.Stop();
            Console.ReadLine();
        }

        public NestHase(string id)
            : base(id)
        {

        }

        private int _count = 0;
        private List<Ei> _eier = new List<Ei>();
        private SchokoHase _hase;
        
        protected override void DoWork()
        {
            _eier.Clear();
            _hase = null;

            _client.LookForBemaltesEi();
            _client.LookForBemaltesEi();
            _client.LookForSchokoHase();
        }

        public override void ReturnBemaltesEi(Sbc11WcfClient.Common.OsterFabrikService.Ei ei)
        {
            _eier.Add(ei);
            TryBuildNest();
        }

        public override void ReturnSchokoHase(Sbc11WcfClient.Common.OsterFabrikService.SchokoHase hase)
        {
            _hase = hase;
            TryBuildNest();
        }

        private void TryBuildNest()
        {
            if (_eier.Count < 2 || _hase == null)
                return;

            Nest nest = new Nest(_id + _count.ToString(), _id, _eier.ToArray(), _hase);

            Console.WriteLine("Nest built");

            new Thread(new ThreadStart(() =>
            {
                _client.AddNest(nest);
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
