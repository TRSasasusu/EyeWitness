using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class OPCPatch {
        //[HarmonyPostfix]
        //[HarmonyPatch(typeof(OrbitalProbeLaunchController), nameof(OrbitalProbeLaunchController.Awake))]
        //public static void OrbitalProbeLaunchController_Awake_Postfix(OrbitalProbeLaunchController __instance) {
        //    EyeWitness.Log("OPC Awake Postfix called");
        //    var player = Locator.GetPlayerTransform();
        //    __instance.transform.LookAt(player);
        //}

        [HarmonyPrefix]
        [HarmonyPatch(typeof(OrbitalProbeLaunchController), nameof(OrbitalProbeLaunchController.OnStartOfTimeLoop))]
        public static void OrbitalProbeLaunchController_OnStartOfTimeLoop_Prefix(OrbitalProbeLaunchController __instance) {
            EyeWitness.Log("OPC OnStartOfTimeLoop Prefix called");

            if(!EyeWitness.HasShipLog("ew_camp_probe_2")) {
                var player = Locator.GetPlayerTransform();
                __instance.transform.LookAt(player);
                return;
            }

            var probe = MarkerItem.GetProbeBasedOnMarker();
            if(probe == null) {
                return;
            }
            __instance.transform.LookAt(probe.transform);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(OrbitalProbeLaunchController), nameof(OrbitalProbeLaunchController.LaunchProbe))]
        public static bool OrbitalProbeLaunchController_LaunchProbe_Prefix(OrbitalProbeLaunchController __instance) {
            EyeWitness.Log("OPC LaunchProbe Prefix called");
            //if(EyeWitness.HasShipLog("ew_camp_probe_2")) {
            //    return true;
            //}

            GameObject probe = null;
            if (!EyeWitness.HasShipLog("ew_camp_probe_2")) {
                probe = ModifyObjects.Instance.ProbeTH;
            }
            else {
                probe = MarkerItem.GetProbeBasedOnMarker();
            }
            if(probe == null) {
                return true;
            }

            __instance._probeBody.gameObject.SetActive(false);

            var basePos = probe.transform.localPosition;
            probe.transform.position = __instance._probeBody.transform.position;
            probe.transform.localScale = Vector3.one;
            probe.SetActive(true);
            probe.transform.DOLocalMove(basePos, 3);
            probe.transform.DOScale(0.25f, 3);

            if(probe == ModifyObjects.Instance.ProbeAT) {
                ModifyObjects.Instance.BrambleRocks.SetActive(false);
                ModifyObjects.Instance.BrambleRocksBroken.SetActive(true);
            }

            return false;
        }
    }
}
