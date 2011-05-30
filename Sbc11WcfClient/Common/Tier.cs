using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sbc11WcfClient.Common.OsterFabrikService;
using System.ServiceModel;

namespace Sbc11WcfClient.Common
{
    // Tiere is the base class for all participants.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public abstract class Tier : IOsterFabrikServiceCallback
    {
        protected OsterFabrikServiceClient _client;
        protected string _id;

        public Tier(string id)
        {
            this._id = id;
         
            InstanceContext ic = new InstanceContext(this);
            _client = new OsterFabrikServiceClient(ic);

            WSDualHttpBinding binding = _client.Endpoint.Binding as WSDualHttpBinding;

            if (binding != null)
            {
                binding.ClientBaseAddress = new Uri(binding.ClientBaseAddress + "/" + _id);
            }
        }

        // work() is called by the main-function or thread.
        // Build space and dio real_work();
        // In early development phase, the cause of some exceptions couldn't be identified, so a while loop keeps things alive.
        public void Work()
        {
            if (!_shouldStop)
            {
                try
                {
                    DoWork();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
                Unload();
        }

        protected abstract void DoWork();

        protected bool _shouldStop = false;

        public virtual void Stop()
        {
            _shouldStop = true;
        }

        public virtual void NotifyUnbemaltesEiChanged(int currentCount)
        {
        }

        public virtual void NotifyBemaltesEiChanged(int currentCount)
        {
        }

        public virtual void NotifySchokoHaseChanged(int currentCount)
        {
        }

        public virtual void NotifyNestChanged(int currentcount)
        {
        }

        public virtual void NotifyLogistikChanged(int currentlyDelivered)
        {
        }

        public virtual void ReturnUnbemaltesEi(Ei ei)
        {
        }

        public virtual void ReturnBemaltesEi(Ei ei)
        {
        }

        public virtual void ReturnSchokoHase(SchokoHase hase)
        {
        }

        public virtual void ReturnNest(Nest nest)
        {
        }

        public virtual void Unload()
        {
            _client.Close();
        }
    }
}
