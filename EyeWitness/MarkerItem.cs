using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

namespace EyeWitness {
    public class MarkerItem : OWItem {
        public static string ParentRootName { get; private set; }
        static Vector3 _prevPos;

        GameObject _markerBeam;

        public static GameObject GetProbeBasedOnMarker() {
            if(ParentRootName == "TimberHearth_Body") {
                return ModifyObjects.Instance.ProbeTH;
            }
            else if(ParentRootName == "Moon_Body") {
                return ModifyObjects.Instance.ProbeTM;
            }
            else if(ParentRootName == "Comet_Body") {
                return ModifyObjects.Instance.ProbeInterloper;
            }
            else if(ParentRootName == "CaveTwin_Body") {
                return ModifyObjects.Instance.ProbeET;
            }
            else if(ParentRootName == "TowerTwin_Body") {
                return ModifyObjects.Instance.ProbeAT;
            }
            else if(ParentRootName == "BrittleHollow_Body") {
                return ModifyObjects.Instance.ProbeBH;
            }
            else if(ParentRootName == "VolcanicMoon_Body") {
                return ModifyObjects.Instance.ProbeVM;
            }
            else if(ParentRootName == "GiantsDeep_Body" || ParentRootName == "QuantumIsland_Body" || ParentRootName == "ConstructionYardIsland_Body" || ParentRootName == "StatueIsland_Body" || ParentRootName == "GabbroIsland_Body" || ParentRootName == "BrambleIsland_Body") {
                return ModifyObjects.Instance.ProbeGiantsDeep;
            }
            else if(ParentRootName == "DarkBramble_Body") {
                return ModifyObjects.Instance.ProbeDB;
            }
            else if(ParentRootName == "WhiteholeStation_Body") {
                return ModifyObjects.Instance.ProbeWhiteholeStation;
            }
            else if(ParentRootName == "RingWorld_Body") {
                return ModifyObjects.Instance.ProbeTS;
            }
            else if(ParentRootName == "GasDwarf_Body") {
                return ModifyObjects.Instance.ProbeGasDwarf;
            }
            return null;
        }

        public override void Awake() {
            base.Awake();
            _type = (ItemType)2048;
            _localDropOffset = new Vector3(0, 0.2f, 0);
        }

        void Start() {
            _markerBeam = transform.Find("marker").gameObject;
            _markerBeam.transform.localScale = new Vector3(50, 50, 1000);

            var probe = GetProbeBasedOnMarker();
            if (probe != null) {
                if(transform.root.name == "OrbitalProbeCannon_Body") {
                    var thMarker = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/OPCMarker");
                    var entrylocation = transform.Find("entrylocation_ew_opc_control");
                    if (entrylocation != null && thMarker != null) {
                        entrylocation.parent = thMarker.transform;
                        entrylocation.transform.localPosition = Vector3.zero;
                        entrylocation.transform.localEulerAngles = Vector3.zero;
                    }
                    gameObject.SetActive(false);
                    return;
                }

                // only TH marker
                _markerBeam.transform.parent = probe.transform;
                _markerBeam.transform.localPosition = Vector3.zero;
                _markerBeam.transform.localEulerAngles = new Vector3(0, 90, 0);
                _markerBeam.transform.parent = probe.transform.parent;
                _markerBeam.SetActive(true);
            }
            else {
                if(transform.root.name == "TimberHearth_Body") {
                    gameObject.SetActive(false);
                    return;
                }
                // only OPC marker
            }

            // disable the marker if the player hasn't touched the probe yet
            if (!EyeWitness.HasShipLog("ew_camp_probe_2")) {
                var computerObj = SearchUtilities.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon/Sector_Module_Intact/text_opc_control_computer");
                if(computerObj != null) {
                    var computer = computerObj.GetComponent<NomaiComputer>();
                    computer.ClearAllEntries();
                    foreach(var renderer in GetComponentsInChildren<MeshRenderer>()) {
                        renderer.enabled = false;
                    }
                    //var collider = GetComponent<Collider>();
                    //collider.enabled = false;
                    EnableInteraction(false);
                    IDisposable disposable = null;
                    disposable = computer.ObserveEveryValueChanged(x => EyeWitness.HasShipLog("ew_camp_probe_2")).Subscribe(touched => {
                        if (touched) {
                            computer.DisplayAllEntries();
                            foreach (var renderer in GetComponentsInChildren<MeshRenderer>()) {
                                renderer.enabled = true;
                            }
                            //collider.enabled = true;
                            EnableInteraction(true);

                            if(disposable != null) {
                                disposable.Dispose();
                            }
                        }
                    }).AddTo(computerObj);
                }
            }
        }

        public override string GetDisplayName() {
            return EyeWitness.Instance.NewHorizons.GetTranslationForUI("OPCMarker");
        }

        public override void PickUpItem(Transform holdTranform) {
            base.PickUpItem(holdTranform);
            ParentRootName = null;
            _markerBeam.SetActive(false);

            if(_markerBeam.transform.parent != transform) {
                _markerBeam.transform.parent = transform;
                _markerBeam.transform.localPosition = Vector3.zero;
                _markerBeam.transform.localEulerAngles = new Vector3(270, 0, 0);
                _markerBeam.transform.localScale = new Vector3(50, 50, 1000);
            }
        }

        public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget) {
            base.DropItem(position, normal, parent, sector, customDropTarget);
            ParentRootName = parent.root.GetPath();

            if(GetProbeBasedOnMarker() != null) {
                _markerBeam.SetActive(true);
                _prevPos = position;
            }
        }
    }
}
