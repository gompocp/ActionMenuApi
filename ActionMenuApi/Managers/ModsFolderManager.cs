using System;
using System.Collections.Generic;

namespace ActionMenuApi.Managers
{
    internal static class ModsFolderManager
    {
        public static List<Action> mods = new ();
        public static List<List<Action>> splitMods;
        
        private static Action openFunc = () => {
            if (mods.Count <= Constants.MAX_PEDALS_PER_PAGE)
            {
                foreach (var action in mods) action.Invoke();
            }
            else
            {
                if(splitMods == null) splitMods = mods.Split((int)Constants.MAX_PEDALS_PER_PAGE);
                for (int i = 0; i < splitMods.Count && i < Constants.MAX_PEDALS_PER_PAGE; i++)
                {
                    int index = i;
                    AMAPI.AddSubMenuToSubMenu($"Page {i+1}", () =>
                    {
                        foreach (var action in splitMods[index]) action.Invoke();
                    }, ResourcesManager.GetPageIcon(i+1));
                }
            }
        };

        public static void AddMod(Action openingAction)
        {
            mods.Add(openingAction);
        }

        /*public void RemoveMod(Action openingAction)
        {
            mods.Remove(openingAction);
        }*/
        
        public static void AddMainPageButton()
        {
            AMAPI.AddSubMenuToSubMenu(Constants.MODS_FOLDER_NAME, openFunc, ResourcesManager.GetModsSectionIcon());
        }
    }
}