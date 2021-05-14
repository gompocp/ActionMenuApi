using ActionMenuApi.Managers;
using MelonLoader;
#pragma warning disable 1591
[assembly: MelonInfo(typeof(ActionMenuApi.ActionMenuApi), "ActionMenuApi", "0.2.0", "gompo", "https://github.com/gompocp/ActionMenuApi/releases")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace ActionMenuApi
{
    public class ActionMenuApi : MelonMod
    {
        public override void OnApplicationStart()
        {
            Patches.PatchAll(Harmony);
            ResourcesManager.LoadTextures();
        }

        public override void VRChat_OnUiManagerInit()
        {
            RadialPuppetManager.Setup();
            FourAxisPuppetManager.Setup();
            ResourcesManager.InitLockGameObject();
        }

        public override void OnUpdate()
        {
            RadialPuppetManager.OnUpdate();
            FourAxisPuppetManager.OnUpdate();
        }

        public ActionMenuApi()
        {
            LoaderCheck.CheckForRainbows();
        }
    }
}