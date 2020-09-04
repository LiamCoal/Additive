using System;

namespace Additive
{
    public static class Logging
    {
        private static string _lineText = "";
        public static string LineText
        {
            get => _lineText;
            set
            {
                _lineText = value;
                Update();
            }
        }
            
        public static void Update() => Console.Write($"\u001b[2K\u001b[1000D{_lineText}");
        public static void Erase() => Console.Write("\u001b[2K\u001b[1000D");
    }
}