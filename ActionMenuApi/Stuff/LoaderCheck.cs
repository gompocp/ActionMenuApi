using System;
using System.IO;
using System.Reflection;
using Harmony;
using MelonLoader;

namespace ActionMenuApi
{
    [HarmonyShield]
    internal static class LoaderCheck
    {
        //Credit to knah: https://github.com/knah/VRCMods/blob/master/UIExpansionKit/LoaderIntegrityCheck.cs
        public static void CheckForRainbows()
        {
            try
            {
                using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActionMenuApi._dummy_.dll");
                using var memStream = new MemoryStream((int) stream.Length);
                stream.CopyTo(memStream);

                var assembly = Assembly.Load(memStream.ToArray());

                RainbowsFound();

                Console.ReadLine();
            }
            catch (BadImageFormatException ex)
            {
            }
            
            try
            {
                using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActionMenuApi._dummy2_.dll");
                using var memStream = new MemoryStream((int) stream.Length);
                stream.CopyTo(memStream);

                var assembly = Assembly.Load(memStream.ToArray());
            }
            catch (BadImageFormatException ex)
            {
                MelonLogger.Error(ex.ToString());
                
                RainbowsFound();

                Console.ReadLine();
            }

            try
            {
                var harmony = HarmonyInstance.Create(Guid.NewGuid().ToString());
                harmony.Patch(AccessTools.Method(typeof(LoaderCheck), nameof(PatchTest)), new HarmonyMethod(typeof(LoaderCheck), nameof(ReturnFalse)));

                PatchTest();
                
                RainbowsFound();

                Console.ReadLine();
            }
            catch (BadImageFormatException ex)
            {
            }
        }

        private static bool ReturnFalse() => false;

        public static void PatchTest()
        {
            throw new BadImageFormatException();
        } 

        private static void RainbowsFound()
        {
            MelonLogger.Error("===================================================================");
            MelonLogger.Error("You're using MelonLoader with important security features missing.");
            MelonLogger.Error("This exposes you to additional risks from certain malicious actors,");
            MelonLogger.Error("including account theft, account bans, and other unwanted consequences");
            MelonLogger.Error("If this is not what you want, download the official installer from");
            MelonLogger.Error("https://github.com/LavaGang/MelonLoader/releases");
            MelonLogger.Error("then close this console, and reinstall MelonLoader using it.");
            MelonLogger.Error("If you want to accept those risks, press Enter to continue");
            MelonLogger.Error("===================================================================");
        }
    }
}