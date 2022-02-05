using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSCounter
{
    public abstract class Counter
    {
        private CPSClient client;

        public void SetClient(CPSClient client) { this.client = client; }

        public virtual void Initialize() { }
        public virtual void OnClick(int cps) { }
        public virtual void OnClickRemoved(int cps) { }

        protected int GetCPS()
        {
            return client.cps.Count();
        }
    }
}
