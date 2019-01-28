namespace eV.Measure.sys
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public sealed class PrimeNumbers : IEnumerable<int>
    {
        #region Implementation of IEnumerable
        public IEnumerator<int> GetEnumerator()
        {
            yield return 2;

            var enumerator = OddInts().GetEnumerator();
            while (enumerator.MoveNext())
            {
                var x = enumerator.Current;
                var sqrt = Math.Sqrt(x);
                if (OddInts().TakeWhile(y => y <= sqrt).All(y => x % y != 0)) yield return x;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region PRIVATE SUPPORT METHODS

        public static IEnumerable<int> OddInts()
        {
            var start = 1;
            while (start > 0) unchecked
            {
                yield return start += 2;
            }
        }

        #endregion
    }
}