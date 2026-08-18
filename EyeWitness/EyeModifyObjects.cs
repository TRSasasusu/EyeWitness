using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UniRx;

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
        public GameObject VesselMermaid { get; private set; }

        public EyeModifyObjects() {
            CapsuleExhibit = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/EyeCapsuleExhibit");
            CapsuleExhibitSign = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/capsule_sign");
            VesselMermaid = SearchUtilities.Find("Vessel_Body/Sector_VesselBridge/EyeFishtailEffect");
            if (!PlayerData.GetPersistentCondition("EW_MET_MERMAID")) {
                if(CapsuleExhibit != null) {
                    CapsuleExhibit.SetActive(false);
                }
                if (CapsuleExhibitSign != null) {
                    CapsuleExhibitSign.SetActive(false);
                }

                if (VesselMermaid != null) {
                    VesselMermaid.SetActive(false);
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

                if(VesselMermaid != null) {
                    VesselMermaid.transform.localScale = Vector3.one * 5;
                    VesselMermaid.transform.localEulerAngles = Vector3.zero;
                    var sub = VesselMermaid.transform.Find("sub");
                    if (sub != null) {
                        sub.localScale = Vector3.one * 5;
                    }

                    Sequence seq = DOTween.Sequence();
                    seq.AppendInterval(2)
                       .Append(VesselMermaid.transform.DOLocalMove(new Vector3(0, -5.211f, 50), 3).SetEase(Ease.Linear))
                       .Append(VesselMermaid.transform.DOLocalMove(new Vector3(0, -5.211f, 500), 3).SetEase(Ease.Linear))
                       .Join(VesselMermaid.transform.DOScale(10, 3))
                       .Join(sub.DOScale(10, 3))
                       .Append(VesselMermaid.transform.DOLocalMove(new Vector3(0, -5.211f, 2750), 3).SetEase(Ease.Linear))
                       .Join(VesselMermaid.transform.DOScale(20, 3))
                       .Join(sub.DOScale(20, 3))
                       .Append(VesselMermaid.transform.DOLocalMove(new Vector3(0, -285.3763f, 2889.097f), 3).SetEase(Ease.Linear))
                       .Join(VesselMermaid.transform.DOLocalRotate(new Vector3(88.4605f, 0, 0), 3))
                       .AppendCallback(() => {
                           VesselMermaid.SetActive(false);
                       })
                       .SetLink(VesselMermaid);
                }
            }
        }
    }
}
