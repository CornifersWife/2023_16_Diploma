using ScriptableObjects.Dialogue;

namespace Interaction {
    public interface ITalkable {
        public void Talk(DialogueText dialogueText);
    }
}