using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SensorServices.Rfid.Management;
using Microsoft.SensorServices.Rfid.Design;
using Microsoft.SensorServices.Rfid.Dspi;

namespace SyncSample2
{
    class Program
    {
        static public void DumpProviderCustomCommands(string provider)
        {
            ProviderManagerProxy pmp = new ProviderManagerProxy();
            ProviderMetadata meta = pmp.GetProviderMetadata(provider);

            // Dump the capability list
            Console.WriteLine("Capabilities for provider {0}", provider);
            foreach (ProviderCapability c in meta.ProviderCapabilities)
            {
                Console.WriteLine("Capability: {0}", c);
            }

            // Dump the vendor extensibility info
            DumpVendorInfo(meta.VendorExtensionsEntityMetadata);
        }

        private static void DumpVendorInfo(Dictionary<VendorEntityKey,
            VendorEntityMetadata> dictionary)
        {
            foreach (VendorEntityKey k in dictionary.Keys)
            {
                if (k.EntityType == EntityType.Command)
                {
                    Console.WriteLine("\r\n----------------------");
                    Console.WriteLine("Custom command: {0}", k.Name);
                    VendorEntityMetadata meta = dictionary[k];
                    Console.WriteLine("Description: {0}", meta.Description);
                    // Some commands do not have parameters
                    if (meta.SubEntities == null)
                    {
                        Console.WriteLine("-> No parameters");
                        continue;
                    }
                    foreach (string parm in meta.SubEntities.Keys)
                    {
                        Console.WriteLine("\r\n=======================");
                        Console.WriteLine("Parameter {0}", parm);
                        DumpParameterInfo(meta.SubEntities[parm]);
                    }
                }
            }
        }

        public static void DumpParameterInfo(VendorEntityParameterMetadata meta)
        {
            // Dump the standard metadata items
            Console.WriteLine("Description:\r\n-------------");
            Console.WriteLine(meta.Description);
            Console.WriteLine("\r\nType: {0}", meta.Type.Name);
            Console.WriteLine("Default: {0}",
            meta.DefaultValue == null ? "None" : meta.DefaultValue.ToString());

            Console.WriteLine(" {0}, {1}, {2}, {3}",
            meta.IsMandatory ? "Mandatory" : "Optional",
            meta.IsPersistent ? "Persistent" : "Temporary",
            meta.IsWritable ? "Writable" : "Read-only",
            meta.RequiresRestart ? "Requires Restart" : "Immediate");

            // Dump the validation metadata (ranges, value sets, etc.)
            if (meta.Type == typeof(string))
            {
                // If the type has a valid set of values, print them
                if (meta.ValueSet != null && meta.ValueSet.Count > 0)
                {
                    Console.Write("Valid values: ");
                    foreach (object o in meta.ValueSet)
                        Console.Write("{0}, ", o.ToString());
                }

                // If the type has a regex validation pattern, print it
                if (meta.ValueExpression != null)
                    Console.WriteLine("Valid pat: {0}", meta.ValueExpression.ToString());
            }

            if (meta.Type == typeof(int) || meta.Type == typeof(double))
            {
                if (meta.HigherRange != double.MaxValue)
                    Console.WriteLine("Max value: {0}", meta.HigherRange);
                if (meta.LowerRange != double.MinValue)
                    Console.WriteLine("Min value: {0}", meta.LowerRange);
            }
        }

        static void Main(string[] args)
        {
            DumpProviderCustomCommands("Alien");
        }

    }
}
