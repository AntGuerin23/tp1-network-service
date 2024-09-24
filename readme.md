# Nom du Projet

**Simulation de Service Réseau**

## Aperçu

Ce projet simule un service réseau connecté entre deux systèmes utilisant des couches transport et réseau. Il permet de gérer l'établissement de connexions, le transfert de données et la libération de connexions. Le projet est implémenté en C# avec des fichiers simulant les échanges entre les couches.

## Structure du Dossier (Revalider les structure de fichiers selon la direction du projet)

```plaintext
/ProjetSimulationReseau
│
├── /tp1-network-service     # Code source du projet
│   ├── Program.cs           # Point d'entrée principal
│   ├── TransportEntity.cs   # Gestion de la couche transport
│   ├── NetworkEntity.cs     # Gestion de la couche réseau
│   ├── FileInterface.cs     # Gestion des opérations sur les fichiers
│   ├── logger.cs            # Gestion des fonctionnalités de loggings des transactions
│
├── /tp1-network-service-tests           # Tests unitaires et d'intégration
│   ├── TestFileInterface.cs
│   ├── TestTransportEntity.cs
│   ├── TestNetworkEntity.cs
│
├── requirements.md   # Fichiers de specification technique
└── README.md         # Ce fichier
```

## Liste des classes et fonctionnalité ((Revalider les structure de fichiers selon la direction du projet))

- Enum XYZ : pour lister toute les primitives et type de messages
- Classe packet : information représentant le message transférer pour cette couche
- Classe :
- Classe : 
- Classe : 

## Commandes pour Exécuter les Tests
```plaintext
dotnet test
```

## Commande pour Exécuter l'Application
```plaintext
dotnet run 
```
