using System;
using System.Diagnostics;
using Core.Diagnostics.Resources;

namespace Core.Diagnostics
{
    public static partial class Requires
    {
        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the <paramref name="condition"/>
        /// is false.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="condition">
        /// True if <paramref name="value"/> is in required range.
        /// </param>
        /// <param name="message">The custom error message.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="condition"/> is false.
        /// </exception>
        [DebuggerStepThrough]
        public static void Range<T>(
            T value,
            string paramName,
            bool condition,
            string message = null)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName, value, message);
            }
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
