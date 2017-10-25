using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xero.Api.Core;
using Xero.Api.Example.Applications.Partner;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Private Application Sample
            var private_app_api = new XeroCoreApi("https://api.xero.com", new PrivateAuthenticator(@"C:\Dev\your_public_privatekey.pfx"),
                new Consumer("your-consumer-key", "your-consumer-secret"), null,
                new DefaultMapper(), new DefaultMapper());

            var org = private_app_api.Organisation;

            var user = new ApiUser { Name = Environment.MachineName };

            // Public Application Sample
            var public_app_api = new XeroCoreApi("https://api.xero.com", new PublicAuthenticator("https://api.xero.com", "https://api.xero.com", "oob",
                new MemoryTokenStore()),
                new Consumer("your-consumer-key", "your-consumer-secret"), user,
                new DefaultMapper(), new DefaultMapper());

            var public_contacts = public_app_api.Contacts.Find().ToList();

            // Partner Application Sample
            var partner_app_api = new XeroCoreApi("https://api-partner.network.xero.com", new PartnerAuthenticator("https://api-partner.network.xero.com",
                "https://api.xero.com", "oob", new MemoryTokenStore(),
                @"C:\Dev\your_public_privatekey.pfx"),
                 new Consumer("your-consumer-key", "your-consumer-secret"), user,
                 new DefaultMapper(), new DefaultMapper());

            var partner_contacts = partner_app_api.Contacts.Find().ToList();
        }
    }
}
