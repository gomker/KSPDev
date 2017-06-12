﻿// Kerbal Development tools.
// Author: igor.zavoychinskiy@gmail.com
// This software is distributed under Public domain license.

using System.Linq;
using UnityEngine;

namespace KSPDev.LogUtils {

/// <summary>Helper class to log a record which is bound to a specific object.</summary>
/// <remarks>
/// <para>
/// It may be useful when there are situations that relate to a specific instance of the commong
/// KSP objects. Like <see cref="Part"/>, <see cref="PartModule"/>, <see cref="Transform"/>, etc.
/// With the hosted logging there will be no need to manually designate for which object the
/// record is being logged.
/// </para>
/// <para>
/// This class has many methods that do exactly the same stuff. They are added for the sake of
/// better type checking to eliminate the case when the call has no host object provided.
/// </para>
/// </remarks>
/// <example><code source="Examples/LogUtils/HostedDebugLog-Examples.cs" region="HostedDebugLog1"/></example>
public static class HostedDebugLog {
  /// <summary>Logs a formatted INFO message with a host identifier.</summary>
  /// <param name="host">
  /// The host object which is bound to the log record. It can be <c>null</c>.
  /// </param>
  /// <param name="format">The format string for the log message.</param>
  /// <param name="args">The arguments for the format string.</param>
  /// <example><code source="Examples/LogUtils/HostedDebugLog-Examples.cs" region="HostedDebugLog1"/></example>
  public static void Info(Part host, string format, params object[] args) {
    Debug.logger.LogFormat(LogType.Log, ObjectToString(host) + " "  + format,
                           args.Select(x => ObjectToString(x)).ToArray());
  }

  /// <inheritdoc cref="Info(Part,string,object[])"/>
  public static void Info(PartModule host, string format, params object[] args) {
    Debug.logger.LogFormat(LogType.Log, ObjectToString(host) + " "  + format,
                           args.Select(x => ObjectToString(x)).ToArray());
  }

  /// <inheritdoc cref="Info(Part,string,object[])"/>
  public static void Info(Transform host, string format, params object[] args) {
    Debug.logger.LogFormat(LogType.Log, ObjectToString(host) + " "  + format,
                           args.Select(x => ObjectToString(x)).ToArray());
  }

  /// <summary>Logs a formatted WARNING message with a host identifier.</summary>
  /// <inheritdoc cref="Info(Part,string,object[])"/>
  public static void Warning(Part host, string format, params object[] args) {
    Debug.logger.LogFormat(LogType.Warning, ObjectToString(host) + " " + format,
                           args.Select(x => ObjectToString(x)).ToArray());
  }

  /// <inheritdoc cref="Warning(Part,string,object[])"/>
  public static void Warning(PartModule host, string format, params object[] args) {
    Debug.logger.LogFormat(LogType.Warning, ObjectToString(host) + " " + format,
                           args.Select(x => ObjectToString(x)).ToArray());
  }

  /// <inheritdoc cref="Warning(Part,string,object[])"/>
  public static void Warning(Transform host, string format, params object[] args) {
    Debug.logger.LogFormat(LogType.Warning, ObjectToString(host) + " " + format,
                           args.Select(x => ObjectToString(x)).ToArray());
  }

  /// <summary>Logs a formatted ERROR message with a host identifier.</summary>
  /// <inheritdoc cref="Info(Part,string,object[])"/>
  public static void Error(Part host, string format, params object[] args) {
    Log(LogType.Error, host, format, args);
  }

  /// <inheritdoc cref="Error(Part,string,object[])"/>
  public static void Error(PartModule host, string format, params object[] args) {
    Log(LogType.Error, host, format, args);
  }

  /// <inheritdoc cref="Error(Part,string,object[])"/>
  public static void Error(Transform host, string format, params object[] args) {
    Log(LogType.Error, host, format, args);
  }

  /// <summary>Generic method to emit a hosted log record.</summary>
  /// <remarks>
  /// For the <see cref="Part"/>, <see cref="PartModule"/> and <see cref="Transform"/> objects
  /// the identifier will be the most informative.
  /// </remarks>
  /// <param name="type">The type of the log record.</param>
  /// <param name="host">
  /// The host object which is bound to the log record. It can be <c>null</c>.
  /// </param>
  /// <param name="format">The format string for the log message.</param>
  /// <param name="args">The arguments for the format string.</param>
  public static void Log(LogType type, object host, string format, params object[] args) {
    Debug.logger.LogFormat(type, ObjectToString(host) + " " + format,
                           args.Select(x => ObjectToString(x)).ToArray());
  }

  /// <summary>Helper method to make a user friendly object name for the logs.</summary>
  /// <remarks>
  /// This method is much more intelligent than a regular <c>ToString()</c>, it can detect some
  /// common types and give a bit more context on them while keeping the output short.
  /// </remarks>
  /// <param name="obj">The object to stringify. It can be <c>null</c>.</param>
  /// <returns>A human friendly string which identifies the host.</returns>
  static string ObjectToString(object obj) {
    if (obj == null) {
      return "[NULL]";
    }
    var partHost = obj as Part;
    if (partHost != null) {
      return "[Part:" + DbgFormatter.PartId(partHost) + "]";
    }
    var moduleHost = obj as PartModule;
    if (moduleHost != null) {
      var moduleNum = moduleHost.part.Modules.IndexOf(moduleHost);
      return "[Part:" + DbgFormatter.PartId(moduleHost.part) + "#Module:" + moduleNum + "]";
    }
    var componentHost = obj as Component;
    if (componentHost != null) {
      return "[" + componentHost.GetType().Name + ":"
          + DbgFormatter.TranformPath(componentHost.transform) + "]";
    }
    return obj.ToString();
  }
}

}  // namespace