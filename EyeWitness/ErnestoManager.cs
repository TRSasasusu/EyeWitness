using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewHorizons.Utility;
using UnityEngine;

namespace EyeWitness {
    public class ErnestoManager {
        public static ErnestoManager Instance { get; private set; }

        public CharacterDialogueTree DialogueTree { get; private set; }
        public bool InConversation { get; private set; }
        GameObject _ernesto;

        public ErnestoManager() {
            Instance = this;

            _ernesto = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_NomaiCrater/Ernesto");
            if(_ernesto != null) {
                _ernesto.SetActive(false);
            }

            if (!PlayerData.GetPersistentCondition("EW_MET_MERMAID")) {
                return;
            }

            DialogueTree = _ernesto.GetComponentInChildren<CharacterDialogueTree>();
            if(DialogueTree != null) {
                DialogueTree.OnEndConversation += () => {
                    InConversation = false;
                };
                DialogueTree.OnAdvancePage += AdvancePage;
            }
        }

        public void StartConversation() {
            InConversation = true;
        }

        void AdvancePage(string nodeName, int pageNum) {

        }
    }
}
