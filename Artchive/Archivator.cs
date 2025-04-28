namespace Artchive;

public static class Archivator
{
    public static byte[] Compress(byte[] data)
    {
        var dictionary = new Dictionary<string, int>();
        var result = new List<(int, byte)>();
        var current = new List<byte>();
        var dictIndex = 1;

        foreach (var b in data)
        {
            current.Add(b);
            var key = Convert.ToHexString(current.ToArray()); // Унікальний ключ для підрядка
            if (!dictionary.ContainsKey(key))
            {
                var prefix = current.Count == 1 ? 0 : dictionary[Convert.ToHexString(current.GetRange(0, current.Count - 1).ToArray())];
                result.Add((prefix, b));
                dictionary[key] = dictIndex++;
                current.Clear();
            }
        }

        if (current.Count > 0)
        {
            var prefix = dictionary.ContainsKey(Convert.ToHexString(current.ToArray())) ? dictionary[Convert.ToHexString(current.ToArray())] : 0;
            result.Add((prefix, 0)); // спецсимвол
        }

        return ToByteArray(result);
    }


    public static byte[] Decompress(byte[] data)
    {
        var compressed = FromByteArray(data);
        var dictionary = new Dictionary<int, List<byte>>();
        var result = new List<byte>();
        var dictIndex = 1;

        foreach (var (prefix, symbol) in compressed)
        {
            List<byte> entry;

            if (prefix == 0)
            {
                entry = symbol == 0 ? new List<byte>() : new List<byte> { symbol };
            }
            else
            {
                entry = new List<byte>(dictionary[prefix]);
                if (symbol != 0)
                    entry.Add(symbol);
            }

            dictionary[dictIndex++] = entry;
            result.AddRange(entry);
        }

        return result.ToArray();
    }
    
    private static byte[] ToByteArray(List<(int, byte)> compressed)
    {
        var bytes = new byte[compressed.Count * 5];
        int offset = 0;

        foreach (var (index, symbol) in compressed)
        {
            BitConverter.GetBytes(index).CopyTo(bytes, offset); // 4 байти
            bytes[offset + 4] = symbol; // 1 байт
            offset += 5;
        }

        return bytes;
    }
    
    private static List<(int, byte)> FromByteArray(byte[] data)
    {
        var result = new List<(int, byte)>();

        for (int i = 0; i < data.Length; i += 5)
        {
            int index = BitConverter.ToInt32(data, i); 
            byte symbol = data[i + 4];
            result.Add((index, symbol));
        }

        return result;
    }

}