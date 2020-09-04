using AdditiveMonogame.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace AdditiveMonogame
{
    public abstract class Addon<T> : IAddon<T>
    {
        private ContentManager _content;
        
        // Addon info
        internal Addon(ContentManager content)
        {
            _content = content;
        }

        public abstract string Name { get; }
        public abstract string Version { get; }
        public abstract string Description { get; }
        
        // Methods
        public abstract void LoadResources();
        public abstract void Update(GameTime gameTime, IInfo<string, T> info);
        public abstract void Draw(GameTime gameTime);
        public abstract void Unload();
    }
}