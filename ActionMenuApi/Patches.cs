using Harmony;
using System.Collections.Generic;
using System.Linq;
using ActionMenuApi.Pedals;
using System.Reflection;

namespace ActionMenuApi
{
    internal static class Patches
    {
        public static List<PedalStruct> configPagePre = new List<PedalStruct>();
        public static List<PedalStruct> configPagePost = new List<PedalStruct>();
        public static List<PedalStruct> emojisPagePre = new List<PedalStruct>();
        public static List<PedalStruct> emojisPagePost = new List<PedalStruct>();
        public static List<PedalStruct> expressionPagePre = new List<PedalStruct>();
        public static List<PedalStruct> expressionPagePost = new List<PedalStruct>();
        public static List<PedalStruct> sdk2ExpressionPagePre = new List<PedalStruct>();
        public static List<PedalStruct> sdk2ExpressionPagePost = new List<PedalStruct>();
        public static List<PedalStruct> mainPagePre = new List<PedalStruct>();
        public static List<PedalStruct> mainPagePost = new List<PedalStruct>();
        public static List<PedalStruct> menuOpacityPagePre = new List<PedalStruct>();
        public static List<PedalStruct> menuOpacityPagePost = new List<PedalStruct>();
        public static List<PedalStruct> menuSizePagePre = new List<PedalStruct>();
        public static List<PedalStruct> menuSizePagePost = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesPagePre = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesPagePost = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesOpacityPagePre = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesOpacityPagePost = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesVisibilityPagePre = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesVisibilityPagePost = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesSizePagePre = new List<PedalStruct>();
        public static List<PedalStruct> nameplatesSizePagePost = new List<PedalStruct>();
        public static List<PedalStruct> optionsPagePre = new List<PedalStruct>();
        public static List<PedalStruct> optionsPagePost = new List<PedalStruct>();

        private static List<string> openConfigPageKeywords = new List<string>(new string[] { "Menu Size", "Menu Opacity" });
        private static List<string> openMainPageKeyWords = new List<string>(new string[] { "Options", "Emojis" });
        private static List<string> openMenuOpacityPageKeyWords = new List<string>(new string[] { "{0}%" });
        private static List<string> openEmojisPageKeyWords = new List<string>(new string[] { " ", "_" });
        private static List<string> openExpressionMenuKeyWords = new List<string>(new string[] { "Reset Avatar" });
        private static List<string> openOptionsPageKeyWords = new List<string>(new string[] { "Config" }); //"Nameplates" and "Close Menu" are ones as well but that update hasnt dropped yet
        private static List<string> openSDK2ExpressionPageKeyWords = new List<string>(new string[] { "EMOTE{0}" });
        
        private static List<string> openNameplatesOpacityPageKeyWords = new List<string>(new string[] { "100%", "80%", "60%", "40%", "20%", "0%" });
        private static List<string> openNameplatesPageKeyWords = new List<string>(new string[] { "Visibility", "Size", "Opacity" });
        private static List<string> openNameplatesVisibilityPageKeyWords = new List<string>(new string[] { "Nameplates Shown", "Icons Only", "Nameplates Hidden" });
        private static List<string> openNameplatesSizePageKeyWords = new List<string>(new string[] { "Large", "Medium", "Normal", "Small", "Tiny" });
        
        private static List<string> openMenuSizePageKeyWords = new List<string>(new string[] { "XXXXXXXXX" }); // No strings found :( Unusable for now. Scanning for methods doesnt help either as there are other functions that yield similar results

        private static HarmonyInstance harmonyInstance { get; set; } = HarmonyInstance.Create("gompo.actionmenuapi");

        public static void PatchAll()
        {
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method_") && Utilities.checkXref(m, openMainPageKeyWords)))
            {
                Logger.Log("Found Main Page: " + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenMainPagePre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenMainPagePost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method_") && Utilities.checkXref(m, openConfigPageKeywords)))
            {
                Logger.Log("Found Config Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenConfigPagePre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenConfigPagePost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method_") && Utilities.checkXref(m, openMenuOpacityPageKeyWords)))
            {
                Logger.Log("Found Menu Opacity Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenMenuOpacityPagePre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenMenuOpacityPagePost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method_") && Utilities.checkXref(m, openEmojisPageKeyWords)))
            {
                Logger.Log("Found Emojis Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenEmojisPagePre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenEmojisPagePost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method_") && Utilities.checkXref(m, openExpressionMenuKeyWords)))
            {
                Logger.Log("Found Expression Menu Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenExpressionMenuPre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenExpressionMenuPost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method") && Utilities.checkXref(m, openNameplatesOpacityPageKeyWords)))
            {
                Logger.Log("Found Nameplates Opacity Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesOpacityPre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesOpacityPost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method") && Utilities.checkXref(m, openNameplatesPageKeyWords)))
            {
                Logger.Log("Found Namesplates Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesPagePre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesPagePost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method") && Utilities.checkXref(m, openNameplatesVisibilityPageKeyWords)))
            {
                Logger.Log("Found Namesplates Visibility Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesVisibilityPre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesVisibilityPost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method") && Utilities.checkXref(m, openNameplatesSizePageKeyWords)))
            {
                Logger.Log("Found Namesplates Size Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesSizePre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenNameplatesSizePost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method_") && Utilities.checkXref(m, openOptionsPageKeyWords)))
            {

                Logger.Log("Found Options Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenOptionsPre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenOptionsPost))));
                break;
            }
            foreach (MethodBase methodBase in typeof(ActionMenu).GetMethods().Where(m => m.Name.StartsWith("Method_") && Utilities.checkXref(m, openSDK2ExpressionPageKeyWords)))
            {
                Logger.Log("Found SDK2 Expression Page:" + methodBase.Name);
                harmonyInstance.Patch(methodBase, new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenSDK2ExpressionPre))), new HarmonyMethod(typeof(Patches).GetMethod(nameof(OpenSDK2ExpressionPost))));
                break;
            }
        }

        public static void OpenConfigPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(configPagePre, __instance);
        }
        public static void OpenConfigPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(configPagePost, __instance);
        }
        public static void OpenMainPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(mainPagePre, __instance);
        }
        public static void OpenMainPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(mainPagePost, __instance);
        }
        public static void OpenMenuOpacityPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(menuOpacityPagePre, __instance);
        }
        public static void OpenMenuOpacityPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(menuOpacityPagePost, __instance);
        }
        public static void OpenEmojisPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(emojisPagePre, __instance);
        }
        public static void OpenEmojisPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(emojisPagePost, __instance);
        }
        public static void OpenExpressionMenuPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(expressionPagePre, __instance);
        }
        public static void OpenExpressionMenuPost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(expressionPagePost, __instance);
        }
        public static void OpenNameplatesOpacityPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesOpacityPagePre, __instance);
        }
        public static void OpenNameplatesOpacityPost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesOpacityPagePost, __instance);
        }
        public static void OpenNameplatesPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesPagePre, __instance);
        }
        public static void OpenNameplatesPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesPagePost, __instance);
        }
        public static void OpenNameplatesVisibilityPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesVisibilityPagePre, __instance);
        }
        public static void OpenNameplatesVisibilityPost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesVisibilityPagePost, __instance);
        }
        public static void OpenNameplatesSizePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesSizePagePre, __instance);
        }
        public static void OpenNameplatesSizePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesSizePagePost, __instance);
        }
        public static void OpenOptionsPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(optionsPagePre, __instance);
        }
        public static void OpenOptionsPost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(optionsPagePost, __instance);
        }
        public static void OpenSDK2ExpressionPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(sdk2ExpressionPagePre, __instance);
        }
        public static void OpenSDK2ExpressionPost(ActionMenu __instance) => Utilities.AddPedalsInList(sdk2ExpressionPagePost, __instance);
        
        public static void OpenMenuSizePre(ActionMenu __instance) => Utilities.AddPedalsInList(menuSizePagePre, __instance);
        
        public static void OpenMenuSizePost(ActionMenu __instance) => Utilities.AddPedalsInList(menuSizePagePost, __instance);

    }
}
