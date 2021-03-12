using System.Diagnostics;
using Harmony;
using MelonLoader;


namespace ActionMenuApi
{
    internal static class ModInfo
    {
        public const string Name = "ActionMenuApi";
        public const string Author = "gompo#6956";
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class ActionMenuApi : MelonMod
    {
        private static MelonMod Instance;
        public static HarmonyInstance HarmonyInstance => Instance.Harmony;
        public override void OnApplicationStart()
        {
            Instance = this;
            Patches.PatchAll();
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