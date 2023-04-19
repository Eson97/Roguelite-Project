using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

namespace ExtensionMethods
{
    public static class IntExtensions
    {
        public static int bit(this int i) => i == 0 ? 0 : 1;
        public static int bitNeg(this int i) => i == 0 ? 1 : 0;
    }
}
