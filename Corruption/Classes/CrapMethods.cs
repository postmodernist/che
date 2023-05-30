using System;
using System.IO; 

namespace Corruption.Classes;

internal static class CrapMethods
{
    
    static readonly byte[] nullByte = new byte[] { 0 };

    internal static long OverwriteNewByte(FileStream fs)
    {
        byte[] newByt = new byte[] { (byte)new Random().Next(1, 255) };
        fs.Write(newByt, 0, newByt.Length);
        fs.Flush();
        return newByt.LongLength;
    }


    internal static long NullifyByte(FileStream fs)
    {
        fs.Write(nullByte, 0, nullByte.Length);
        fs.Flush();
        return nullByte.LongLength;
    }

    internal static long SwapBytes(FileStream fs, long startingPoint)
    {
        long lastKnownPos1, lastKnownPos2;
        int len = 1;
        if (fs.Length > 64)
            len = new Random().Next(1, 64);

        byte[] firstByte = new byte[len];
        byte[] secByte = new byte[len];

        ChangeStreamPosition(fs, startingPoint); // Randomize the first position.

        lastKnownPos1 = fs.Position;
        fs.Read(firstByte, 0, firstByte.Length);

        ChangeStreamPosition(fs, startingPoint); // Randomize the second position.

        lastKnownPos2 = fs.Position;
        fs.Read(secByte, 0, secByte.Length);

        // Begin the swapping phase.
        fs.Position = lastKnownPos1;
        fs.Write(secByte, 0, secByte.Length);
        fs.Position = lastKnownPos2;
        fs.Write(firstByte, 0, firstByte.Length);

        fs.Flush();

        Array.Clear(firstByte, 0, firstByte.Length);
        Array.Clear(secByte, 0, secByte.Length);

        return len;
    }
    internal static void ChangeStreamPosition(FileStream f, long startingpoint)
    {
        long len = f.Length;
        f.Position = startingpoint;
        long nextPos;
        try
        {
            nextPos = new Random().NextInt64(startingpoint, len);
        }
        catch
        {
            nextPos = startingpoint;
        }
        f.Seek(nextPos, SeekOrigin.Begin);
    }
}