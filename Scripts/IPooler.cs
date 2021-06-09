﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressoBits.Pools
{
    public interface IPooler
    {
        /// <summary>
        /// Called when pool object is enable
        /// </summary>
        void OnPoolerEnable();

        /// <summary>
        /// Called when pool object is disabled
        /// </summary>
        void OnPoolerDisable();
    }
}
