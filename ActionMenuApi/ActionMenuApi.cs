using System.Collections;
using ActionMenuApi.Managers;
using ActionMenuApi.ModMenu;
using MelonLoader;
using Harmony;
using UnityEngine;

[assembly: MelonInfo(typeof(ActionMenuApi.ActionMenuApi), "ActionMenuApi", "0.1.2", "gompo", "https://github.com/gompocp/ActionMenuApi/releases")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace ActionMenuApi
{
    public class ActionMenuApi : MelonMod
    {
        public override void OnApplicationStart()
        {
            Patches.PatchAll(Harmony);
            ModsFolder.CreateInstance();
        }

        public override void VRChat_OnUiManagerInit()
        {
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