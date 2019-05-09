using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressoBits.PoolSimply
{
    public interface IPooler
    {
        void OnPoolerEnable();
        void OnPoolerDisable();
    }
}
