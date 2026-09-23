using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class ShipLogPatch {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ShipLogEntry), nameof(ShipLogEntry.GetAstroObjectID))]
        public static bool ShipLogEntry_GetAstroObjectID_Prefix(ShipLogEntry __instance, ref string __result) {
            if(__instance._id != "ew_mermaid") {
                return true;
            }

            if(EyeWitness.HasShipLog("ew_mermaid_1") || EyeWitness.HasShipLog("ew_mermaid_rumor1")) {
                return true;
            }

            if(EyeWitness.HasShipLog("ew_gas_dwarf_1")) {
                __result = "Gas Dwarf";
                return false;
            }

            __result = "QUANTUM_MOON";
            return false;
        }
    }
}
