using System.Windows.Controls;
using Hdc.Mvvm.Converters;

namespace Hdc.Controls
{
    public class Operatiors
    {
        public static string Plus
        {
            get
            {
                return "+";
            }
        }

        public static string Minus
        {
            get
            {
                return "-";
            }
        }

        public static string Multiply
        {
            get
            {
                return "*";
            }
        }

        public static string Divide
        {
            get
            {
                return "/";
            }
        }
    }

    public static class Converters
    {
        public static readonly OperatorConverter OperatorConverter = new OperatorConverter();
    }
}