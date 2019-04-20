namespace eV.Measure.sys
{
    public interface IMeasureFactory<Q> where Q : class, IQuantity<Q>
    {
        Q New(double amount);
        Q New(double amount, IUnit<Q> unit);    
        Q New(float amount);
        Q New(float amount, IUnit<Q> unit);
        Q New(decimal amount);
        Q New(decimal amount, IUnit<Q> unit);
        IMeasure<Q> NewPreserveUnit(double amount, IUnit<Q> unit);
        IMeasure<Q> NewPreserveUnit(float amount, IUnit<Q> unit);
        IMeasure<Q> NewPreserveUnit(decimal amount, IUnit<Q> unit);
    }
}