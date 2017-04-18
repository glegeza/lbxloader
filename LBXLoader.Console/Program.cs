namespace DLS.LBXLoader.Console
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var containerFiles = new List<Container>();
            foreach (var file in args)
            {
                var bytes = System.IO.File.ReadAllBytes(file);
                var container = new Container(bytes);
                containerFiles.Add(container);
            }

            foreach (var container in containerFiles)
            {
                foreach (var packedFile in container.Files)
                {
                    if (packedFile.Description != null)
                    {
                        Console.WriteLine("Name {0}", packedFile.Description.Name);
                        Console.WriteLine("Desc {0}", packedFile.Description.Description);
                        var filename = GetStrippedString(packedFile.Description.Name);
                        try
                        {
                            System.IO.File.WriteAllBytes(filename, packedFile.FileBytes);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Failed to write file {0}: {1}", filename, e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown Name");
                    }
                    Console.WriteLine("Size {0}, Byte Array {0}", packedFile.Size, packedFile.FileBytes.Length);
                }
            }
        }

        private static string GetStrippedString(string filename)
        {
            char[] arr = filename.ToCharArray();

            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c)
                                              || char.IsWhiteSpace(c)
                                              || c == '-')));
            filename = new string(arr);

            return filename;
        }
    }
}
