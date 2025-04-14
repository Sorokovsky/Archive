using Artchive;

const string input = "ABABABABAB";

var compressed = Archivator.Compress(input);
Console.WriteLine("Стиснено:");
foreach (var (index, symbol) in compressed)
{
    Console.WriteLine($"({index},'{symbol.ToString()}')");
}