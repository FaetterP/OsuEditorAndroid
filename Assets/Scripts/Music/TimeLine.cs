using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Music
{
    class TimeLine : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private Text timer;
                         private Slider thisSlider;

        void Awake()
        {
            thisSlider = GetComponent<Slider>();
        }

        public void UpdateMax(int max)
        {
            thisSlider.maxValue = max;
        }

        void Update()
        {
            Global.MusicTime = (int)(music.time * 1000);
            if (music.isPlaying)
            {
                thisSlider.value = music.time * 1000;
            }
            else
            {
                music.time = (float)(1.0 * thisSlider.value / 1000);
            }
            timer.text = OsuMath.ConvertTimestampToString((int)(music.time * 1000));
        }
    }
}
