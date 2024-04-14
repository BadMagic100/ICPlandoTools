using Modding;
using System;

namespace ICPlandoTools
{
    public class ICPlandoToolsMod : Mod
    {
        private static ICPlandoToolsMod? _instance;

        internal static ICPlandoToolsMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(ICPlandoToolsMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public ICPlandoToolsMod() : base("ICPlandoTools")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            // put additional initialization logic here

            Log("Initialized");
        }
    }
}
