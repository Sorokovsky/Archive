using Artchive;

var compressed = Archivator.Compress("Привіт");
var decompressed = Archivator.Decompress(compressed);

Console.WriteLine(string.Join(", ", compressed));
Console.WriteLine(decompressed);