using System.Collections.Generic;
using ScriptableObjects.Dialogue;
using UnityEngine;

namespace SaveSystem.SaveData {
    [System.Serializable]
    public class SaveData {
        //Player data
        public struct PlayerData {
            public Transform playerTransform;
        }
        public PlayerData playerData;

        //NPC data
        public struct NPCData {
            public int id;
            public List<DialogueText> dialogueTexts;
        }
        public List<NPCData> npcDatas;

        //Enemy data
        public struct EnemyData {
            public int id;
            public EnemyState state;
        }
        public List<EnemyData> enemyDatas;

        //Inventory Data
        //TODO inventory data
        public struct ItemData {
            public int index;
            public string name;
            public string image;
        }
    
        public struct CardSetItemData {
            public int index;
            public string name;
            public string image;
            public string cardSetData;
        }
        
        public List<ItemData> itemDatas;
        public List<CardSetItemData> deckDatas;
        public List<CardSetItemData> cardSetDatas;

        //Settings data
        public struct SettingsData {
            public float musicVolume;
            public float SFXVolume;
            public int resolutionWidth;
            public int resolutionHeight;
            public int resolutionIndex;
            public bool fullscreen;
            public bool mouseOn;
            public bool keyboardOn;
        }
        public SettingsData settingsData;

        //World data
        //TODO world data
        public struct CollectibleItemData {
            public int id;
            public bool isCollected;
        }
        
        public struct FogData {
            public int id;
            public bool isActive;
        }

        public List<CollectibleItemData> CollectibleItemDatas;
        public List<FogData> fogDatas;

    }
}