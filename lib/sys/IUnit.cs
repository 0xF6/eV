namespace eV.Measure.sys
{
    using AmountType = System.Single;
    public interface IUnit
    {
        IQuantity Quantity { get; }
        bool IsStandardUnit { get; }
        string Symbol { get; }
        string DisplayName { get; }
        float ConvertAmountToStandardUnit(float amount);
        float ConvertStandardAmountToUnit(float standardAmount);
    }

    public interface IUnit<Q> : IUnit where Q : class, IQuantity<Q>
    {
        new IQuantity<Q> Quantity { get; }
    }
}