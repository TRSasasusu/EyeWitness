using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;

namespace EyeWitness {
    public class EyeWitness : ModBehaviour {
        public static EyeWitness Instance;
        public INewHorizons NewHorizons;

        ModifyObjects _modifyObjects;
        SkyIslandManager _skyIslandManager;
        EyeShrineDepthManager _eyeShrineDepthManager;
        PathToHighEnergyLabManager _pathToHighEnergyLabManager;

        public static void Log(string text, MessageType messageType = MessageType.Message) {
            Instance.ModHelper.Console.WriteLine(text, messageType);
        }

        public static bool HasShipLog(string id) {
            return PlayerData._currentGameSave.shipLogFactSaves.ContainsKey(id) && PlayerData._currentGameSave.shipLogFactSaves[id].revealOrder > -1;
        }

        public void Awake() {
            Instance = this;
            // You won't be able to access OWML's mod helper in Awake.
            // So you probably don't want to do anything here.
            // Use Start() instead.
        }

        public void Start() {
            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"{nameof(EyeWitness)} is loaded!", MessageType.Success);

            // Get the New Horizons API and load configs
            NewHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
            NewHorizons.LoadConfigs(this);

            new Harmony("orclecle.EyeWitness").PatchAll(Assembly.GetExecutingAssembly());

            // Example of accessing game code.
            //OnCompleteSceneLoad(OWScene.TitleScreen, OWScene.TitleScreen); // We start on title screen
            //LoadManager.OnCompleteSceneLoad += OnCompleteSceneLoad;
            NewHorizons.GetStarSystemLoadedEvent().AddListener(loadScene => {
                if (loadScene == "SolarSystem") {
                    _modifyObjects = new ModifyObjects();
                    _skyIslandManager = new SkyIslandManager();
                    _eyeShrineDepthManager = new EyeShrineDepthManager();
                    _pathToHighEnergyLabManager = new PathToHighEnergyLabManager();
                }
            });
        }

        //public void OnCompleteSceneLoad(OWScene previousScene, OWScene newScene) {
        //    if (newScene != OWScene.SolarSystem) return;
        //    ModHelper.Console.WriteLine("Loaded into solar system!", MessageType.Success);

        //    //_modifyObjects = new ModifyObjects();
        //}
    }

}
