using Microsoft.Xna.Framework;

namespace AdditiveMonogame.Interface
{
    public interface IAddon<T2>
    {
        public void LoadResources();
        public void Update(GameTime time, IInfo<string, T2> info);
        public void Draw(GameTime time);
        
        public void Unload();
    }
}