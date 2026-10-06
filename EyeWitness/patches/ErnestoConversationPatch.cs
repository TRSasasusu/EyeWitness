using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class ErnestoConversationPatch {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CharacterDialogueTree), nameof(CharacterDialogueTree.StartConversation))]
        public static void CharacterDialogueTree_StartConversation_Prefix(CharacterDialogueTree __instance) {
            if (ErnestoManager.Instance != null && __instance == ErnestoManager.Instance.DialogueTree) {
                ErnestoManager.Instance.StartConversation();
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.GetFreezeTimeWhileReadingConversations))]
        public static bool PlayerData_GetFreezeTimeWhileReadingConversations_Prefix(ref bool __result) {
            if (ErnestoManager.Instance != null && ErnestoManager.Instance.InConversation) {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
