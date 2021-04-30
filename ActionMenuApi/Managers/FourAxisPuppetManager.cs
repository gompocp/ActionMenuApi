using System;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.Managers
{
    public static class FourAxisPuppetManager
    {
        private static AxisPuppetMenu fourAxisPuppetMenuRight;
        private static AxisPuppetMenu fourAxisPuppetMenuLeft;
        public static AxisPuppetMenu current { get; private set; }
        private static ActionMenuHand hand;

        public static Vector2 fourAxisPuppetValue { get; set; }
        private static bool open = false;
        
        public static Action<Vector2> onUpdate { get; set; }
        
        public static void Setup()
        {
            MelonCoroutines.Start(WaitForAxisMenu());
        }
        
        
        private static System.Collections.IEnumerator WaitForAxisMenu()
        {
            while (GameObject.Find("UserInterface/ActionMenu/MenuR/ActionMenu/AxisPuppetMenu") == null) yield return null;
            fourAxisPuppetMenuLeft = Utilities.CloneGameObject("UserInterface/ActionMenu/MenuL/ActionMenu/AxisPuppetMenu", "UserInterface/ActionMenu/MenuL/ActionMenu").GetComponent<AxisPuppetMenu>();
            fourAxisPuppetMenuRight = Utilities.CloneGameObject("UserInterface/ActionMenu/MenuR/ActionMenu/AxisPuppetMenu", "UserInterface/ActionMenu/MenuR/ActionMenu").GetComponent<AxisPuppetMenu>();
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
                            CloseFourAxisMenu(); 
                            return;
                        }
                    }
                    else if (hand == ActionMenuHand.Left)
                    {
                        if (Input.GetAxis(InputAxes.LeftTrigger) >= 0.4)
                        {
                            CloseFourAxisMenu();
                            return;
                        }
                    }
                }
                else if (Input.GetMouseButton(0))
                {
                    CloseFourAxisMenu();
                    return;
                }

                try
                {
                    current.Method_Private_Void_Vector2_Boolean_1(fourAxisPuppetValue, false);
                }catch {}
                fourAxisPuppetValue =  ((hand == ActionMenuHand.Left) ? Utilities.GetCursorPosLeft() : Utilities.GetCursorPosRight())/ 16;
                float x = fourAxisPuppetValue.x;
                float y = fourAxisPuppetValue.y;
                if (x >= 0) {
                    current.GetFillLeft().SetAlpha(0);
                    current.GetFillRight().SetAlpha(x);
                }else {
                    current.GetFillLeft().SetAlpha(Math.Abs(x));
                    current.GetFillRight().SetAlpha(0);
                }
                if (y >= 0) {
                    current.GetFillDown().SetAlpha(0);
                    current.GetFillUp().SetAlpha(y);
                }else {
                    current.GetFillDown().SetAlpha(Math.Abs(y));
                    current.GetFillUp().SetAlpha(0);
                }
                UpdateMathStuff();
                onUpdate.Invoke(fourAxisPuppetValue);
            }
        }
        public static void OpenFourAxisMenu(string title, Action<Vector2> update, PedalOption pedalOption)
        {
            if(open) return;
            switch (Utilities.GetActionMenuHand())
            {
                case ActionMenuHand.Invalid:
                    return;
                case ActionMenuHand.Left:
                    current = fourAxisPuppetMenuLeft;
                    hand = ActionMenuHand.Left;
                    open = true;
                    break;
                case ActionMenuHand.Right:
                    current = fourAxisPuppetMenuRight;
                    hand = ActionMenuHand.Right;
                    open = true;
                    break;
            }
            Input.ResetInputAxes();
            onUpdate = update;
            current.gameObject.SetActive(true);
            current.GetTitle().text = title;
            current.transform.localPosition = pedalOption.GetActionButton().transform.localPosition;
        }

        public static void CloseFourAxisMenu()
        {
            current.gameObject.SetActive(false);
            current = null;
            open = false;
            hand = ActionMenuHand.Invalid;
            onUpdate.Invoke(fourAxisPuppetValue);
        }

        private static void UpdateMathStuff()
        {
            Vector2 mousePos = (hand == ActionMenuHand.Left) ? Utilities.GetCursorPosLeft() : Utilities.GetCursorPosRight();
            current.GetCursor().transform.localPosition = mousePos * 4;
        }
    }
}