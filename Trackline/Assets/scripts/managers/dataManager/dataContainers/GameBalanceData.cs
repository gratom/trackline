using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Данные игрового баланса, такие как ингридиенты, свойства ингридиентов, названия, описания
//Варочные котлы, и тд. Настройки сеток, жидкостей и тд.

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "GameBalanceData", menuName = "Scriptables/Game balance data", order = 51)]
    public class GameBalanceData : ScriptableObject
    {
#if UNITY_EDITOR
        private void OnValidate()
        {
        }
#endif
    }
}
