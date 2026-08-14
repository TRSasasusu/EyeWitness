using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness {
    public class MermaidNoiseMaker : NoiseMaker {
        bool _setPlayer;

        public override void Awake() {
            base.Awake();
            _noiseRadius = 1000;
        }

        void Start() {
            var sub = transform.Find("sub");
            if (sub != null) {
                sub.localScale = Vector3.one * 0.1f;
            }
        }

        void Update() {
            if(_setPlayer) {
                return;
            }

            var player = Locator.GetPlayerBody();
            if(player != null) {
                _attachedBody = player;
                _setPlayer = true;
            }
        }
    }
}
