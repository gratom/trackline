using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Global.Managers.Game
{
    public class GameManager : BaseManager
    {
        public override Type ManagerType => typeof(GameManager);

        [SerializeField] private MapGeneratorSetting mapGeneratorSetting;

        protected override bool OnInit()
        {
            return true;
        }

        public void StartNewGame()
        {
            //create map

            int width = 100;
            int height = 100;

            float[,] mapHeight = new float[width, height];
            float[,] mapCostDifficult = new float[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    mapHeight[i, j] = mapGeneratorSetting.height.Rand;
                    GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    g.transform.position = new Vector3(i, j, mapHeight[i, j]);
                }
            }

            //test map genarator

            //
        }
    }

    [Serializable]
    public class MapGeneratorSetting
    {
        public FloatRange height;
        public FloatRange costDifficult;
    }

    [Serializable]
    public struct FloatRange
    {
        public float min;
        public float max;
        public float Rand => Random.Range(min, max);
    }
}
