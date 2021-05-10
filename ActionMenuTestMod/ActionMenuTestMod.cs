using System.IO;
using System.Reflection;
using ActionMenuApi;
using ActionMenuApi.Types;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

[assembly: MelonInfo(typeof(ActionMenuTestMod.ActionMenuTestMod), "ActionMenuTestMod", "1.0.0", "gompo")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace ActionMenuTestMod
{
    // Icons from https://uxwing.com/
    public class ActionMenuTestMod : MelonMod
    {

        private float testFloatValue = 50;
        private float testFloatValue2 = 50;
        private bool testBool = false;
        private bool testBool2 = false;
        private bool riskyFunctionsAllowed = false;
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

            AMAPI.AddButtonPedalToMenu(ActionMenuPageType.Main, "Button",() => MelonLogger.Msg("Pressed Button"), buttonIcon);

            AMAPI.AddTogglePedalToMenu(ActionMenuPageType.Config, "Toggle", testBool, b => testBool = b);
            
            AMAPI.AddModFolder(
                "Test Stuff",
                delegate
                {
                    AMAPI.AddTogglePedalToSubMenu("Risky Functions", !riskyFunctionsAllowed, (b) =>
                    {
                        riskyFunctionsAllowed = !b;
                        AMAPI.RefreshActionMenu();
                    });
                    //No properties here are saved because I'm lazy af
                    AMAPI.AddLockableTogglePedalToSubMenu("Enable Hax", false, b => { }, riskyFunctionsAllowed);
                    AMAPI.AddLockableRadialPedalToSubMenu("Volume", f => { }, riskyFunctionsAllowed);
                    AMAPI.AddLockableSubMenuToSubMenu("Whatever", () => { }, riskyFunctionsAllowed);
                    AMAPI.AddLockableButtonPedalToSubMenu("Risky Function", () =>
                    {
                        MelonLogger.Msg("Locked Pedal Func ran");
                    },riskyFunctionsAllowed, buttonIcon);
                    AMAPI.AddLockableFourAxisPedalToSubMenu("Move", vector2 => { }, riskyFunctionsAllowed);
                },
                subMenuIcon
            );
            AMAPI.AddModFolder(
                "Cube Stuff",
                delegate
                {
                    AMAPI.AddFourAxisPedalToSubMenu("Reposition cube X/Y", (v) => RePositionCubeXY(v), toggleIcon);
                    AMAPI.AddFourAxisPedalToSubMenu("Reposition cube Z/Y", RePositionCubeZY, toggleIcon);
                    AMAPI.AddFourAxisPedalToSubMenu("Reposition cube X/Z", RePositionCubeXZ, toggleIcon);
                    AMAPI.AddRadialPedalToSubMenu("X",RotateCubeX, x,radialIcon); //Rotation a bit borked
                    AMAPI.AddTogglePedalToSubMenu("Test Toggle", testBool2, (b) => testBool2 = b);
                    AMAPI.AddRadialPedalToSubMenu("Y",RotateCubeY, y,radialIcon);
                    AMAPI.AddRadialPedalToSubMenu("Z",RotateCubeZ, z,radialIcon);
                    AMAPI.AddButtonPedalToSubMenu("Spawn Cube", CreateCube, buttonIcon);
                    AMAPI.AddButtonPedalToSubMenu("Tp Cube To Player",() => controllingGameObject.transform.localPosition = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.localPosition, buttonIcon);
                },
                subMenuIcon
            );
            for (int i = 0; i < 2; i++) //Set to a high number if you want to test the page functionality 
            {
                AMAPI.AddModFolder($"Example Mod {i+2}", () => {}, subMenuIcon); 
            }
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                AMAPI.RefreshActionMenu();
            }
        }

        private static void CreateCube()
        {
            controllingGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            controllingGameObject.GetComponent<Collider>().enabled = false;
            x = controllingGameObject.transform.eulerAngles.x*360;
            y = controllingGameObject.transform.eulerAngles.y*360;
            z = controllingGameObject.transform.eulerAngles.z*360;
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
            controllingGameObject.transform.eulerAngles = new Vector3((rotation)*360, old.y, old.z);
            x = rotation;
        }
        
        private static void RotateCubeY(float rotation)
        {
            Vector3 old = controllingGameObject.transform.eulerAngles;
            //MelonLogger.Msg($"Old Angles: {old.ToString()}");
            controllingGameObject.transform.eulerAngles = new Vector3(old.x, (rotation)*360, old.z);
            y = rotation;
        }
        private static void RotateCubeZ(float rotation)
        {
            Vector3 old = controllingGameObject.transform.eulerAngles;
            //MelonLogger.Msg($"Old Angles: {old.ToString()}");
            controllingGameObject.transform.eulerAngles = new Vector3(old.x, old.y, (rotation)*360);
            z = rotation;
        }
        
        private static GameObject controllingGameObject;
    }
}