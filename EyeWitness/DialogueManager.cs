using NewHorizons.Builder.Props;
using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using static XGamingRuntime.SDK;

namespace EyeWitness {
    public class DialogueManager {
        static string _gabbroAdditionalDialogueXML;
        static string _chertAdditionalDialogueXML;

        GabbroDialogueSwapper _gabbroDialogueSwapper;
        ChertDialogueSwapper _chertDialogueSwapper;

        public DialogueManager() {
            if(_gabbroAdditionalDialogueXML == null) {
                _gabbroAdditionalDialogueXML = File.ReadAllText(Path.Combine(EyeWitness.Instance.ModHelper.Manifest.ModFolderPath, "planets/dialogues/gabbro.xml"));
            }
            if(_chertAdditionalDialogueXML == null) {
                _chertAdditionalDialogueXML = File.ReadAllText(Path.Combine(EyeWitness.Instance.ModHelper.Manifest.ModFolderPath, "planets/dialogues/chert.xml"));
            }

            var gabbroDialogueSwapperObj = SearchUtilities.Find("GabbroIsland_Body/Sector_GabbroIsland/Interactables_GabbroIsland/Traveller_HEA_Gabbro/ConversationZone_Gabbro");
            if(gabbroDialogueSwapperObj != null) {
                _gabbroDialogueSwapper = gabbroDialogueSwapperObj.GetComponent<GabbroDialogueSwapper>();

                foreach(var conditionalDialogue in _gabbroDialogueSwapper._conditionalDialogues) {
                    conditionalDialogue.dialogueTextAsset = AddToExistingTextAsset(conditionalDialogue.dialogueTextAsset, _gabbroAdditionalDialogueXML);
                }
            }

            var chertDialogueSwapperObj = SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin/Sector_NorthHemisphere/Sector_NorthSurface/Sector_Lakebed/Interactables_Lakebed/Traveller_HEA_Chert/ConversationZone_Chert");
            if(chertDialogueSwapperObj != null) {
                _chertDialogueSwapper = chertDialogueSwapperObj.GetComponent<ChertDialogueSwapper>();

                foreach(var conditionalDialogue in _chertDialogueSwapper._conditionalDialogues) {
                    conditionalDialogue.dialogueTextAsset = AddToExistingTextAsset(conditionalDialogue.dialogueTextAsset, _chertAdditionalDialogueXML);
                }
            }
        }

        // Copied from https://github.com/Outer-Wilds-New-Horizons/new-horizons/blob/5611a9ce01f881d8798de5c0342f99ba3cee3e47/NewHorizons/Builder/Props/DialogueBuilder.cs#L110-L163
        TextAsset AddToExistingTextAsset(TextAsset existingAsset, string xml) {
            var existingText = existingAsset.text;

            var existingDialogueDoc = new XmlDocument();
            existingDialogueDoc.LoadXml(existingText);
            var existingDialogueTree = existingDialogueDoc.DocumentElement.SelectSingleNode("//DialogueTree");

            var existingDialogueNodesByName = new Dictionary<string, XmlNode>();
            foreach (XmlNode existingDialogueNode in existingDialogueTree.GetChildNodes("DialogueNode")) {
                var name = existingDialogueNode.GetChildNode("Name").InnerText;
                existingDialogueNodesByName[name] = existingDialogueNode;
            }

            var additionalDialogueDoc = new XmlDocument();
            additionalDialogueDoc.LoadXml(xml);
            var newDialogueNodes = additionalDialogueDoc.DocumentElement.SelectSingleNode("//DialogueTree").GetChildNodes("DialogueNode");

            foreach (XmlNode newDialogueNode in newDialogueNodes) {
                var name = newDialogueNode.GetChildNode("Name").InnerText;

                if (existingDialogueNodesByName.TryGetValue(name, out var existingNode)) {
                    // We just have to merge the dialogue options
                    var dialogueOptions = newDialogueNode.GetChildNode("DialogueOptionsList").GetChildNodes("DialogueOption");
                    var existingDialogueOptionsList = existingNode.GetChildNode("DialogueOptionsList");
                    if (existingDialogueOptionsList == null) {
                        existingDialogueOptionsList = existingDialogueDoc.CreateElement("DialogueOptionsList");
                        existingNode.AppendChild(existingDialogueOptionsList);
                    }
                    foreach (XmlNode node in dialogueOptions) {
                        var importedNode = existingDialogueOptionsList.OwnerDocument.ImportNode(node, true);
                        // We add them to the start because normally the last option is to return to menu or exit
                        existingDialogueOptionsList.PrependChild(importedNode);
                    }
                }
                else {
                    // We add the new dialogue node to the existing dialogue
                    var importedNode = existingDialogueTree.OwnerDocument.ImportNode(newDialogueNode, true);
                    existingDialogueTree.AppendChild(importedNode);
                }
            }

            // Character name is required for adding translations, something to do with how OW prefixes its dialogue
            var characterName = existingDialogueTree.SelectSingleNode("NameField").InnerText;
            DialogueBuilder.AddTranslation(additionalDialogueDoc.GetChildNode("DialogueTree"), characterName);

            var newTextAsset = new TextAsset(existingDialogueDoc.OuterXml) {
                name = existingAsset.name
            };

            return newTextAsset;
        }
    }
}
