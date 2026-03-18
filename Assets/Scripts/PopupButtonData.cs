using System;

namespace TestTask_OctoGames
{
    public class PopupButtonData
    {
        public string Text;
        public Action Callback;

        public PopupButtonData(string text, Action callback)
        {
            Text = text;
            Callback = callback;
        }
    }
}