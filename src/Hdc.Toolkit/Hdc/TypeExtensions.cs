using System;
using System.Reflection;
using Hdc;

namespace Hdc
{
	/// <summary>
	/// Provides extension methods for <see cref="Type"/>-based objects.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Gets the root element type for a given <see cref="Type"/>.
		/// </summary>
		/// <param name="this">The <see cref="Type"/> to check.</param>
		/// <returns>Returns the root element <see cref="Type"/>.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Certain types (e.g. arrays and byrefs) have an element type. For example,
		/// a byref array of integers ("int[]&amp;") has an element type of "int[]",
		/// which in turn has an element type of "int". This method makes it easier
		/// to find the root element type for any given <see cref="Type"/>.
		/// </remarks>
		public static Type GetRootElementType(this Type @this)
		{
			@this.CheckParameterForNull("@this");
			
			var type = @this;

			while(type.HasElementType)
			{
				type = type.GetElementType();
			}

			return type;
		}

        public static Type GetType(string typeFullName, string assemblyName)
        {
            if (assemblyName == null)
                return Type.GetType(typeFullName);

            //搜索当前域中已加载的程序集
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in asms)
            {
                string[] names = asm.FullName.Split(',');
                if (names[0].Trim() == assemblyName.Trim())
                    return asm.GetType(typeFullName);
            }

            //加载目标程序集
            Assembly tarAssem = Assembly.Load(assemblyName);
            if (tarAssem != null)
                return tarAssem.GetType(typeFullName);

            return null;
        }

        public static Type GetType(string full)
        {
            if (string.IsNullOrEmpty(full))
                return null;
            var strs = full.Split(',');
            var fullName = strs[0].Trim();

            var asmFullName = full.Remove(0, strs[0].Length + 1).Trim();

            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in asms)
            {
                if (asm.FullName == asmFullName)
                    return asm.GetType(fullName);
            }
            return null;
        }

	}
}
