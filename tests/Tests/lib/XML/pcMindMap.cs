using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;

namespace LamedalCore.Test.Tests.lib.XML
{
    /// <summary>
    /// public class
    /// </summary>
    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_State)]
    public sealed class pcMindMap
    {
        public XElement mm;
        public Dictionary<string, XElement>  mmDictionary = new Dictionary<string, XElement>();

        public pcMindMap(XElement mindmap)
        {
            this.mm = mindmap;
        }
    }
}
