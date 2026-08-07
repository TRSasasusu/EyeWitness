using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EyeWitness {
    public class NotificationWithNewShipLog : MonoBehaviour {
        public string shipLogId;
        public SphereShape sphereShape;
        public string notificationText;

        void Start() {
            if(EyeWitness.HasShipLog(shipLogId)) {
                gameObject.SetActive(false);
                return;
            }

            var sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = sphereShape.radius;
        }

        void OnTriggerEnter(Collider other) {
            var root = other.transform.root;
            EyeWitness.Log($"NotificationWithNewShipLog: {other.gameObject.name}, root: {root.name}");
            if (root == Locator.GetPlayerTransform() || (root == Locator.GetShipTransform() && PlayerState.IsInsideShip())) {
                var data = new NotificationData(NotificationTarget.All, EyeWitness.Instance.NewHorizons.GetTranslationForUI(notificationText), 8f, true);
                NotificationManager.SharedInstance.PostNotification(data);
            }
        }

        void OnTriggerExit(Collider other) {
            EyeWitness.Log($"NotificationWithNewShipLog: {other.gameObject.name}");
            gameObject.SetActive(false);
        }
    }
}
