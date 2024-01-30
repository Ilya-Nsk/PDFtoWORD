// Decompiled with JetBrains decompiler
// Type: Costura.AssemblyLoader
// Assembly: PDFtoWORD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04E2525B-49CD-437A-AAAB-5C0C310B7016
// Assembly location: E:\АРХИВ\БГУ АРХИВ\PDFtoWORD.exe

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Costura
{
  [CompilerGenerated]
  internal static class AssemblyLoader
  {
    private static object nullCacheLock = new object();
    private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();
    private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();
    private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();
    private static int isAttached;

    private static string CultureToString(CultureInfo culture)
    {
      return culture == null ? "" : culture.Name;
    }

    private static Assembly ReadExistingAssembly(AssemblyName name)
    {
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        AssemblyName name1 = assembly.GetName();
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        if (string.Equals(name1.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name1.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
          return assembly;
      }
      return (Assembly) null;
    }

    private static void CopyTo(Stream source, Stream destination)
    {
      byte[] buffer = new byte[81920];
      int count;
      while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
        destination.Write(buffer, 0, count);
    }

    private static Stream LoadStream(string fullName)
    {
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      if (!fullName.EndsWith(".compressed"))
        return executingAssembly.GetManifestResourceStream(fullName);
      using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName))
      {
        using (DeflateStream source = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
        {
          MemoryStream destination = new MemoryStream();
          // ISSUE: reference to a compiler-generated method
          AssemblyLoader.CopyTo((Stream) source, (Stream) destination);
          destination.Position = 0L;
          return (Stream) destination;
        }
      }
    }

    private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
    {
      string fullName;
      // ISSUE: reference to a compiler-generated method
      return resourceNames.TryGetValue(name, out fullName) ? AssemblyLoader.LoadStream(fullName) : (Stream) null;
    }

    private static byte[] ReadStream(Stream stream)
    {
      byte[] buffer = new byte[stream.Length];
      stream.Read(buffer, 0, buffer.Length);
      return buffer;
    }

    private static Assembly ReadFromEmbeddedResources(
      Dictionary<string, string> assemblyNames,
      Dictionary<string, string> symbolNames,
      AssemblyName requestedAssemblyName)
    {
      string name = requestedAssemblyName.Name.ToLowerInvariant();
      if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
        name = requestedAssemblyName.CultureInfo.Name + "." + name;
      byte[] rawAssembly;
      // ISSUE: reference to a compiler-generated method
      using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, name))
      {
        if (stream == null)
          return (Assembly) null;
        // ISSUE: reference to a compiler-generated method
        rawAssembly = AssemblyLoader.ReadStream(stream);
      }
      // ISSUE: reference to a compiler-generated method
      using (Stream stream = AssemblyLoader.LoadStream(symbolNames, name))
      {
        if (stream != null)
        {
          // ISSUE: reference to a compiler-generated method
          byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream);
          return Assembly.Load(rawAssembly, rawSymbolStore);
        }
      }
      return Assembly.Load(rawAssembly);
    }

    public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
    {
      // ISSUE: reference to a compiler-generated field
      lock (AssemblyLoader.nullCacheLock)
      {
        // ISSUE: reference to a compiler-generated field
        if (AssemblyLoader.nullCache.ContainsKey(e.Name))
          return (Assembly) null;
      }
      AssemblyName assemblyName = new AssemblyName(e.Name);
      // ISSUE: reference to a compiler-generated method
      Assembly assembly1 = AssemblyLoader.ReadExistingAssembly(assemblyName);
      if ((object) assembly1 != null)
        return assembly1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      Assembly assembly2 = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
      if ((object) assembly2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        lock (AssemblyLoader.nullCacheLock)
        {
          // ISSUE: reference to a compiler-generated field
          AssemblyLoader.nullCache[e.Name] = true;
        }
        if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
          assembly2 = Assembly.Load(assemblyName);
      }
      return assembly2;
    }

    static AssemblyLoader()
    {
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("costura", "costura.costura.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.symbolNames.Add("costura", "costura.costura.pdb.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("microsoft.win32.primitives", "costura.microsoft.win32.primitives.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("sautinsoft.pdffocus", "costura.sautinsoft.pdffocus.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.appcontext", "costura.system.appcontext.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.console", "costura.system.console.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.diagnostics.diagnosticsource", "costura.system.diagnostics.diagnosticsource.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.diagnostics.tracing", "costura.system.diagnostics.tracing.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.globalization.calendars", "costura.system.globalization.calendars.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.io.compression", "costura.system.io.compression.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.io.compression.zipfile", "costura.system.io.compression.zipfile.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.io", "costura.system.io.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.io.filesystem", "costura.system.io.filesystem.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.io.filesystem.primitives", "costura.system.io.filesystem.primitives.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.linq", "costura.system.linq.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.linq.expressions", "costura.system.linq.expressions.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.net.http", "costura.system.net.http.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.net.sockets", "costura.system.net.sockets.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.reflection", "costura.system.reflection.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.runtime", "costura.system.runtime.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.runtime.extensions", "costura.system.runtime.extensions.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.runtime.interopservices", "costura.system.runtime.interopservices.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.runtime.interopservices.runtimeinformation", "costura.system.runtime.interopservices.runtimeinformation.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.security.cryptography.algorithms", "costura.system.security.cryptography.algorithms.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.security.cryptography.encoding", "costura.system.security.cryptography.encoding.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.security.cryptography.primitives", "costura.system.security.cryptography.primitives.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.security.cryptography.x509certificates", "costura.system.security.cryptography.x509certificates.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.text.regularexpressions", "costura.system.text.regularexpressions.dll.compressed");
      // ISSUE: reference to a compiler-generated field
      AssemblyLoader.assemblyNames.Add("system.xml.readerwriter", "costura.system.xml.readerwriter.dll.compressed");
    }

    public static void Attach()
    {
      // ISSUE: reference to a compiler-generated field
      if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
        return;
      AppDomain.CurrentDomain.AssemblyResolve += (ResolveEventHandler) ((sender, e) =>
      {
        // ISSUE: reference to a compiler-generated field
        lock (AssemblyLoader.nullCacheLock)
        {
          // ISSUE: reference to a compiler-generated field
          if (AssemblyLoader.nullCache.ContainsKey(e.Name))
            return (Assembly) null;
        }
        AssemblyName assemblyName = new AssemblyName(e.Name);
        // ISSUE: reference to a compiler-generated method
        Assembly assembly1 = AssemblyLoader.ReadExistingAssembly(assemblyName);
        if ((object) assembly1 != null)
          return assembly1;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        Assembly assembly2 = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
        if ((object) assembly2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          lock (AssemblyLoader.nullCacheLock)
          {
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.nullCache[e.Name] = true;
          }
          if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
            assembly2 = Assembly.Load(assemblyName);
        }
        return assembly2;
      });
    }
  }
}
