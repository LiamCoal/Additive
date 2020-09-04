using System;
using System.Data;
using Additive;
using static Additive.Logging;

namespace TestAdditiveAddon
{
    public class TestAdditiveAddon : Addon
    {
        protected override void ProvideResources()
        {
            Resources.Add("test.resource", 6);
            Resources.Add("test.resource.2", (name, _) => 7);
        }

        protected override void OnLoad()
        {
            //LineText = ResourceManager.Get("test.resource")?.ToString() ?? throw new NullReferenceException();
        }

        protected override void OnUnload()
        {
            //LineText = "unload";
        }

        public override string AddonName => "TestAdditiveAddon";
    }
}