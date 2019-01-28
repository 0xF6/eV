namespace eV.Measure.sys
{
    using System;

    public static class Factors
    {
        public const float One = 1.0f;
        public const float Yotta = 1.0e24f;
        public const float Zetta = 1.0e21f;
        public const float Exa = 1.0e18f;
        public const float Peta = 1.0e15f;
        public const float Tera = 1.0e12f;
        public const float Giga = 1.0e9f;
        public const float Mega = 1.0e6f;
        public const float Kilo = 1.0e3f;
        public const float Hecto = 1.0e2f;
        public const float Deka = 1.0e1f;
        public const float Deci = 1.0e-1f;
        public const float Centi = 1.0e-2f;
        public const float Milli = 1.0e-3f;
        public const float Micro = 1.0e-6f;
        public const float Nano = 1.0e-9f;
        public const float Pico = 1.0e-12f;
        public const float Femto = 1.0e-15f;
        public const float Atto = 1.0e-18f;
        public const float Zepto = 1.0e-21f;
        public const float Yocto = 1.0e-24f;
        public const float SecondsPerMinute = 60.0f;
        public const float SecondsPerHour = SecondsPerMinute * 60.0f;
        public const float SecondsPerDay = SecondsPerHour * 24.0f;
        public const float SecondsPerWeek = SecondsPerDay * 7.0f;
        public const float SecondsPerJulianYear = SecondsPerDay * 365.25f;
        public const float KelvinCelsiusIntercept = 273.15f;
        public const float KelvinFahrenheitIntercept = 459.67f;
        public const float KelvinFahrenheitSlope = 5.0f / 9.0f;
        public const float CoulombsPerElementaryCharge = 1.6021765314e-19f;
        public const float JoulesPerElectronVolt = CoulombsPerElementaryCharge;
        public const float MetersPerAngstrom = 1.0e-10f;
        public const float MetersPerInch = 0.0254f;
        public const float MetersPerFoot = MetersPerInch * 12.0f;
        public const float MetersPerYard = MetersPerFoot * 3.0f;
        public const float MetersPerMile = MetersPerYard * 1760.0f;
        public const float MetersPerNauticalMile = 1852.0f;
        public const float SquareMetersPerBarn = 1.0e-28f;
        public const float CubicMetersPerUSLiquidGallon = 3.78541178e-3f;
        public const float KiloGramsPerElectronMass = 9.109382616e-31f;
        public const float KiloGramsPerAtomicMassUnit = 1.6605388628e-27f;
        public const float BecquerelPerCurie = 3.7e10f;
        public static readonly float RadiansPerDegree = (float)Math.PI / 180.0f;
        public static readonly float RadiansPerMinute = RadiansPerDegree / 60.0f;
        public static readonly float RadiansPerSecond = RadiansPerMinute / 60.0f;

        public static float Square(float a) { return a * a; }
        public static float Cube(float a) { return a * a * a; }
    }
}