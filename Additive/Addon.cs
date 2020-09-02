namespace Additive
{
    public abstract class Addon
    {
        protected ResourceManager Resources;

        protected Addon()
        {
            Resources = new ResourceManager(AddonName);
        }

        /// <summary>
        /// Access <code>Resources</code> to add resources.
        /// </summary>
        protected abstract void ProvideResources();
        protected abstract void OnLoad();
        protected abstract void OnUnload();
        public abstract string AddonName { get; }
        
        public void Load()
        {
            ProvideResources();
            Resources.Load();
            OnLoad();
        }

        public void Unload()
        {
            OnUnload();
        }
    }
}