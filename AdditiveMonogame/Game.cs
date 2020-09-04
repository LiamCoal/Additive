using System;
using System.Collections.Generic;
using AdditiveMonogame.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdditiveMonogame
{
    public abstract class Game<T> : Game
    {
        protected SpriteBatch SpriteBatch;
        public IInfo<string, T> Info;

        protected List<Addon<T>> Addons { get; } = new List<Addon<T>>();
        
        protected Game(IInfo<string, T> info)
        {
            Info = info;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected abstract void LoadAllAddons();

        protected void UseAddons(string directory)
        {
            List<Addon<T>> addons = AdditiveMonogame.LoadAddons<T>(directory);
            Addons.AddRange(addons);
        }

        protected override void LoadContent()
        {
            LoadAllAddons();
            Addons.ForEach(addon => addon.LoadResources());
            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            Addons.ForEach(addon => addon.Draw(gameTime));
            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            Addons.ForEach(addon => addon.Update(gameTime, Info));
            base.Update(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Addons.ForEach(addon => addon.Unload());
            base.OnExiting(sender, args);
        }
    }
}