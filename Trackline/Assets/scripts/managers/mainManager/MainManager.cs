using System;
using Global.Components.UserInterface;
using Global.Managers.Game;
using Global.Managers.UserInterface;
using UnityEngine;

namespace Global.Managers
{
    using Datas;

    public class MainManager : BaseManager
    {
        public override Type ManagerType => typeof(MainManager);

        protected override bool OnInit()
        {
            return true;
        }

        public void EntryPoint()
        {
            Services.GetManager<UIManager>().ShowWindow<MainMenuWindow>();
        }

        public void StartPlay()
        {
            //start play from main menu, new game

            //hide main menu
            Services.GetManager<UIManager>().HideWindow<MainMenuWindow>();

            //show loading?

            //make map

            Services.GetManager<GameManager>().StartNewGame();
        }
    }
}
