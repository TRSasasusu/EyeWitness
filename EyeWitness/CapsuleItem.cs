using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace EyeWitness {
    public class CapsuleItem : OWItem {
        public static CapsuleItem PickedOne;

        public bool _hasLiquid;

        Transform _liquid;
        bool _usingLiquid;

        public override void Awake() {
            base.Awake();
            _type = (ItemType)2048;
            _localDropOffset = new Vector3(0, 0.1f, 0);
        }

        void Start() {
            _liquid = transform.Find("capsule_gas/liquid");
        }

        public override string GetDisplayName() {
            return EyeWitness.Instance.NewHorizons.GetTranslationForUI("Capsule");
        }

        public override void PickUpItem(Transform holdTranform) {
            base.PickUpItem(holdTranform);
            if(_hasLiquid) {
                if(!EyeWitness.HasShipLog("ew_gas_dwarf_storage_b_2")) {
                    Locator.GetShipLogManager().RevealFact("ew_gas_dwarf_storage_b_2");
                }
            }
            PickedOne = this;
        }

        public void UseLiquid() {
            if(_usingLiquid) {
                return;
            }
            _usingLiquid = true;

            _liquid.DOScale(Vector3.one * 0.0001f, 10).OnComplete(() => {
                _liquid.gameObject.SetActive(false);
                _hasLiquid = false;
            }).SetLink(gameObject);
        }
    }
}
