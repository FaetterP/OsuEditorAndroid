using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utilities
{
    class PrinterNumber : MonoBehaviour
    {
        private GameObject[] Numbers = new GameObject[10];
        public int num;

        void Awake()
        {
            Numbers[0] = Resources.Load("Number0") as GameObject;
            Numbers[1] = Resources.Load("Number1") as GameObject;
            Numbers[2] = Resources.Load("Number2") as GameObject;
            Numbers[3] = Resources.Load("Number3") as GameObject;
            Numbers[4] = Resources.Load("Number4") as GameObject;
            Numbers[5] = Resources.Load("Number5") as GameObject;
            Numbers[6] = Resources.Load("Number6") as GameObject;
            Numbers[7] = Resources.Load("Number7") as GameObject;
            Numbers[8] = Resources.Load("Number8") as GameObject;
            Numbers[9] = Resources.Load("Number9") as GameObject;
        }

        public void Print()
        {
            int offset = 0;
            while (num / 10 != 0)
            {
                GameObject created = Instantiate(Numbers[num % 10], transform);
                created.transform.localPosition = new Vector2(offset, 0);
                offset -= 17;
                num = num / 10;
            } 

            GameObject created1 = Instantiate(Numbers[num % 10], transform);
            created1.transform.localPosition = new Vector2(offset, 0);
        }
    }
}
