using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeWitness {
    public class MarkerItem : OWItem {
        public override string GetDisplayName() {
            return EyeWitness.Instance.NewHorizons.GetTranslationForUI("OPCMarker");
        }
    }
}
