namespace Corruption.Classes;

public class FileSystemExtras
{
    public static bool IsBinary(byte[] bytes)
    {
        
        if (bytes == null) throw new ArgumentNullException("bytes");
        if (bytes.Length > 4096) throw new ArgumentException("Byte length must be at least 4096 bytes (4 KB).");

        if (bytes.Length == 0) return false;
        int ctrlCharCounts = 0;
        for (int i = 0; i < bytes.Length; i++)
        {
            if (IsCtrlChar(bytes[i]))
                ctrlCharCounts++;
        }
        int perce = ctrlCharCounts * 100 / bytes.Length;
        return perce > 10;
    }
    private static bool IsCtrlChar(int chr)
    {
        return (chr > 0 && chr < 8) || (chr > 13 && chr < 26);
    }
}