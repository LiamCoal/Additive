using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static Additive.Logging;

namespace Additive
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            List<Addon> addons = LoadAddons();
            
            if (Additive.Entrance != null)
                Additive.Entrance();
            
            addons.ForEach(addon => addon.Unload());
        }

        private static List<Addon> LoadAddons()
        {
            List<Addon> addons = new List<Addon>();
            foreach (var filename in Directory.EnumerateFiles("Addons", "*.dll"))
            {
                var asm = Assembly.LoadFile(Path.GetFullPath(filename));
                foreach (var type in asm.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(Addon)))
                    {
                        Addon addon = Activator.CreateInstance(type) as Addon ?? throw new NullReferenceException($"Null addon. [in {filename}]");
                        LineText = $"Loading addon: {addon.AddonName}";
                        addon.Load();
                        addons.Add(addon);
                        Erase();
                        Console.WriteLine($"Loaded Addon {addon.AddonName} from {filename}");
                        Update();
                    }
                }
            }

            LineText = "Finished loading addons.";

            return addons;
        }
    }
}