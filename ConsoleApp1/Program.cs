using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Xero.Api.Core;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Private Application Sample

            X509Certificate2 cert = new X509Certificate2();
            cert.Import(@"..\public_privatekey.pfx", "NMCuong@1992", X509KeyStorageFlags.MachineKeySet);

            var private_app_api = new XeroCoreApi("https://api.xero.com/api.xro/2.0/", new PrivateAuthenticator(cert),
                new Consumer("WQLCRSSUUFJFEFHQ7KHKPV43GFJC7F", "ML6MSKXJOYBH5ZBFYHZXH5Z3WGTGPM"), null,
                new DefaultMapper(), new DefaultMapper());

            var org = private_app_api.Organisation;

            var invoices = private_app_api.Invoices.OrderByDescending("Total").Find();
            Console.WriteLine(invoices.Count());

            foreach (var invoice in invoices)
            {
                Console.WriteLine(invoice.Contact.Name + ", " + invoice.Contact.Name);
            }


            var user = new ApiUser { Name = Environment.MachineName };

            // Public Application Sample
            //var public_app_api = new XeroCoreApi("https://api.xero.com", new PublicAuthenticator("https://api.xero.com", "https://api.xero.com", "oob",
            //    new MemoryTokenStore()),
            //    new Consumer("your-consumer-key", "your-consumer-secret"), user,
            //    new DefaultMapper(), new DefaultMapper());

            //var public_contacts = public_app_api.Contacts.Find().ToList();

            // Partner Application Sample
            //var partner_app_api = new XeroCoreApi("https://api-partner.network.xero.com", new PartnerAuthenticator("https://api-partner.network.xero.com",
            //    "https://api.xero.com", "oob", new MemoryTokenStore(),
            //    @"C:\Dev\your_public_privatekey.pfx"),
            //     new Consumer("your-consumer-key", "your-consumer-secret"), user,
            //     new DefaultMapper(), new DefaultMapper());

            //var partner_contacts = partner_app_api.Contacts.Find().ToList();
        }
    }
}
