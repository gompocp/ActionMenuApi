using MelonLoader;


namespace ActionMenuApi
{
    public static class ModInfo
    {
        public const string Name = "ActionMenuApi";
        public const string Author = "gompo#6956";
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class ActionMenuApi : MelonMod
    {
        public override void OnApplicationStart()
        {
            Patches.PatchAll();
            RadialPuppetManager.Setup();
        }

        public override void OnUpdate()
        {
            RadialPuppetManager.OnUpdate();
        }
    }
}