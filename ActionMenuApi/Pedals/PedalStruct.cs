using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public class PedalStruct
    {
        public string text { get; set; }
        public Texture2D icon { get; set; }
        public System.Action triggerEvent { get; set; }
        
        public PedalType Type { get; set; }
    }
}