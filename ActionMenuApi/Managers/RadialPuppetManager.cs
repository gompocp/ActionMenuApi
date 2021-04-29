using System;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.Managers
{
    internal static class RadialPuppetManager
    {

        public static void Setup()
        {
            MelonCoroutines.Start(WaitForRadialMenu());
        }
        
        private static RadialPuppetMenu radialPuppetMenuRight;
        private static RadialPuppetMenu radialPuppetMenuLeft;
        private static RadialPuppetMenu current;
        private static ActionMenuHand hand;
        
        public static float radialPuppetValue { get; set; }
        private static bool open = false;
        
        public static Action<float> onUpdate { get; set; }
        
        public static Action<float> onClose { get; set; }
        
        
        private static System.Collections.IEnumerator WaitForRadialMenu()
        {
            while (GameObject.Find("UserInterface/ActionMenu/MenuR/ActionMenu/RadialPuppetMenu") == null) yield return null;
            radialPuppetMenuLeft = Utilities.CloneGameObject("UserInterface/ActionMenu/MenuL/ActionMenu/RadialPuppetMenu", "UserInterface/ActionMenu/MenuL/ActionMenu").GetComponent<RadialPuppetMenu>();
            radialPuppetMenuRight = Utilities.CloneGameObject("UserInterface/ActionMenu/MenuR/ActionMenu/RadialPuppetMenu", "UserInterface/ActionMenu/MenuR/ActionMenu").GetComponent<RadialPuppetMenu>();
            yield break;
        }

        public static void OnUpdate()
        {
            //Probably a better more efficient way to do all this
            if (current != null && current.gameObject.active)
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
                radialPuppetValue = (current.GetFill().field_Public_Single_3 / 360) * 100; //IK this is bad I'll refactor it later 
                if(onUpdate != null) onUpdate.Invoke(radialPuppetValue/100);
            }
        }

        public static void OpenRadialMenu(float startingValue, Action<float> close, string title)
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
            onClose = close;
            current.gameObject.SetActive(true);
            current.GetFill().field_Public_Single_3 = startingValue*100; //Please dont break
            onUpdate = onClose;
            current.GetTitle().text = title;
            current.GetCenterText().text = (Math.Round(radialPuppetMenuRight.GetFill().field_Public_Single_3 / 360 * 100)) + "%";
            current.GetFill().UpdateGeometry();
            current.transform.localPosition = new Vector3(-256f, 0, 0); //TODO: Place it correctly
            double angleOriginal = (startingValue/100)*360;
            double eulerAngle = Utilities.ConvertFromDegToEuler(angleOriginal);
            current.UpdateArrow(angleOriginal, eulerAngle);
        }

        public static void CloseRadialMenu()
        {
            current.gameObject.SetActive(false);
            current = null;
            open = false;
            hand = ActionMenuHand.Invalid;
            onClose.Invoke(radialPuppetValue);
        }

        private static void UpdateMathStuff()
        {
            Vector2 mousePos = (hand == ActionMenuHand.Left) ? Utilities.GetCursorPosLeft() : Utilities.GetCursorPosRight();
            radialPuppetMenuRight.GetCursor().transform.localPosition = mousePos * 4;

            if (Vector2.Distance(mousePos, Vector2.zero) > 12)
            {
                double angleOriginal = Math.Round(((float)Math.Atan2(mousePos.y, mousePos.x)) * Constants.radToDeg);
                double eulerAngle = Utilities.ConvertFromDegToEuler(angleOriginal);
                current.SetAngle((float)eulerAngle);
                current.UpdateArrow(angleOriginal, eulerAngle);
            }
        }
  
    }
}