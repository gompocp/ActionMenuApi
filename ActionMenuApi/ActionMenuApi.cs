using System;
using System.Collections;
using ActionMenuApi.Managers;
using MelonLoader;

#pragma warning disable 1591
[assembly: MelonInfo(typeof(ActionMenuApi.ActionMenuApi), "ActionMenuApi", "0.3.1", "gompo", "https://github.com/gompocp/ActionMenuApi/releases")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: VerifyLoaderBuild("10254481011005649504957561025057535457484899102569857565510010155494950")]
[assembly: VerifyLoaderVersion(0, 4, 2, true)]

namespace ActionMenuApi
{
    public partial class ActionMenuApi : MelonMod
    {

        public override void OnApplicationStart()
        {
            ResourcesManager.LoadTextures();
            MelonCoroutines.Start(WaitForActionMenuInit());
            try
            {
                Patches.PatchAll(HarmonyInstance);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Patching failed with exception: {e.Message}");
            }
        }

        private IEnumerator WaitForActionMenuInit()
        {
            while (ActionMenuDriver.prop_ActionMenuDriver_0 == null) //VRCUIManager Init is too early 
                yield return null;
            if (string.IsNullOrEmpty(ID)) yield break;
            ResourcesManager.InitLockGameObject();
            RadialPuppetManager.Setup();
            FourAxisPuppetManager.Setup();
        }


        public override void OnUpdate()
        {
            RadialPuppetManager.OnUpdate();
            FourAxisPuppetManager.OnUpdate();
        }

        private static string ID = "gompo";
    }
}