namespace eV.Measure.sys
{
    using AmountType = System.Single;
    public class Constants
    {
        public const float Zero = 0.0f;
        public const float One = 1.0f;
        public const float Two = 2.0f;
        public const float Half = 0.5f;
        public static readonly float MachineEpsilon;

        #region CONSTRUCTORS

        static Constants()
        {
            var machEps = One;

            do
            {
                machEps *= Half;
            }
            while (One + Half * machEps != One);

            MachineEpsilon = machEps;
        }

        #endregion
    }
}