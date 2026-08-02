using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewHorizons.Utility;
using UnityEngine;

namespace EyeWitness {
    public class ModifyObjects {
        public static ModifyObjects Instance;

        public GameObject ProbeTH { get; private set; }

        public ModifyObjects() {
            EyeWitness.Log("ModifyObjects constructor called");
            Instance = this;

            ProbeTH = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Interactables_Village/LaunchTower/Effects_HEA_Campfire/Probe_TH");
            ProbeTH.SetActive(false);
        }
    }
}
