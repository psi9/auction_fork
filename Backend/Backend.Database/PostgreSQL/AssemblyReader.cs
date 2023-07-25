using System.Reflection;

namespace Backend.Database.PostgreSQL;

/// <summary>
/// Вспомогательный класс для кеширования ресурсов сборки
/// </summary>
internal class AssemblyReader
{
    /// <summary>
    /// Словарь .sql скриптов
    /// </summary>
    private static readonly Dictionary<string, string> Scripts = new();

    /// <summary>
    /// .ctor
    /// </summary>
    static AssemblyReader()
    {
        var databaseAssembly = Assembly.GetExecutingAssembly();
        
        var resourceNames = databaseAssembly.GetManifestResourceNames()
            .Where(rn => rn.EndsWith(".sql"));

        foreach (var resourceName in resourceNames)
        {
            using var stream = databaseAssembly.GetManifestResourceStream(resourceName);
            using var streamReader = new StreamReader(stream!);
            
            Scripts.Add(resourceName.Replace(".sql", string.Empty), streamReader.ReadToEnd());
        }
    }

    /// <summary>
    /// Получить скрипт
    /// </summary>
    /// <param name="scriptName">Нвазвание скрипта</param>
    /// <returns>Тело скрипта</returns>
    public static string GetScript(string scriptName)
    {
        return Scripts[scriptName];
    }
}