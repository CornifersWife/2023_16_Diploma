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

        /*if (GUILayout.Button("Add New Spell Card")) { // Example for another type of card
            CreateAndAddSpellCard(cardSet);
        }*/

        // Ensure the changes made by the buttons are saved
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }

    private void CreateAndAddMinionCard(CardSetData cardSet) {
        string folderPath = AssetDatabase.GetAssetPath(cardSet).Replace(cardSet.name + ".asset", "") + cardSet.name +
                            "/Cards";

        if (!AssetDatabase.IsValidFolder(folderPath)) {
            AssetDatabase.CreateFolder(AssetDatabase.GetAssetPath(cardSet).Replace("/" + cardSet.name + ".asset", ""),
                cardSet.name + "/Cards");
        }

        MinionCardData newCard = CreateNewCard<MinionCardData>("New Minion Card");
        cardSet.cards.Add(newCard);
        AssetDatabase.AddObjectToAsset(newCard, cardSet);
        AssetDatabase.SaveAssets();
    }

    private void CreateAndAddSpellCard(CardSetData cardSet) { // Example method for another type of car{
        // Similar to CreateAndAddMinionCard, but for a different card type
    }

    private T CreateNewCard<T>(string name) where T : BaseCardData {
        T newCard = ScriptableObject.CreateInstance<T>();
        newCard.name = name; // Set the name of the new card
        return newCard;
    }
}