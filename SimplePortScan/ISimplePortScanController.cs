using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePortScan
{
    interface ISimplePortScanController
    {
        bool InputValuesAcceptable(string target, string startPort, string endPort);
        IEnumerable<ScannedPort> ScanPorts(string target, string startPort, string endPort);
    }
}
