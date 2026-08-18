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
        Transform _sub;

        public override void Awake() {
            base.Awake();
            _noiseRadius = 1000;
        }

        void Start() {
            _sub = transform.Find("sub");
            if (_sub != null) {
                _sub.localScale = Vector3.one * 0.1f;
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

                transform.localScale = Vector3.one * 0.1f;
                if (_sub != null) {
                    _sub.localScale = Vector3.one * 0.1f;
                }
            }
            else {
                _attachedBody = _defaultAttachedBody;

                transform.localScale = Vector3.one * 0.5f;
                if (_sub != null) {
                    _sub.localScale = Vector3.one * 0.5f;
                }
            }
        }
    }
}
