using Global.Managers;
using UnityEngine;

namespace Global.Components.UserInterface
{
    public class MainMenuWindow : BaseWindow
    {
        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
        }

        public void OnPlayPress()
        {
            Services.GetManager<MainManager>().StartPlay();
        }
    }
}
