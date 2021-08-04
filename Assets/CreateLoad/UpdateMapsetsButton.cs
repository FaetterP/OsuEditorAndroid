using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;

namespace Assets.CreateLoad
{
    class UpdateMapsetsButton : MonoBehaviour
    {
        [SerializeField] private LoaderAllMapsets loader;
        void OnMouseDown()
        {
            loader.UpdateMapsets();
        }
    }
}
