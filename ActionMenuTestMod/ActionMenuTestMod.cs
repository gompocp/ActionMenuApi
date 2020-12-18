using System.IO;
using System.Reflection;
using ActionMenuApi;
using MelonLoader;
using ActionMenuApi.API;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace ActionMenuTestMod
{
    public static class ModInfo
    {
        public const string Name = "ActionMenuTestMod";
        public const string Author = "gompo#6956";
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }
    // Icons from https://uxwing.com/
    public class ActionMenuTestMod : MelonMod
    {
        private float testFloatValue = 50;
        private bool testBool = false;
        
        private static AssetBundle iconsAssetBundle = null;
        private static Texture2D toggleIcon;
        private static Texture2D radialIcon;
        private static Texture2D subMenuIcon;
        private static Texture2D buttonIcon;


        public override void OnApplicationStart()
        {
            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("ActionMenuTestMod.customicons"))
            using (var tempStream = new MemoryStream((int) stream.Length))
            {

                stream.CopyTo(tempStream);
                iconsAssetBundle = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                iconsAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            }

            radialIcon = iconsAssetBundle
                .LoadAsset_Internal("Assets/Resources/Icons/sound-full.png", Il2CppType.Of<Texture2D>())
                .Cast<Texture2D>();
            radialIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            toggleIcon = iconsAssetBundle
                .LoadAsset_Internal("Assets/Resources/Icons/zero.png", Il2CppType.Of<Texture2D>())
                .Cast<Texture2D>();
            toggleIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            subMenuIcon = iconsAssetBundle
                .LoadAsset_Internal("Assets/Resources/Icons/file-transfer.png", Il2CppType.Of<Texture2D>())
                .Cast<Texture2D>();
            subMenuIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            buttonIcon = iconsAssetBundle
                .LoadAsset_Internal("Assets/Resources/Icons/cloud-data-download.png", Il2CppType.Of<Texture2D>())
                .Cast<Texture2D>();
            buttonIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;


            Tools.AddButtonPedalToMenu(ActionMenuPageType.Main, delegate { MelonLogger.Log("Pressed Button"); },
                "Button", buttonIcon);

            Tools.AddSubMenuToMenu(ActionMenuPageType.Main, delegate { MelonLogger.Log("Opened Menu"); },
                "Sub Menu", subMenuIcon);

            Tools.AddRadialPedalToMenu(ActionMenuPageType.Main, f => testFloatValue = f, "Radial", testFloatValue,
                radialIcon);

            Tools.AddTogglePedalToMenu(ActionMenuPageType.Main, testBool, b => testBool = b, "Toggle", toggleIcon);


            }
    }
}