using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    internal class Evaluator
    {
        public static string Add(string leftAddend, string rightAddend)
        {
            string result = String.Empty;
            double left = double.Parse(leftAddend);
            double right = double.Parse(rightAddend);
            double internalResult = left + right;

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Subtract(string minuend, string subtrahend)
        {
            string result = String.Empty;
            double min = double.Parse(minuend);
            double sub = double.Parse(subtrahend);
            double internalResult = min - sub;

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Multiply(string multiplicand, string multiplier)
        {
            string result = String.Empty;
            double cand = double.Parse(multiplicand);
            double mult = double.Parse(multiplier);
            double internalResult = cand * mult;

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Divide(string dividend, string divisor)
        {
            string result = String.Empty;
            double dend = double.Parse(dividend);
            double div = double.Parse(divisor);
            double internalResult = dend / div;

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Sine(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Sin(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Cosine(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Cos(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Tangent(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Tan(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string InverseSine(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Asin(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string InverseCosine(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Acos(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string InverseTangent(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Atan(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        #region Private Methods

        private static bool DecimalHasValue(string value)
        {
            bool result = true;

            if (value.EndsWith(".0"))
            {
                result = false;
            }

            return result;
        }

        private static string Normalize(string value)
        {
            string result = value;

            if (value.Contains(","))
            {
                value = value.Replace(",", String.Empty);
            }

            return result;
        }

        #endregion
    }
}
