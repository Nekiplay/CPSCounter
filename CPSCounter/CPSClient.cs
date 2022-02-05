using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSCounter
{
    public class CPSClient
    {
        public CPSClient()
        {
            MouseHook.Start();
            MouseHook.MouseAction += new EventHandler(Event);
        }
        private void Event(object sender, EventArgs e) 
        {
            long time = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds + 1000L; 
            clicks.Enqueue(time);
            foreach (Counter counter in counters)
            {
                counter.OnClick(clicks.Count());
            }
        }
        private Queue<long> clicks = new Queue<long>();
        public Queue<long> cps
        {
            get
            {
                return clicks;
            }
        }
        private List<Counter> counters = new List<Counter>();

        public void LoadCounter(Counter counter)
        {
            counter.SetClient(this);
            counters.Add(counter);
            counter.Initialize();
        }
        public void getCps()
        {
            long time = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;

            if (clicks.Count != 0)
            {
                time = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                long peek = clicks.Peek();
                //Console.WriteLine(peek + " & " + time);
                if (peek < time)
                {
                    clicks.Dequeue();
                    foreach (Counter counter in counters)
                    {
                        counter.OnClickRemoved(clicks.Count());
                    }
                }
            }
        }
        public void OnUpdate()
        {
            getCps();
        }
    }
    
}
