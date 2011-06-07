using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePortScan
{
    class SimplePortScanController : ISimplePortScanController
    {

        public bool InputValuesAcceptable(string target, string startPort, string endPort)
        {
            ushort StartingPort;
            bool StartResult = ushort.TryParse(startPort, out StartingPort);

            ushort EndingPort;
            bool EndResult = ushort.TryParse(endPort, out EndingPort);

            if ((StartResult && EndResult) && 
                (EndingPort > StartingPort) &&
                (StartingPort > 0))
                return true;

            return false;
        }

        public IEnumerable<ScannedPort> ScanPorts(string target, string startPort, string endPort)
        {
            if (InputValuesAcceptable(target, startPort, endPort))
                return MultiPortScanner.ScanPorts(IPAddressResolver.ResolveIpAddress(target), 
                    ushort.Parse(startPort), ushort.Parse(endPort));

            throw new ArgumentException("Unable to perform port scan. One or more arguments are invalid.");
        }
    }
}
