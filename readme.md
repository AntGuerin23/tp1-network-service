
# Projet Réseaux d'Ordinateurs (INF1010)

## Introduction
Ce projet implémente un service connecté de réseau en utilisant **C# avec .NET 8**.  
Le système simule la communication entre deux entités réseau selon trois phases :  
- **Établissement de connexion**  
- **Transfert de données**  
- **Libération de connexion**

## Prérequis
- **.NET 8 SDK** installé.
- Un éditeur de code (par exemple : Visual Studio, Visual Studio Code).
- **Cloner le projet** ou extraire l’archive `.zip` fournie.

## Exécution du Projet
1. **Compiler et exécuter** :
   ```bash
   dotnet run
   ```

   **Important :** Le dossier `/Resources` contenant les fichiers de communication doit se trouver à la racine.

2. **Configurer le fichier d’entrée** :  
   Remplissez le fichier `S_LEC.txt` avec les entrées nécessaires, ligne par ligne, pour simuler la communication.

3. **Sortie des résultats** :  
   Les résultats seront enregistrés dans le fichier `S_ECR.txt`.

4. **Gestion des erreurs** :  
   En cas d’erreur, la console affichera les détails de l’échec et les informations de la connexion concernée.

## Structure du Projet
- **Library** : Contient les classes nécessaires au fonctionnement.
- **CLI** : Fichier `Program.cs` qui utilise la bibliothèque.
- **Tests** : Tests unitaires pour valider certaines classes.

## Fichiers Utilisés
- `S_LEC.txt` : Entrées simulées provenant de la couche transport.
- `S_ECR.txt` : État final de la table des connexions.
- `L_LEC.txt` : Fichier FIFO pour la communication liaison-transport.
- `L_ECR.txt` : Fichier FIFO pour la communication transport-liaison.

## Spécifications Techniques

### Spécification 1
Le projet devait vérifier le **numéro d’adresse source** pour déterminer si le système B doit répondre aux paquets.  
Puisque l’adresse source n'est pas transférée dans le paquet, nous utilisons le **modulo du numéro de connexion** pour implémenter cette fonctionnalité.

### Spécification 2
Le fichier `L_LEC.txt` fonctionne comme un **FIFO** pour gérer la communication entre les systèmes.  
Les lignes ajoutées sont traitées par un thread dédié, et les résultats sont affichés dans la console.

## Auteurs
- **Mathyas Lefebvre**  
- **Julien Deguire**  
- **Antoine Guérin**  
- **Samuel Tessier**
- **Simon Lavigne**

## Date de Remise
**25 octobre 2024**
