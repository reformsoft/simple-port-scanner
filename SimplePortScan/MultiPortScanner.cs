using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

namespace SimplePortScan
{ 
    class MultiPortScanner
    {
        public static IEnumerable<ScannedPort> ScanPorts(IPAddress target, ushort startPort, ushort endPort)
        {
            int max = endPort - startPort + 1;
            PortScanner[] portScannerArray = new PortScanner[max];
            Parallel.For(0, endPort, i =>
            {
                PortScanner ps = new PortScanner(target, i + startPort);
                portScannerArray[i] = ps;
                ps.ScanPort();
            });

            return (from ps in portScannerArray select new ScannedPort(ps.Port, ps.IsOpen)).ToList();
        }
    }
}
