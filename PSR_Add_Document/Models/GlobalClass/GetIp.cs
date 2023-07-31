using System.Net.Sockets;
using System.Net;

namespace PSR_Add_Document.Models.GlobalClass
{
    public static class GetIpAddress
    {
        private static string GetIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddresses = new List<string>();

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddresses.Add(ip.ToString());
                }
            }

            string ip2 = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();
            if (ip2 != null)
            {
                ipAddresses.Add(ip2);
                // Assign the value of ip2 to the SubIP field
            }

            return ipAddresses.FirstOrDefault(); // Return the first IP address from the list, or null if the list is empty
        }
    }
}
