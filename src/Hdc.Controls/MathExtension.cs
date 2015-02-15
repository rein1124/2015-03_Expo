using System;
using System.Windows.Markup;

namespace Hdc.Controls
{
    [MarkupExtensionReturnType(typeof(double))]
    public class MathExtension : MarkupExtension
    {
        private double _f1;

        private string _operator;

        private double _f2;

//        [TypeConverter(typeof(D))]
        public double F1
        {
            get
            {
                return _f1;
            }
            set
            {
                _f1 = value;
            }
        }

        public double F2
        {
            get
            {
                return _f2;
            }
            set
            {
                _f2 = value;
            }
        }

        public string Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                _operator = value;
            }
        }

        public MathExtension()
        {
        }
//
//        public MathExtension(object o)
//        {
//            var p = o;
//        }

        //        public MathExtension(int s1, int op, int s2)
        //        {
        //            _f1 = s1;
        //            _operator = op;
        //            _f2 = s2;
        //        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
//            var i = _f1;
//            var j = _f2;
//            return (double)(i + j);

            return Calculate(_f1, _f2, _operator);
        }

        private static double Calculate(double f1, double f2, string operatorString)
        {
            var result = f1;
            switch (operatorString)
            {
                case "+":
                    result += f2;

                    break;
                case "-":

                    result -= f2;

                    break;
                case "*":

                    result *= f2;

                    break;
                case "/":

                    result /= f2;

                    break;
            }
            return result;
        }
    }
}