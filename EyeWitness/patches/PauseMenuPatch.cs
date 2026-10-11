using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class PauseMenuPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PauseMenuManager), nameof(PauseMenuManager.Start))]
        public static void PauseMenuManager_Start_Postfix(PauseMenuManager __instance) {
            __instance._skipToNextLoopButton.SetActive(TimeLoop.IsTimeLoopEnabled());
        }
    }
}
