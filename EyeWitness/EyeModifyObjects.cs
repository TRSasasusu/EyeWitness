using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness {
    public class EyeModifyObjects {
        public GameObject CapsuleExhibit { get; private set; }
        public GameObject CapsuleExhibitSign { get; private set; }
        public GameObject Plank { get; private set; }
        public GameObject Metal { get; private set; }
        public GameObject Glass { get; private set; }
        public GameObject Cloth { get; private set; }
        public GameObject OriginalPlank { get; private set; }
        public GameObject OriginalMetal { get; private set; }
        public GameObject OriginalGlass { get; private set; }
        public GameObject OriginalCloth { get; private set; }

        public EyeModifyObjects() {
            CapsuleExhibit = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/EyeCapsuleExhibit");
            CapsuleExhibitSign = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/capsule_sign");
            if (!PlayerData.GetPersistentCondition("EW_MET_MERMAID")) {
                if(CapsuleExhibit != null) {
                    CapsuleExhibit.SetActive(false);
                }
                if (CapsuleExhibitSign != null) {
                    CapsuleExhibitSign.SetActive(false);
                }
            }
            else {
                if(CapsuleExhibit != null) {
                    Plank = CapsuleExhibit.transform.Find("GameObject/exhibit_plank").gameObject;
                    Metal = CapsuleExhibit.transform.Find("GameObject/exhibit_metal").gameObject;
                    Glass = CapsuleExhibit.transform.Find("GameObject/exhibit_glass").gameObject;
                    Cloth = CapsuleExhibit.transform.Find("GameObject/exhibit_cloth").gameObject;

                    OriginalPlank = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Geo_Observatory/ObservatoryPivot/Observatory_Interior/Interior_Exhibits/Exhibits_Planks");
                    if(OriginalPlank != null) {
                        Plank.GetComponent<Renderer>().sharedMaterial = OriginalPlank.GetComponent<Renderer>().sharedMaterial;
                    }
                    OriginalMetal = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Geo_Observatory/ObservatoryPivot/Observatory_Interior/Interior_Exhibits/Exhibits_Metal");
                    if(OriginalMetal != null) {
                        Metal.GetComponent<Renderer>().sharedMaterial = OriginalMetal.GetComponent<Renderer>().sharedMaterial;
                    }
                    OriginalGlass = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Geo_Observatory/ObservatoryPivot/Observatory_Interior/Interior_Exhibits/Exhibits_Glass");
                    if(OriginalGlass != null) {
                        Glass.GetComponent<Renderer>().sharedMaterial = OriginalGlass.GetComponent<Renderer>().sharedMaterial;
                    }
                    OriginalCloth = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Geo_Observatory/ObservatoryPivot/Observatory_Interior/Interior_Exhibits/Exhibits_Cloth");
                    if(OriginalCloth != null) {
                        Cloth.GetComponent<Renderer>().sharedMaterial = OriginalCloth.GetComponent<Renderer>().sharedMaterial;
                    }
                }
            }
        }
    }
}
