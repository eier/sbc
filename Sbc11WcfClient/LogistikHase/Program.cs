using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sbc11WcfClient.Common;
using System.Threading;

namespace LogistikHase
{
    public class LogistikHase : Tier
    {
        static void Main(string[] args)
        {
            LogistikHase lh = new LogistikHase(args[0]);
            lh.Work();

            Console.WriteLine("Press any key to end...");
            Console.ReadLine();

            lh.Stop();
            Console.ReadLine();
        }

        public LogistikHase(string id)
            : base(id)
        {
        }

        protected override void DoWork()
        {
            _client.LookForNest();
        }

        public override void ReturnNest(Sbc11WcfClient.Common.OsterFabrikService.Nest nest)
        {
            Console.WriteLine("Nest ausgeliefert");

            new Thread(new ThreadStart(() =>
            {
                _client.NotifyNestVerschickt(nest);
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
