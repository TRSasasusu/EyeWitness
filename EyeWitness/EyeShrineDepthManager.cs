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
    public class EyeShrineDepthManager {
        List<GameObject> _singularities;
        GameObject _emberTwin;
        GameObject _hangingCity;
        VisibilityObject _emberEyeSymbol;
        VisibilityObject _hangingCityEyeSymbol;
        List<OWCamera> _probeCameras;

        public EyeShrineDepthManager() {
            _singularities = new List<GameObject> {
                SearchUtilities.Find("Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/bh_to_eyeshrine_depth"),
                SearchUtilities.Find("Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/bh_from_eyeshrine_depth"),
                SearchUtilities.Find("Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/wh_to_eyeshrine_depth"),
                SearchUtilities.Find("Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3/EyeShrineDepth/wh_from_eyeshrine_depth"),
            };
            foreach (var singularity in _singularities) {
                if (singularity != null) {
                    singularity.SetActive(false);
                }
            }

            {
                _emberTwin = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin");
                var tracker = new GameObject("EmberEyeSymbolVisibilityTracker");
                tracker.transform.parent = _emberTwin.transform;
                tracker.transform.localPosition = new Vector3(65.7284f, -128.0212f, -17.8615f);
                tracker.transform.localEulerAngles = Vector3.zero;
                var shape = tracker.AddComponent<SphereShape>();
                shape.radius = 3.5f;
                var eyeSymbol = tracker.AddComponent<ShapeVisibilityTracker>();
                _emberEyeSymbol = tracker.AddComponent<VisibilityObject>();
            }

            {
                _hangingCity = SearchUtilities.Find("Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District3");
                var tracker = new GameObject("HangingCityEyeSymbolVisibilityTracker");
                tracker.transform.parent = _hangingCity.transform;
                tracker.transform.localPosition = new Vector3(92.4507f, 210.4211f, 1.85f);
                tracker.transform.localEulerAngles = Vector3.zero;
                var shape = tracker.AddComponent<SphereShape>();
                shape.radius = 4.5f;
                var eyeSymbol = tracker.AddComponent<ShapeVisibilityTracker>();
                _hangingCityEyeSymbol = tracker.AddComponent<VisibilityObject>();
            }


            _hangingCityEyeSymbol.UpdateAsObservable().Subscribe(_ => {
                UpdateRoutine();
            });
        }

        void UpdateRoutine() {
            if (_singularities[0].activeSelf) {
                return;
            }

            if (!_emberEyeSymbol._visibilityTrackers[0].enabled) {
                return;
            }
            if (!_hangingCityEyeSymbol._visibilityTrackers[0].enabled) {
                return;
            }

            if (_probeCameras == null || _probeCameras.Count == 0) {
                var probe = Locator.GetProbe();
                _probeCameras = new List<OWCamera> {
                    probe.GetForwardCamera().GetOWCamera(),
                    probe.GetReverseCamera().GetOWCamera(),
                    probe.GetRotatingCamera().GetOWCamera(),
                };
            }

            if((_emberEyeSymbol.IsVisible() || _probeCameras.Any(cam => cam != null && _emberEyeSymbol.CheckVisibilityFromProbe(cam))) &&
                (_hangingCityEyeSymbol.IsVisible() || _probeCameras.Any(cam => cam != null && _hangingCityEyeSymbol.CheckVisibilityFromProbe(cam)))) {
                foreach(var singularity in _singularities) {
                    if(singularity != null) {
                        singularity.SetActive(true);
                    }
                }
            }
        }
    }
}
