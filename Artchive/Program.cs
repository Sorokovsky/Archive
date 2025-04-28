using Artchive;

var text = "ABABABABAB";
var data = System.Text.Encoding.ASCII.GetBytes(text);

var compressed = Archivator.Compress(data);
var decompressed = Archivator.Decompress(compressed);

var recoveredText = System.Text.Encoding.ASCII.GetString(decompressed);
Console.WriteLine(recoveredText);