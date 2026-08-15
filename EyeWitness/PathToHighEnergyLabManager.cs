using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NewHorizons.Utility;
using UniRx;

namespace EyeWitness {
    public class PathToHighEnergyLabManager {
        GameObject _emberTwin;
        TractorBeamController _tractorBeam;
        TractorBeamSwitch _topSwitch;
        TractorBeamSwitch _bottomSwitch;
        SectorFixOfPathToHighEnergeLab _fixOfPathToHighEnergeLab;
        GameObject _rumorRevealVolume;
        Collider _textBrambleTower;

        public PathToHighEnergyLabManager() {
            var tractorBeam = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/Sector_SouthHemisphere/Sector_SouthUnderground/Sector_SpeedCave/Interactables_SpeedCave/Prefab_NOM_TractorBeam");
            if(tractorBeam != null) {
                _tractorBeam = tractorBeam.GetComponent<TractorBeamController>();
            }

            var topSwitch = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/top_switch_tractorbeam");
            if (topSwitch != null) {
                _topSwitch = topSwitch.GetComponent<TractorBeamSwitch>();
            }

            var bottomSwitch = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/bottom_switch_tractorbeam");
            if (bottomSwitch != null) {
                _bottomSwitch = bottomSwitch.GetComponent<TractorBeamSwitch>();
            }

            _topSwitch._linkedSwitches = new TractorBeamSwitch[] { _bottomSwitch };
            _bottomSwitch._linkedSwitches = new TractorBeamSwitch[] { _topSwitch };

            _topSwitch._tractorBeams = new TractorBeamController[] { _tractorBeam };
            _bottomSwitch._tractorBeams = new TractorBeamController[] { _tractorBeam };

            _topSwitch.SetInitialState(TractorBeamSwitch.State.FORWARD);
            _bottomSwitch.SetInitialState(TractorBeamSwitch.State.FORWARD);

            _topSwitch.Start();
            _bottomSwitch.Start();

            _emberTwin = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin");

            var sectorFix = new GameObject("SectorFixOfPathToHighEnergeLab");
            sectorFix.transform.parent = _emberTwin.transform;
            sectorFix.transform.localPosition = new Vector3(75.8831f, -57.4033f, 29.7446f);
            sectorFix.transform.localEulerAngles = Vector3.zero;
            var sphereCollider = sectorFix.AddComponent<SphereCollider>();
            sphereCollider.radius = 12;
            sphereCollider.isTrigger = true;
            _fixOfPathToHighEnergeLab = sectorFix.AddComponent<SectorFixOfPathToHighEnergeLab>();

            var textBrambleTower = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/text_bramble_tower");
            if(textBrambleTower != null) {
                _textBrambleTower = textBrambleTower.GetComponent<BoxCollider>();
            }

            _rumorRevealVolume = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/shiplog_ew_bramble_tower_rumor1");
            _rumorRevealVolume.SetActive(false);
            Observable.Timer(TimeSpan.FromSeconds(60 * 6 - 2)).Subscribe(_ => {
                _rumorRevealVolume.SetActive(true);
                _textBrambleTower.enabled = false;
            }).AddTo(_rumorRevealVolume);
        }
    }
}
