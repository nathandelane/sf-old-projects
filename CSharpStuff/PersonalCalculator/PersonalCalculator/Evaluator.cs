using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    public class Evaluator
    {
        #region Public Methods

        public static string Negate(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = (-1) * rt;

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

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

        public static string HyperbolicSine(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Sinh(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string HyperbolicCosine(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Cosh(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string HyperbolicTangent(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Tanh(rt);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Factorial(string left)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double internalResult = lt;

            while (lt > 1)
            {
                internalResult = internalResult * (lt - 1);
                lt--;
            }

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Power(string b, string n)
        {
            string result = String.Empty;
            double bas = double.Parse(b);
            double pow = double.Parse(n);
            double internalResult = System.Math.Pow(bas, pow);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string Logarithm(string b, string r)
        {
            string result = String.Empty;
            double bas = double.Parse(b);
            double real = double.Parse(r);
            double internalResult = System.Math.Log(real, bas);

            result = String.Format("{0}", internalResult);

            if (!DecimalHasValue(result))
            {
                Int64 intResult = Int64.Parse(result);
                result = String.Format("{0}", intResult);
            }

            return result;
        }

        public static string ToBinary(string right)
        {
            string result = String.Empty;
            int rt = int.Parse(right);

            result = Convert.ToString(rt, 2);

            return result;
        }

        public static string ToHexadecimal(string right)
        {
            string result = String.Empty;
            int rt = int.Parse(right);

            result = Convert.ToString(rt, 16);

            return result;
        }

        public static string ToOctal(string right)
        {
            string result = String.Empty;
            int rt = int.Parse(right);

            result = Convert.ToString(rt, 8);

            return result;
        }

        public static string ToRadians(string right)
        {
            string result = String.Empty;
            int rt = int.Parse(right);

            result = String.Format("{0}", (System.Math.PI * rt / 180.0));

            return result;
        }

        public static string ToDegrees(string right)
        {
            string result = String.Empty;
            int rt = int.Parse(right);

            result = String.Format("{0}", (rt * (180 / System.Math.PI)));

            return result;
        }

        public static string BitwiseAnd(string left, string right)
        {
            string result = String.Empty;
            Int64 lt = Int64.Parse(left);
            Int64 rt = Int64.Parse(right);
            Int64 internalResult = lt & rt;

            result = String.Format("{0}", internalResult);

            return result;
        }

        public static string BitwiseOr(string left, string right)
        {
            string result = String.Empty;
            Int64 lt = Int64.Parse(left);
            Int64 rt = Int64.Parse(right);
            Int64 internalResult = lt | rt;

            result = String.Format("{0}", internalResult);

            return result;
        }

        public static string BitwiseXor(string left, string right)
        {
            string result = String.Empty;
            Int64 lt = Int64.Parse(left);
            Int64 rt = Int64.Parse(right);
            Int64 internalResult = lt ^ rt;

            result = String.Format("{0}", internalResult);

            return result;
        }

        public static string GetE()
        {
            return String.Format("{0}", System.Math.E);
        }

        public static string GetPi()
        {
            return String.Format("{0}", System.Math.PI);
        }

        public static string NotEqual(string left, string right)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double rt = double.Parse(right);
            bool internalResult = lt != rt;

            result = NormalizeBoolean(String.Format("{0}", internalResult));

            return result;
        }

        public static string LesThanOrEqual(string left, string right)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double rt = double.Parse(right);
            bool internalResult = lt <= rt;

            result = NormalizeBoolean(String.Format("{0}", internalResult));

            return result;
        }

        public static string GreaterThanOrEqual(string left, string right)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double rt = double.Parse(right);
            bool internalResult = lt >= rt;

            result = NormalizeBoolean(String.Format("{0}", internalResult));

            return result;
        }

        public static string AreEqual(string left, string right)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double rt = double.Parse(right);
            bool internalResult = lt == rt;

            result = NormalizeBoolean(String.Format("{0}", internalResult));

            return result;
        }

        public static string LessThan(string left, string right)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double rt = double.Parse(right);
            bool internalResult = lt < rt;

            result = NormalizeBoolean(String.Format("{0}", internalResult));

            return result;
        }

        public static string GreaterThan(string left, string right)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double rt = double.Parse(right);
            bool internalResult = lt > rt;

            result = NormalizeBoolean(String.Format("{0}", internalResult));

            return result;
        }

        public static string Modulus(string left, string right)
        {
            string result = String.Empty;
            double lt = double.Parse(left);
            double rt = double.Parse(right);
            double internalResult = lt % rt;

            result = String.Format("{0}", internalResult);

            return result;
        }

        public static string Round(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Round(rt);

            result = String.Format("{0}", internalResult);

            return result;
        }

        public static string Ceiling(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Ceiling(rt);

            result = String.Format("{0}", internalResult);

            return result;
        }

        public static string Floor(string right)
        {
            string result = String.Empty;
            double rt = double.Parse(right);
            double internalResult = System.Math.Floor(rt);

            result = String.Format("{0}", internalResult);

            return result;
        }

        #endregion

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

        private static string NormalizeBoolean(string value)
        {
            string result = value;

            if (value.Equals("True"))
            {
                result = "1";
            }
            else if (value.Equals("False"))
            {
                result = "0";
            }

            return result;
        }

        #endregion
    }
}
