using System;

namespace ActionMenuApi.ModMenu
{
    internal class Page
    {
        private int count = 0;
        private int maxPerPage = 8;
        private Action onOpen = delegate {  };
    }
}