using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Settings.TimingPoints.AddParent
{
    class Metronome : MonoBehaviour
    {
        [SerializeField] Color Color;
        private Image thisImage;

        void Awake()
        {
            thisImage = GetComponent<Image>();
            thisImage.color = Color.gray;
        }
        public IEnumerator Bell()
        {
            thisImage.color = Color;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(SetGray());
        }

        private IEnumerator SetGray()
        {
            thisImage.color = Color.gray;
            yield break;
        }
    }
}
