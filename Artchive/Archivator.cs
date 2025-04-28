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

        // якщо залишився "current" — його треба правильно допакувати
        if (!string.IsNullOrEmpty(current))
        {
            var prefixIndex = dictionary.ContainsKey(current) ? dictionary[current] : 0;
            // УВАГА: додаємо спецсимвол '\0', бо нового символа немає
            result.Add((prefixIndex, '\0'));
        }

        return result.ToArray();
    }

    public static string Decompress((int, char)[] compressed)
    {
        var dictionary = new Dictionary<int, string>();
        var result = new List<char>();
        var dictIndex = 1;

        foreach (var (prefixIndex, symbol) in compressed)
        {
            string entry;
        
            if (prefixIndex == 0)
            {
                entry = symbol.ToString();
            }
            else
            {
                entry = dictionary[prefixIndex] + symbol;
            }

            dictionary[dictIndex++] = entry;
            result.AddRange(entry); 
        }

        return new string(result.ToArray());
    }

}