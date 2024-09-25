// See https://aka.ms/new-console-template for more information

using System.Text;
using tp1_network_service;

FileManager manager = new FileManager("S_lec.txt");

var data = manager.Read();

FileManager manager2 = new FileManager("S_ecr.txt");
manager2.Write(data);


