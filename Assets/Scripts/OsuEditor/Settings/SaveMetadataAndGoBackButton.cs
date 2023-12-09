using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OsuEditor.Settings
{
    class SaveMetadataAndGoBackButton : MonoBehaviour
    {
        [SerializeField] InputField artist, artistU, title, titleU, creator, difficulty, source, tags;

        void OnEnable()
        {
            artist.text = Global.Map.Metadata.Artist;
            artistU.text = Global.Map.Metadata.ArtistUnicode;
            title.text = Global.Map.Metadata.Title;
            titleU.text = Global.Map.Metadata.TitleUnicode;
            creator.text = Global.Map.Metadata.Creator;
            difficulty.text = Global.Map.Metadata.Version;
            source.text = Global.Map.Metadata.Source;
            tags.text = Global.Map.Metadata.Tags;
        }

        void OnMouseDown()
        {
            Global.Map.Metadata.Artist = artist.text;
            Global.Map.Metadata.ArtistUnicode = artistU.text;
            Global.Map.Metadata.Title = title.text;
            Global.Map.Metadata.TitleUnicode = titleU.text;
            Global.Map.Metadata.Creator = creator.text;
            Global.Map.Metadata.Version = difficulty.text;
            Global.Map.Metadata.Source = source.text;
            Global.Map.Metadata.Tags = tags.text;
        }
    }
}
