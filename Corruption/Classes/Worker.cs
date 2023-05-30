namespace Corruption.Classes;

internal static class Worker
{
    internal static void Fuschia(string[] fileArray , Random rnd)
    {
        
        for (int i = 0; i < fileArray.Length; i++)
        {
            using (FileStream fs = new FileStream(fileArray[i], FileMode.Open))
            {
                byte[] firstBytes = new byte[fs.Length > 4096 ? 4096 : fs.Length];
                long startingPoint = FileSystemExtras.IsBinary(firstBytes) ? 128 : 0;
                for (int j = 0; j < 1000; j++)
                {
                    CrapMethods.ChangeStreamPosition(fs, startingPoint);
                    switch (rnd.Next(0, 3))
                    {
                        case 0:
                            CrapMethods.OverwriteNewByte(fs);
                            break;
                        case 1:
                            CrapMethods.SwapBytes(fs, startingPoint);
                            break;
                        case 2:
                            CrapMethods.NullifyByte(fs);
                            break;
                    }
                }
            }
        }
    }
}