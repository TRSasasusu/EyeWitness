using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeWitness {
    public class MarkerItem : OWItem {
        public override void Awake() {
            base.Awake();
            _type = (ItemType)2048;
        }

        public override string GetDisplayName() {
            return EyeWitness.Instance.NewHorizons.GetTranslationForUI("OPCMarker");
        }
    }
}
