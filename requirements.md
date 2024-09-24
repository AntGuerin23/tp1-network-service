# Simulation de Service Réseau (Ajouter les détails de logging , réviser le documents)

## 1. Vue d’ensemble

Ce projet implémente une simulation d’un service de réseau connecté entre deux systèmes, appelés **Système A** et **Système B**, chacun contenant une couche transport et une couche réseau. L’objectif est de développer et de simuler l’interaction entre ces couches, en mettant l’accent sur :
- **L’établissement de connexion**
- **Le transfert de données**
- **La libération de connexion**

Le service réseau suit un modèle client-serveur où la **Couche Transport (ET)** agit comme le client, et la **Couche Réseau (ER)** agit comme le serveur. Chaque couche suit des protocoles et des primitives spécifiques pour échanger des informations.

---

## 2. Architecture du Système

### 2.1 Modèle en Couches
- **Couche Transport (ET)** : Responsable de la gestion de la communication à un niveau supérieur. Elle gère les demandes de connexion, l'envoi et la réception de données, ainsi que la fermeture des connexions.
- **Couche Réseau (ER)** : Fournit des services à la couche transport en gérant les paquets, en établissant les connexions et en assurant le transfert fiable des données sur le réseau physique.

Chaque couche communique avec son homologue dans l'autre système. La **Couche Réseau** offre des services à la **Couche Transport** en utilisant les primitives de service réseau (définies ci-dessous).

---

## 3. Phases d’Opération

La communication entre les systèmes se déroule en trois phases distinctes :

### 3.1 Établissement de Connexion
Cette phase commence lorsque la couche transport du Système A demande une connexion à la couche transport du Système B.

- **Primitives** : `N_CONNECT.req`
- **Flux** : La couche transport envoie une demande de connexion (`N_CONNECT.req`) à la couche réseau. Si la couche transport destinataire accepte, la connexion est établie à l’aide de `N_CONNECT.conf`. Si la connexion est refusée, `N_DISCONNECT.req` est émis et la connexion est terminée.

### 3.2 Transfert de Données
Une fois la connexion établie, la couche transport peut envoyer des données entre le Système A et le Système B.

- **Primitives** : `N_DATA.req`
- **Flux** : Les données sont transmises à l’aide de `N_DATA.req` par la couche transport. La couche réseau transmet les données en utilisant `N_DATA.ind` à la couche transport du système destinataire. Des accusés de réception (`ACK`) sont renvoyés pour assurer la fiabilité des données.

### 3.3 Libération de Connexion
Une fois la communication terminée, la connexion doit être fermée.

- **Primitives** : `N_DISCONNECT.req`
- **Flux** : Chaque système peut initier une déconnexion. Une demande (`N_DISCONNECT.req`) est envoyée, suivie d'une indication (`N_DISCONNECT.ind`), signalant que la connexion est fermée.

---

## 4. Primitives de Service Réseau

L'interaction entre les couches transport et réseau est définie par un ensemble de primitives de service. Ces primitives sont classées en trois catégories selon la phase de communication :

### 4.1 Primitives d’Établissement de Connexion
- **`N_CONNECT.req`** : La couche transport demande à la couche réseau d’établir une connexion. Elle inclut les adresses source et destination.
- **`N_CONNECT.ind`** : Indique à la couche transport du côté récepteur qu’une demande de connexion a été faite.
- **`N_CONNECT.resp`** : La couche transport réceptrice répond à la demande de connexion.
- **`N_CONNECT.conf`** : La couche transport confirme l’établissement de la connexion au côté demandeur.

### 4.2 Primitives de Transfert de Données
- **`N_DATA.req`** : Utilisée par la couche transport pour envoyer des données à travers le réseau.
- **`N_DATA.ind`** : Indique à la couche transport réceptrice que des données ont été reçues.

### 4.3 Primitives de Libération de Connexion
- **`N_DISCONNECT.req`** : Envoyée par la couche transport pour demander la libération de la connexion.
- **`N_DISCONNECT.ind`** : Indique que la connexion a été libérée.

---

## 5. Structure et Format des Messages

Chaque message ou paquet envoyé entre les couches transport et réseau suit un format strict. La structure du paquet est la suivante :

### 5.1 Paquets d’Établissement de Connexion
- **Type de Paquet** : 
  - `00001011` pour `N_CONNECT.req` (demande de connexion)
  - `00001111` pour `N_CONNECT.conf` (connexion établie)
  - `00010011` pour `N_DISCONNECT.ind` (déconnexion)
- **Champs** :
  - **Numéro de connexion** : Attribué à chaque connexion.
  - **Adresse source** : Adresse du système émetteur.
  - **Adresse destination** : Adresse du système récepteur.

### 5.2 Paquets de Transfert de Données
- **Type de Paquet** : `00000001` pour le transfert de données.
- **Champs** :
  - **Numéro de connexion**
  - **Adresses source et destination**
  - **Données** : Données de l’utilisateur
  - **Numéro de séquence** : Utilisé pour suivre la séquence des paquets.
  - **Numéro d’accusé de réception** : Envoyé en réponse aux données reçues.

### 5.3 Paquets de Déconnexion
- **Type de Paquet** : `00010011` pour la déconnexion.
- **Champs** :
  - **Numéro de connexion**
  - **Adresses source et destination**
  - **Raison** : Raison de la déconnexion.

---

## 6. Communication Basée sur des Fichiers

La simulation utilise des fichiers pour représenter la communication entre les couches. Chaque couche lit et écrit dans des fichiers spécifiques pour envoyer et recevoir des messages.

### 6.1 Structure des Fichiers
- **`S_lec.txt`** : Utilisé par la couche transport pour lire les messages entrants (de la couche application).
- **`S_ecr.txt`** : Utilisé par la couche transport pour écrire les messages sortants.
- **`L_ecr.txt`** : Utilisé par la couche réseau pour écrire les paquets sortants.
- **`L_lec.txt`** : Utilisé par la couche réseau pour lire les paquets entrants (de la couche liaison).

### 6.2 Opérations sur les Fichiers
Chaque opération sur un fichier consiste à :
- **Lire le fichier** : Pour récupérer les demandes, données ou statuts de connexion entrants.
- **Écrire dans le fichier** : Pour envoyer des demandes, transférer des données ou confirmer des actions.

Les fichiers sont lus et écrits dans des formats spécifiques pour assurer la cohérence entre les couches.

---

## 7. Processus de Simulation

### 7.1 Établissement de Connexion
- **ET (Couche Transport)** envoie `N_CONNECT.req` à **ER (Couche Réseau)** en écrivant dans `L_ecr.txt`.
- **ER** lit la demande, la traite et écrit soit une acceptation (`N_CONNECT.conf`), soit un refus (`N_DISCONNECT.ind`) dans `L_ecr.txt`.
- **ET** lit la réponse et met à jour l’état de la connexion.

### 7.2 Transfert de Données
- **ET** envoie des données à l’aide de `N_DATA.req` en écrivant dans `L_ecr.txt`.
- **ER** lit les données, les accuse réception, puis les envoie à la couche transport du système récepteur.

### 7.3 Libération de Connexion
- **ET** envoie `N_DISCONNECT.req` pour fermer la connexion en écrivant dans `L_ecr.txt`.
- **ER** traite la déconnexion et écrit `N_DISCONNECT.ind` pour indiquer que la connexion a été fermée.

---

## 8. Gestion des Erreurs et Simulation des Cas Limites

Le projet doit simuler des situations où :
  - Une demande de connexion est refusée en raison de contraintes de ressources du système ou par la couche transport destinataire.
  - Des paquets de données sont perdus ou non accusés réception.
  - Le système doit gérer les mécanismes de retransmission et de temporisation si un accusé de réception n'est pas reçu.

---

## 9. Spécifications Supplémentaires

### 9.1 Adressage
- Le réseau peut gérer jusqu'à 255 stations (adresses de 0 à 254). Les adresses source et destination de chaque connexion sont attribuées aléatoirement mais doivent être distinctes.

### 9.2 Simulation de la Couche Liaison
- La couche liaison est simulée à l’aide des fichiers `L_ecr.txt` et `L_lec.txt`. Les réponses entrantes de la couche liaison sont simulées par la lecture de `L_lec.txt`.

### 9.3 Comportement Aléatoire
- La simulation inclut de la randomisation dans le comportement de la couche liaison :
    - **Les demandes de connexion** sont abandonnées si l’adresse source est un multiple de 19.
    - **Les paquets de données** ne sont pas accusés réception si l’adresse source est un multiple de 15.

