using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public static class MathHelper
    {
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}