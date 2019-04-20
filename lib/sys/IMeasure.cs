namespace eV.Measure.sys
{
    using System;
    using AmountType = System.Single;

    public interface IMeasure : IComparable<IMeasure>, IEquatable<IMeasure>
    {
        float Amount { get; }
        float StandardAmount { get; }
        IUnit Unit { get; }
        float GetAmount(IUnit iUnit);
        IMeasure this[IUnit iUnit] { get; }
    }
    public interface IMeasure<Q> : IMeasure, IComparable<IMeasure<Q>>, IEquatable<IMeasure<Q>> where Q : class, IQuantity<Q>
    {
        new IUnit<Q> Unit { get; }
        float GetAmount(IUnit<Q> unit);
        IMeasure<Q> this[IUnit<Q> iUnit] { get; }
    }
}