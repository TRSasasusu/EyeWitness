using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

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

        public CylinderShape EndlessCylinderForest { get; private set; }
        public GameObject OwlkEyeTemple { get; private set; }
        public GameObject OwlkEyeSymbolTrigger { get; private set; }
        public GameObject EyeTower { get; private set; }
        public GameObject MermaidCapsule { get; private set; }
        public GameObject MermaidCapsuleRender { get; private set; }
        public GameObject LaunchTower { get; private set; }
        public GameObject InstrumentZoneParent { get; private set; }
        public GameObject Mitis { get; private set; }
        public NomaiComputer FutureComputer { get; private set; }

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

            ModifyCamp();
        }

        void ModifyCamp() {
            if (!PlayerData.GetPersistentCondition("EW_MET_MERMAID")) {
            }
            else {
                var endlessCylinderForest = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Volumes_Campfire/EndlessCylinder_Forest");
                if(endlessCylinderForest != null) {
                    EndlessCylinderForest = endlessCylinderForest.GetComponent<CylinderShape>();
                    EndlessCylinderForest.height = 285;
                    EndlessCylinderForest.radius = 135;//200;
                }

                InstrumentZoneParent = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EWEyeInstrumentZoneParent");
                MermaidCapsule = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EyeGatheredCapsuleItem");
                Mitis = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EWEyeInstrumentZoneParent/EyeGasDwarfTower/Mitis");
                OwlkEyeTemple = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EWEyeInstrumentZoneParent/OwlkEyeTemple");
                OwlkEyeSymbolTrigger = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EWEyeInstrumentZoneParent/OwlkEyeTemple/Prop_IP_EyeSymbol/COL_EyeSymbol");
                EyeTower = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EWEyeInstrumentZoneParent/EyeGasDwarfTower");
                LaunchTower = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EWEyeInstrumentZoneParent/LaunchTower");
                var futureComputer = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EWEyeInstrumentZoneParent/EyeGasDwarfTower/text_eye_tower_future_1");
                if (futureComputer != null) {
                    FutureComputer = futureComputer.GetComponent<NomaiComputer>();
                }
                if (OwlkEyeTemple != null) {
                    OwlkEyeTemple.SetActive(false);
                }
                if (EyeTower != null) {
                    EyeTower.SetActive(false);
                }
                if (LaunchTower != null) {
                    LaunchTower.SetActive(false);
                    foreach(var child in LaunchTower.GetComponentsInChildren<MeshFilter>(true)) {
                        if(child.gameObject.activeSelf) {
                            child.gameObject.AddComponent<MeshCollider>();
                        }
                    }
                    var childCollider = new GameObject("LaunchTowerCollider");
                    childCollider.transform.parent = LaunchTower.transform;
                    childCollider.transform.localPosition = new Vector3(50.4861f, 45.0792f, 1.0775f);
                    childCollider.transform.localEulerAngles = new Vector3(0, 0, 347.3517f);
                    var boxCollider = childCollider.AddComponent<BoxCollider>();
                    boxCollider.size = new Vector3(3, 1, 10);
                }
                if(MermaidCapsule != null) {
                    MermaidCapsuleRender = MermaidCapsule.transform.Find("capsule_gas").gameObject;
                    MermaidCapsuleRender.SetActive(false);
                    MermaidCapsule.GetComponent<BoxCollider>().enabled = false;
                }

                if (InstrumentZoneParent != null) {
                    var sphereCollider = InstrumentZoneParent.AddComponent<SphereCollider>();
                    sphereCollider.radius = 25;
                    sphereCollider.isTrigger = true;
                    InstrumentZoneParent.OnTriggerEnterAsObservable().Subscribe(collider => {
                        EyeWitness.Log("InstrumentZoneParent collided with " + collider.name);
                        var root = collider.transform.root;
                        if (root == Locator.GetPlayerTransform()) {
                            if (OwlkEyeTemple != null) {
                                OwlkEyeTemple.SetActive(true);
                            }
                            if(MermaidCapsule != null) {
                                MermaidCapsule.transform.parent = OwlkEyeSymbolTrigger.transform.parent;
                                MermaidCapsule.transform.localPosition = new Vector3(0, 0, 0);
                                MermaidCapsule.transform.localEulerAngles = new Vector3(0, 0, 0);
                            }
                            sphereCollider.enabled = false;
                        }
                    });
                    InstrumentZoneParent.transform.parent.UpdateAsObservable().Subscribe(_ => {
                        if (DialogueConditionManager.SharedInstance.GetConditionState("EW_EYE_MERMAID_GATHERED")) {
                            if(EyeTower != null) {
                                if(EyeTower.activeSelf) {
                                    EyeTower.SetActive(false);
                                }
                            }
                            if (LaunchTower != null) {
                                if (!LaunchTower.activeSelf) {
                                    LaunchTower.SetActive(true);
                                }
                            }
                            if(!InstrumentZoneParent.activeSelf) {
                                InstrumentZoneParent.SetActive(true);
                            }
                        }
                    });
                }

                if (OwlkEyeSymbolTrigger != null) {
                    var boxCollider = OwlkEyeSymbolTrigger.AddComponent<BoxCollider>();
                    boxCollider.size = new Vector3(15, 2, 15);
                    boxCollider.isTrigger = true;
                    OwlkEyeSymbolTrigger.OnTriggerEnterAsObservable().Subscribe(collider => {
                        EyeWitness.Log("OwlkEyeSymbol collided with " + collider.name);
                        var root = collider.transform.root;
                        if (root == Locator.GetPlayerTransform()) {
                            if (OwlkEyeTemple != null) {
                                OwlkEyeTemple.SetActive(false);
                            }
                            if(Mitis != null && MermaidCapsule != null) {
                                MermaidCapsule.transform.parent = Mitis.transform.Find("Nomai_Rig_v01:TrajectorySHJnt/Nomai_Rig_v01:ROOTSHJnt/Nomai_Rig_v01:Spine_01SHJnt/Nomai_Rig_v01:Spine_02SHJnt/Nomai_Rig_v01:Spine_TopSHJnt/Nomai_Rig_v01:RT_Arm_ClavicleSHJnt/Nomai_Rig_v01:RT_Arm_ShoulderSHJnt/Nomai_Rig_v01:RT_Arm_ElbowSHJnt/Nomai_Rig_v01:RT_Arm_WristSHJnt");
                                MermaidCapsule.transform.localPosition = new Vector3(0.3032f, -0.0074f, -0.0306f);
                                MermaidCapsule.transform.localEulerAngles = new Vector3(358.1064f, 106.472f, 179.7202f);
                                MermaidCapsuleRender.SetActive(true);
                            }
                            if (EyeTower != null) {
                                EyeTower.SetActive(true);
                                Observable.NextFrame().Subscribe(_ => {
                                    FutureComputer.enabled = true;
                                }).AddTo(FutureComputer.gameObject);
                                GlobalMessenger<float, float>.FireEvent("FlickerOffAndOn", 0.5f, 1.5f);
                            }
                        }
                    });
                }

                if(Mitis != null) {
                    var childObj = new GameObject("MitisCollider");
                    childObj.transform.parent = Mitis.transform;
                    childObj.transform.localPosition = new Vector3(1.6426f, 2.5599f, -6.7162f);
                    childObj.transform.localEulerAngles = new Vector3(0, 350.0326f, 0);
                    var mitisCollider = childObj.AddComponent<BoxCollider>();
                    mitisCollider.size = new Vector3(14, 5, 14);
                    mitisCollider.isTrigger = true;
                    childObj.OnTriggerEnterAsObservable().Subscribe(collider => {
                        EyeWitness.Log("Mitis collided with " + collider.name);
                        var root = collider.transform.root;
                        if (root == Locator.GetPlayerTransform()) {
                            if (MermaidCapsule != null) {
                                MermaidCapsule.transform.parent = InstrumentZoneParent.transform.parent;
                                MermaidCapsule.transform.localPosition = new Vector3(-74.3307f, 80.271f, 7573.689f);
                                MermaidCapsule.transform.localEulerAngles = new Vector3(0, 0, 0);
                                MermaidCapsule.GetComponent<BoxCollider>().enabled = true;
                            }
                            Mitis.SetActive(false);
                            GlobalMessenger<float, float>.FireEvent("FlickerOffAndOn", 0.5f, 1.5f);
                        }
                    });
                }
            }
        }
    }
}
