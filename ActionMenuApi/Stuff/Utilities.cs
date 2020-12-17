using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ActionMenuApi.Pedals;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2

namespace ActionMenuApi
{
    public static class Utilities
    {
        public static bool checkXref(MethodBase m, params  string[] keywords)
        {
            try
            {
                foreach (string keyword in keywords)
                {

                    if (!XrefScanner.XrefScan(m).Any(
                        instance => instance.Type == XrefType.Global && instance.ReadAsObject() != null && instance.ReadAsObject().ToString()
                            .Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch { }
            return false;
        }
        
        public static bool checkXref(MethodBase m, List<string> keywords)
        {
            try
            {
                foreach (string keyword in keywords)
                {

                    if (!XrefScanner.XrefScan(m).Any(
                        instance => instance.Type == XrefType.Global && instance.ReadAsObject() != null && instance.ReadAsObject().ToString()
                            .Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch { }
            return false;
        }

        public static void AddPedalsInList(List<PedalStruct> list, ActionMenu instance)
        {
            Logger.Log("Adding pedals in list");
            foreach (var pedalStruct in list)
            {
                Logger.Log(pedalStruct.Type.ToString());
                PedalOption pedalOption = instance.AddOption();
                switch (pedalStruct.Type)
                {
                    case PedalType.Button:
                        pedalOption.setText(pedalStruct.text);
                        pedalOption.setIcon(pedalStruct.icon); 
                        pedalOption.triggerEvent = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(pedalStruct.triggerEvent);
                        break;
                    case PedalType.SubMenu:
                        pedalOption.setText(pedalStruct.text);
                        pedalOption.setIcon(pedalStruct.icon);
                        pedalOption.button.prop_Texture2D_2 = GetExpressionsIcons().typeFolder;
                        pedalOption.triggerEvent = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(pedalStruct.triggerEvent);
                        break;
                    case PedalType.RadialPuppet:
                        PedalRadial pedalRadial = (PedalRadial) pedalStruct;
                        pedalOption.setText(pedalStruct.text);
                        pedalOption.setIcon(pedalStruct.icon);
                        pedalOption.button.prop_Texture2D_2 = GetExpressionsIcons().typeRadial;
                        pedalOption.button.prop_String_1 = $"{Math.Round(pedalRadial.currentValue)}%";
                        pedalRadial.pedal = pedalOption;
                        pedalOption.triggerEvent = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(pedalStruct.triggerEvent);
                        break;
                }

                
            }
        }
        
        
        public static double ConvertFromDegToEuler(double angle)
        {
            if (angle >= 0 && angle <= 90) return 90 - angle;
            else if (angle > 90 && angle <= 180) return angle = 360 - (angle - 90);
            else if (angle <= -90 && angle >= -180) return angle = 270 - (angle + 180);
            else if (angle <= 0 && angle >= -90) return angle = 180 - (angle + 180) + 90;
            return 0;
        }
        public static Vector2 GetCursorPosLeft()
        {
            if (UnityEngine.XR.XRDevice.isPresent)
                return new Vector2(Input.GetAxis(InputAxes.LeftHorizontal), Input.GetAxis(InputAxes.LeftVertical)) * 16;
            else
                return ActionMenuDriver.prop_ActionMenuDriver_0.openerL.actionMenu.field_Private_Vector2_0;
        }
        public static Vector2 GetCursorPosRight()
        {
            if (UnityEngine.XR.XRDevice.isPresent)
                return new Vector2(Input.GetAxis(InputAxes.RightHorizontal), Input.GetAxis(InputAxes.RightVertical)) * 16;
            else
                return ActionMenuDriver.prop_ActionMenuDriver_0.openerR.actionMenu.field_Private_Vector2_0;
        }
        
        public static GameObject CloneGameObject(string pathToGameObject, string pathToParent)
        {
            return GameObject.Instantiate(GameObject.Find(pathToGameObject).transform, GameObject.Find(pathToParent).transform).gameObject;
        }

        public static ActionMenuDriver.ExpressionIcons GetExpressionsIcons()
        {
            return ActionMenuDriver.prop_ActionMenuDriver_0.expressionIcons;
        }

        // Didnt know what to name this function
        public static ActionMenuHand GetActionMenuHand()
        {
            if (!ActionMenuDriver._instance.openerL.isOpen() && ActionMenuDriver._instance.openerR.isOpen())
            {
                return ActionMenuHand.Right;
            }
            else if (ActionMenuDriver._instance.openerL.isOpen() && !ActionMenuDriver._instance.openerR.isOpen())
            {
                return ActionMenuHand.Left;
            }
            else return ActionMenuHand.Invalid;
        }
        
        public static ActionMenuOpener GetActionMenuOpener()
        {
            if (!ActionMenuDriver._instance.openerL.isOpen() && ActionMenuDriver._instance.openerR.isOpen())
            {
                return ActionMenuDriver._instance.openerR;
            }
            else if (ActionMenuDriver._instance.openerL.isOpen() && !ActionMenuDriver._instance.openerR.isOpen())
            {
                return ActionMenuDriver._instance.openerL;
            }
            else return null;
            /*
            else if (ActionMenuDriver._instance.openerL.isOpen() && ActionMenuDriver._instance.openerR.isOpen())
            {
                return null; //Which one to return ¯\_(ツ)_/¯ Mystery till I figure something smart out
            }
            */
        }


    }
}