using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Utilities;
using UnityEngine;

namespace Assets.CreateLoad
{
    class UpdateMapsetsButton : IClickable
    {
        [SerializeField] private LoaderAllMapsets loader;
        public override void Click()
        {
            loader.UpdateMapsets();
        }
    }
}
