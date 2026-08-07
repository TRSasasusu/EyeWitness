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
        public GameObject ProbeTHShipLog { get; private set; }
        public NotificationWithNewShipLog NotificationWithNewShipLogForProbeTH { get; private set; }
        public MarkerItem MarkerItem { get; private set; }
        public GameObject BrambleRocks { get; private set; }
        public GameObject BrambleRocksBroken { get; private set; }

        public ModifyObjects() {
            EyeWitness.Log("ModifyObjects constructor called");
            Instance = this;

            ProbeTH = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Interactables_Village/LaunchTower/Effects_HEA_Campfire/Probe_TH");
            if(ProbeTH != null) {
                ProbeTH.SetActive(false);

                ProbeTHShipLog = ProbeTH.transform.Find("shiplog_ew_camp_probe_2").gameObject;

                var notificationWithNewShipLogForProbeTH = new GameObject("NotificationWithNewShipLogForProbeTH");
                notificationWithNewShipLogForProbeTH.transform.SetParent(ProbeTHShipLog.transform);
                notificationWithNewShipLogForProbeTH.transform.localPosition = Vector3.zero;
                notificationWithNewShipLogForProbeTH.transform.localEulerAngles = Vector3.zero;
                NotificationWithNewShipLogForProbeTH = notificationWithNewShipLogForProbeTH.AddComponent<NotificationWithNewShipLog>();
                NotificationWithNewShipLogForProbeTH.shipLogId = "ew_camp_probe_2";
                NotificationWithNewShipLogForProbeTH.notificationText = "SHIP_LOG_ERROR_NOTIFICATION";
                NotificationWithNewShipLogForProbeTH.sphereShape = ProbeTHShipLog.GetComponent<SphereShape>();
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
