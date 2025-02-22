﻿namespace UglyToad.PdfPig.Functions
{
    using System;
    using System.Collections.Generic;
    using UglyToad.PdfPig.Tokens;

    /// <summary>
    /// Exponential interpolation function
    /// </summary>
    internal sealed class PdfFunctionType2 : PdfFunction
    {
        /// <summary>
        /// Exponential interpolation function
        /// </summary>
        internal PdfFunctionType2(DictionaryToken function) : base(function)
        {
            if (GetDictionary().TryGet(NameToken.C0, out ArrayToken array0))
            {
                C0 = array0;
            }
            else
            {
                C0 = new ArrayToken(new List<IToken>());
            }
            if (C0.Length == 0)
            {
                C0 = new ArrayToken(new List<NumericToken>() { new NumericToken(0) });
            }

            if (GetDictionary().TryGet(NameToken.C1, out ArrayToken array1))
            {
                C1 = array1;
            }
            else
            {
                C1 = new ArrayToken(new List<IToken>());
            }
            if (C0.Length == 0)
            {
                C1 = new ArrayToken(new List<NumericToken>() { new NumericToken(1) });
            }

            if (GetDictionary().TryGet(NameToken.N, out NumericToken exp))
            {
                N = exp.Double;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal PdfFunctionType2(StreamToken function) : base(function)
        {
            if (GetDictionary().TryGet(NameToken.C0, out ArrayToken array0))
            {
                C0 = array0;
            }
            else
            {
                C0 = new ArrayToken(new List<IToken>());
            }
            if (C0.Length == 0)
            {
                C0 = new ArrayToken(new List<NumericToken>() { new NumericToken(0) });
            }

            if (GetDictionary().TryGet(NameToken.C1, out ArrayToken array1))
            {
                C1 = array1;
            }
            else
            {
                C1 = new ArrayToken(new List<IToken>());
            }
            if (C0.Length == 0)
            {
                C1 = new ArrayToken(new List<NumericToken>() { new NumericToken(1) });
            }

            if (GetDictionary().TryGet(NameToken.N, out NumericToken exp))
            {
                N = exp.Double;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override FunctionTypes FunctionType
        {
            get
            {
                return FunctionTypes.Exponential;
            }
        }

        public override double[] Eval(double[] input)
        {
            // exponential interpolation
            double xToN = Math.Pow(input[0], N); // x^exponent

            double[] result = new double[Math.Min(C0.Length, C1.Length)];
            for (int j = 0; j < result.Length; j++)
            {
                double c0j = ((NumericToken)C0[j]).Double;
                double c1j = ((NumericToken)C1[j]).Double;
                result[j] = c0j + xToN * (c1j - c0j);
            }

            return ClipToRange(result);
        }

        /// <summary>
        /// The C0 values of the function, 0 if empty.
        /// </summary>
        public ArrayToken C0 { get; }

        /// <summary>
        /// The C1 values of the function, 1 if empty.
        /// </summary>
        public ArrayToken C1 { get; }

        /// <summary>
        /// The exponent of the function.
        /// </summary>
        public double N { get; }

        public override string ToString()
        {
            return "FunctionType2{"
                + "C0: " + C0 + " "
                + "C1: " + C1 + " "
                + "N: " + N + "}";
        }
    }
}
