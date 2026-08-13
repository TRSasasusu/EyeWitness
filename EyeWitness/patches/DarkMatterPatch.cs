using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class DarkMatterPatch {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(DarkMatterVolume), nameof(DarkMatterVolume.OnEffectVolumeEnter))]
        public static bool DarkMatterVolume_OnEffectVolumeEnter_Prefix(DarkMatterVolume __instance) {
            if(CapsuleItem.PickedOne != null && CapsuleItem.PickedOne._hasLiquid) {
                CapsuleItem.PickedOne.UseLiquid();
                __instance.gameObject.SetActive(false);

                foreach(var particle in __instance.transform.parent.GetComponentsInChildren<ParticleSystem>()) {
                    if(particle.name.Contains("AuroraWisps")) {
                        particle.gameObject.SetActive(false);
                    }
                }

                return false;
            }
            return true;
        }
    }
}
