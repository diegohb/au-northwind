namespace Northwind.Core;

using System.Reflection;

public static class TypeProvider
{
  public static Type? GetTypeFromAnyReferencingAssembly(string typeNameParam)
  {
    var referencedAssemblies = Assembly.GetEntryAssembly()
      ?.GetReferencedAssemblies()
      .Select(a => a.FullName);

    return AppDomain.CurrentDomain.GetAssemblies()
      .Where(a => referencedAssemblies!.Contains(a.FullName))
      .SelectMany(a => a.GetTypes().Where(x => x.Name == typeNameParam))
      .FirstOrDefault();
  }
}