using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Sbc11WorkflowService.Communication
{
    public interface IOsterFabrikCallback
    {
        [OperationContract()]
        void NotifyUnbemaltesEiChanged(int currentCount);
        [OperationContract()]
        void NotifyBemaltesEiChanged(int currentCount);
        [OperationContract()]
        void NotifySchokoHaseChanged(int currentCount);
        [OperationContract()]
        void NotifyNestChanged(int currentcount);
        [OperationContract()]
        void NotifyLogistikChanged(int currentlyDelivered);

        [OperationContract()]
        void ReturnUnbemaltesEi(Ei ei);
        [OperationContract()]
        void ReturnBemaltesEi(Ei ei);
        [OperationContract()]
        void ReturnSchokoHase(SchokoHase hase);
        [OperationContract()]
        void ReturnNest(Nest nest);
    }
}
