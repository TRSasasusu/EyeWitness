using NewHorizons.Utility.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness {
    public class PostCreditsHelper {
        public static AssetBundle _assetBundle;
        public static PostCreditsHelper Instance;

        public GameObject _ewPostCredit;

        public PostCreditsHelper() {
            Instance = this;

            var image = _assetBundle.LoadAsset<GameObject>("Assets/MyAssets/Prefabs/EWPostCredit.prefab");

            var canvas = GameObject.Find("PostCreditsScene/Canvas").transform;
            image = GameObject.Instantiate(image, canvas);
            image.name = "EWPostCredit";

            AssetBundleUtilities.ReplaceShaders(image);
            image.transform.localPosition = new Vector3(27, 50, -28);

            image.transform.SetSiblingIndex(4);
            image.SetActive(false);
            _ewPostCredit = image;
        }
    }
}
