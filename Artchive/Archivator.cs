namespace Artchive;

public static class Archivator
{
    public static (int, char)[] Compress(string text)
    {
        var dictionary = new Dictionary<string, int>();
        var result = new List<(int, char)>();
        var current = "";
        var dictIndex = 1;

        foreach (var c in text)
        {
            var next = current + c;
            if (!dictionary.ContainsKey(next))
            {
                var prefixIndex = current == "" ? 0 : dictionary[current];
                result.Add((prefixIndex, c));
                dictionary[next] = dictIndex++;
                current = "";
            }
            else
            {
                current = next;
            }
        }
        
        if (current != "")
        {
            result.Add((dictionary[current], '\0'));
        }

        return result.ToArray();
    }
}