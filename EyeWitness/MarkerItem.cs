using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
                _markerBeam.transform.parent = probe.transform;
                _markerBeam.transform.localPosition = Vector3.zero;
                _markerBeam.transform.localEulerAngles = new Vector3(0, 90, 0);
                _markerBeam.transform.parent = probe.transform.parent;
                _markerBeam.SetActive(true);
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
