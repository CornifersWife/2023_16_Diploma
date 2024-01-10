using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MinionCard", menuName = "Card/Minion")]
public class MinionCardData : BaseCardData {
    public int power;
    public int health;
    // Add other minion-specific properties
}
