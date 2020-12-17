using ActionMenuApi;
using MelonLoader;
using ActionMenuApi.API;

namespace ActionMenuTestMod
{
    public static class ModInfo
    {
        public const string Name = "ActionMenuTestMod";
        public const string Author = "gompo#6956";
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }
    
    public class ActionMenuTestMod : MelonMod
    {
        private float testFloatValue = 50;
        
        public override void OnApplicationStart()
        {
            Tools.AddButtonPedalToMenu(ActionMenuPageType.Main, delegate
            {
                MelonLogger.Log("Pressed Button");
            }, "Custom Menu");
            
            Tools.AddSubMenuToMenu(ActionMenuPageType.Main, delegate
            {
            }, "Custom Menu");
            
            Tools.AddRadialPedalToMenu(ActionMenuPageType.Main, f => testFloatValue = f, "Example", testFloatValue);

        }
    }
}