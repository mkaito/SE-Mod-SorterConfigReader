using System;
using System.Diagnostics.CodeAnalysis;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Utils;

// https://raw.githubusercontent.com/THDigi/SE-ModScript-Examples/master/Data/Scripts/Examples/AttachedLights/SimpleLog.cs
namespace SorterConfigReader
{
    public class SimpleLog
    {
        public static void Error(object caller, Exception e)
        {
            MyLog.Default.WriteLineAndConsole($"ERROR {caller.GetType().FullName}: {e.ToString()}");
            MyLog.Default.Flush();

            if (MyAPIGateway.Session?.Player != null)
                MyAPIGateway.Utilities.ShowNotification(
                    $"[ERROR: {caller.GetType().FullName}: {e.Message}] | Send SpaceEngineers.Log to mod author", 10000,
                    MyFontEnum.Red);
        }

        public static void Error(object caller, string message)
        {
            MyLog.Default.WriteLineAndConsole($"ERROR {caller.GetType().FullName}: {message}");
            MyLog.Default.Flush();

            if (MyAPIGateway.Session?.Player != null)
                MyAPIGateway.Utilities.ShowNotification(
                    $"[ERROR: {caller.GetType().FullName}: {message}] | Send SpaceEngineers.Log to mod author", 10000,
                    MyFontEnum.Red);
        }

        public static void Info(object caller, string message, bool notify = false, int notifyTime = 5000)
        {
            MyLog.Default.WriteLineAndConsole($"WARNING {caller.GetType().FullName}: {message}");
            MyLog.Default.Flush();

            if (notify)
                MyAPIGateway.Utilities?.ShowNotification($"[WARNING {caller.GetType().FullName}: {message}]",
                    notifyTime, MyFontEnum.Green);
        }
    }
}