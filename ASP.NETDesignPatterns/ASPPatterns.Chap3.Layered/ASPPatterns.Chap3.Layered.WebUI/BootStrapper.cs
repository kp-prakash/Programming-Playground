using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;

namespace ASPPatterns.Chap3.Layered.WebUI
{
    public class BootStrapper
    {
        public static void ConfigureStructureMap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ProductRegistry>();
            });
        }
    }
}