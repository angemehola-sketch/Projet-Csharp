# Projet: Concession automobile

Nous avons développé une application en C# permettant de gérer une concession (les voitures, les clients et les ventes). Nos données sont stockées dans une base PostgreSQL, accessible grâce à Entity Framework Core.

Projet réalisé en groupe dans le cadre du module de développement C# à Sup de Vinci.

## Ce que fait Notre application

Au premier lancement, l'application importe les deux fichiers CSV de notre base de données :

 "client.csv" avec le: nom, prénom, date de naissance, téléphone, email
 "voiture.csv" (le/la): marque, modèle, année, prix hors taxes, couleur, vendue ou non

Les voitures déjà vendues sont ensuite attribuées au hasard à des clients. Aux lancements suivants, rien n'est réimporté.

Nous avons mis en place un menu qui permet de :

1. Voir toutes les voitures
2. Voir tous les clients
3. Consulter le détail d'une voiture (caractéristiques et propriétaires)
4. Consulter le détail d'un client (informations et voitures possédées)
5. Vendre une voiture à un client
6. Changer le propriétaire d'une voiture déjà vendue
7. Connaître les gains de la semaine (somme des prix hors taxes des ventes des 7 derniers jours)

## Règles de gestion données dans la consigne

- Un client possède 2 voitures au maximum.
- Une voiture peut appartenir à plusieurs clients.
- Une voiture déjà vendue ne peut pas être vendue une seconde fois, mais elle peut changer de propriétaire.

## Les technologies que nous avons utilisées:

- C# et .NET 10
- Entity Framework Core 
- PostgreSQL 17
- Docker et Docker Compose pour la base de données

## Notre processus d'installation et lancement

Pour la réalisation de notre projet nous avons:

1. Cloner le dépôt et se placer dans le dossier du projet.

2. Démarrer la base de données avec la commande "docker compose -f Docker/docker-compose. up -d"

3. Créer les tables avec la migration avec la commande "dotnet ef database update"

4. Lancer l'application avec la commande "dotnet run"

NB: La connexion à notre base de donnée et les chemins des fichiers CSV se règlent dans le fichier "appsettings.json".

## Organisation du code

 Le dossier "Models" contenant les fichiers "Car", "Customer" et "Ownership",
 Le dossier "InterfaceRepository"  contenant les interfaces des dépôts de données et la classe d'initialisation "DbConnection"
 Le dossier "Repository" contenat l'accès aux données tel que consultation, vente, changement de propriétaire, gains 
 Le dossier  "Utils" contenant la lecture des CSV, la conversion des dates et le menu 
 Le dossier  "Migrations" contenant l'historique de la structure de la base 
 Le dossier  "Files-csv" contenant les fichiers de données CSV (générés avec une IA) 
 Le dossier  "Docker" contenant le fichier de configuration de PostgreSQL 

NB: une voiture et un client sont reliés par la table "Ownership", qui garde aussi la date de vente. Cela permet à une voiture d'avoir plusieurs propriétaires et à un client d'avoir plusieurs voitures.


## Notre travail en groupe

- Nous avons travaillé sur des branches, puis fusionné dans `main` avec des demandes de fusion "pull requests".
- Nous avons résolu ensemble des conflits apparus lors des fusions.
- Nous nous sommes appuyés sur le cours et sur le projet réalisé avec le formateur, ainsi que de la documentation en ligne("https://learn.microsoft.com/en-us/dotnet/csharp/") et de l'aide du frère de note collègue Ange avec la même structure (contexte, interfaces, dépôts), mêmes outils (Linq, Include, migrations) que le projet réaliser en cours avec notre formateur.

## Auteurs

- Ange N'DAH
- Christ MBOCHI
- Lucien EBATA

Sup de Vinci, Bachelor Informatique, 2026
