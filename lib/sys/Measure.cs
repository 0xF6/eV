﻿namespace eV.Measure.sys
{
    using System;
    using System.Globalization;
    using AmountType = System.Single;
    public class Measure<Q> : IMeasure<Q> where Q : class, IQuantity<Q>, new()
    {
        #region FIELDS

        private static readonly IMeasureFactory<Q> Factory = new Q().Factory;

        private readonly float amount;
        private readonly IUnit<Q> unit;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Copy constructor, initializes a measure based on the specified measure object.
        /// </summary>
        /// <param name="measure">Measure to be copied.</param>
        public Measure(IMeasure<Q> measure)
        {
            if (measure == null)
            {
                throw new ArgumentNullException(nameof(measure));
            }

            this.amount = measure.Amount;
            this.unit = measure.Unit;
        }

        /// <summary>
        /// Initializes a measure to the specified amount and unit
        /// </summary>
        /// <param name="amount">Measured amount</param>
        /// <param name="unit">Unit of measure</param>
        /// <exception cref="ArgumentNullException">if the specified unit is null</exception>
        public Measure(double amount, IUnit<Q> unit)
        {
            this.amount = (float)amount;
            this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        /// <summary>
        /// Initializes a measure to the specified amount and unit
        /// </summary>
        /// <param name="amount">Measured amount</param>
        /// <param name="unit">Unit of measure</param>
        /// <exception cref="ArgumentNullException">if the specified unit is null</exception>
        public Measure(float amount, IUnit<Q> unit)
        {
            this.amount = (float)amount;
            this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        /// <summary>
        /// Initializes a measure to the specified amount and unit
        /// </summary>
        /// <param name="amount">Measured amount</param>
        /// <param name="unit">Unit of measure</param>
        /// <exception cref="ArgumentNullException">if the specified unit is null</exception>
        public Measure(decimal amount, IUnit<Q> unit)
        {
            this.amount = (float)amount;
            this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        #endregion

        #region Implementation of IMeasure<Q>

        /// <summary>
        /// Gets the measured amount in the <see cref="Unit">current unit of measure</see>
        /// </summary>
        public float Amount => this.amount;

        /// <summary>
        /// Gets the measured amount in the standard unit of measure for the <typeparam name="Q">specified quantity</typeparam>
        /// </summary>
        public float StandardAmount => this.unit.ConvertAmountToStandardUnit(this.amount);

        /// <summary>
        /// Gets the unit of measure
        /// </summary>
        IUnit IMeasure.Unit => this.unit;

        /// <summary>
        /// Gets the amount of this measure in the requested unit
        /// </summary>
        /// <param name="unit">Unit to which the measured amount should be converted</param>
        /// <returns>Measured amount converted into <paramref name="unit">specified unit</paramref></returns>
        float IMeasure.GetAmount(IUnit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            if (!unit.Quantity.Equals(default(Q)))
            {
                throw new ArgumentException("Unit is not the same quantity as measure");
            }

            return unit.ConvertStandardAmountToUnit(this.StandardAmount);
        }

        /// <summary>
        /// Gets a new unit specific measure based on this measure but in the <paramref name="unit">specified unit</paramref>
        /// </summary>
        /// <param name="unit">Unit in which the new measure should be specified</param>
        /// <exception cref="ArgumentNullException">if specified unit is null or if specified unit is not of the 
        /// <typeparamref name="Q">valid quantity</typeparamref></exception>
        IMeasure IMeasure.this[IUnit unit] => this[unit as IUnit<Q>];

        /// <summary>
        /// Gets the quantity-typed unit of measure
        /// </summary>
        public IUnit<Q> Unit => this.unit;

        /// <summary>
        /// Gets the amount of this measure in the requested unit
        /// </summary>
        /// <param name="unit">Unit to which the measured amount should be converted</param>
        /// <returns>Measured amount converted into <paramref name="unit">specified unit</paramref></returns>
        public float GetAmount(IUnit<Q> unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            return unit.ConvertStandardAmountToUnit(this.StandardAmount);
        }

        /// <summary>
        /// Gets a new unit specific measure based on this measure but in the <paramref name="unit">specified unit</paramref>
        /// </summary>
        /// <param name="unit">Unit in which the new measure should be specified</param>
        IMeasure<Q> IMeasure<Q>.this[IUnit<Q> unit] => this[unit];

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets a new unit specific measure based on this measure but in the <paramref name="unit">specified unit</paramref>
        /// </summary>
        /// <param name="unit">Unit in which the new measure should be specified</param>
        public Measure<Q> this[IUnit<Q> unit]
        {
            get
            {
                if (unit == null)
                {
                    throw new ArgumentNullException(nameof(unit));
                }

                return new Measure<Q>(this.GetAmount(unit), unit);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IMeasure<Q> other)
        {
            if (ReferenceEquals(objA: null, objB: other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.amount.Equals(other.GetAmount(this.unit));
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        bool IEquatable<IMeasure>.Equals(IMeasure other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (!other.Unit.Quantity.Equals(default(Q)))
            {
                throw new ArgumentException("Measures are of different quantities");
            }

            return this.amount.Equals(other.GetAmount(this.unit));
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: 
        ///                     Value 
        ///                     Meaning 
        ///                     Less than zero 
        ///                     This object is less than the <paramref name="other"/> parameter.
        ///                     Zero 
        ///                     This object is equal to <paramref name="other"/>. 
        ///                     Greater than zero 
        ///                     This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IMeasure<Q> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return this.amount.CompareTo(other.GetAmount(this.unit));
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: 
        /// Value              Meaning
        /// Less than zero     This object is less than the <paramref name="other"/> parameter.
        /// Zero               This object is equal to <paramref name="other"/>. 
        /// Greater than zero  This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        int IComparable<IMeasure>.CompareTo(IMeasure other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (!other.Unit.Quantity.Equals(default(Q)))
            {
                throw new ArgumentException("Measures are of different quantities");
            }

            return this.amount.CompareTo(other.GetAmount(this.unit));
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(objA: null, objB: obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(Measure<Q>))
            {
                return false;
            }

            return this.Equals((Measure<Q>)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return this.StandardAmount.GetHashCode();
        }

        /// <summary>
        /// Returns the actual value with the quantity suffixed
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a the actual value with the quantity symbol appended
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the actual value in formatted form with the quantity suffixed
        /// </summary>
        /// <param name="format">Format string to display the value with</param>
        /// <returns>A <see cref="T:System.String"/> containing a the actual value in formatted form with the quantity symbol appended</returns>
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the actual value in formatted form with the quantity suffixed
        /// </summary>
        /// <param name="provider">Formatting provider to format the value with</param>
        /// <returns>Formatted string.</returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString("G", provider);
        }

        /// <summary>
        /// Returns the actual value in formatted form with the quantity suffixed
        /// </summary>
        /// <param name="format">Format string to display the value with</param>
        /// <param name="provider">Formatting provider to format the value with</param>
        /// <returns>Formatted string.</returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format("{0} {1}", this.amount.ToString(format, provider), this.unit.Symbol).TrimEnd();
        }

        #endregion

        #region OPERATORS

        /// <summary>
        /// Cast operator to quantity measure in standard unit.
        /// </summary>
        /// <param name="measure">Measure object subject to cast.</param>
        /// <returns>Quantity measure in standard unit.</returns>
        public static explicit operator Q(Measure<Q> measure)
        {
            return Factory.New(measure.StandardAmount);
        }

        /// <summary>
        /// Adds two measure objects provided the measured quantities are equal
        /// </summary>
        /// <param name="lhs">First measure term</param>
        /// <param name="rhs">Second measure term</param>
        /// <returns>Sum of the two measure objects in the unit of the <paramref name="lhs">left-hand side measure</paramref></returns>
        public static Measure<Q> operator +(Measure<Q> lhs, Measure<Q> rhs)
        {
            return new Measure<Q>(lhs.amount + rhs.GetAmount(lhs.unit), lhs.unit);
        }

        /// <summary>
        /// Adds two measure objects provided the measured quantities are equal
        /// </summary>
        /// <param name="lhs">First measure term</param>
        /// <param name="rhs">Second measure term (any object implementing the IMeasure interface)</param>
        /// <returns>Sum of the two measure objects in the unit of the <paramref name="lhs">left-hand side measure</paramref></returns>
        public static Measure<Q> operator +(Measure<Q> lhs, IMeasure<Q> rhs)
        {
            return new Measure<Q>(lhs.amount + rhs.GetAmount(lhs.unit), lhs.unit);
        }

        /// <summary>
        /// Subtract two measure objects of the same quantity
        /// </summary>
        /// <param name="lhs">First measure object</param>
        /// <param name="rhs">Second measure object</param>
        /// <returns>Difference of the measure objects</returns>
        public static Measure<Q> operator -(Measure<Q> lhs, Measure<Q> rhs)
        {
            return new Measure<Q>(lhs.amount - rhs.GetAmount(lhs.unit), lhs.unit);
        }

        /// <summary>
        /// Subtract two measure objects of the same quantity
        /// </summary>
        /// <param name="lhs">First measure object</param>
        /// <param name="rhs">Second measure object (any object implementing the IMeasure interface)</param>
        /// <returns>Difference of the measure objects</returns>
        public static Measure<Q> operator -(Measure<Q> lhs, IMeasure<Q> rhs)
        {
            return new Measure<Q>(lhs.amount - rhs.GetAmount(lhs.unit), lhs.unit);
        }

        /// <summary>
        /// Multiply a scalar and a measure object
        /// </summary>
        /// <param name="scalar">Floating-point scalar</param>
        /// <param name="measure">Measure object</param>
        /// <returns>Product of the scalar and the measure object</returns>
        public static Measure<Q> operator *(float scalar, Measure<Q> measure)
        {
            return new Measure<Q>(scalar * measure.amount, measure.unit);
        }

        /// <summary>
        /// Multiply a measure object and a scalar
        /// </summary>
        /// <param name="measure">Measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Product of the measure object and the scalar</returns>
        public static Measure<Q> operator *(Measure<Q> measure, float scalar)
        {
            return new Measure<Q>(measure.amount * scalar, measure.unit);
        }

        /// <summary>
        /// Divide a measure object with a scalar
        /// </summary>
        /// <param name="measure">measure object</param>
        /// <param name="scalar">Floating-point scalar</param>
        /// <returns>Quotient of the measure object and the scalar</returns>
        public static Measure<Q> operator /(Measure<Q> measure, float scalar)
        {
            return new Measure<Q>(measure.amount / scalar, measure.unit);
        }

        /// <summary>
        /// Less than operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is less than second measure object; false otherwise</returns>
        public static bool operator <(Measure<Q> lhs, Measure<Q> rhs)
        {
            return lhs.amount < rhs.GetAmount(lhs.unit);
        }

        /// <summary>
        /// Greater than operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is greater than second measure object; false otherwise</returns>
        public static bool operator >(Measure<Q> lhs, Measure<Q> rhs)
        {
            return lhs.amount > rhs.GetAmount(lhs.unit);
        }

        /// <summary>
        /// Less than or equal to operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is less than or equal to second measure object; false otherwise</returns>
        public static bool operator <=(Measure<Q> lhs, Measure<Q> rhs)
        {
            return lhs.amount <= rhs.GetAmount(lhs.unit);
        }

        /// <summary>
        /// Greater than or equal to operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if first measure object is greater than or equal to second measure object; false otherwise</returns>
        public static bool operator >=(Measure<Q> lhs, Measure<Q> rhs)
        {
            return lhs.amount >= rhs.GetAmount(lhs.unit);
        }

        /// <summary>
        /// Equality operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if the two measure objects are equal; false otherwise</returns>
        public static bool operator ==(Measure<Q> lhs, Measure<Q> rhs)
        {
            return lhs.amount == rhs.GetAmount(lhs.unit);
        }

        /// <summary>
        /// Inequality operator for measure objects
        /// </summary>
        /// <param name="lhs">First object</param>
        /// <param name="rhs">Second object</param>
        /// <returns>true if the two measure objects are not equal; false if they are equal</returns>
        public static bool operator !=(Measure<Q> lhs, Measure<Q> rhs)
        {
            return lhs.amount != rhs.GetAmount(lhs.unit);
        }

        #endregion
    }
}