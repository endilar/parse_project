using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebApplication7.Entities.Enums
{
    /// <summary>
    /// ParserTypeEnum:
    /// - 1 - Comfy
    /// - 2 - Foxtrot
    /// - 3 - Hotline
    /// </summary>
    public enum ParserTypeEnum : int
    {
        [XmlEnum("1")]
        [Description("Comfy")]
        Comfy = 1,

        [XmlEnum("2")]
        [Description("Foxtrot")]
        Foxtrot = 2,

        [XmlEnum("3")]
        [Description("Hotline")]
        Hotline = 3,
    }
}
