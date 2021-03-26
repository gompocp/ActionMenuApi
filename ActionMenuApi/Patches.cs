using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ActionMenuApi.Pedals;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

namespace ActionMenuApi
{
    [SuppressMessage("ReSharper", "Unity.IncorrectMonoBehaviourInstantiation")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class Patches
    {
        public static List<PedalStruct> 
            configPagePre = new(), configPagePost = new (), 
            emojisPagePre = new (), emojisPagePost = new (), 
            expressionPagePre = new (), expressionPagePost = new (), 
            sdk2ExpressionPagePre = new (), sdk2ExpressionPagePost = new (), 
            mainPagePre = new (), mainPagePost = new (),
            menuOpacityPagePre = new (), menuOpacityPagePost = new (),
            menuSizePagePre = new (), menuSizePagePost = new (),
            nameplatesPagePre = new (), nameplatesPagePost = new (), 
            nameplatesOpacityPagePre = new (), nameplatesOpacityPagePost = new (),
            nameplatesVisibilityPagePre = new (), nameplatesVisibilityPagePost = new (),
            nameplatesSizePagePre = new (), nameplatesSizePagePost = new (),
            optionsPagePre = new (), optionsPagePost = new ();

        private static readonly List<string> openConfigPageKeywords = new (new [] { "Menu Size", "Menu Opacity" });
        private static readonly List<string> openMainPageKeyWords = new (new [] { "Options", "Emojis" });
        private static readonly List<string> openMenuOpacityPageKeyWords = new (new [] { "{0}%" });
        private static readonly List<string> openEmojisPageKeyWords = new (new [] { " ", "_" });
        private static readonly List<string> openExpressionMenuKeyWords = new (new [] { "Reset Avatar" });
        private static readonly List<string> openOptionsPageKeyWords = new (new [] { "Config" }); 
        private static readonly List<string> openSDK2ExpressionPageKeyWords = new (new [] { "EMOTE{0}" });
        private static readonly List<string> openNameplatesOpacityPageKeyWords = new (new [] { "100%", "80%", "60%", "40%", "20%", "0%" });
        private static readonly List<string> openNameplatesPageKeyWords = new (new [] { "Visibility", "Size", "Opacity" });
        private static readonly List<string> openNameplatesVisibilityPageKeyWords = new (new [] { "Nameplates Shown", "Icons Only", "Nameplates Hidden" });
        private static readonly List<string> openNameplatesSizePageKeyWords = new (new [] { "Large", "Medium", "Normal", "Small", "Tiny" });
        private static readonly List<string> openMenuSizePageKeyWords = new (new [] { "XXXXXXXXX" }); // No strings found :( Unusable for now. Scanning for methods doesnt help either as there are other functions that yield similar results

        public static void PatchAll()
        {
            try {
                unsafe
                {
                    BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic;
                    
                    //Hooking mechanism adapted from Knah's Advanced Safety Mod 
                    var originalConfigMethod = FindAMMethod(openConfigPageKeywords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr) (&originalConfigMethod),typeof(Patches).GetMethod(nameof(OpenConfigPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openConfigPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenConfigPageDelegate>(originalConfigMethod);

                    var originalOptionsMethod = FindAMMethod(openOptionsPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr) (&originalOptionsMethod),typeof(Patches).GetMethod(nameof(OpenOptionsPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openOptionsPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenOptionsPageDelegate>(originalOptionsMethod);

                    var originalMainMethod = FindAMMethod(openMainPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr) (&originalMainMethod), typeof(Patches).GetMethod(nameof(OpenMainPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openMainPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenMainPageDelegate>(originalMainMethod);
                    
                    var originalMenuOpacityMethod = FindAMMethod(openMenuOpacityPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalMenuOpacityMethod), typeof(Patches).GetMethod(nameof(OpenMenuOpacityPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openMenuOpacityPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenMenuOpacityPageDelegate>(originalMenuOpacityMethod);
    
                    var originalEmojisMethod = FindAMMethod(openEmojisPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalEmojisMethod), typeof(Patches).GetMethod(nameof(OpenEmojisPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openEmojisPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenEmojisPageDelegate>(originalEmojisMethod);
    
                    var originalExpressionMethod = FindAMMethod(openExpressionMenuKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalExpressionMethod),typeof(Patches).GetMethod(nameof(OpenExpressionMenuPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openExpressionMenuPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenExpressionMenuPageDelegate>(originalExpressionMethod);
    
                    var originalNameplatesOpacityMethod = FindAMMethod(openNameplatesOpacityPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalNameplatesOpacityMethod),typeof(Patches).GetMethod(nameof(OpenNameplatesOpacityPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openNameplatesOpacityPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenNameplatesOpacityPageDelegate>(originalNameplatesOpacityMethod);
    
                    var originalNameplatesMethod = FindAMMethod(openNameplatesPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalNameplatesMethod),typeof(Patches).GetMethod(nameof(OpenNameplatesPagePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openNameplatesPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenNameplatesPageDelegate>(originalNameplatesMethod);
    
                    var originalNameplatesVisibilityMethod = FindAMMethod(openNameplatesVisibilityPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalNameplatesVisibilityMethod),typeof(Patches).GetMethod(nameof(OpenNameplatesVisibilityPatch), flags)!.MethodHandle.GetFunctionPointer());
                    openNameplatesVisibilityPageDelegate = Marshal.GetDelegateForFunctionPointer<OpenNameplatesVisibilityPageDelegate>(originalNameplatesVisibilityMethod);
    
                    var originalNameplatesSizeMethod = FindAMMethod(openNameplatesSizePageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalNameplatesSizeMethod),typeof(Patches).GetMethod(nameof(OpenNameplatesSizePatch), flags)!.MethodHandle.GetFunctionPointer());
                    openNameplatesSizePageDelegate =Marshal.GetDelegateForFunctionPointer<OpenNameplatesSizePageDelegate>(originalNameplatesVisibilityMethod);
    
                    var originalSDK2ExpressionMethod = FindAMMethod(openSDK2ExpressionPageKeyWords).Il2CppPtr();
                    MelonUtils.NativeHookAttach((IntPtr)(&originalSDK2ExpressionMethod),typeof(Patches).GetMethod(nameof(OpenSDK2ExpressionPatch), flags)!.MethodHandle.GetFunctionPointer());
                    openSDK2ExpressionPageDelegate =Marshal.GetDelegateForFunctionPointer<OpenSDK2ExpressionPageDelegate>(originalSDK2ExpressionMethod);
                }
            }catch (Exception e) {MelonLogger.Error($"Hooking failed with exception: {e}");}
        }

        private static void OpenConfigPagePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(configPagePre, actionMenu);
            openConfigPageDelegate(thisPtr);
            Utilities.AddPedalsInList(configPagePost, actionMenu);
        }
        private static void OpenOptionsPagePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(optionsPagePre, actionMenu);
            openOptionsPageDelegate(thisPtr);
            Utilities.AddPedalsInList(optionsPagePost, actionMenu);
        }
        private static void OpenMainPagePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(mainPagePre, actionMenu);
            openMainPageDelegate(thisPtr);
            Utilities.AddPedalsInList(mainPagePost, actionMenu);
        }
        private static void OpenMenuOpacityPagePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(menuOpacityPagePre, actionMenu);
            openMenuOpacityPageDelegate(thisPtr);
            Utilities.AddPedalsInList(menuOpacityPagePost, actionMenu);
        }
        private static void OpenEmojisPagePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(emojisPagePre, actionMenu);
            openEmojisPageDelegate(thisPtr);
            Utilities.AddPedalsInList(emojisPagePost, actionMenu);
        }
        private static void OpenExpressionMenuPagePatch(IntPtr thisPtr, IntPtr expressionsMenuPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(expressionPagePre, actionMenu);
            openExpressionMenuPageDelegate(thisPtr, expressionsMenuPtr);
            Utilities.AddPedalsInList(expressionPagePost, actionMenu);
        }
        private static void OpenNameplatesOpacityPagePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(nameplatesOpacityPagePre, actionMenu);
            openNameplatesOpacityPageDelegate(thisPtr);
            Utilities.AddPedalsInList(nameplatesOpacityPagePost, actionMenu);
        }
        private static void OpenNameplatesPagePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(nameplatesPagePre, actionMenu);
            openNameplatesPageDelegate(thisPtr);
            Utilities.AddPedalsInList(nameplatesPagePost, actionMenu);
        }
        private static void OpenNameplatesVisibilityPatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(nameplatesVisibilityPagePre, actionMenu);
            openNameplatesVisibilityPageDelegate(thisPtr);
            Utilities.AddPedalsInList(nameplatesVisibilityPagePost, actionMenu);
        }
        private static void OpenNameplatesSizePatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(nameplatesSizePagePre, actionMenu);
            openNameplatesSizePageDelegate(thisPtr);
            Utilities.AddPedalsInList(nameplatesSizePagePost, actionMenu);
        }

        private static void OpenSDK2ExpressionPatch(IntPtr thisPtr)
        {
            ActionMenu actionMenu = new ActionMenu(thisPtr);
            Utilities.AddPedalsInList(sdk2ExpressionPagePre, actionMenu);
            openSDK2ExpressionPageDelegate(thisPtr);
            Utilities.AddPedalsInList(sdk2ExpressionPagePost, actionMenu);
        }

        public static void OpenMenuSizePre(ActionMenu __instance) => Utilities.AddPedalsInList(menuSizePagePre, __instance);
        
        public static void OpenMenuSizePost(ActionMenu __instance) => Utilities.AddPedalsInList(menuSizePagePost, __instance);

        private static MethodInfo FindAMMethod(List<String> keywords) => typeof(ActionMenu).GetMethods().First(m => m.Name.StartsWith("Method") && Utilities.checkXref(m, keywords));
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenMainPageDelegate(IntPtr thisPtr);
        private static OpenMainPageDelegate openMainPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenConfigPageDelegate(IntPtr thisPtr);
        private static OpenConfigPageDelegate openConfigPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenMenuOpacityPageDelegate(IntPtr thisPtr);
        private static OpenMenuOpacityPageDelegate openMenuOpacityPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenEmojisPageDelegate(IntPtr thisPtr);
        private static OpenEmojisPageDelegate openEmojisPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenExpressionMenuPageDelegate(IntPtr thisPtr, IntPtr vrcExpressionsMenu);
        private static OpenExpressionMenuPageDelegate openExpressionMenuPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenNameplatesOpacityPageDelegate(IntPtr thisPtr);
        private static OpenNameplatesOpacityPageDelegate openNameplatesOpacityPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenNameplatesPageDelegate(IntPtr thisPtr);
        private static OpenNameplatesPageDelegate openNameplatesPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenNameplatesVisibilityPageDelegate(IntPtr thisPtr);
        private static OpenNameplatesVisibilityPageDelegate openNameplatesVisibilityPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenNameplatesSizePageDelegate(IntPtr thisPtr);
        private static OpenNameplatesSizePageDelegate openNameplatesSizePageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenOptionsPageDelegate(IntPtr thisPtr);
        private static OpenOptionsPageDelegate openOptionsPageDelegate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OpenSDK2ExpressionPageDelegate(IntPtr thisPtr);
        private static OpenSDK2ExpressionPageDelegate openSDK2ExpressionPageDelegate;
    }
}
