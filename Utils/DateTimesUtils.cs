using System.Globalization;

namespace Projet_Csharp.Utils;

public static class DateTimeUtils
{
    // Formats de date acceptés dans les fichiers CSV
    private static readonly string[] Formats = { "yyyy-MM-dd", "dd/MM/yyyy" };

    // Convertit une date texte en date UTC
    public static DateTime ConvertToDateTime(string date)
    {
        if (DateTime.TryParseExact(date.Trim(), Formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime resultat))
        {
            return DateTime.SpecifyKind(resultat, DateTimeKind.Utc);
        }

        Console.WriteLine($"La date {date} est mal renseignée");
        return DateTime.UtcNow;
    }
}