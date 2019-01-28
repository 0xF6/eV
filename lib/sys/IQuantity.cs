namespace eV.Measure.sys
{
    using System;

    public interface IQuantity : IEquatable<IQuantity>
    {
        string DisplayName { get; }
        QuantityDimension Dimension { get; }
        IUnit StandardUnit { get; }
    }

    public interface IQuantity<Q> : IQuantity where Q : class, IQuantity<Q>
    {
        new IUnit<Q> StandardUnit { get; }
        IMeasureFactory<Q> Factory { get; }
    }
}