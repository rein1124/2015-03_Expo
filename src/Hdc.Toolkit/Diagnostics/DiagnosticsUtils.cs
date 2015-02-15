using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.IO;
using System.Collections;
using System.Reflection; 
using System.Web;
using System.Diagnostics;

namespace Hdc.Diagnostics
{
    /// <summary>
    /// Utility class for diagnostics.
    /// </summary>
    public class DiagnosticsUtils
    {
        /// <summary>
        /// Returns a key value pair list of drives and available space.
        /// </summary>
        public static IDictionary GetDrivesInfo()
        {
            string[] drives = Environment.GetLogicalDrives();
            StringBuilder buffer = new StringBuilder();
            IDictionary allDriveInfo = new SortedDictionary<object, object>();

            foreach (string drive in drives)
            {
                global::System.IO.DriveInfo di = new global::System.IO.DriveInfo(drive);
                if (di.IsReady)
                {
                    SortedDictionary<string, string> driveInfo = new SortedDictionary<string, string>();
                    driveInfo["AvailableFreeSpace"] = (di.AvailableFreeSpace / 1000000).ToString() + " Megs";
                    driveInfo["DriveFormat"] = di.DriveFormat;
                    driveInfo["DriveType"] = di.DriveType.ToString();
                    driveInfo["Name"] = di.Name;
                    driveInfo["TotalFreeSpace"] = (di.TotalFreeSpace / 1000000).ToString() + " Megs";
                    driveInfo["TotalSize"] = (di.TotalSize / 1000000).ToString() + " Megs";
                    driveInfo["VolumeLabel"] = di.VolumeLabel;
                    driveInfo["RootDirectory"] = di.RootDirectory.FullName;
                    allDriveInfo[drive] = driveInfo;                    
                }
            }
            return allDriveInfo as IDictionary;
        }


        /// <summary>
        /// Get the machine level information.
        /// </summary>
        /// <returns></returns>
        public static IDictionary GetMachineInfo()
        {
            // Get all the machine info.
            IDictionary machineInfo = new SortedDictionary<string, object>();
            machineInfo["Machine Name"] = Environment.MachineName;
            machineInfo["Domain"] = Environment.UserDomainName;
            machineInfo["User Name"] = Environment.UserName;
            machineInfo["CommandLine"] = Environment.CommandLine;
            machineInfo["ProcessorCount"] = Environment.ProcessorCount;
            machineInfo["OS Version Platform"] = Environment.OSVersion.Platform.ToString();
            machineInfo["OS Version ServicePack"] = Environment.OSVersion.ServicePack;
            machineInfo["OS Version Version"] = Environment.OSVersion.Version.ToString();
            machineInfo["OS Version VersionString"] = Environment.OSVersion.VersionString;
            machineInfo["System Directory"] = Environment.SystemDirectory;
            machineInfo["Memory"] = Environment.WorkingSet.ToString();
            machineInfo["Version"] = Environment.Version.ToString();
            machineInfo["Current Directory"] = Environment.CurrentDirectory;
            return machineInfo;
        }


      

        /// <summary>
        /// Get information about the currently executing process.
        /// </summary>
        /// <returns></returns>
        public static IDictionary GetAppDomainInfo()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            Assembly[] loadedAssemblies = domain.GetAssemblies();
            IDictionary assemblyInfo = new SortedDictionary<object, object>();

            foreach (Assembly assembly in loadedAssemblies)
            {
                string name = assembly.CodeBase.Substring(assembly.CodeBase.LastIndexOf("/") + 1);
                assemblyInfo[name] = assembly.ImageRuntimeVersion + ", " + assembly.GlobalAssemblyCache.ToString() + ", " + assembly.CodeBase;
            }
            return assemblyInfo;
        }


        /// <summary>
        /// Get information about the currently executing process.
        /// </summary>
        /// <returns></returns>
        public static IDictionary GetProcesses()
        {
            Process[] processlist = Process.GetProcesses();
            IDictionary processInfo = new SortedDictionary<object, object>();

            foreach (Process p in processlist)
            {
                processInfo[p.ProcessName] = "";
            }
            return processInfo;
        }
    }
}