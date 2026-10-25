using Projet_Csharp.InterfaceRepository;

namespace Projet_Csharp.Utils;

public class Menu
{
    private readonly ICarRepository _cars;
    private readonly ICustomerRepository _customers;

    public Menu(ICarRepository cars, ICustomerRepository customers)
    {
        _cars = cars;
        _customers = customers;
    }

    #region Boucle principale

    public void Run()
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            // Affichage des choix possibles

            Console.WriteLine();
            Console.WriteLine("===== CONCESSION =====");
            Console.WriteLine("1 - Voir toutes les voitures");
            Console.WriteLine("2 - Voir tous les clients");
            Console.WriteLine("3 - Détail d'une voiture");
            Console.WriteLine("4 - Détail d'un client");
            Console.WriteLine("5 - Vendre une voiture");
            Console.WriteLine("6 - Changer le propriétaire d'une voiture vendue");
            Console.WriteLine("7 - Gains de la semaine");
            Console.WriteLine("0 - Quitter");
            Console.Write("Votre choix : ");

            switch (Console.ReadLine())
            {
                case "1": ShowCars(); break;
                case "2": ShowCustomers(); break;
                case "3": ShowCarDetails(); break;
                case "4": ShowCustomerDetails(); break;
                case "5": SellCar(); break;
                case "6": ChangeOwner(); break;
                case "7": ShowWeeklyRevenue(); break;
                case "0": keepRunning = false; break;
                default: Console.WriteLine("Choix invalide."); break;
            }
        }
    }

    #endregion

    #region Actions
    // Affiche la liste de toutes les voitures
    private void ShowCars()
    {
        foreach (Car car in _cars.GetAll())
            Console.WriteLine(CarLabel(car));
    }
    // Affiche la liste de tous les client
    private void ShowCustomers()
    {
        foreach (Customer customer in _customers.GetAll())
            Console.WriteLine(CustomerLabel(customer));
    }
    // Affiche les caractéristiques d'une voiture et ses propriétaires
    private void ShowCarDetails()
    {
        // L'utilisateur choisit une voiture dans la liste
        Car? choice = Choose(_cars.GetAll(), CarLabel);
        if (choice == null) return;

        // Rechargement de la voiture avec ses propriétaires
        Car? car = _cars.GetWithOwners(choice.Id);
        if (car == null) return;

        Console.WriteLine($"Marque : {car.Brand}");
        Console.WriteLine($"Modèle : {car.Model}");
        Console.WriteLine($"Année : {car.Year}");
        Console.WriteLine($"Couleur : {car.Color}");
        Console.WriteLine($"Prix HT : {car.PriceExclTax:0.00} €");
        Console.WriteLine($"Statut : {(car.IsSold ? "vendue" : "disponible")}");

        // Liste des propriétaires de la voiture
        foreach (Ownership ownership in car.Ownerships)
            Console.WriteLine($"Propriétaire : {CustomerLabel(ownership.Customer)} (depuis le {ownership.SaleDate:dd/MM/yyyy})");
    }
    // Affiche les informations d'un client et ses voitures
    private void ShowCustomerDetails()
    {
        // L'utilisateur choisit un client dans la liste
        Customer? choice = Choose(_customers.GetAll(), CustomerLabel);
        if (choice == null) return;

        // Rechargement du client avec ses voitures
        Customer? customer = _customers.GetWithCars(choice.Id);
        if (customer == null) return;

        Console.WriteLine($"Nom : {customer.LastName}");
        Console.WriteLine($"Prénom : {customer.FirstName}");
        Console.WriteLine($"Date de naissance : {customer.BirthDate:dd/MM/yyyy}");
        Console.WriteLine($"Téléphone : {customer.Phone}");
        Console.WriteLine($"Email : {customer.Email}");

        // Liste des voitures possédées par le client
        foreach (Ownership ownership in customer.Ownerships)
            Console.WriteLine($"Voiture : {CarLabel(ownership.Car)}");
    }

    // Vend une voiture disponible à un client
    private void SellCar()
    {
        // Seules les voitures non vendues sont proposées
        Car? car = Choose(_cars.GetAvailable(), CarLabel);
        if (car == null) return;

        Customer? customer = Choose(_customers.GetAll(), CustomerLabel);
        if (customer == null) return;

        // Le dépôt vérifie les règles et renvoie un message
        Console.WriteLine(_cars.Sell(car.Id, customer.Id));
    }

    // Change le propriétaire d'une voiture déjà vendue
    private void ChangeOwner()
    {
        // Seules les voitures vendues sont proposées
        Car? choice = Choose(_cars.GetSold(), CarLabel);
        if (choice == null) return;

        Car? car = _cars.GetWithOwners(choice.Id);
        if (car == null) return;

        // Choix de l'ancien propriétaire à remplacer
        Console.WriteLine("Quel propriétaire remplacer ?");
        Ownership? ownership = Choose(car.Ownerships.ToList(), o => CustomerLabel(o.Customer));
        if (ownership == null) return;

        // Choix du nouveau propriétaire
        Console.WriteLine("Nouveau propriétaire :");
        Customer? newOwner = Choose(_customers.GetAll(), CustomerLabel);
        if (newOwner == null) return;

        Console.WriteLine(_cars.ChangeOwner(ownership.Id, newOwner.Id));
    }

    // Affiche la somme gagnée avec les ventes des 7 derniers jours
    private void ShowWeeklyRevenue()
    {
        Console.WriteLine($"Gains de la semaine : {_cars.GetWeeklyRevenue():0.00} € HT");
    }

    #endregion

    #region Outils d'affichage

    // Affiche une liste numérotée et renvoie l'élément choisi ou null
    private static T? Choose<T>(List<T> items, Func<T, string> label) where T : class
    {
        // Rien à choisir si la liste est vide
        if (items.Count == 0)
        {
            Console.WriteLine("Aucun élément disponible.");
            return null;
        }

        // Affichage de la liste numérotée à partir de 1
        for (int i = 0; i < items.Count; i++)
            Console.WriteLine($"{i + 1} - {label(items[i])}");

        // Lecture et vérification du numéro saisi
        Console.Write("Votre choix (numéro) : ");
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= items.Count)
            return items[number - 1];

        Console.WriteLine("Choix invalide.");
        return null;
    }

    // Texte affiché pour une voiture
    private static string CarLabel(Car c)
    {
        // Statut de la voiture selon qu'elle est vendue ou non
        string status = "disponible";
        if (c.IsSold)
        {
            status = "vendue";
        }

        return $"{c.Brand} {c.Model} ({c.Year}) - {c.Color} - {c.PriceExclTax:0.00} € HT - {status}";
    }

    // Texte affiché pour un client
    private static string CustomerLabel(Customer c)
    {
        return $"{c.FirstName} {c.LastName}";
    }

    #endregion
}