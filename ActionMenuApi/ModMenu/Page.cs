using System;

namespace ActionMenuApi.ModMenu
{
    public class Page
    {
        private int count = 0;
        private int maxPerPage = 8;
        private Action onOpen = delegate {  };
    }
}