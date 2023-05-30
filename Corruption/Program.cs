using Corruption.Classes;

Console.WriteLine("Hello! ");
Console.WriteLine("Enter target folder address");

string TargetFolder = Console.ReadLine();
    
string[] fileArray = Directory.GetFiles(TargetFolder);
if (fileArray.Length == 0)
{
    Console.WriteLine("Folder empty");
    Environment.Exit(-1);
}

foreach (string file in fileArray)
        Console.WriteLine(file);

Worker.Fuschia(fileArray, new Random());


Console.WriteLine("Good Bye");