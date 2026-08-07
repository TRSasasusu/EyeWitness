using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NewHorizons.Utility;
using UniRx;
using UniRx.Triggers;

namespace EyeWitness {
    public class SkyIslandManager {
        GameObject _gasDwarf;
        GameObject _giantsDeep;
        GameObject _parentOfSignalForIslandOnSkyDummy;
        GameObject _signalForIslandOnSkyDummy;
        GameObject _triggerForWarp;
        PlayerSpawner _playerSpawner;
        SpawnPoint _spawnPointInGasDwarf;
        SpawnPoint _spawnPointInGiantsDeep;

        bool _insideGasDwarf = false;

        public SkyIslandManager() {
            _gasDwarf = SearchUtilities.Find("GasDwarf_Body");
            if(_gasDwarf != null) {
                Observable.NextFrame().Subscribe(_ => {
                    //_gasDwarf.SetActive(false);
                    DisableGasDwarf();
                }).AddTo(_gasDwarf);
                //_gasDwarf.SetActive(false);

                var spawnPoint = new GameObject("SpawnPointInGasDwarf");
                spawnPoint.transform.SetParent(_gasDwarf.transform);
                spawnPoint.transform.localPosition = new Vector3(0f, 0f, -190f);
                _spawnPointInGasDwarf = spawnPoint.AddComponent<SpawnPoint>();
            }

            _giantsDeep = SearchUtilities.Find("GiantsDeep_Body");
            if(_giantsDeep != null) {
                var spawnPoint = new GameObject("SpawnPointInGiantsDeep");
                spawnPoint.transform.SetParent(_giantsDeep.transform);
                spawnPoint.transform.localPosition = UnityEngine.Random.onUnitSphere * 800;
                _spawnPointInGiantsDeep = spawnPoint.AddComponent<SpawnPoint>();
            }

            _parentOfSignalForIslandOnSkyDummy = SearchUtilities.Find("GiantsDeep_Body/Sector_GD/parent_of_signal_for_island_on_sky_dummy");
            _signalForIslandOnSkyDummy = SearchUtilities.Find("GiantsDeep_Body/Sector_GD/parent_of_signal_for_island_on_sky_dummy/signal_for_island_on_sky_dummy");
            if(_parentOfSignalForIslandOnSkyDummy != null) {
                _signalForIslandOnSkyDummy = _parentOfSignalForIslandOnSkyDummy.transform.Find("signal_for_island_on_sky_dummy").gameObject;
                _parentOfSignalForIslandOnSkyDummy.transform.localPosition = UnityEngine.Random.onUnitSphere * 900;

                _triggerForWarp = new GameObject("TriggerForWarp");
                _triggerForWarp.transform.SetParent(_parentOfSignalForIslandOnSkyDummy.transform);
                _triggerForWarp.transform.localPosition = Vector3.zero;
                _triggerForWarp.transform.localEulerAngles = Vector3.zero;
                var sphereCollider = _triggerForWarp.AddComponent<SphereCollider>();
                sphereCollider.radius = 40f;
                sphereCollider.isTrigger = true;
                _triggerForWarp.OnTriggerEnterAsObservable().Subscribe(other => {
                    EyeWitness.Log($"TriggerForWarp: {other.gameObject.name}, root: {other.transform.root.name}");
                    //if (other.CompareTag("Player")) {
                    var root = other.transform.root;
                    if (root == Locator.GetPlayerTransform() || (root == Locator.GetShipTransform() && PlayerState.IsInsideShip())) {
                        if(_playerSpawner == null) {
                            _playerSpawner = Locator.GetPlayerBody().GetComponent<PlayerSpawner>();
                        }
                        _playerSpawner.DebugWarp(_spawnPointInGasDwarf);
                        EnableGasDwarf();
                        Observable.TimerFrame(2, FrameCountType.FixedUpdate).Subscribe(_ => {
                            _insideGasDwarf = true;
                        }).AddTo(_gasDwarf);
                    }
                });

                _parentOfSignalForIslandOnSkyDummy.UpdateAsObservable().Subscribe(_ => {
                    var playerBody = Locator.GetPlayerBody();
                    if (playerBody == null) {
                        return;
                    }
                    if (_giantsDeep == null) {
                        return;
                    }

                    var distanceFromGiantsDeep = Vector3.Distance(playerBody.transform.position, _giantsDeep.transform.position);
                    if (distanceFromGiantsDeep < 950 && distanceFromGiantsDeep > 840 && EyeWitness.HasShipLog("ew_signal_for_island_1")) {
                        if (!_signalForIslandOnSkyDummy.activeSelf) {
                            _signalForIslandOnSkyDummy.SetActive(true);
                            _triggerForWarp.SetActive(true);

                            Vector3 forwardPos;
                            EyeWitness.Log($"dot: {Vector3.Dot(playerBody.transform.forward, (playerBody.transform.position - _giantsDeep.transform.position).normalized)}");
                            if(Mathf.Abs(Vector3.Dot(playerBody.transform.forward, (playerBody.transform.position - _giantsDeep.transform.position).normalized)) > 0.8f) {
                                forwardPos = playerBody.transform.position + playerBody.transform.up * (200f + UnityEngine.Random.value * 20);
                            }
                            else {
                                forwardPos = playerBody.transform.position + playerBody.transform.forward * (200f + UnityEngine.Random.value * 20);
                            }
                            var gdToForwardPosVector = forwardPos - _giantsDeep.transform.position;
                            _parentOfSignalForIslandOnSkyDummy.transform.position = gdToForwardPosVector.normalized * 900 + _giantsDeep.transform.position;
                        }
                    }
                    else {
                        if (_signalForIslandOnSkyDummy.activeSelf) {
                            _signalForIslandOnSkyDummy.SetActive(false);
                            _triggerForWarp.SetActive(false);
                        }
                    }

                    if(_gasDwarf == null || !_gasDwarf.activeSelf) {
                        return;
                    }
                    if(!_insideGasDwarf) {
                        return;
                    }
                    var distanceFromGasDwarf = Vector3.Distance(playerBody.transform.position, _gasDwarf.transform.position);
                    if(distanceFromGasDwarf > 220) {
                        if(_playerSpawner == null) {
                            _playerSpawner = Locator.GetPlayerBody().GetComponent<PlayerSpawner>();
                        }
                        _playerSpawner.DebugWarp(_spawnPointInGiantsDeep);
                        DisableGasDwarf();
                        _insideGasDwarf = false;
                        Observable.TimerFrame(2, FrameCountType.FixedUpdate).Subscribe(_ => {
                            _spawnPointInGiantsDeep.transform.localPosition = UnityEngine.Random.onUnitSphere * 800;
                        }).AddTo(_spawnPointInGiantsDeep);
                    }
                });

                _signalForIslandOnSkyDummy.SetActive(false);
                _triggerForWarp.SetActive(false);
            }
        }

        void DisableGasDwarf() {
            if (_gasDwarf != null) {
                foreach(Transform child in _gasDwarf.transform) {
                    child.gameObject.SetActive(false);
                }
            }
        }

        void EnableGasDwarf() {
            if (_gasDwarf != null) {
                foreach (Transform child in _gasDwarf.transform) {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
