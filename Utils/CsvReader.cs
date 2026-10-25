using System.Globalization;

namespace Projet_Csharp.Utils;

public static class CsvReader
{
	#region Lecture des clients
	
	// Lit le fichier CSV des clients et renvoie la liste des clients
	// Colonnes : Nom@Prénom@Date de naissance@Téléphone@Email

	public static List<Customer> ReadCustomers(string path, char separator = '@')
	{
		// Liste qui contiendra tous les clients lus
		List<Customer> customers = new List<Customer>();
		string[] lines = File.ReadAllLines(path);

		// On commence à 1 pour ignorer la ligne d'en-tête
		for (int i = 1; i < lines.Length; i++)
		{
			if (string.IsNullOrWhiteSpace(lines[i])) continue;
			string[] columns = lines[i].Split(separator);
			
			// Création du client et remplissage de ses information
			customers.Add(new Customer
			{
				LastName = columns[0],
				FirstName = columns[1],
				BirthDate = DateTimeUtils.ConvertToDateTime(columns[2]),
				Phone = columns[3],
				Email = columns[4]

			});
		}
		return customers;
	}

	#endregion

	#region Lecture des voitures

	// Colonnes : Marque$Modele$Annee$Prix_HT$Couleur$Vendue
	public static List<Car> ReadCars(string path, char separator = '$')
	{
		List<Car> cars = new List<Car>();
		string[] lines = File.ReadAllLines(path);

		for (int i = 1; i < lines.Length; i++)
		{
			if (string.IsNullOrWhiteSpace(lines[i])) continue;
			string[] columns = lines[i].Split(separator);

			cars.Add(new Car
			{
				Brand = columns[0],
				Model = columns[1],
				Year = int.Parse(columns[2]),
				PriceExclTax = decimal.Parse(columns[3], CultureInfo.InvariantCulture),
				Color = columns[4],
				IsSold = bool.Parse(columns[5])
			});
		}
		return cars;
	}

	#endregion
}