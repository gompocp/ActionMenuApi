using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using ActionMenuApi;
using Harmony;
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
#pragma warning disable 414
        private float testFloatValue = 50;
        private float testFloatValue2 = 50;
        private bool testBool = false;
        private bool testBool2 = false;
        private Vector2 testVector = new Vector2();
        private Vector2 testVector2 = new Vector2();
        private static float x = 0;
        private static float y = 0;
        private static float z = 0;
        private static AssetBundle iconsAssetBundle = null;
        private static Texture2D toggleIcon;
        private static Texture2D radialIcon;
        private static Texture2D subMenuIcon;
        private static Texture2D buttonIcon;
#pragma warning restore 414
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
                .LoadAsset_Internal("Assets/Resources/Icons/zero.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            toggleIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            subMenuIcon = iconsAssetBundle
                .LoadAsset_Internal("Assets/Resources/Icons/file-transfer.png", Il2CppType.Of<Texture2D>())
                .Cast<Texture2D>();
            subMenuIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            buttonIcon = iconsAssetBundle
                .LoadAsset_Internal("Assets/Resources/Icons/cloud-data-download.png", Il2CppType.Of<Texture2D>())
                .Cast<Texture2D>();
            buttonIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            AMAPI.AddButtonPedalToMenu(ActionMenuPageType.Main, () => MelonLogger.Msg("Pressed Button"), "Button", buttonIcon);

            AMAPI.AddSubMenuToMenu(ActionMenuPageType.Options,
                delegate
                {
                    MelonLogger.Msg("Sub Menu Opened");
                    AMAPI.AddFourAxisPedalToSubMenu("Reposition cube X/Y", testVector, (v) => RePositionCubeXY(v), toggleIcon);
                    AMAPI.AddFourAxisPedalToSubMenu("Reposition cube Z/Y", testVector, (v) => RePositionCubeZY(v), toggleIcon);
                    AMAPI.AddFourAxisPedalToSubMenu("Reposition cube X/Z", testVector, (v) => RePositionCubeXZ(v), toggleIcon);
                    AMAPI.AddRadialPedalToSubMenu(f => RotateCubeX(f), "X", x,radialIcon);
                    AMAPI.AddRadialPedalToSubMenu(f => RotateCubeY(f), "Y", y,radialIcon);
                    AMAPI.AddRadialPedalToSubMenu(f => RotateCubeZ(f), "Z", z,radialIcon);
                    AMAPI.AddButtonPedalToSubMenu(CreateCube, "Spawn Cube", buttonIcon);
                    AMAPI.AddButtonPedalToSubMenu(() => controllingGameObject.transform.localPosition = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.localPosition, "Tp Cube To Player", buttonIcon);
                },
                "Sub Menu",
                subMenuIcon
            );
        }

        private static void CreateCube()
        {
            controllingGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            controllingGameObject.GetComponent<Collider>().enabled = false;
            x = controllingGameObject.transform.eulerAngles.x*100;
            y = controllingGameObject.transform.eulerAngles.y*100;
            z = controllingGameObject.transform.eulerAngles.z*100;
        }
        private static void RePositionCubeXY(Vector3 v)
        {
            controllingGameObject.transform.localPosition += v/25;
        }
        private static void RePositionCubeZY(Vector2 v)
        {
            controllingGameObject.transform.localPosition += new Vector3(0, v.y/25, v.x/25);
        }
        private static void RePositionCubeXZ(Vector2 v)
        {
            controllingGameObject.transform.localPosition += new Vector3(v.x/25, 0, v.y/25);
        }
        private static void RotateCubeX(float rotation)
        {

            Vector3 old = controllingGameObject.transform.eulerAngles;
            controllingGameObject.transform.eulerAngles = new Vector3((rotation / 100)*360, old.y, old.z);
        }
        private static void RotateCubeY(float rotation)
        {
            Vector3 old = controllingGameObject.transform.eulerAngles;
            controllingGameObject.transform.eulerAngles = new Vector3(old.x, (rotation / 100)*360, old.z);
        }
        private static void RotateCubeZ(float rotation)
        {
            Vector3 old = controllingGameObject.transform.eulerAngles;
            controllingGameObject.transform.eulerAngles = new Vector3(old.x, old.y, (rotation / 100)*360);
        }
        private static GameObject controllingGameObject;
    }
}