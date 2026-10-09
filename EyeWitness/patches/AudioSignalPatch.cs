using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class AudioSignalPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(AudioSignal), nameof(AudioSignal.IdentifySignal))]
        public static void AudioSignal_IdentifySignal_Postfix(AudioSignal __instance) {
            if (__instance._name == SignalName.Traveler_Gabbro) {
                if (SkyIslandManager.Instance != null && SkyIslandManager.Instance.BaseAudioSignalDetectionTrigger != null) {
                    Observable.NextFrame().Subscribe(_ => {
                        EyeWitness.Log("AudioSignal_IdentifySignal_Postfix: Resetting _isDetecting to false for BaseAudioSignalDetectionTrigger");
                        SkyIslandManager.Instance.BaseAudioSignalDetectionTrigger._isDetecting = false; // this recalls unidentified signal nearby notification after newly acquiring gabbro signal
                    }).AddTo(__instance);
                }
            }
        }
    }
}
