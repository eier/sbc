using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sbc11WcfClient.Common;
using System.Threading;
using Sbc11WcfClient.Common.OsterFabrikService;

namespace Sbc11WcfClient
{
    public class Henne : Tier
    {
        private Random _random = new Random();
        protected int _count;

        public Henne(string id, int count)
            : base(id)
        {
            this._count = count;
        }

        protected override void DoWork()
        {
            for (int i = 0; i < this._count; i++)
            {
                Thread.Sleep(_random.Next(4) * 1000);
                _client.AddUnbemaltesEi(new Ei(_id + "_" + i.ToString(), _id));
            }
        }
    }
}
