﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServMonAPI.Models
{
    public class CPUDevice
    {
        public Dictionary<string,string> CPUDeviceProperties { get; private set; }

        public CPUDevice(Dictionary<string, string> ProcessorDeviceProperties)
        {
            CPUDeviceProperties = ProcessorDeviceProperties;
        }

        public static string FormatMemoryBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }
}