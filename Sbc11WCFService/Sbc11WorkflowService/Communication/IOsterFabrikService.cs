using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Sbc11WorkflowService.Communication
{
    [ServiceContract(CallbackContract = typeof(IOsterFabrikCallback))]
    public interface IOsterFabrikService
    {
        [OperationContract()]
        void AddUnbemaltesEi(Ei ei);
        [OperationContract()]
        void AddBemaltesEi(Ei ei);
        [OperationContract()]
        void AddSchokoHase(SchokoHase schokoHase);
        [OperationContract()]
        void AddNest(Nest nest);

        [OperationContract()]
        void LookForUnbemaltesEi();
        [OperationContract()]
        void LookForBemaltesEi();
        [OperationContract()]
        void LookForSchokoHase();
        [OperationContract()]
        void LookForNest();

        [OperationContract()]
        void NotifyNestVerschickt(Nest nest);

        [OperationContract()]
        void RegisterForUnbemalteEierNotifications();
        [OperationContract()]
        void RegisterForBemalteEierNotifications();
        [OperationContract()]
        void RegisterForSchokoHasenNotifications();
        [OperationContract()]
        void RegisterForNesterNotifications();
        [OperationContract()]
        void RegisterForLogistikNotifications();

        [OperationContract()]
        void UnRegisterFromUnbemalteEierNotifications();
        [OperationContract()]
        void UnRegisterFromBemalteEierNotifications();
        [OperationContract()]
        void UnRegisterFromSchokoHasenNotifications();
        [OperationContract()]
        void UnRegisterFromNesterNotifications();
        [OperationContract()]
        void UnRegisterFromLogistikNotifications();
    }
}
