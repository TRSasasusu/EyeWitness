using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace EyeWitness.patches {
    [HarmonyPatch]
    public static class WarpTransmitterPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(NomaiWarpTransmitter), nameof(NomaiWarpTransmitter.ReceiveWarpedBody))]
        public static void NomaiWarpTransmitter_ReceiveWarpedBody_Postfix(NomaiWarpTransmitter __instance) {
            if(__instance.name == "warpTransmitter_db_datura") {
                EyeWitness.Log("Patch ReceiveWarpedBody");
                __instance._objectsOnPlatform.Clear(); // avoid continuous warp. specifically, in DB, continuous warp causes DB entrance warp in some reason.
                Observable.NextFrame().Subscribe(_ => {
                    Observable.NextFrame().Subscribe(_ => {
                        __instance._objectsOnPlatform.Clear();
                    }).AddTo(__instance);
                    Observable.TimerFrame(2, FrameCountType.FixedUpdate).Subscribe(_ => {
                        __instance._objectsOnPlatform.Clear();
                    }).AddTo(__instance);
                }).AddTo(__instance);
            }
        }
    }
}
