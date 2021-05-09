using ActionMenuApi.Types;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    internal class PedalStruct
    {
        public string text { get; protected set; }
        public Texture2D icon { get; protected set; }
        public System.Action triggerEvent { get; protected set; }
        public PedalType Type { get; protected set; }
    }
}