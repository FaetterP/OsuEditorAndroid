using System;
using UnityEngine;

namespace Assets.Utilities
{
    class PrinterNumber : MonoBehaviour
    {
        private GameObject[] Numbers = new GameObject[10];
        public int number;

        void Awake()
        {
            for(int i = 0; i <= 9; i++)
            {
                Numbers[i] = Resources.Load("Number" + i) as GameObject;
            }
        }

        public void Print()
        {
            int offset = 0;
            for (int i = 0; i <= (int)Math.Log10(number); i++)
            {
                GameObject created = Instantiate(Numbers[number % 10], transform);
                created.transform.localPosition = new Vector2(offset, 0);
                offset -= 17;
                number = number / 10;
            }
        }
    }
}
