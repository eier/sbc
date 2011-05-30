using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using Sbc11WorkflowService.Communication;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Sbc11WorkflowService
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prg = new Program();

            prg.StartService(new OsterFarbrikService());
            
            Console.WriteLine("Hit any key to stop the service...");
            Console.ReadLine();

            prg.StopService();
        }

        private ServiceHost _selfHost;

        public void StartService(IOsterFabrikService serviceImpl)
        {
            Uri baseAdress = new Uri("http://localhost:8800/OsterFabrik");

            _selfHost = new ServiceHost(serviceImpl, baseAdress);

            _selfHost.AddServiceEndpoint(typeof(IOsterFabrikService), new WSDualHttpBinding(), "Service");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();

            smb.HttpGetEnabled = true;

            _selfHost.Description.Behaviors.Add(smb);

            foreach (var b in _selfHost.Description.Behaviors)
            {
                if (b is ServiceDebugBehavior)
                {
                    ((ServiceDebugBehavior)b).IncludeExceptionDetailInFaults = true;
                    break;
                }
            }

            _selfHost.Open();
        }

        public void StopService()
        {
            _selfHost.Close();
            _selfHost = null;
        }
    }
}
