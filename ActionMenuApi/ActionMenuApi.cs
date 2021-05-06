using System.Collections;
using ActionMenuApi.Managers;
using ActionMenuApi.ModMenu;
using MelonLoader;
using Harmony;
using UnityEngine;

namespace ActionMenuApi
{
    internal static class ModInfo
    {
        public const string Name = "ActionMenuApi";
        public const string Author = "gompo";
        public const string Version = "0.1.2";
        public const string DownloadLink = "https://github.com/gompocp/ActionMenuApi/releases";
    }
    
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