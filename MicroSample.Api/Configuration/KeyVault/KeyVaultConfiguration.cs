using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroSample.Api.Configuration.KeyVault
{
    internal class KeyVaultConfiguration
    {
        public const  string Section = "KeyVault";

        public bool IsEnabled { get; set; }
        public string Uri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiPrefix { get; set; }
    }
}
