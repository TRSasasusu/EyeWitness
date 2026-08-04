using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewHorizons.Utility;
using UnityEngine;

namespace EyeWitness {
    public class ModifyObjects {
        public static ModifyObjects Instance;

        public GameObject ProbeTH { get; private set; }
        public MarkerItem MarkerItem { get; private set; }
        public GameObject BrambleRocks { get; private set; }
        public GameObject BrambleRocksBroken { get; private set; }

        public ModifyObjects() {
            EyeWitness.Log("ModifyObjects constructor called");
            Instance = this;

            ProbeTH = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Interactables_Village/LaunchTower/Effects_HEA_Campfire/Probe_TH");
            if(ProbeTH != null) {
                ProbeTH.SetActive(false);
            }

            //var textStatueIsland = SearchUtilities.Find("StatueIsland_Body/Sector_StatueIsland/text_statue_island");
            //if(textStatueIsland != null) {
            //    textStatueIsland.transform.localPosition = 
            //}

            var markerItem = SearchUtilities.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon/Sector_Module_Intact/pedestal_for_marker/OPCMarker");
            if (markerItem != null) {
                MarkerItem = markerItem.AddComponent<MarkerItem>();
            }

            BrambleRocks = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/BrambleRocks");
            BrambleRocksBroken = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/BrambleRocksBroken");
            if(BrambleRocksBroken != null) {
                BrambleRocksBroken.SetActive(false);
            }
        }
    }
}
