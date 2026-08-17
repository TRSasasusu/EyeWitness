using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness {
    public class MermaidNoiseMaker : NoiseMaker {
        //bool _setPlayer;
        OWRigidbody _defaultAttachedBody;

        public override void Awake() {
            base.Awake();
            _noiseRadius = 1000;
        }

        void Start() {
            var sub = transform.Find("sub");
            if (sub != null) {
                sub.localScale = Vector3.one * 0.1f;
            }

            _defaultAttachedBody = _attachedBody;
        }

        void Update() {
            //if(_setPlayer) {
            //    return;
            //}

            //var player = Locator.GetPlayerBody();
            //if(player != null) {
            //    _attachedBody = player;
            //    _setPlayer = true;
            //}

            if(transform.root.name == "Player_Body" || transform.root.name == "Ship_Body") {
                _attachedBody = Locator.GetPlayerBody();
            }
            else {
                _attachedBody = _defaultAttachedBody;
            }
        }
    }
}
