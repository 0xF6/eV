namespace eV.Measure.sys
{
    using System;
    using System.Collections.Generic;
    using AmountType = System.Single;
    public enum UnitPrefix
    {
        Yocto = -24,
        Zepto = -21,
        Atto = -18,
        Femto = -15,
        Pico = -12,
        Nano = -9,
        Micro = -6,
        Milli = -3,
        Centi = -2,
        Deci = -1,
        Deka = 1,
        Hecto = 2,
        Kilo = 3,
        Mega = 6,
        Giga = 9,
        Tera = 12,
        Peta = 15,
        Exa = 18,
        Zetta = 21,
        Yotta = 24
    }

    internal static class UnitPrefixMethods
    {
        #region STATIC MEMBER VARIABLES

        private static readonly Dictionary<UnitPrefix, string> smkPrefixSymbolsMap;

        #endregion

        #region CONSTRUCTORS

        static UnitPrefixMethods()
        {
            smkPrefixSymbolsMap = new Dictionary<UnitPrefix, string>
              {
                  { UnitPrefix.Yotta, "Y" },
                  { UnitPrefix.Zetta, "Z" },
                  { UnitPrefix.Exa, "E" },
                  { UnitPrefix.Peta, "P" },
                  { UnitPrefix.Tera, "T" },
                  { UnitPrefix.Giga, "G" },
                  { UnitPrefix.Mega, "M" },
                  { UnitPrefix.Kilo, "k" },
                  { UnitPrefix.Hecto, "h" },
                  { UnitPrefix.Deka, "da" },
                  { UnitPrefix.Deci, "d" },
                  { UnitPrefix.Centi, "c" },
                  { UnitPrefix.Milli, "m" },
                  { UnitPrefix.Micro, "µ" },
                  { UnitPrefix.Nano, "n" },
                  { UnitPrefix.Pico, "p" },
                  { UnitPrefix.Femto, "f" },
                  { UnitPrefix.Atto, "a" },
                  { UnitPrefix.Zepto, "z" },
                  { UnitPrefix.Yocto, "y" }
              };
        }

        #endregion

        #region EXTENSION METHODS

        /// <summary>
        /// Gets printed symbol associated with unit prefix
        /// </summary>
        /// <param name="iPrefix">Requested unit prefix</param>
        /// <returns>Unit prefix symbol</returns>
        public static string GetSymbol(this UnitPrefix iPrefix) 
            => smkPrefixSymbolsMap[iPrefix];

        /// <summary>
        /// Gets scaling factor associated with unit prefix
        /// </summary>
        /// <param name="iPrefix">Requested unit prefix</param>
        /// <returns>Unit prefix scaling factor</returns>
        public static float GetFactor(this UnitPrefix iPrefix) 
            => (float)Math.Pow(x: 10.0, y: (double)iPrefix);

        #endregion
    }
}