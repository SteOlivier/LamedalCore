LamedaL (Software done right)
================================
* Project : https://sites.google.com/site/lamedalwiki/
* LamedaL Nuget package: https://www.nuget.org/packages/LamedalCore/
* PM> Install-Package LamedalCore

# Usage

Sample OWIN Startup class
-------------------------

using System;
using LamedalCore;

namespace a24_Core_JsonEdit
{
    public class Program
    {
        private static readonly LamedalCore_ _lamed = LamedalCore_.Instance;
        
        public static void Main(string[] args)
        {
            _lamed.About_();
            
            // Methods dot show more info
            // ==========================
            _lamed.lib.About.*

            // Console menthods
            // ================
            _lamed.lib.Command.*

        }
    }
}
