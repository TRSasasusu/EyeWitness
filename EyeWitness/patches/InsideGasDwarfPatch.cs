using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class InsideGasDwarfPatch {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AudioSignal), nameof(AudioSignal.UpdateSignalStrength))]
        public static bool AudioSignal_UpdateSignalStrength_Prefix(AudioSignal __instance) {
            if(SkyIslandManager.Instance != null && SkyIslandManager.Instance.InsideGasDwarf) {
                if(__instance.name != "signal_for_island_on_sky") {
                    __instance._signalStrength = 0f;
                    __instance._degreesFromScope = 180f;
                    return false;
                }
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ShipHUDMarker), nameof(ShipHUDMarker.RefreshOwnVisibility))]
        public static bool ShipHUDMarker_RefreshOwnVisibility_Prefix(ShipHUDMarker __instance) {
            if (SkyIslandManager.Instance != null && SkyIslandManager.Instance.InsideGasDwarf) {
                __instance._isVisible = false;
                if (__instance._canvasMarker != null) {
                    __instance._canvasMarker.SetVisibility(__instance._isVisible);
                }
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ShipLogEntryHUDMarker), nameof(ShipLogEntryHUDMarker.RefreshOwnVisibility))]
        public static bool ShipLogEntryHUDMarker_RefreshOwnVisibility_Prefix(ShipLogEntryHUDMarker __instance) {
            if (SkyIslandManager.Instance != null && SkyIslandManager.Instance.InsideGasDwarf) {
                //if(__instance.transform.root.name != "GasDwarf_Body") {
                if(ShipLogEntryHUDMarker.s_entryLocation == null || ShipLogEntryHUDMarker.s_entryLocation.transform.root.name != "GasDwarf_Body") {
                    __instance._isVisible = false;
                    if (__instance._canvasMarker != null) {
                        __instance._canvasMarker.SetVisibility(__instance._isVisible);
                    }
                    return false;
                }
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ReferenceFrameTracker), nameof(ReferenceFrameTracker.Update))]
        public static bool ReferenceFrameTracker_Update_Prefix(ReferenceFrameTracker __instance) {
            if(__instance._activeCam == null) {
                return true;
            }

            if (SkyIslandManager.Instance != null && SkyIslandManager.Instance.InsideGasDwarf) {
                __instance.UntargetReferenceFrame();
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ReferenceFrameTracker), nameof(ReferenceFrameTracker.UpdateTargeting))]
        public static bool ReferenceFrameTracker_UpdateTargeting_Prefix(ReferenceFrameTracker __instance) {
            if (SkyIslandManager.Instance != null && SkyIslandManager.Instance.InsideGasDwarf) {
                if (OWInput.IsNewlyPressed(InputLibrary.lockOn, InputMode.Character | InputMode.Map | InputMode.ScopeZoom | InputMode.ShipCockpit | InputMode.LandingCam)) {
                    __instance.UntargetReferenceFrame();
                }
                __instance._hasPossibleTarget = false;
                return false;
            }
            return true;
        }
    }
}
