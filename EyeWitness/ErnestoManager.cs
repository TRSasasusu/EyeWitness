using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewHorizons.Utility;
using UnityEngine;
using UniRx;

namespace EyeWitness {
    public class ErnestoManager {
        public static ErnestoManager Instance { get; private set; }

        public CharacterDialogueTree DialogueTree { get; private set; }
        public bool InConversation { get; private set; }
        GameObject _ernesto;
        PlayerSpawner _playerSpawner;

        GameObject _probeTH;
        GameObject _startingCamp;
        SpawnPoint _spCamp;
        bool _probeTHActive;
        GameObject _statueIslandInterior;
        GameObject _additionalWhiteboard;
        GameObject _apStatue;
        SpawnPoint _spStatueIslandInterior;
        GameObject _gabbroIsland;
        SpawnPoint _spGabbroIsland;
        GameObject _gasDwarf;
        SpawnPoint _spGasDwarf;
        //GameObject _apGasDwarfStoreRoom2Entrance;
        GameObject _gasDwarfStoreRoom2BrokenDoor;
        SpawnPoint _spGasDwarfStoreRoom2Entrance;
        GameObject _lamiText;
        SpawnPoint _spGasDwarfStoreRoom2Inside;
        SpawnPoint _spGasDwarfLab;
        GameObject _controlModule;
        SpawnPoint _spControlModule;
        GameObject _markerPedestal;
        GameObject _gdProjectionStone;
        GameObject _whiteholeStationSuperStructure;
        GameObject _whiteholeScroll;
        SpawnPoint _spWhiteholeStationSuperStructure;
        GameObject _bhEyeShrine;
        GameObject _bhEyeVisibilityTracker;
        GameObject _bedBells;
        GameObject _strangerEyeSymbol;
        SpawnPoint _spBhEyeShrine;
        QuantumMoon _quantumMoon;
        GameObject _moon6thLocation;
        GameObject _signalMoonGap;
        SpawnPoint _spSignalMoonGap;
        GameObject _moon1stWhiteboard;
        GameObject _moonSCPart;
        SpawnPoint _spMoonLab;
        GameObject _centerComputer;
        SpawnPoint _spGasDwarfTower;
        GameObject _mitis;
        SpawnPoint _spGasDwarfTowerTop;
        GameObject _sandET;
        GameObject _helCave;
        GameObject _helText;
        GameObject _helSwitch;
        SpawnPoint _spHel;
        GameObject _sandAT;
        GameObject _sandColumnEffect;
        GameObject _sandColumnVolume;
        GameObject _at;
        GameObject _warpReceiver;
        SpawnPoint _spAt;
        GameObject _brambleRocks;
        GameObject _brambleRocksBroken;
        GameObject _probeAT;
        bool _probeATActive;

        public ErnestoManager() {
            Instance = this;

            _ernesto = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_NomaiCrater/Ernesto");
            if(_ernesto != null) {
                //_ernesto.SetActive(false); // TODO
            }

            if (!PlayerData.GetPersistentCondition("EW_MET_MERMAID")) {
                return;
            }

            DialogueTree = _ernesto.GetComponentInChildren<CharacterDialogueTree>();
            if(DialogueTree != null) {
                DialogueTree.OnEndConversation += () => {
                    InConversation = false;
                };
                DialogueTree.OnAdvancePage += AdvancePage;
            }

            _probeTH = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Interactables_Village/LaunchTower/Effects_HEA_Campfire/Probe_TH");
            _startingCamp = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp");
            _spCamp = MakeSpawnPoint("sp_camp", _startingCamp.transform, new Vector3(45.6334f, 5.6142f, -20.4885f), new Vector3(350f, 340f, 350f), true);

            _statueIslandInterior = SearchUtilities.Find("StatueIsland_Body/Sector_StatueIsland/Sector_StatueIslandInterior");
            _additionalWhiteboard = SearchUtilities.Find("StatueIsland_Body/Sector_StatueIsland/Sector_StatueIslandInterior/additional_whiteboard");
            _apStatue = MakePoint("statue_point", _statueIslandInterior.transform, new Vector3(-6.1971f, 5.537f, 24.5244f), new Vector3(0f, 0f, 0f));
            _spStatueIslandInterior = MakeSpawnPoint("sp_statue_island_interior", _statueIslandInterior.transform, new Vector3(-5.0712f, 1.9321f, 35.9373f), new Vector3(0f, 225f, 0f), true);

            _gabbroIsland = SearchUtilities.Find("GabbroIsland_Body/Sector_GabbroIsland");
            _spGabbroIsland = MakeSpawnPoint("sp_gabbro_island", _gabbroIsland.transform, new Vector3(1.8617f, 1.7274f, 35.546f), new Vector3(0f, 140f, 0f), true);

            _gasDwarf = SearchUtilities.Find("GasDwarf_Body/Sector");
            _spGasDwarf = MakeSpawnPoint("sp_gas_dwarf", _gasDwarf.transform, new Vector3(-29.9767f, -3.5245f, -51.1695f), new Vector3(0f, 30f, 0f), true);
            //_apGasDwarfStoreRoom2Entrance = MakePoint("ap_gas_dwarf_store_room_2_entrance", _gasDwarf.transform, new Vector3(-57.775f, -34.0345f, 22.5876f), new Vector3(0f, 50f, 0f));
            _gasDwarfStoreRoom2BrokenDoor = SearchUtilities.Find("GasDwarf_Body/Sector/SkyIslandBuildings/GasDwarfStorageB/Prefab_NOM_RotatingDoor_Broken");
            _spGasDwarfStoreRoom2Entrance = MakeSpawnPoint("sp_gas_dwarf_store_room_2_entrance", _gasDwarf.transform, new Vector3(-59.0041f, -37.4431f, 31.1005f), new Vector3(0f, 70f, 0f));
            _lamiText = SearchUtilities.Find("GasDwarf_Body/Sector/SkyIslandBuildings/GasDwarfStorageB/text_gas_dwarf_storage_b_lami");
            _spGasDwarfStoreRoom2Inside = MakeSpawnPoint("sp_gas_dwarf_store_room_2_inside", _gasDwarf.transform, new Vector3(10.0864f, -45.3554f, 13.0409f), new Vector3(0f, 230f, 0f));
            _spGasDwarfLab = MakeSpawnPoint("sp_gas_dwarf_lab", _gasDwarf.transform, new Vector3(6.3269f, -6.8424f, 76.1526f), new Vector3(0f, 30f, 0f));

            _controlModule = SearchUtilities.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon/Sector_Module_Intact");
            _spControlModule = MakeSpawnPoint("sp_control_module", _controlModule.transform, new Vector3(-5.3207f, 0.441f, 7.8103f), new Vector3(0f, 270f, 90f), true);
            _markerPedestal = SearchUtilities.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon/Sector_Module_Intact/pedestal_for_marker");
            _gdProjectionStone = SearchUtilities.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon/Sector_Module_Intact/Interactables_Module_Intact/Prefab_NOM_SharedStone_ConstructionYard");

            _whiteholeStationSuperStructure = SearchUtilities.Find("WhiteholeStationSuperstructure_Body/Sector");
            _whiteholeScroll = SearchUtilities.Find("WhiteholeStationSuperstructure_Body/Sector/text_eye_shrine");
            _spWhiteholeStationSuperStructure = MakeSpawnPoint("sp_whitehole_station_superstructure", _whiteholeStationSuperStructure.transform, new Vector3(-2.8654f, 35.2288f, -8.6227f), new Vector3(60f, 180f, 180f), true);

            _bhEyeShrine = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3");
            _bhEyeVisibilityTracker = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/HangingCityEyeSymbolVisibilityTracker");
            _bedBells = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/Bed");
            _strangerEyeSymbol = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/Prop_IP_EyeSymbol");
            _spBhEyeShrine = MakeSpawnPoint("sp_bh_eye_shrine", _bhEyeShrine.transform, new Vector3(96.5063f, 196.8519f, 0.3324f), new Vector3(338f, 283f, 356f), true);

            _quantumMoon = SearchUtilities.Find("QuantumMoon_Body").GetComponent<QuantumMoon>();
            _moon6thLocation = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE");
            _signalMoonGap = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/signal_qm_depth");
            _spSignalMoonGap = MakeSpawnPoint("sp_signal_moon_gap", _moon6thLocation.transform.parent, new Vector3(9.0542f, -40.0642f, 64.2634f), new Vector3(320f, 300f, 235f), true);
            _moon1stWhiteboard = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/QMDepth/GameObject/text_moon");
            _moonSCPart = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/QMDepth/GameObject/Main/Floor (33)");
            _spMoonLab = MakeSpawnPoint("sp_moon_lab", _moon6thLocation.transform.parent, new Vector3(55.9546f, 17.2699f, 5.8926f), new Vector3(15f, 305f, 245f));

            _centerComputer = SearchUtilities.Find("GasDwarf_Body/Sector/GasDwarfTower/text_gas_dwarf_memory_now_5");
            _spGasDwarfTower = MakeSpawnPoint("sp_gas_dwarf_tower", _gasDwarf.transform, new Vector3(20.783f, 50.2835f, 35.22f), new Vector3(20f, 140f, 0f), true);
            _mitis = SearchUtilities.Find("GasDwarf_Body/Sector/GasDwarfTower/Mitis");
            _spGasDwarfTowerTop = MakeSpawnPoint("sp_gas_dwarf_tower_top", _gasDwarf.transform, new Vector3(19.1979f, 64.9822f, 29.6869f), new Vector3(0, 150, 0));

            _sandET = SearchUtilities.Find("CaveTwin_Body/SandSphere_Rising");
            _helCave = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/Sector_SouthHemisphere/Sector_SouthUnderground/Sector_SpeedCave");
            _helText = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/text_bramble_tower");
            _helSwitch = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/bottom_switch_tractorbeam");
            _spHel = MakeSpawnPoint("sp_hel", _helCave.transform, new Vector3(39.2969f, -27.7762f, 11.7172f), new Vector3(350f, 350f, 230f));
            _spHel._triggerVolumes = new OWTriggerVolume[] { _helCave.GetComponent<Sector>()._owTriggerVolume, _helCave.transform.parent.GetComponent<Sector>()._owTriggerVolume };

            _sandAT = SearchUtilities.Find("TowerTwin_Body/SandSphere_Draining");
            _sandColumnEffect = SearchUtilities.Find("SandFunnel_Body/ScaleRoot/Geo_SandFunnel/Effects_HT_SandColumn");
            _sandColumnVolume = SearchUtilities.Find("SandFunnel_Body/ScaleRoot/Volumes_SandFunnel");
            _warpReceiver = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/BrambleRocksBroken/Prefab_NOM_WarpReceiver");
            _at = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin");
            _brambleRocks = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/BrambleRocks");
            _brambleRocksBroken = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/BrambleRocksBroken");
            _probeAT = SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin/Probe_AT");
            _spAt = MakeSpawnPoint("sp_at", _at.transform, new Vector3(88.1632f, -0.1838f, -15.2526f), new Vector3(90f, 100f, 0f), true);
        }

        public void StartConversation() {
            InConversation = true;
        }

        void AdvancePage(string nodeName, int pageNum) {
            EyeWitness.Log($"ErnestoManager.AdvancePage: nodeName={nodeName}, pageNum={pageNum}");
            if (nodeName != "ErnestoGo") {
                return;
            }

            if (_playerSpawner == null) {
                _playerSpawner = Locator.GetPlayerBody().GetComponent<PlayerSpawner>();
            }

            if (pageNum == 1) {
                _playerSpawner.DebugWarp(_spCamp);
                _ernesto.transform.parent = _spCamp.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            }
            else if (pageNum == 2) {
                _probeTHActive = _probeTH.activeSelf;
                _probeTH.SetActive(true);
                LockOn(_probeTH.transform, new Vector3(0f, 0.5f, 0f));
            }
            else if (pageNum == 5) {
                _probeTH.SetActive(_probeTHActive);
                _playerSpawner.DebugWarp(_spStatueIslandInterior);
                _ernesto.transform.parent = _spStatueIslandInterior.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 6f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_ernesto.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 6) {
                LockOn(_additionalWhiteboard.transform, new Vector3(0f, 0.5f, 0f));
            }
            else if (pageNum == 7) {
                LockOn(_apStatue.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 8) {
                _playerSpawner.DebugWarp(_spGabbroIsland);
                _ernesto.transform.parent = _spGabbroIsland.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 6f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_ernesto.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 11) {
                SkyIslandManager.Instance.WarpToGasDwarf();
                _playerSpawner.DebugWarp(_spGasDwarf);
                _ernesto.transform.parent = _spGasDwarf.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_ernesto.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 12) {
                _playerSpawner.DebugWarp(_spGasDwarfLab);
                _ernesto.transform.parent = _spGasDwarfLab.transform;
                _ernesto.transform.localPosition = new Vector3(0.7332f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_ernesto.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 13) {
                _playerSpawner.DebugWarp(_spGasDwarfStoreRoom2Entrance);
                _ernesto.transform.parent = _spGasDwarfStoreRoom2Entrance.transform;
                _ernesto.transform.localPosition = new Vector3(0f, -2f, 30f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_gasDwarfStoreRoom2BrokenDoor.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 14) {
                _playerSpawner.DebugWarp(_spGasDwarfStoreRoom2Inside);
                _ernesto.transform.parent = _spGasDwarfStoreRoom2Inside.transform;
                _ernesto.transform.localPosition = new Vector3(0f, -3f, 8f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_lamiText.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 15) {
                SkyIslandManager.Instance.WarpToGiantsDeep();
                _playerSpawner.DebugWarp(_spControlModule);
                _ernesto.transform.parent = _spControlModule.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_markerPedestal.transform, new Vector3(0f, 0.5f, 0f));
            }
            else if (pageNum == 16) {
                LockOn(_gdProjectionStone.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 17) {
                LockOn(_ernesto.transform, new Vector3(0f, 0f, 0f));
            }
            else if(pageNum == 18) {
                _playerSpawner.DebugWarp(_spWhiteholeStationSuperStructure);
                _ernesto.transform.parent = _spWhiteholeStationSuperStructure.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 20f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_whiteholeScroll.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 20) {
                _playerSpawner.DebugWarp(_spBhEyeShrine);
                _ernesto.transform.parent = _spBhEyeShrine.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 6f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_ernesto.transform, new Vector3(0f, 0f, 0f));
            }
            else if(pageNum == 21) {
                LockOn(_bhEyeVisibilityTracker.transform, new Vector3(0f, 0f, 0f));
            }
            else if(pageNum == 22) {
                LockOn(_strangerEyeSymbol.transform, new Vector3(0f, 0f, 0f));
            }
            else if (pageNum == 23) {
                LockOn(_bedBells.transform, new Vector3(0f, 0f, 0f));
            }
            else if(pageNum == 24) {
                _playerSpawner.DebugWarp(_spSignalMoonGap);
                _ernesto.transform.parent = _spSignalMoonGap.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 5f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_signalMoonGap.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 25) {
                _playerSpawner.DebugWarp(_spMoonLab);
                _ernesto.transform.parent = _spMoonLab.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_moon1stWhiteboard.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 26) {
                LockOn(_moonSCPart.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 27) {
                SkyIslandManager.Instance.WarpToGasDwarf();
                _playerSpawner.DebugWarp(_spGasDwarfTower);
                _ernesto.transform.parent = _spGasDwarfTower.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_centerComputer.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 28) {
                _playerSpawner.DebugWarp(_spGasDwarfTowerTop);
                _ernesto.transform.parent = _spGasDwarfTowerTop.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_mitis.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 30) {
                _sandET.SetActive(false);
                SkyIslandManager.Instance.WarpToGiantsDeep();
                _playerSpawner.DebugWarp(_spHel);
                _ernesto.transform.parent = _spHel.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_helText.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 31) {
                LockOn(_ernesto.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 32) {
                LockOn(_helSwitch.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 33) {
                _sandAT.SetActive(false);
                _sandColumnEffect.SetActive(false);
                _sandColumnVolume.SetActive(false);
                _probeATActive = _probeAT.activeSelf;
                _brambleRocks.SetActive(false);
                _brambleRocksBroken.SetActive(true);
                _probeAT.SetActive(true);
                _warpReceiver.SetActive(true);
                _playerSpawner.DebugWarp(_spAt);
                _ernesto.transform.parent = _spAt.transform;
                _ernesto.transform.localPosition = new Vector3(0f, 0f, 9f);
                _ernesto.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                LockOn(_warpReceiver.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 34) {
                _sandET.SetActive(true);
            }
            else if(pageNum == 35) {
                LockOn(_probeAT.transform, new Vector3(0, 0, 0));
            }
            else if(pageNum == 36) {
            }
        }

        SpawnPoint MakeSpawnPoint(string name, Transform parent, Vector3 pos, Vector3 rot, bool setOWTriggerVolume = false) {
            var spawnPoint = MakePoint(name, parent, pos, rot).AddComponent<SpawnPoint>();
            if (setOWTriggerVolume) {
                spawnPoint._triggerVolumes = new OWTriggerVolume[] {
                    parent.GetComponent<Sector>()._owTriggerVolume,
                };
            }
            else {
                spawnPoint._triggerVolumes = new OWTriggerVolume[] { };
            }
            return spawnPoint;
        }

        GameObject MakePoint(string name, Transform parent, Vector3 pos, Vector3 rot) {
            var pointObj = new GameObject(name);
            pointObj.transform.parent = parent;
            pointObj.transform.localPosition = pos;
            pointObj.transform.localEulerAngles = rot;
            return pointObj;
        }

        void LockOn(Transform attentionPoint, Vector3 attentionPointOffset) {
            Locator.GetPlayerTransform().GetRequiredComponent<PlayerLockOnTargeting>().LockOn(attentionPoint, attentionPointOffset, 2f, false, 1f);
        }
    }
}
