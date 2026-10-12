using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewHorizons.Utility;
using UnityEngine;

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
            _spWhiteholeStationSuperStructure = MakeSpawnPoint("sp_whitehole_station_superstructure", _whiteholeStationSuperStructure.transform, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), true);

            _bhEyeShrine = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3");
            _bhEyeVisibilityTracker = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/HangingCityEyeSymbolVisibilityTracker");
            _bedBells = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/Bed");
            _strangerEyeSymbol = SearchUtilities.Find("BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/Prop_IP_EyeSymbol");
            _spBhEyeShrine = MakeSpawnPoint("sp_bh_eye_shrine", _bhEyeShrine.transform, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), true);
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
        }

        SpawnPoint MakeSpawnPoint(string name, Transform parent, Vector3 pos, Vector3 rot, bool setOWTriggerVolume = false) {
            var spawnPoint = MakePoint(name, parent, pos, rot).AddComponent<SpawnPoint>();
            if (setOWTriggerVolume) {
                spawnPoint._triggerVolumes = new OWTriggerVolume[] {
                    parent.GetComponent<Sector>()._owTriggerVolume,
                };
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
