using ActionMenuApi.Types;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    internal class PedalStruct
    {
        public string text { get; set; }
        public Texture2D icon { get; set; }
        public System.Action triggerEvent { get; set; }
        
        public PedalType Type { get; set; }
    }
}