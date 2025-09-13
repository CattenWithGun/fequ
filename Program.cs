internal class Program
{
    public static void Main(string[] args)
    {
        if(args.Length == 0)
        {
            Console.WriteLine("File Equivalence Checker Written By Catten");
            Console.WriteLine("Check the differences between two files like this:");
            Console.WriteLine("fequ /path/to/first/file /path/to/second/file");
            return;
        }
        else if(args.Length != 2)
        {
            PrintError("Incorrect number of arguments");
            return;
        }

        for(int i = 0; i < args.Length; i++)
        {
            if(!File.Exists(args[i]))
            {
                PrintError($"File doesn't exist at '{Path.GetFullPath(args[i])}'");
                return;
            }
        }

        using FileStream fileStream1 = new(args[0], FileMode.Open, FileAccess.Read, FileShare.Read);
        using BufferedStream bufStream1 = new(fileStream1);

        using FileStream fileStream2 = new(args[1], FileMode.Open, FileAccess.Read, FileShare.Read);
        using BufferedStream bufStream2 = new(fileStream2);

        int readByte1;
        int readByte2;
        do
        {
            readByte1 = bufStream1.ReadByte();
            readByte2 = bufStream2.ReadByte();
            if(readByte1 != readByte2 && readByte1 != -1 && readByte2 != -1)
            {
                string hex1 = readByte1.ToString("X2");
                string hex2 = readByte2.ToString("X2");
                Console.WriteLine($"Diff at byte {bufStream1.Position}, 0x{hex1} =/= 0x{hex2}");
            }
        }
        while(readByte1 != -1 && readByte2 != -1);

        if(readByte1 != -1 ^ readByte2 != -1)
        {
            Console.WriteLine("File sizes were different");
        }

        fileStream1.Dispose();
        bufStream1.Dispose();

        fileStream2.Dispose();
        bufStream2.Dispose();
    }

    private static void PrintError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }
}
