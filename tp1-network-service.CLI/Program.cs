// See https://aka.ms/new-console-template for more information

using tp1_network_service.Messages;

FileManager fileManager = new FileManager("S_ecr.txt", "S_lec.txt");

// // Lire le fichier
var line = fileManager.Read();

// Écrire sur le fichier
fileManager.Write(line);

