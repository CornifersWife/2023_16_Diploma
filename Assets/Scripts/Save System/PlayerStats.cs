using System;

[Serializable]
public class PlayerStats {
    public float[] position;

    public PlayerStats() {
        position = new float[3];
    }
}