using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AdditiveMonogame.Interface;
using static AdditiveMonogame.Logging;

namespace AdditiveMonogame
{
    public class AdditiveMonogame
    {

        public static List<Addon<T>> LoadAddons<T>(string directory)
        {
            List<Addon<T>> addons = new List<Addon<T>>();
            foreach (var filename in Directory.EnumerateFiles("Addons", "*.dll"))
            {
                var asm = Assembly.LoadFile(Path.GetFullPath(filename));
                foreach (var type in asm.GetTypes())
                {
                    if (!type.IsSubclassOf(typeof(Addon<T>))) continue;
                    Addon<T> addon = Activator.CreateInstance(type) as Addon<T> ?? throw new NullReferenceException($"Null addon. [in {filename}]");
                    LineText = $"Loading addon: {addon.Name}";
                    addon.LoadResources();
                    addons.Add(addon);
                    Erase();
                    Console.WriteLine($"Loaded Addon {addon.Name} from {filename}");
                    Update();
                }
            }

            LineText = "Finished loading addons.";

            return addons;
        }
    }
}