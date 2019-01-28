namespace eV.Measure.sys
{
    using System.Linq;
    using System.Reflection;
    using AmountType = System.Single;
    internal static class UnitHelpers
    {
        internal static string CreateUnitDisplayName(IUnit unit)
        {
            var fieldInfo =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => type.IsInstanceOfType(unit.Quantity) && !type.IsInterface)
                    .SelectMany(type => type.GetFields(BindingFlags.Public | BindingFlags.Static))
                    .SingleOrDefault(info => ReferenceEquals(info.GetValue(obj: null), unit));

            return fieldInfo == null
                ? unit.Symbol
                : $"{fieldInfo.Name} | {(string.IsNullOrWhiteSpace(unit.Symbol) ? "<none>" : unit.Symbol)}";
        }
    }
    public abstract class Unit<Q> : IUnit<Q> where Q : class, IQuantity<Q>, new()
    {
        #region FIELDS

        // ReSharper disable once InconsistentNaming
        private static readonly Q quantity = new Q();

        private readonly bool isStandardUnit;

        private readonly string symbol;

        private string displayName;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initialize a physical unit object that is the standard unit of the specific quantity
        /// </summary>
        /// <param name="isStandardUnit">Indicates whether this is a standard unit.</param>
        /// <param name="symbol">Unit display symbol.</param>
        protected Unit(bool isStandardUnit, string symbol)
        {
            this.isStandardUnit = isStandardUnit;
            this.symbol = symbol;
            this.displayName = null;
        }

        #endregion

        #region Implementation of IUnit<Q>

        /// <summary>
        /// Gets the quantity associated with the unit
        /// </summary>
        IQuantity IUnit.Quantity => quantity;

        /// <summary>
        /// Gets a value indicating whether or not the unit is a standard unit of the associated quantity
        /// </summary>
        public bool IsStandardUnit => this.isStandardUnit;

        /// <summary>
        /// Gets the typed quantity associated with the unit
        /// </summary>
        public IQuantity<Q> Quantity => quantity;

        /// <summary>
        /// Gets the display symbol of the unit
        /// </summary>
        public string Symbol => this.symbol;

        /// <summary>
        /// Gets the fully qualified display name of the unit
        /// </summary>
        public string DisplayName => this.displayName ?? (this.displayName = UnitHelpers.CreateUnitDisplayName(this));

        /// <summary>
        /// Convert the amount from the current unit to the standard unit of the specified quantity
        /// </summary>
        /// <param name="amount">Amount in this unit</param>
        /// <returns>Amount converted to standard unit</returns>
        public abstract float ConvertAmountToStandardUnit(float amount);

        /// <summary>
        /// Convert a standard amount to this unit of the specified quantity
        /// to the current unit
        /// </summary>
        /// <param name="standardAmount">Standard amount of the current <see cref="IUnit.Quantity"/>.</param>
        /// <returns>Amount in this unit.</returns>
        public abstract float ConvertStandardAmountToUnit(float standardAmount);

        #endregion

        #region OPERATORS

        /// <summary>
        /// Creates a standard unit measure object, corresponding to 1.0 of the specified <paramref name="unit"/>.
        /// </summary>
        /// <param name="unit">Defining unit of the standard measure.</param>
        /// <returns>Standard unit measure object, corresponding to 1.0 of the specified <paramref name="unit"/>.</returns>
        public static implicit operator Q(Unit<Q> unit)
        {
            return quantity.Factory.New(amount: 1.0, unit: unit);
        }

        /// <summary>
        /// Creates a standard unit measure object of the specified quantity.
        /// </summary>
        /// <param name="amount">The quantity amount.</param>
        /// <param name="unit">The quantity unit.</param>
        /// <returns>A new standard unit measure object of the specified quantity.</returns>
        public static Q operator *(double amount, Unit<Q> unit)
        {
            return quantity.Factory.New(amount, unit);
        }

        /// <summary>
        /// Creates a standard unit measure object of the specified quantity.
        /// </summary>
        /// <param name="amount">The quantity amount.</param>
        /// <param name="unit">The quantity unit.</param>
        /// <returns>A new standard unit measure object of the specified quantity.</returns>
        public static Q operator *(float amount, Unit<Q> unit)
        {
            return quantity.Factory.New(amount, unit);
        }

        /// <summary>
        /// Creates a standard unit measure object of the specified quantity.
        /// </summary>
        /// <param name="amount">The quantity amount.</param>
        /// <param name="unit">The quantity unit.</param>
        /// <returns>A new standard unit measure object of the specified quantity.</returns>
        public static Q operator *(decimal amount, Unit<Q> unit)
        {
            return quantity.Factory.New(amount, unit);
        }

        /// <summary>
        /// Creates a new unit preserving measure object of the specified quantity.
        /// </summary>
        /// <param name="amount">The quantity amount.</param>
        /// <param name="unit">The quantity unit.</param>
        /// <returns>A new unit preserving measure object of the specified quantity.</returns>
        public static IMeasure<Q> operator |(double amount, Unit<Q> unit)
        {
            return quantity.Factory.NewPreserveUnit(amount, unit);
        }

        /// <summary>
        /// Creates a new unit preserving measure object of the specified quantity.
        /// </summary>
        /// <param name="amount">The quantity amount.</param>
        /// <param name="unit">The quantity unit.</param>
        /// <returns>A new unit preserving measure object of the specified quantity.</returns>
        public static IMeasure<Q> operator |(float amount, Unit<Q> unit)
        {
            return quantity.Factory.NewPreserveUnit(amount, unit);
        }

        /// <summary>
        /// Creates a new unit preserving measure object of the specified quantity.
        /// </summary>
        /// <param name="amount">The quantity amount.</param>
        /// <param name="unit">The quantity unit.</param>
        /// <returns>A new unit preserving measure object of the specified quantity.</returns>
        public static IMeasure<Q> operator |(decimal amount, Unit<Q> unit)
        {
            return quantity.Factory.NewPreserveUnit(amount, unit);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="Symbol">unit symbol</see>
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return this.DisplayName;
        }

        #endregion
    }
}