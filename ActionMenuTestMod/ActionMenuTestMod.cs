using System.IO;
using System.Reflection;
using ActionMenuApi;
using MelonLoader;
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
        private float testFloatValue2 = 50;
        private bool testBool = false;
        private bool testBool2 = false;
        private Vector2 testVector = new Vector2();
        private Vector2 testVector2 = new Vector2();
        private static AssetBundle iconsAssetBundle = null;
        private static Texture2D toggleIcon;
        private static Texture2D radialIcon;
        private static Texture2D subMenuIcon;
        private static Texture2D buttonIcon;
        
        public override void OnApplicationStart()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActionMenuTestMod.customicons"))
            using (var tempStream = new MemoryStream((int) stream.Length))
            {
                stream.CopyTo(tempStream);
                iconsAssetBundle = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                iconsAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            }
            radialIcon = iconsAssetBundle.LoadAsset_Internal("Assets/Resources/Icons/sound-full.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            radialIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            toggleIcon = iconsAssetBundle.LoadAsset_Internal("Assets/Resources/Icons/zero.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            toggleIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            subMenuIcon = iconsAssetBundle.LoadAsset_Internal("Assets/Resources/Icons/file-transfer.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            subMenuIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            buttonIcon = iconsAssetBundle.LoadAsset_Internal("Assets/Resources/Icons/cloud-data-download.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            buttonIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            
            AMAPI.AddButtonPedalToMenu(ActionMenuPageType.Main, () => MelonLogger.Msg("Pressed Button") , "Button", buttonIcon);

            AMAPI.AddSubMenuToMenu(ActionMenuPageType.Main, 
                delegate {
                    MelonLogger.Msg("Sub Menu Opened");
                    AMAPI.AddButtonPedalToSubMenu(() => MelonLogger.Msg("Pressed Button In Sub Menu"), "Sub Menu Button", buttonIcon);
                    AMAPI.AddTogglePedalToSubMenu(b => testBool2 = b, testBool2, "Sub Menu Toggle", toggleIcon);
                    AMAPI.AddRadialPedalToSubMenu(f => testFloatValue2 = f, "Sub Menu Radial", testFloatValue2, radialIcon);
                    AMAPI.AddFourAxisPedalToSubMenu("Sub Menu Four Axis", testVector, v => testVector = v, toggleIcon);
                },
                "Sub Menu", 
                subMenuIcon
            );
            AMAPI.AddRadialPedalToMenu(ActionMenuPageType.Main, f => testFloatValue = f, "Radial", testFloatValue, radialIcon);
            AMAPI.AddFourAxisPedalToMenu(ActionMenuPageType.Main, "Four Axis", testVector2, vector2 => testVector2 = vector2, toggleIcon);
            AMAPI.AddTogglePedalToMenu(ActionMenuPageType.Main, testBool, b => testBool = b, "Toggle", toggleIcon);
        }
    }
}