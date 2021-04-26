using MelonLoader;
using ActionMenuApi.Managers;
using Harmony;
using UnityEngine;

namespace ActionMenuApi
{
    internal static class ModInfo
    {
        public const string Name = "ActionMenuApi";
        public const string Author = "gompo";
        public const string Version = "0.1.0";
        public const string DownloadLink = null;
    }

    public class ActionMenuApi : MelonMod
    {
        public override void OnApplicationStart()
        {
            Patches.PatchAll(Harmony);
            RadialPuppetManager.Setup();
            FourAxisPuppetManager.Setup();
        }
        
        public override void OnUpdate()
        {
            RadialPuppetManager.OnUpdate();
            FourAxisPuppetManager.OnUpdate();
        }
    }
}