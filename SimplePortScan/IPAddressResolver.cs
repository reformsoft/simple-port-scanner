using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace SimplePortScan
{
    class IPAddressResolver
    {
        static bool IsAnIpAddress(string address)
        {
            IPAddress ipAddress = null;
            return IPAddress.TryParse(address, out ipAddress);
        }

        static IPAddress GetIpAddressFromHostName(string scanAddress)
        {
            IPHostEntry NameToIpAddress = Dns.GetHostEntry(scanAddress);

            if (NameToIpAddress.AddressList.Length > 0)
            {
                // filter out IPv6
                var address = NameToIpAddress.AddressList.Where(a => a.AddressFamily ==
                    System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();

                if (address != null)
                    return address;
            }

            throw new ArgumentException(string.Format("Unable to lookup host name '{0}'.", scanAddress));
        }


        public static IPAddress ResolveIpAddress(string target)
        {
            if (IsAnIpAddress(target))
                return IPAddress.Parse(target);

            try
            {
                var ipAddress = GetIpAddressFromHostName(target);
                return ipAddress;
            }
            catch (ArgumentException)
            {
                // Unable to lookup name, we're returning null instead of causing a fuss
                // other exceptions are valid/unexpected
            }

            return null;
        }
    }
}
