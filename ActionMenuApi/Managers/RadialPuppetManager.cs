using System;
using System.Reflection;
using ActionMenuApi.Types;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.Managers
{
    internal static class RadialPuppetManager
    {

        public static void Setup()
        {
            radialPuppetMenuLeft = Utilities.CloneGameObject("UserInterface/ActionMenu/MenuL/ActionMenu/RadialPuppetMenu", "UserInterface/ActionMenu/MenuL/ActionMenu").GetComponent<RadialPuppetMenu>();
            radialPuppetMenuRight = Utilities.CloneGameObject("UserInterface/ActionMenu/MenuR/ActionMenu/RadialPuppetMenu", "UserInterface/ActionMenu/MenuR/ActionMenu").GetComponent<RadialPuppetMenu>();
        }
        
        private static RadialPuppetMenu radialPuppetMenuRight;
        private static RadialPuppetMenu radialPuppetMenuLeft;
        private static RadialPuppetMenu current;
        private static ActionMenuHand hand;
        
        public static float radialPuppetValue { get; set; }
        private static bool open = false;
        public static Action<float> onUpdate { get; set; }
        
        public static void OnUpdate()
        {
            //Probably a better more efficient way to do all this
            if (current != null && current.gameObject.gameObject.active)
            {
                if (UnityEngine.XR.XRDevice.isPresent)
                {
                    if (hand == ActionMenuHand.Right)
                    {
                        if (Input.GetAxis(InputAxes.RightTrigger) >= 0.4)
                        { 
                            CloseRadialMenu();
                            return;
                        }
                    }
                    else if (hand == ActionMenuHand.Left)
                    {
                        if (Input.GetAxis(InputAxes.LeftTrigger) >= 0.4) 
                        {
                            CloseRadialMenu();
                            return;
                        }
                    }
                }
                else if (Input.GetMouseButton(0))
                {
                    CloseRadialMenu();
                    return;
                }
                UpdateMathStuff();
                CallUpdateAction();
            }
        }

        public static void OpenRadialMenu(float startingValue, Action<float> onUpdate, string title, PedalOption pedalOption)
        {
            if(open) return;
            switch (Utilities.GetActionMenuHand())
            {
                case ActionMenuHand.Invalid:
                    return;
                case ActionMenuHand.Left:
                    current = radialPuppetMenuLeft;
                    hand = ActionMenuHand.Left;
                    open = true;
                    break;
                case ActionMenuHand.Right:
                    current = radialPuppetMenuRight;
                    hand = ActionMenuHand.Right;
                    open = true;
                    break;
            }
            Input.ResetInputAxes();
            current.gameObject.SetActive(true);
            current.GetFill().SetFillAngle(startingValue*360); //Please dont break
            RadialPuppetManager.onUpdate = onUpdate;
            current.GetTitle().text = title;
            current.GetCenterText().text = (Math.Round(startingValue*100f)) + "%";
            current.GetFill().UpdateGeometry(); ;
            current.transform.localPosition = pedalOption.GetActionButton().transform.localPosition;  //new Vector3(-256f, 0, 0); 
            float angleOriginal =  Utilities.ConvertFromEuler(startingValue*360);
            float eulerAngle = Utilities.ConvertFromDegToEuler(angleOriginal);
            current.UpdateArrow(angleOriginal, eulerAngle);
        }

        public static void CloseRadialMenu()
        {
            CallUpdateAction();
            current.gameObject.SetActive(false);
            current = null;
            open = false;
            hand = ActionMenuHand.Invalid;
        }

        private static void CallUpdateAction()
        {
            try
            {
                onUpdate?.Invoke(current.GetFill().GetFillAngle() / 360f);
            }
            catch(Exception e)
            {
                MelonLogger.Error($"Exception caught in onUpdate action passed to Radial Puppet: {e}");
            }
        }

        private static void UpdateMathStuff()
        {
            Vector2 mousePos = (hand == ActionMenuHand.Left) ? Utilities.GetCursorPosLeft() : Utilities.GetCursorPosRight();
            radialPuppetMenuRight.GetCursor().transform.localPosition = mousePos * 4;

            if (Vector2.Distance(mousePos, Vector2.zero) > 12)
            {
                float angleOriginal = Mathf.Round(Mathf.Atan2(mousePos.y, mousePos.x) * Constants.RAD_TO_DEG);
                float eulerAngle = Utilities.ConvertFromDegToEuler(angleOriginal);
                current.SetAngle(eulerAngle);
                current.UpdateArrow(angleOriginal, eulerAngle);
            }
        }
  
    }
}