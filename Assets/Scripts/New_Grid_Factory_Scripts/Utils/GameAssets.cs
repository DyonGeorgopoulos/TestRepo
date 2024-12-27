/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour {

    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    private void Awake() {
        _i = this;
    }



    [System.Serializable]
    public class PlacedObjectTypeSO_Refs {

        public PlacedObjectTypeSO conveyorBelt;
        public PlacedObjectTypeSO miningMachine;
        public PlacedObjectTypeSO smelter;
        public PlacedObjectTypeSO grabber;
        public PlacedObjectTypeSO assembler;
        public PlacedObjectTypeSO storage;

    }

    public PlacedObjectTypeSO_Refs placedObjectTypeSO_Refs;



    [System.Serializable]
    public class ItemSO_Refs {

        public ItemSO ironOre;
        public ItemSO goldOre;
        public ItemSO ironIngot;
        public ItemSO goldIngot;
        public ItemSO computer;
        public ItemSO copperOre;
        public ItemSO copperIngot;
        public ItemSO microchip;

        public ItemSO any;
        public ItemSO none;
    }


    public ItemSO_Refs itemSO_Refs;



    [System.Serializable]
    public class ItemRecipeSO_Refs {

        public ItemRecipeSO ironIngot;
        public ItemRecipeSO goldIngot;
        public ItemRecipeSO computer;
        public ItemRecipeSO microchip;
        public ItemRecipeSO copperIngot;
    }


    public ItemRecipeSO_Refs itemRecipeSO_Refs;


    public Transform pfWorldItem;
    public Transform pfBeltDebugVisualNode;
    public Transform pfBeltDebugVisualLine;

    public Transform fxBuildingPlaced;

    public Transform sndBuilding;





    public Sprite s_ShootFlash;

    public Transform pfSwordSlash;
    public Transform pfEnemy;
    public Transform pfEnemyFlyingBody;
    public Transform pfImpactEffect;
    public Transform pfDamagePopup;
    public Transform pfDashEffect;
    public Transform pfProjectileArrow;
    public Transform pfBolt;
    public Transform pfSmoke;
    public Transform pfChatBubble;

    public Material m_WeaponTracer;
    public Material m_MarineSpriteSheet;

    public Material m_DoorRed;
    public Material m_DoorGreen;
    public Material m_DoorBlue;

    public Material m_DoorKeyHoleRed;
    public Material m_DoorKeyHoleGreen;
    public Material m_DoorKeyHoleBlue;
}
