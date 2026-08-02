using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            EyeWitness.Log("OPC OnStartOfTimeLoop Prefix called by x100");
            var player = Locator.GetPlayerTransform();
            var targetPos = player.position + new Vector3(100, 100, 100); //player.up * 100f;
            __instance.transform.LookAt(targetPos);
        }
    }
}
