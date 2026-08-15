using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace EyeWitness {
    public class MermaidConversation : MonoBehaviour {
        GameObject _dialogue;
        bool _end;
        Transform _body;

        void Start() {
            _dialogue = transform.Find("dialogue").gameObject;
            _body = transform.Find("Body");
            if (_body != null) {
                _body.DOLocalMoveY(0.1f, 1.5f).SetLoops(-1, LoopType.Yoyo).SetLink(_body.gameObject);
            }
        }

        void Update() {
            if(_end) {
                return;
            }

            if(DialogueConditionManager.SharedInstance.GetConditionState("ReadMermaidFinal")) {
                _end = true;
                if(_dialogue != null) {
                    _dialogue.SetActive(false);
                }
                Locator.GetDeathManager().FinishedDLC();
                GameObject.Find("FlashbackCamera").transform.Find("Canvas_EchoesOver/EchoesOfTheEye").GetComponent<Text>().text = "EyeWitness";
                Locator.GetDeathManager().KillPlayer(DeathType.Meditation);
            }
        }
    }
}
