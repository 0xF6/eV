namespace eV.Measure.sys
{
    using AmountType = System.Single;
    using System.Collections.Generic;
    using System.Linq;
    public static class MeasureEnumerable
    {
        #region METHODS

        /// <summary>
        /// Cast collection of amounts into corresponding measure enumerable
        /// </summary>
        /// <typeparam name="Q">Quantity of measurements</typeparam>
        /// <param name="amounts">Collection of amounts to be cast into a collection of standard measures</param>
        /// <returns>Collection of standard measures</returns>
        public static IEnumerable<Q> Cast<Q>(this IEnumerable<double> amounts)
            where Q : class, IQuantity<Q>, IMeasure<Q>, new()
        {
            var factory = new Q().Factory;
            return amounts.Select(val => factory.New(val));
        }

        /// <summary>
        /// Cast collection of amounts into corresponding measure enumerable
        /// </summary>
        /// <typeparam name="Q">Quantity of measurements</typeparam>
        /// <param name="amounts">Collection of amounts to be cast into a collection of standard measures</param>
        /// <param name="unit">Unit in which the amounts are specified</param>
        /// <returns>Collection of standard measures, converted from specified unit into quantity's standard unit</returns>
        public static IEnumerable<Q> Cast<Q>(this IEnumerable<double> amounts, IUnit<Q> unit)
            where Q : class, IQuantity<Q>, IMeasure<Q>, new()
        {
            var factory = new Q().Factory;
            return amounts.Select(val => factory.New(factory.New(val, unit).StandardAmount));
        }

        /// <summary>
        /// Cast collection of amounts into corresponding StandardMeasure enumerable
        /// </summary>
        /// <typeparam name="Q">Quantity of measurements</typeparam>
        /// <param name="amounts">Collection of amounts to be cast into a collection of standard measures</param>
        /// <returns>Collection of standard measures</returns>
        public static IEnumerable<Q> Cast<Q>(this IEnumerable<float> amounts) where Q : class, IQuantity<Q>, IMeasure<Q>, new()
        {
            var factory = new Q().Factory;
            return amounts.Select(val => factory.New(val));
        }

        /// <summary>
        /// Cast collection of amounts into corresponding StandardMeasure enumerable
        /// </summary>
        /// <typeparam name="Q">Quantity of measurements</typeparam>
        /// <param name="amounts">Collection of amounts to be cast into a collection of standard measures</param>
        /// <param name="unit">Unit in which the amounts are specified</param>
        /// <returns>Collection of standard measures, converted from specified unit into quantity's standard unit</returns>
        public static IEnumerable<Q> Cast<Q>(this IEnumerable<float> amounts, IUnit<Q> unit)
            where Q : class, IQuantity<Q>, IMeasure<Q>, new()
        {
            var factory = new Q().Factory;
            return amounts.Select(val => factory.New(factory.New(val, unit).StandardAmount));
        }

        /// <summary>
        /// Cast collection of amounts into corresponding StandardMeasure enumerable
        /// </summary>
        /// <typeparam name="Q">Quantity of measurements</typeparam>
        /// <param name="amounts">Collection of amounts to be cast into a collection of standard measures</param>
        /// <returns>Collection of standard measures</returns>
        public static IEnumerable<Q> Cast<Q>(this IEnumerable<decimal> amounts)
            where Q : class, IQuantity<Q>, IMeasure<Q>, new()
        {
            var factory = new Q().Factory;
            return amounts.Select(val => factory.New(val));
        }

        /// <summary>
        /// Cast collection of amounts into corresponding StandardMeasure enumerable
        /// </summary>
        /// <typeparam name="Q">Quantity of measurements</typeparam>
        /// <param name="amounts">Collection of amounts to be cast into a collection of standard measures</param>
        /// <param name="unit">Unit in which the amounts are specified</param>
        /// <returns>Collection of standard measures, converted from specified unit into quantity's standard unit</returns>
        public static IEnumerable<Q> Cast<Q>(this IEnumerable<decimal> amounts, IUnit<Q> unit)
            where Q : class, IQuantity<Q>, IMeasure<Q>, new()
        {
            var factory = new Q().Factory;
            return amounts.Select(val => factory.New(factory.New(val, unit).StandardAmount));
        }

        /// <summary>
        /// Get enumerable of amounts in standard unit from collection of measures.
        /// </summary>
        /// <typeparam name="Q">Quantity of measures.</typeparam>
        /// <param name="measures">Collection of measures.</param>
        /// <returns>Enumerable of amounts in standard unit.</returns>
        public static IEnumerable<float> Cast<Q>(this IEnumerable<IMeasure<Q>> measures)
            where Q : class, IQuantity<Q>, new()
        {
            return measures.Select(measure => measure.StandardAmount);
        }

        /// <summary>
        /// Get enumerable of amounts in specified <paramref name="unitOfAmounts">unit of amounts</paramref>.
        /// </summary>
        /// <typeparam name="Q">Quantity of measures.</typeparam>
        /// <param name="measures">Collection of measures.</param>
        /// <param name="unitOfAmounts">Unit in which the amounts should be given.</param>
        /// <returns>Enumerable of amounts in specified <paramref name="unitOfAmounts">unit of amounts</paramref>.</returns>
        public static IEnumerable<float> Cast<Q>(this IEnumerable<IMeasure<Q>> measures, IUnit<Q> unitOfAmounts)
            where Q : class, IQuantity<Q>, new()
        {
            return measures.Select(measure => measure.GetAmount(unitOfAmounts));
        }

        #endregion
    }
}