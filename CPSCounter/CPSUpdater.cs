using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CPSCounter
{
    public class CPSUpdater
    {
        private Thread netRead = null;
        List<CPSClient> _handlers = new List<CPSClient>();
        public void AddHandler(CPSClient _handler)
        {
            this._handlers.Add(_handler);
        }
        #region Системное
        public void Stop()
        {
            if (netRead != null)
            {
                netRead.Abort();
                netRead = null;
            }
        }
        public void StartUpdater()
        {
            if (netRead == null)
            {
                netRead = new Thread(new ThreadStart(Updater));
                netRead.Name = "Updater";
                netRead.Start();
            }
        }
        private void Updater()
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                while (true)
                {
                    stopWatch.Start();
                    foreach (var client in _handlers)
                    {
                        client.OnUpdate();
                    }
                    stopWatch.Stop();
                    int elapsed = stopWatch.Elapsed.Milliseconds;
                    stopWatch.Reset();
                    if (elapsed < 1)
                    {
                        Thread.Sleep(1 - elapsed);
                    }
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
        }
        #endregion
    }
}
