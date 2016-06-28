using System;
using System.ComponentModel;
using System.Diagnostics;
using Core.Diagnostics.Resources;

namespace Core.Diagnostics
{
    /// <summary>
    /// Provides methods to perform assertion on the range of the parameter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public struct ArgumentRange<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the value of the parameter.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string ParamName { get; }

        internal ArgumentRange(T value, string paramName)
        {
            Value = value;
            ParamName = paramName;
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is less than or equal
        /// to the given value.
        /// </summary>
        /// <param name="value">The value to be compared with the parameter.</param>
        /// <param name="message">The custom error message.</param>
        /// <returns>
        /// The same <see cref="ArgumentRange{T}"/> struct if no exception is thrown.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <see cref="Value"/> is less than or euqal to <paramref name="value"/>.
        /// </exception>
        [DebuggerStepThrough]
        public ArgumentRange<T> GreaterThan(T value, string message = null)
        {
            if (Value.CompareTo(value) <= 0)
            {
                throw new ArgumentOutOfRangeException(ParamName, Value, message);
            }
            return this;
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is greater than the
        /// given value.
        /// </summary>
        /// <param name="value">The value to be compared with the parameter.</param>
        /// <param name="message">The custom error message.</param>
        /// <returns>
        /// The same <see cref="ArgumentRange{T}"/> struct if no exception is thrown.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <see cref="Value"/> is greater than <paramref name="value"/>.
        /// </exception>
        [DebuggerStepThrough]
        public ArgumentRange<T> NoGreaterThan(T value, string message = null)
        {
            if (Value.CompareTo(value) > 0)
            {
                throw new ArgumentOutOfRangeException(ParamName, Value, message);
            }
            return this;
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is greater than or
        /// equal to the given value.
        /// </summary>
        /// <param name="value">The value to be compared with the parameter.</param>
        /// <param name="message">The custom error message.</param>
        /// <returns>
        /// The same <see cref="ArgumentRange{T}"/> struct if no exception is thrown.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <see cref="Value"/> is greater than or euqal to <paramref name="value"/>.
        /// </exception>
        [DebuggerStepThrough]
        public ArgumentRange<T> LessThan(T value, string message = null)
        {
            if (Value.CompareTo(value) >= 0)
            {
                throw new ArgumentOutOfRangeException(ParamName, Value, message);
            }
            return this;
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is less than the
        /// given value.
        /// </summary>
        /// <param name="value">The value to be compared with the parameter.</param>
        /// <param name="message">The custom error message.</param>
        /// <returns>
        /// The same <see cref="ArgumentRange{T}"/> struct if no exception is thrown.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <see cref="Value"/> is less than <paramref name="value"/>.kd
        /// </exception>
        [DebuggerStepThrough]
        public ArgumentRange<T> NoLessThan(T value, string message = null)
        {
            if (Value.CompareTo(value) < 0)
            {
                throw new ArgumentOutOfRangeException(ParamName, Value, message);
            }
            return this;
        }
    }

    public static partial class Requires
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentRange{T}(T, string)"/> stuct
        /// which can be used to perform assertion on the range of the parameter.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>
        /// An <see cref="ArgumentRange{T}(T, string)"/> struct for the given parameter.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is null.
        /// </exception>
        public static ArgumentRange<T> ArgumentRange<T>(T value, string paramName)
            where T : IComparable<T>
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return new ArgumentRange<T>(value, paramName);
        }

        private static void Positive<T>(T value, string paramName)
            where T : IComparable<T>
        {
            if (value.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, Strings.Positive);
            }
        }

        private static void NonPositive<T>(T value, string paramName)
            where T : IComparable<T>
        {
            if (value.CompareTo(default(T)) > 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, Strings.NonPositive);
            }
        }

        private static void Negative<T>(T value, string paramName)
           where T : IComparable<T>
        {
            if (value.CompareTo(default(T)) >= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, Strings.Negative);
            }
        }

        private static void NonNegative<T>(T value, string paramName)
          where T : IComparable<T>
        {
            if (value.CompareTo(default(T)) < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, Strings.NonNegative);
            }
        }

        #region Positive

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(byte value, string paramName)
        {
            Positive<byte>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(decimal value, string paramName)
        {
            Positive<decimal>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(double value, string paramName)
        {
            Positive<double>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(short value, string paramName)
        {
            Positive<short>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(int value, string paramName)
        {
            Positive<int>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(long value, string paramName)
        {
            Positive<long>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(sbyte value, string paramName)
        {
            Positive<sbyte>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(float value, string paramName)
        {
            Positive<float>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(ushort value, string paramName)
        {
            Positive<ushort>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(uint value, string paramName)
        {
            Positive<uint>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero.
        /// </exception>
        [DebuggerStepThrough]
        public static void Positive(ulong value, string paramName)
        {
            Positive<ulong>(value, paramName);
        }

        #endregion

        #region NonPositive

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonPositive(decimal value, string paramName)
        {
            NonPositive<decimal>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonPositive(double value, string paramName)
        {
            NonPositive<double>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonPositive(short value, string paramName)
        {
            NonPositive<short>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonPositive(int value, string paramName)
        {
            NonPositive<int>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonPositive(long value, string paramName)
        {
            NonPositive<long>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonPositive(sbyte value, string paramName)
        {
            NonPositive<sbyte>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonPositive(float value, string paramName)
        {
            NonPositive<float>(value, paramName);
        }

        #endregion

        #region Negative

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void Negative(decimal value, string paramName)
        {
            Negative<decimal>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void Negative(double value, string paramName)
        {
            Negative<double>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void Negative(short value, string paramName)
        {
            Negative<short>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void Negative(int value, string paramName)
        {
            Negative<int>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void Negative(long value, string paramName)
        {
            Negative<long>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void Negative(sbyte value, string paramName)
        {
            Negative<sbyte>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is zero or positive.
        /// </exception>
        [DebuggerStepThrough]
        public static void Negative(float value, string paramName)
        {
            Negative<float>(value, paramName);
        }

        #endregion

        #region NonNegative

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonNegative(decimal value, string paramName)
        {
            NonNegative<decimal>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonNegative(double value, string paramName)
        {
            NonNegative<double>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonNegative(short value, string paramName)
        {
            NonNegative<short>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonNegative(int value, string paramName)
        {
            NonNegative<int>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonNegative(long value, string paramName)
        {
            NonNegative<long>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonNegative(sbyte value, string paramName)
        {
            NonNegative<sbyte>(value, paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.
        /// </exception>
        [DebuggerStepThrough]
        public static void NonNegative(float value, string paramName)
        {
            NonNegative<float>(value, paramName);
        }

        #endregion
    }
}
