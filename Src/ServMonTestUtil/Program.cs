﻿using System;
using ServMonAPI;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            ServMonAPI.Utilities.NICApi.ListNICs();
            //ServMonAPI.Utilities.NICApi.MonitorNetworkIO();
        }
    }
}