using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ComponentModel;

namespace Sbc11WorkflowService.Communication
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Multiple, InstanceContextMode=InstanceContextMode.Single)]
    public class OsterFarbrikService : IOsterFabrikService
    {
        #region Declarations

        private Queue<Ei> _unbemalteEier = new Queue<Ei>();
        private Queue<Ei> _bemalteEier = new Queue<Ei>();
        private Queue<SchokoHase> _schokoHasen = new Queue<SchokoHase>();
        private Queue<Nest> _nester = new Queue<Nest>();
        private Queue<Nest> _ausgeliefert = new Queue<Nest>();

        private Queue<IOsterFabrikCallback> _waitForUnbemaltesEi = new Queue<IOsterFabrikCallback>();
        private Queue<IOsterFabrikCallback> _waitForBemaltesEi = new Queue<IOsterFabrikCallback>();
        private Queue<IOsterFabrikCallback> _waitForSchokoHase = new Queue<IOsterFabrikCallback>();
        private Queue<IOsterFabrikCallback> _waitForNest = new Queue<IOsterFabrikCallback>();

        private HashSet<IOsterFabrikCallback> _unbemalteEierObserver = new HashSet<IOsterFabrikCallback>();
        private HashSet<IOsterFabrikCallback> _bemalteEierObserver = new HashSet<IOsterFabrikCallback>();
        private HashSet<IOsterFabrikCallback> _schokoHasenObserver = new HashSet<IOsterFabrikCallback>();
        private HashSet<IOsterFabrikCallback> _nesterObserver = new HashSet<IOsterFabrikCallback>();
        private HashSet<IOsterFabrikCallback> _logistikObserver = new HashSet<IOsterFabrikCallback>();

        private BackgroundWorker _bwUnbemalteEier = new BackgroundWorker() { WorkerSupportsCancellation = true };
        private BackgroundWorker _bwBemalteEier = new BackgroundWorker() { WorkerSupportsCancellation = true };
        private BackgroundWorker _bwSchokoHasen = new BackgroundWorker() { WorkerSupportsCancellation = true };
        private BackgroundWorker _bwNester = new BackgroundWorker() { WorkerSupportsCancellation = true };
        private BackgroundWorker _bwLogistik = new BackgroundWorker() { WorkerSupportsCancellation = true };

        public OsterFarbrikService()
        {
            _bwBemalteEier.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            _bwLogistik.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            _bwNester.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            _bwSchokoHasen.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            _bwUnbemalteEier.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);

            _bwBemalteEier.DoWork += new DoWorkEventHandler(_bwBemalteEier_DoWork);
            _bwLogistik.DoWork += new DoWorkEventHandler(_bwLogistik_DoWork);
            _bwNester.DoWork += new DoWorkEventHandler(_bwNester_DoWork);
            _bwSchokoHasen.DoWork += new DoWorkEventHandler(_bwSchokoHasen_DoWork);
            _bwUnbemalteEier.DoWork += new DoWorkEventHandler(_bwUnbemalteEier_DoWork);
        }

        #endregion

        private bool IsCommunicationOpened(IOsterFabrikCallback callback)
        {
            return ((ICommunicationObject)callback).State == CommunicationState.Opened;
        }

        #region Add...

        public void AddUnbemaltesEi(Ei ei)
        {
            lock (_unbemalteEier)
            {
                lock (_waitForUnbemaltesEi)
                {
                    while (_waitForUnbemaltesEi.Count > 0 && ei != null)
                    {
                        IOsterFabrikCallback callback = _waitForUnbemaltesEi.Dequeue();

                        if (IsCommunicationOpened(callback))
                        {
                            try
                            {
                                callback.ReturnUnbemaltesEi(ei);
                                ei = null;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }

                if (ei != null)
                {
                    _unbemalteEier.Enqueue(ei);
                    NotifyUnbemalteEier();
                }
            }
        }

        public void AddBemaltesEi(Ei ei)
        {
            lock (_bemalteEier)
            {
                lock (_waitForBemaltesEi)
                {
                    while (_waitForBemaltesEi.Count > 0 && ei != null)
                    {
                        IOsterFabrikCallback callback = _waitForBemaltesEi.Dequeue();

                        if (IsCommunicationOpened(callback))
                        {
                            try
                            {
                                callback.ReturnBemaltesEi(ei);
                                ei = null;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }

                if (ei != null)
                {
                    _bemalteEier.Enqueue(ei);
                    NotifyBemalteEier();
                }
            }
        }

        public void AddSchokoHase(SchokoHase schokoHase)
        {
            lock (_schokoHasen)
            {
                lock (_waitForSchokoHase)
                {
                    while (_waitForSchokoHase.Count > 0 && schokoHase != null)
                    {
                        IOsterFabrikCallback callback = _waitForSchokoHase.Dequeue();

                        if (IsCommunicationOpened(callback))
                        {
                            try
                            {
                                callback.ReturnSchokoHase(schokoHase);
                                schokoHase = null;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }

                if (schokoHase != null)
                {
                    _schokoHasen.Enqueue(schokoHase);
                    NotifySchokoHasen();
                }
            }
        }

        public void AddNest(Nest nest)
        {
            lock (_nester)
            {
                lock (_waitForNest)
                {
                    while (_waitForNest.Count > 0 && nest != null)
                    {
                        IOsterFabrikCallback callback = _waitForNest.Dequeue();

                        if (IsCommunicationOpened(callback))
                        {
                            try
                            {
                                callback.ReturnNest(nest);
                                nest = null;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }

                if (nest != null)
                {
                    _nester.Enqueue(nest);
                    NotifyNester();
                }
            }
        }

        public void NotifyNestVerschickt(Nest nest)
        {
            lock (_ausgeliefert)
            {
                _ausgeliefert.Enqueue(nest);
                DoNotifyNestVerschickt();
            }
        }

        #endregion

        #region LookFor...

        public void LookForUnbemaltesEi()
        {
            IOsterFabrikCallback callback = GetCallbackChannel();

            lock (_unbemalteEier)
            {
                if (_unbemalteEier.Count > 0)
                {
                    Ei ei = _unbemalteEier.Dequeue();
                    try
                    {
                        callback.ReturnUnbemaltesEi(ei);
                        NotifyUnbemalteEier();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        _unbemalteEier.Enqueue(ei);
                    }
                }
                else
                {
                    lock (_waitForUnbemaltesEi)
                    {
                        _waitForUnbemaltesEi.Enqueue(callback);
                    }
                }
            }
        }

        public void LookForBemaltesEi()
        {
            IOsterFabrikCallback callback = GetCallbackChannel();

            lock (_bemalteEier)
            {
                if (_bemalteEier.Count > 0)
                {
                    Ei ei = _bemalteEier.Dequeue();
                    try
                    {
                        callback.ReturnBemaltesEi(ei);
                        NotifyBemalteEier();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        _bemalteEier.Enqueue(ei);
                    }
                }
                else
                {
                    lock (_waitForBemaltesEi)
                    {
                        _waitForBemaltesEi.Enqueue(callback);
                    }
                }
            }
        }

        public void LookForSchokoHase()
        {
            IOsterFabrikCallback callback = GetCallbackChannel();

            lock (_schokoHasen)
            {
                if (_schokoHasen.Count > 0)
                {
                    SchokoHase hase = _schokoHasen.Dequeue();
                    try
                    {
                        callback.ReturnSchokoHase(hase);
                        NotifySchokoHasen();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        _schokoHasen.Enqueue(hase);
                    }
                }
                else
                {
                    lock (_waitForSchokoHase)
                    {
                        _waitForSchokoHase.Enqueue(callback);
                    }
                }
            }
        }

        public void LookForNest()
        {
            IOsterFabrikCallback callback = GetCallbackChannel();

            lock (_nester)
            {
                if (_nester.Count > 0)
                {
                    Nest nest = _nester.Dequeue();
                    try
                    {
                        callback.ReturnNest(nest);
                        NotifyNester();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        _nester.Enqueue(nest);
                    }
                }
                else
                {
                    lock (_waitForNest)
                    {
                        _waitForNest.Enqueue(callback);
                    }
                }
            }
        }

        #endregion

        private IOsterFabrikCallback GetCallbackChannel()
        {
            return OperationContext.Current.GetCallbackChannel<IOsterFabrikCallback>();
        }

        #region Regsiter / Unregister

        public void RegisterForUnbemalteEierNotifications()
        {
            lock (_unbemalteEierObserver)
            {
                _unbemalteEierObserver.Add(GetCallbackChannel());
            }
        }

        public void RegisterForBemalteEierNotifications()
        {
            lock (_bemalteEierObserver)
            {
                _bemalteEierObserver.Add(GetCallbackChannel());
            }
        }

        public void RegisterForSchokoHasenNotifications()
        {
            lock (_schokoHasenObserver)
            {
                _schokoHasenObserver.Add(GetCallbackChannel());
            }
        }

        public void RegisterForNesterNotifications()
        {
            lock (_nesterObserver)
            {
                _nesterObserver.Add(GetCallbackChannel());
            }
        }

        public void RegisterForLogistikNotifications()
        {
            lock (_logistikObserver)
            {
                _logistikObserver.Add(GetCallbackChannel());
            }
        }

        public void UnRegisterFromUnbemalteEierNotifications()
        {
            lock (_unbemalteEierObserver)
            {
                _unbemalteEierObserver.Remove(GetCallbackChannel());
            }
        }

        public void UnRegisterFromBemalteEierNotifications()
        {
            lock (_bemalteEierObserver)
            {
                _bemalteEierObserver.Remove(GetCallbackChannel());
            }
        }

        public void UnRegisterFromSchokoHasenNotifications()
        {
            lock (_schokoHasenObserver)
            {
                _schokoHasenObserver.Remove(GetCallbackChannel());
            }
        }

        public void UnRegisterFromNesterNotifications()
        {
            lock (_nesterObserver)
            {
                _nesterObserver.Remove(GetCallbackChannel());
            }
        }

        public void UnRegisterFromLogistikNotifications()
        {
            lock (_logistikObserver)
            {
                _logistikObserver.Remove(GetCallbackChannel());
            }
        }

        #endregion

        #region Notifications

        private void NotifyUnbemalteEier()
        {
            lock (_bwUnbemalteEier)
            {
                if (_bwUnbemalteEier.IsBusy)
                    _bwUnbemalteEier.CancelAsync();
                else if (!_bwUnbemalteEier.CancellationPending)
                    _bwUnbemalteEier.RunWorkerAsync();
            }
        }

        private void NotifyBemalteEier()
        {
            lock (_bwBemalteEier)
            {
                if (_bwBemalteEier.IsBusy)
                    _bwBemalteEier.CancelAsync();
                else if (!_bwBemalteEier.CancellationPending)
                    _bwBemalteEier.RunWorkerAsync();
            }
        }

        private void NotifySchokoHasen()
        {
            lock (_bwSchokoHasen)
            {
                if (_bwSchokoHasen.IsBusy)
                    _bwSchokoHasen.CancelAsync();
                else if (!_bwSchokoHasen.CancellationPending)
                    _bwSchokoHasen.RunWorkerAsync();
            }
        }

        private void NotifyNester()
        {
            lock (_bwNester)
            {
                if (_bwNester.IsBusy)
                    _bwNester.CancelAsync();
                else if (!_bwNester.CancellationPending)
                    _bwNester.RunWorkerAsync();
            }
        }

        private void DoNotifyNestVerschickt()
        {
            lock (_bwLogistik)
            {
                if (_bwLogistik.IsBusy)
                    _bwLogistik.CancelAsync();
                else if (!_bwLogistik.CancellationPending)
                    _bwLogistik.RunWorkerAsync();
            }
        }

        void _bwUnbemalteEier_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (_unbemalteEierObserver)
            {
                _unbemalteEierObserver.AsParallel().ForAll((callback) =>
                    {
                        if (_bwUnbemalteEier.CancellationPending)
                            return;

                        if (IsCommunicationOpened(callback))
                        {
                            try
                            {
                                callback.NotifyUnbemaltesEiChanged(_unbemalteEier.Count);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                _unbemalteEierObserver.Remove(callback);
                            }
                        }
                        else
                            _unbemalteEierObserver.Remove(callback);
                    });
            }
        }

        void _bwSchokoHasen_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (_schokoHasenObserver)
            {
                _schokoHasenObserver.AsParallel().ForAll((callback) =>
                {
                    if (_bwSchokoHasen.CancellationPending)
                        return;

                    if (IsCommunicationOpened(callback))
                    {
                        try
                        {
                            callback.NotifySchokoHaseChanged(_schokoHasen.Count);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            _schokoHasenObserver.Remove(callback);
                        }
                    }
                    else
                        _schokoHasenObserver.Remove(callback);
                });
            }
        }

        void _bwNester_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (_nesterObserver)
            {
                _nesterObserver.AsParallel().ForAll((callback) =>
                {
                    if (_bwNester.CancellationPending)
                        return;

                    if (IsCommunicationOpened(callback))
                    {
                        try
                        {
                            callback.NotifyNestChanged(_nester.Count);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            _nesterObserver.Remove(callback);
                        }
                    }
                    else
                        _nesterObserver.Remove(callback);
                });
            }
        }

        void _bwLogistik_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (_logistikObserver)
            {
                _logistikObserver.AsParallel().ForAll((callback) =>
                {
                    if (_bwLogistik.CancellationPending)
                        return;

                    if (IsCommunicationOpened(callback))
                    {
                        try
                        {
                            callback.NotifyLogistikChanged(_ausgeliefert.Count);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            _logistikObserver.Remove(callback);
                        }
                    }
                    else
                        _logistikObserver.Remove(callback);
                });
            }
        }

        void _bwBemalteEier_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (_bemalteEierObserver)
            {
                _bemalteEierObserver.AsParallel().ForAll((callback) =>
                {
                    if (_bwBemalteEier.CancellationPending)
                        return;

                    if (IsCommunicationOpened(callback))
                    {
                        try
                        {
                            callback.NotifyBemaltesEiChanged(_bemalteEier.Count);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            _bemalteEierObserver.Remove(callback);
                        }
                    }
                    else
                        _bemalteEierObserver.Remove(callback);
                });
            }
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                ((BackgroundWorker)sender).RunWorkerAsync();
        }

        #endregion
    }
}
