using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NewHorizons.Utility;

namespace EyeWitness {
    public class SectorFixOfPathToHighEnergeLab : MonoBehaviour {
        Sector _southUnderground;
        Sector _speedCave;

        void Start() {
            var southUnderground = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/Sector_SouthHemisphere/Sector_SouthUnderground");
            if (southUnderground != null) {
                _southUnderground = southUnderground.GetComponent<Sector>();
            }
            var speedCave = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/Sector_SouthHemisphere/Sector_SouthUnderground/Sector_SpeedCave");
            if (speedCave != null) {
                _speedCave = speedCave.GetComponent<Sector>();
            }
        }

        void OnTriggerEnter(Collider other) {
            var playerSectorDetector = other.transform.root.GetComponentInChildren<PlayerSectorDetector>(true);
            if(playerSectorDetector == null) {
                return;
            }

            _southUnderground.AddOccupant(playerSectorDetector);
            _speedCave.AddOccupant(playerSectorDetector);
        }
    }
}
