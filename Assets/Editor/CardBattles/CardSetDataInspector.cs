using CardBattles.CardScripts.CardDatas;
using UnityEditor;
using UnityEngine;

namespace Editor.CardBattles {
    [CustomEditor(typeof(CardSetData))]
    public class CardSetDataInspector : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI(); 

            CardSetData cardSet = (CardSetData)target;

            if (GUILayout.Button("Add New Minion")) {
                CreateAndAddMinionCard(cardSet);
            }

            if (GUILayout.Button("Add New Spell")) {
                CreateAndAddSpellCard(cardSet);
            }

            EditorGUILayout.LabelField("Minion Cards", EditorStyles.boldLabel);
            for (int i = cardSet.cards.Count - 1; i >= 0; i--) {
                if (cardSet.cards[i] is MinionData) {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(cardSet.cards[i], typeof(CardData), false);

                    if (GUILayout.Button("Remove", GUILayout.Width(70))) {
                        RemoveCard(cardSet, i);
                    }

                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.LabelField("Spell Cards", EditorStyles.boldLabel);
            for (int i = cardSet.cards.Count - 1; i >= 0; i--) {
                if (cardSet.cards[i] is SpellData) {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(cardSet.cards[i], typeof(CardData), false);

                    if (GUILayout.Button("Remove", GUILayout.Width(70))) {
                        RemoveCard(cardSet, i);
                    }

                    GUILayout.EndHorizontal();
                }
            }

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
        }


        private void CreateAndAddMinionCard(CardSetData cardSet) {
            int cardCount = Random.Range(1000, 10000);
            string baseName = cardSet.name; // Use the CardSetData's name as the base
            string newCardName = $"{baseName}_Minion_{cardCount}";


            MinionData newCard = CreateNewCard<MinionData>(newCardName);
            newCard.cardName = newCardName;
            cardSet.cards.Add(newCard);
            AssetDatabase.AddObjectToAsset(newCard, cardSet);
            AssetDatabase.SaveAssets();
        }

        private void CreateAndAddSpellCard(CardSetData cardSet) { // Example method for another type of car{
            int cardCount = Random.Range(1000, 10000);
            string baseName = cardSet.name; // Use the CardSetData's name as the base
            string newCardName = $"{baseName}_Spell_{cardCount}";


            SpellData newCard = CreateNewCard<SpellData>(newCardName);
            newCard.cardName = newCardName;
            cardSet.cards.Add(newCard);
            AssetDatabase.AddObjectToAsset(newCard, cardSet);
            AssetDatabase.SaveAssets();
        }

        private T CreateNewCard<T>(string cardName) where T : CardData {
            T newCard = ScriptableObject.CreateInstance<T>();
            newCard.name = cardName;
            return newCard;
        }

        private void RemoveCard(CardSetData cardSet, int index) {
            CardData cardToRemove = cardSet.cards[index];
            cardSet.cards.RemoveAt(index);

            AssetDatabase.RemoveObjectFromAsset(cardToRemove);
            Object.DestroyImmediate(cardToRemove, true); // Destroy the object

            AssetDatabase.SaveAssets(); // Save the changes to the asset database
            AssetDatabase.Refresh(); // Refresh the Asset Database to show changes immediately
        }
    }
}