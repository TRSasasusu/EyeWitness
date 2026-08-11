using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness {
    public class CapsuleItem : OWItem {
        public static CapsuleItem PickedOne;

        public bool _hasLiquid;

        public override string GetDisplayName() {
            return EyeWitness.Instance.NewHorizons.GetTranslationForUI("Capsule");
        }

        public override void PickUpItem(Transform holdTranform) {
            base.PickUpItem(holdTranform);
            if(_hasLiquid) {
                if(!EyeWitness.HasShipLog("ew_gas_dwarf_storage_2")) {
                    Locator.GetShipLogManager().RevealFact("ew_gas_dwarf_storage_2");
                }
            }
            PickedOne = this;
        }
    }
}
