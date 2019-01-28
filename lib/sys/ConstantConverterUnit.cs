namespace eV.Measure.sys
{
    using AmountType = System.Single;
    public sealed class ConstantConverterUnit<Q> : Unit<Q> where Q : class, IQuantity<Q>, new()
    {
        #region FIELDS

        private readonly float amountToStandardUnitFactor;
        private readonly float standardAmountToUnitFactor;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initialize a physical unit object that is the standard unit of the specific quantity
        /// </summary>
        /// <param name="symbol">Unit display symbol</param>
        public ConstantConverterUnit(string symbol) : base(isStandardUnit: true, symbol: symbol)
        {
            this.amountToStandardUnitFactor = Constants.One;
            this.standardAmountToUnitFactor = Constants.One;
        }

        /// <summary>
        /// Convenience constructor for initializing prefixed non-standard unit
        /// </summary>
        /// <param name="prefix">Prefix to use in unit naming and scaling vis-a-vis standard unit</param>
        public ConstantConverterUnit(UnitPrefix prefix)
            : this($"{prefix.GetSymbol()}{new Q().StandardUnit.Symbol}", prefix.GetFactor())
        {
        }

        /// <summary>
        /// Initialize a physical unit object
        /// </summary>
        /// <param name="symbol">Unit display symbol</param>
        /// <param name="toStandardUnitFactor">Amount converter factor from this unit to quantity's standard unit</param>
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        public ConstantConverterUnit(string symbol, float toStandardUnitFactor)
            : base(toStandardUnitFactor == Constants.One, symbol)
        {
            this.amountToStandardUnitFactor = toStandardUnitFactor;
            this.standardAmountToUnitFactor = Constants.One / toStandardUnitFactor;
        }

        #endregion

        #region Implementation of IUnit<Q>
        public override float ConvertAmountToStandardUnit(float amount) 
            => this.amountToStandardUnitFactor * amount;
        public override float ConvertStandardAmountToUnit(float standardAmount) 
            => this.standardAmountToUnitFactor * standardAmount;
        #endregion
    }
}