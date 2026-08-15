using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class PostCreditsPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PostCreditsManager), nameof(PostCreditsManager.Update))]
        public static void PostCreditsManager_Update_Postfix() {
            if(PlayerData.GetPersistentCondition("EW_MET_MERMAID")) {
                if(PostCreditsHelper.Instance != null && PostCreditsHelper.Instance._ewPostCredit != null && !PostCreditsHelper.Instance._ewPostCredit.activeSelf) {
                    PostCreditsHelper.Instance._ewPostCredit.SetActive(true);
                }
            }
        }
    }
}
