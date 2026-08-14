using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewHorizons.Utility;
using UniRx;
using UnityEngine;

namespace EyeWitness {
    public class ModifyObjects {
        public static ModifyObjects Instance;

        public GameObject ProbeTH { get; private set; }
        public GameObject ProbeTM { get; private set; }
        public GameObject ProbeInterloper { get; private set; }
        public GameObject ProbeET { get; private set; }
        public GameObject ProbeAT { get; private set; }
        public GameObject ProbeBH { get; private set; }
        public GameObject ProbeVM { get; private set; }
        public GameObject ProbeGiantsDeep { get; private set; }
        public GameObject ProbeDB { get; private set; }
        public GameObject ProbeTS { get; private set; }
        public GameObject ProbeGasDwarf { get; private set; }
        public GameObject ProbeTHShipLog { get; private set; }
        public NotificationWithNewShipLog NotificationWithNewShipLogForProbeTH { get; private set; }
        public MarkerItem MarkerItem { get; private set; }
        public GameObject BrambleRocks { get; private set; }
        public GameObject BrambleRocksBroken { get; private set; }
        public GameObject BrambleTowerWarpReceiver { get; private set; }
        public GameObject BrambleComputerLock { get; private set; }
        public GameObject BrambleComputerSand { get; private set; }
        public GameObject BrambleComputerAvailable { get; private set; }
        public GameObject BrambleTowerWarpTransmitterShipLog { get; private set; }
        public NotificationWithNewShipLog NotificationWithNewShipLogForBrambleTowerWarp { get; private set; }
        public GameObject GasDwarf { get; private set; }
        public GameObject GasDwarfStorageA { get; private set; }
        public GameObject GasDwarfStorageB { get; private set; }
        public MermaidConversation Mermaid { get; private set; }
        public MermaidNoiseMaker MermaidNoiseMaker { get; private set; }

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
            ProbeTM = SearchUtilities.Find("Moon_Body/Sector_THM/Probe_TM");
            if(ProbeTM != null) {
                ProbeTM.SetActive(false);
            }
            ProbeInterloper = SearchUtilities.Find("Comet_Body/Sector_CO/Probe_Interloper");
            if (ProbeInterloper != null) {
                ProbeInterloper.SetActive(false);
            }
            ProbeET = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/Probe_ET");
            if(ProbeET != null) {
                ProbeET.SetActive(false);
            }
            ProbeAT = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/Probe_AT");
            if (ProbeAT != null) {
                ProbeAT.SetActive(false);
            }
            ProbeBH = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_QuantumFragment/Probe_BH");
            if (ProbeBH != null) {
                ProbeBH.SetActive(false);
            }
            ProbeVM = SearchUtilities.Find("VolcanicMoon_Body/Sector_VM/Probe_VM");
            if (ProbeVM != null) {
                ProbeVM.SetActive(false);
            }
            ProbeGiantsDeep = SearchUtilities.Find("GiantsDeep_Body/Sector_GD/Probe_GiantsDeep");
            if (ProbeGiantsDeep != null) {
                ProbeGiantsDeep.SetActive(false);
            }
            ProbeDB = SearchUtilities.Find("DarkBramble_Body/Sector_DB/Probe_DB");
            if(ProbeDB != null) {
                ProbeDB.SetActive(false);
            }
            ProbeTS = SearchUtilities.Find("RingWorld_Body/Sector_RingWorld/Probe_TS");
            if (ProbeTS != null) {
                ProbeTS.SetActive(false);
            }
            ProbeGasDwarf = SearchUtilities.Find("GasDwarf_Body/Sector/Probe_GasDwarf");
            if (ProbeGasDwarf != null) {
                ProbeGasDwarf.SetActive(false);
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
                var brambleTowerWarpReceiver = BrambleRocksBroken.transform.Find("Prefab_NOM_WarpReceiver");
                if (brambleTowerWarpReceiver != null) {
                    BrambleTowerWarpReceiver = brambleTowerWarpReceiver.gameObject;
                    BrambleTowerWarpReceiver.SetActive(false);
                }
            }
            BrambleComputerLock = SearchUtilities.Find("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension/text_bramble_computer_lock");
            BrambleComputerSand = SearchUtilities.Find("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension/text_bramble_computer_sand");
            if (BrambleComputerSand != null) {
                Observable.NextFrame().Subscribe(_ => {
                    BrambleComputerSand.SetActive(false);
                }).AddTo(BrambleComputerSand);
            }
            BrambleComputerAvailable = SearchUtilities.Find("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension/text_bramble_computer_available");
            if (BrambleComputerAvailable != null) {
                Observable.NextFrame().Subscribe(_ => {
                    BrambleComputerAvailable.SetActive(false);
                }).AddTo(BrambleComputerAvailable);
            }

            BrambleTowerWarpTransmitterShipLog = SearchUtilities.Find("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension/warpTransmitter_db_datura/shiplog_ew_bramble_tower_warp_1");
            if (BrambleTowerWarpTransmitterShipLog != null) {
                var notificationWithNewShipLogForBrambleTowerWarp = new GameObject("NotificationWithNewShipLogForBrambleTowerWarp");
                notificationWithNewShipLogForBrambleTowerWarp.transform.SetParent(BrambleTowerWarpTransmitterShipLog.transform);
                notificationWithNewShipLogForBrambleTowerWarp.transform.localPosition = Vector3.zero;
                notificationWithNewShipLogForBrambleTowerWarp.transform.localEulerAngles = Vector3.zero;
                NotificationWithNewShipLogForBrambleTowerWarp = notificationWithNewShipLogForBrambleTowerWarp.AddComponent<NotificationWithNewShipLog>();
                NotificationWithNewShipLogForBrambleTowerWarp.shipLogId = "ew_bramble_tower_warp_1";
                NotificationWithNewShipLogForBrambleTowerWarp.notificationText = "WARP_DETECT_NOTIFICATION";
                NotificationWithNewShipLogForBrambleTowerWarp.sphereShape = BrambleTowerWarpTransmitterShipLog.GetComponent<SphereShape>();
            }

            GasDwarf = SearchUtilities.Find("GasDwarf_Body/Sector");
            if(GasDwarf != null) {
                foreach(Transform child in GasDwarf.transform) {
                    if(child != null && child.name == "LightShaft") {
                        child.localScale = new Vector3(2, 20, 2);
                    }
                }
            }

            GasDwarfStorageA = SearchUtilities.Find("GasDwarf_Body/Sector/SkyIslandBuildings/GasDwarfStorageA");
            if (GasDwarfStorageA != null) {
                foreach (Transform transform in GasDwarfStorageA.transform) {
                    if (transform.name.Contains("CapsuleItemEmpty")) {
                        transform.gameObject.AddComponent<CapsuleItem>();
                    }
                }
            }
            GasDwarfStorageB = SearchUtilities.Find("GasDwarf_Body/Sector/SkyIslandBuildings/GasDwarfStorageB");
            if (GasDwarfStorageB != null) {
                foreach (Transform transform in GasDwarfStorageB.transform) {
                    if (transform.name.Contains("CapsuleItemEmpty")) {
                        transform.gameObject.AddComponent<CapsuleItem>();
                    }
                    else if(transform.name == "CapsuleItem") {
                        transform.gameObject.AddComponent<CapsuleItem>()._hasLiquid = true;
                    }
                }
            }

            var mermaid = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_2/Mermaid");
            if (mermaid != null) {
                Mermaid = mermaid.AddComponent<MermaidConversation>();
            }

            var mermaidNoiseMaker = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/Sector_TimeLoopInterior/Interactables_TimeLoopInterior/WarpCoreSocket/Prefab_NOM_WarpCoreVessel/FishtailEffect");
            if(mermaidNoiseMaker != null) {
                MermaidNoiseMaker = mermaidNoiseMaker.AddComponent<MermaidNoiseMaker>();
            }
        }
    }
}
