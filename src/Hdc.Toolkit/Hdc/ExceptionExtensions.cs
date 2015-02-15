using Hdc;

namespace Hdc
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Provides extension methods for <see cref="Exception"/>-based objects.
    /// </summary>
    public static class ExceptionExtensions
    {
        private static string FormatMethod(MethodBase targetMethod)
        {
            var paramBuilder = new StringBuilder();

            for (var i = 0; i < targetMethod.GetParameters().Length; i++)
            {
                var param = targetMethod.GetParameters()[i];
                paramBuilder.Append(param.ParameterType.FullName);

                if (i < (targetMethod.GetParameters().Length - 1))
                {
                    paramBuilder.Append(", ");
                }
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                "[{0}], {1}::{2}({3})",
                targetMethod.DeclaringType.Assembly.GetName().Name,
                targetMethod.DeclaringType,
                targetMethod.Name,
                paramBuilder.ToString());
        }

        /// <summary>
        /// Prints the contents of <paramref name="this"/> to the console's output stream.
        /// </summary>
        /// <param name="this">The <see cref="Exception"/> to print.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static void Print(this Exception @this)
        {
            @this.Print(Console.Out);
        }

        /// <summary>
        /// Prints the contents of <paramref name="this"/> to the given <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="this">The <see cref="Exception"/> to print.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write exception information to.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if either <paramref name="this"/> or <paramref name="writer"/> is <c>null</c>.
        /// </exception>
        public static void Print(this Exception @this, TextWriter writer)
        {
            @this.CheckParameterForNull("@this");
            writer.CheckParameterForNull("writer");

            writer.WriteLine("Type Name: {0}", @this.GetType().FullName);

            writer.WriteLine("\tSource: {0}", @this.Source);
            writer.WriteLine("\tTargetSite: {0}", ExceptionExtensions.FormatMethod(@this.TargetSite));
            writer.WriteLine("\tMessage: {0}", @this.Message);
            writer.WriteLine("\tHelpLink: {0}", @this.HelpLink);

            @this.PrintCustomProperties(writer);
            @this.PrintStackTrace(writer);
            @this.PrintData(writer);

            if (@this.InnerException != null)
            {
                writer.WriteLine();
                @this.InnerException.Print(writer);
            }
        }

        private static void PrintCustomProperties(this Exception @this, TextWriter writer)
        {
            var properties =
                from property in
                    @this.GetType().GetProperties(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                where property.CanRead
                where property.GetGetMethod() != null
                select property;

            if (properties.Count() > 0)
            {
                writer.WriteLine();
                writer.WriteLine("\tCustom Properties (" + properties.Count() + "):");

                foreach (var property in properties)
                {
                    writer.WriteLine(
                        string.Format(
                            CultureInfo.CurrentCulture, "\t\t{0} = {1}", property.Name, property.GetValue(@this, null)));
                }
            }
        }

        private static void PrintData(this Exception @this, TextWriter writer)
        {
            if (@this.Data.Count > 0)
            {
                writer.WriteLine();

                writer.WriteLine("\tData:");

                foreach (DictionaryEntry dataPair in @this.Data)
                {
                    writer.WriteLine("\t\tKey: {0}, Value: {1}", dataPair.Key.ToString(), dataPair.Value.ToString());
                }
            }
        }

        private static void PrintStackTrace(this Exception @this, TextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("\tStackTrace:");

            var trace = new StackTrace(@this, true);

            for (var i = 0; i < trace.FrameCount; i++)
            {
                writer.WriteLine("\t\tFrame: {0}", i);
                var frame = trace.GetFrame(i);
                writer.WriteLine("\t\t\tMethod: {0}", ExceptionExtensions.FormatMethod(frame.GetMethod()));
                writer.WriteLine("\t\t\tFile: {0}", frame.GetFileName());
                writer.WriteLine("\t\t\tColumn: {0}", frame.GetFileColumnNumber());
                writer.WriteLine("\t\t\tLine: {0}", frame.GetFileLineNumber());
                writer.WriteLine("\t\t\tIL Offset: {0}", frame.GetILOffset());
                writer.WriteLine("\t\t\tNative Offset: {0}", frame.GetNativeOffset());
            }
        }
    }
}