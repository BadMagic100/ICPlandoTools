using ItemChanger;
using ItemChanger.Tags;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;

namespace ICPlandoTools.Tags
{
    [PlacementTag]
    public class SuppressVisitStateTag : Tag
    {
        private static readonly MethodInfo abstractPlacement_AddVisitFlag 
            = typeof(AbstractPlacement).GetMethod(nameof(AbstractPlacement.AddVisitFlag));

        private AbstractPlacement? placement;
        private Hook? onAddVisitFlag;

        public VisitState StatesToSuppress { get; set; } = VisitState.None;

        public override void Load(object parent)
        {
            placement = (AbstractPlacement)parent;
            onAddVisitFlag = new Hook(abstractPlacement_AddVisitFlag, SuppressVisitState);   
        }

        public override void Unload(object parent)
        {
            placement = null;
            onAddVisitFlag?.Dispose();
            onAddVisitFlag = null;
        }

        private void SuppressVisitState(Action<AbstractPlacement, VisitState> orig, AbstractPlacement self, VisitState flags)
        {
            // if it's someone else's visit state we don't care
            if (!ReferenceEquals(self, placement))
            {
                orig(self, flags);
                return;
            }

            VisitState suppressedFlags = flags & ~StatesToSuppress;
            // only call orig (and trigger OnVisitStateChanged) if an actual change will occur after suppression
            if (suppressedFlags != VisitState.None)
            {
                orig(self, suppressedFlags);
            }
        }
    }
}
