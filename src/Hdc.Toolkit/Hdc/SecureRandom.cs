﻿using Hdc;

namespace Hdc
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;

    /// <summary>
    /// Combines the security of <see cref="RandomNumberGenerator"/>
    /// with the simple interface of <see cref="Random"/>.
    /// </summary>
    public class SecureRandom : Random
    {
        private const string ErrorTooManyUniqueElements =
            "Cannot create the number of unique elements requested - maximum allowed is {0}.";

        private const double MaxInt32Inverse = 1.0 / (double)Int32.MaxValue;

        /// <summary>
        /// Creates a new <see cref="SecureRandom"/> instance.
        /// </summary>
        public SecureRandom()
            : base()
        {
            this.Generator = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Creates a new <see cref="SecureRandom"/> instance
        /// based on the given <see cref="RandomNumberGenerator"/>.
        /// </summary>
        /// <param name="generator">The <see cref="RandomNumberGenerator"/> to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="generator"/> is <c>null</c>.</exception>
        public SecureRandom(RandomNumberGenerator generator)
        {
            generator.CheckParameterForNull("generator");
            this.Generator = generator;
        }

        /// <summary>
        /// Gets an array of random <see cref="byte"/> values.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of random elements to get.
        /// </param>
        /// <param name="values">
        /// Specifies if the values should be unique.
        /// </param>
        /// <returns>
        /// Returns an array of random <see cref="byte"/> values.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="numberOfElements"/> exceeds the maximum value of a <see cref="byte"/>
        /// and <paramref name="values"/> is equal to <c>Unique</c>.
        /// </exception>
        /// <remarks>
        /// If <paramref name="values"/> is equal to <see cref="ValueGeneration.UniqueValuesOnly"/>,
        /// then the value of <paramref name="numberOfElements"/> should be small relative to 
        /// <see cref="byte.MaxValue"/>. The closer the ratio of <c>numberOfElements/byte.MaxValue</c> is to 1, 
        /// the longer it will take for <c>GetByteValues</c> to produce a unique random set of values.
        /// </remarks>
        public byte[] GetByteValues(uint numberOfElements, ValueGeneration values)
        {
            if (values == ValueGeneration.UniqueValuesOnly && numberOfElements > byte.MaxValue)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, SecureRandom.ErrorTooManyUniqueElements, byte.MaxValue),
                    "numberOfElements");
            }

            var elements = new byte[numberOfElements];

            if (values == ValueGeneration.DuplicatesAllowed)
            {
                this.Generator.GetBytes(elements);
            }
            else
            {
                var createdElementsCount = 0;

                while (createdElementsCount < numberOfElements)
                {
                    var value = new byte[1];
                    this.Generator.GetBytes(value);

                    if (!elements.Contains(value[0]))
                    {
                        elements[createdElementsCount] = value[0];
                        createdElementsCount++;
                    }
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets an array of random <see cref="double"/> values.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of random elements to get.
        /// </param>
        /// <returns>
        /// Returns an array of random <see cref="double"/> values.
        /// </returns>
        public double[] GetDoubleValues(uint numberOfElements)
        {
            var elements = new double[numberOfElements];

            for (var i = 0; i < numberOfElements; i++)
            {
                elements[i] = this.NextDouble();
            }

            return elements;
        }

        /// <summary>
        /// Gets an array of random <see cref="int"/> values.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of random elements to get.
        /// </param>
        /// <param name="values">
        /// Specifies if the values should be unique.
        /// </param>
        /// <returns>
        /// Returns an array of random <see cref="int"/> values.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="numberOfElements"/> exceeds the maximum value of an <see cref="int"/>
        /// and <paramref name="values"/> is equal to <c>Unique</c>.
        /// </exception>
        /// <remarks>
        /// If <paramref name="values"/> is equal to <see cref="ValueGeneration.UniqueValuesOnly"/>,
        /// then the value of <paramref name="numberOfElements"/> should be small relative to 
        /// <see cref="int.MaxValue"/>. The closer the ratio of <c>numberOfElements/int.MaxValue</c> is to 1, 
        /// the longer it will take for <c>GetInt32Values</c> to produce a unique random set of values.
        /// </remarks>
        public int[] GetInt32Values(uint numberOfElements, ValueGeneration values)
        {
            if (values == ValueGeneration.UniqueValuesOnly && numberOfElements > int.MaxValue)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, SecureRandom.ErrorTooManyUniqueElements, int.MaxValue),
                    "numberOfElements");
            }

            var elements = new int[numberOfElements];

            if (values == ValueGeneration.DuplicatesAllowed)
            {
                for (var i = 0; i < numberOfElements; i++)
                {
                    elements[i] = this.Next();
                }
            }
            else
            {
                var createdElementsCount = 0;

                while (createdElementsCount < numberOfElements)
                {
                    var value = this.Next();

                    if (!elements.Contains(value))
                    {
                        elements[createdElementsCount] = value;
                        createdElementsCount++;
                    }
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets a random <see cref="int"/> value.
        /// </summary>
        /// <returns>
        /// Returns a new random <see cref="int"/> value between 0 (inclusive) 
        /// and <c>Int32.MaxValue</c> (exclusive).
        /// </returns>
        public override int Next()
        {
            return this.Next(Int32.MaxValue);
        }

        /// <summary>
        /// Gets a random <see cref="int"/> value.
        /// </summary>
        /// <param name="maxValue">The upper bound of the generated random number.</param>
        /// <returns>
        /// Returns a new random <see cref="int"/> value between 0 (inclusive) 
        /// and <paramref name="maxValue"/> (exclusive).
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="maxValue"/> is less than zero.</exception>
        public override int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentException("maxValue must be greater than or equal to zero.", "maxValue");
            }

            byte[] newNumber = new byte[4];
            this.Generator.GetBytes(newNumber);
            return (int)(BitConverter.ToUInt32(newNumber, 0) % (uint)maxValue);
        }

        /// <summary>
        /// Gets a random <see cref="int"/> value.
        /// </summary>
        /// <param name="minValue">The lower bound of the generated random number.</param>
        /// <param name="maxValue">The upper bound of the generated random number.</param>
        /// <returns>
        /// Returns a new random <see cref="int"/> value between <paramref name="minValue"/> (inclusive) 
        /// and <paramref name="maxValue"/> (exclusive).
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="maxValue"/> is less than <paramref name="minValue"/>.
        /// </exception>
        public override int Next(int minValue, int maxValue)
        {
            int value = 0;

            if (maxValue < minValue)
            {
                throw new ArgumentException("maxValue must be greater than minValue.", "maxValue");
            }

            if (maxValue == minValue)
            {
                value = minValue;
            }
            else
            {
                byte[] newNumber = new byte[4];
                this.Generator.GetBytes(newNumber);

                uint range = (uint)maxValue - (uint)minValue;
                value = (int)((BitConverter.ToUInt32(newNumber, 0) % range) + minValue);
            }

            return value;
        }

        /// <summary>
        /// Gets a random <see cref="bool"/> value.
        /// </summary>
        /// <returns>
        /// Returns a new random <see cref="bool"/> value.
        /// </returns>
        public bool NextBoolean()
        {
            return this.Next(2) == 0;
        }

        /// <summary>
        /// Fills the given buffer with random bits.
        /// </summary>
        /// <param name="buffer">The buffer to populate.</param>
        public override void NextBytes(byte[] buffer)
        {
            this.Generator.GetBytes(buffer);
        }

        /// <summary>
        /// Gets a random <see cref="double"/> number.
        /// </summary>
        /// <returns>A <see cref="double"/> number.</returns>
        public override double NextDouble()
        {
            return (double)this.Next(Int32.MaxValue) * MaxInt32Inverse;
        }

        /// <summary>
        /// Gets the underlying <see cref="RandomNumberGenerator"/>.
        /// </summary>
        public RandomNumberGenerator Generator { get; private set; }
    }
}