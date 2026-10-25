using System.Globalization;

namespace Projet_Csharp.Utils;

public static class CsvLecteur
{
	#region Lecture des clients

	// Colonnes : Nom@Prénom@Date de naissance@Téléphone@Email
	public static List<Customer> LireClients(string chemin, char separateur = '@')
	{
		List<Customer> clients = new List<Customer>();
		string[] lignes = File.ReadAllLines(chemin);

		// On commence à 1 pour ignorer la ligne d'en-tête
		for (int i = 1; i < lignes.Length; i++)
		{
			if (string.IsNullOrWhiteSpace(lignes[i])) continue;
			string[] colonnes = lignes[i].Split(separateur);

			clients.Add(new Customer
			{
				LastName = colonnes[0],
				FirstName = colonnes[1],
				BirthDate = DateTimeUtils.ConvertToDateTime(colonnes[2]),
				Phone = colonnes[3],
				Email = colonnes[4]
			});
		}
		return clients;
	}

	#endregion

	#region Lecture des voitures

	// Colonnes : Marque$Modele$Annee$Prix_HT$Couleur$Vendue
	public static List<Car> LireVoitures(string chemin, char separateur = '$')
	{
		List<Car> voitures = new List<Car>();
		string[] lignes = File.ReadAllLines(chemin);

		for (int i = 1; i < lignes.Length; i++)
		{
			if (string.IsNullOrWhiteSpace(lignes[i])) continue;
			string[] colonnes = lignes[i].Split(separateur);

			voitures.Add(new Car
			{
				Brand = colonnes[0],
				Model = colonnes[1],
				Year = int.Parse(colonnes[2]),
				PriceExclTax = decimal.Parse(colonnes[3], CultureInfo.InvariantCulture),
				Color = colonnes[4],
				IsSold = bool.Parse(colonnes[5])
			});
		}
		return voitures;
	}

	#endregion
}