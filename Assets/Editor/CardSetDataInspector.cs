using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardSetData))]
public class CardSetDataInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI(); // Draws the default inspector

        CardSetData cardSet = (CardSetData)target;

        if (GUILayout.Button("Add New Minion Card")) {
            CreateAndAddMinionCard(cardSet);
        }

        for (int i = cardSet.cards.Count - 1; i >= 0; i--) {
            GUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField("Card", cardSet.cards[i], typeof(BaseCardData), false);

            if (GUILayout.Button("Remove", GUILayout.Width(100))) {
                RemoveCard(cardSet, i);
            }

            GUILayout.EndHorizontal();
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }

    private void CreateAndAddMinionCard(CardSetData cardSet) {
        int cardCount = cardSet.cards.Count;
        string baseName = cardSet.name; // Use the CardSetData's name as the base
        string newCardName = $"{baseName}_Card_{cardCount}";


        MinionCardData newCard = CreateNewCard<MinionCardData>(newCardName);
        newCard.cardName = newCardName;
        cardSet.cards.Add(newCard);
        AssetDatabase.AddObjectToAsset(newCard, cardSet);
        AssetDatabase.SaveAssets();
    }

    private void CreateAndAddSpellCard(CardSetData cardSet) { // Example method for another type of car{
        // Similar to CreateAndAddMinionCard, but for a different card type
    }

    private T CreateNewCard<T>(string name) where T : BaseCardData {
        T newCard = ScriptableObject.CreateInstance<T>();
        newCard.name = name;
        return newCard;
    }

    private void RemoveCard(CardSetData cardSet, int index) {
        BaseCardData cardToRemove = cardSet.cards[index];
        cardSet.cards.RemoveAt(index); // Remove from the list

        AssetDatabase.RemoveObjectFromAsset(cardToRemove);
        Object.DestroyImmediate(cardToRemove, true); // Destroy the object

        AssetDatabase.SaveAssets(); // Save the changes to the asset database
        AssetDatabase.Refresh(); // Refresh the Asset Database to show changes immediately
    }
}