using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class TextPatch {
        //[HarmonyPostfix]
        //[HarmonyPatch(typeof(TranslatorWord), MethodType.Constructor)]
        //public static void TranslatorWord_Constructor_Postfix(TranslatorWord __instance) {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(NomaiTranslatorProp), nameof(NomaiTranslatorProp.SwitchTextNode))]
        public static void NomaiTranslatorProp_SwitchTextNode_Prefix(ref string textNode) {
            //if(__instance.TranslatedText.Contains("<")) {
            //    string text = string.Concat(Mathf.Max(0f, Mathf.Floor((660f - TimeLoop.GetSecondsElapsed()) / 60f)));
            //    __instance.TranslatedText = __instance.TranslatedText.Replace("<EWMinutesDBWarp>", text);
            //    text = string.Concat(Mathf.Max(0f, (660f - Mathf.Floor(TimeLoop.GetSecondsElapsed())) % 60f));
            //    __instance.TranslatedText = __instance.TranslatedText.Replace("<EWSecondsDBWarp>", text);
            //}
            if(!string.IsNullOrEmpty(textNode) && textNode.Contains("<")) {
                string text = string.Concat(Mathf.Max(0f, Mathf.Floor((660f - TimeLoop.GetSecondsElapsed()) / 60f)));
                textNode = textNode.Replace("<EWMinutesDBWarp>", text);
                text = string.Concat(Mathf.Max(0f, (660f - Mathf.Floor(TimeLoop.GetSecondsElapsed())) % 60f));
                textNode = textNode.Replace("<EWSecondsDBWarp>", text);
            }
        }
    }
}
