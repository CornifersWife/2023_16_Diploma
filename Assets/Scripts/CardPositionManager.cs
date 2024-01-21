using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CardPositionManager : MonoBehaviour {
    public int spotsPerSide = 4;
    public Transform playerStart;
    public Transform playerEnd;

    private List<Vector3> _playerPositions;
    private List<Vector3> _opponentPositions;

    void Start() {
        GeneratePositions();
    }

    void GeneratePositions() {
        _playerPositions = GenerateSidePositions(playerStart.position, playerEnd.position);
        _opponentPositions = new List<Vector3>(_playerPositions.Count);

        foreach (var position in _playerPositions) {
            // Mirror the position along the z-axis
            Vector3 mirroredPosition = new Vector3(position.x, position.y, -position.z);
            _opponentPositions.Add(mirroredPosition);
        }
    }

    List<Vector3> GenerateSidePositions(Vector3 start, Vector3 end) {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < spotsPerSide; i++) {
            float t = (float)i / (spotsPerSide - 1);
            positions.Add(Vector3.Lerp(start, end, t));
        }

        return positions;
    }

    public Vector3 GetPlayerPosition(int index) {
        if (index >= 0 && index < _playerPositions.Count) {
            return _playerPositions[index];
        }

        return Vector3.zero;
    }
    public Vector3 GetOpponentPosition(int index) {
        if (index >= 0 && index < _opponentPositions.Count) {
            return _opponentPositions[index];
        }

        return Vector3.zero;
    }


    void OnDrawGizmos() {
        GeneratePositions(); // Call this here to update positions in the editor
        

        Gizmos.color = Color.red;
        foreach (var pos in _playerPositions) {
            Gizmos.DrawSphere(pos, 0.1f); // Draw small spheres at each card position
        }
        foreach (var pos in _opponentPositions) {
            Gizmos.DrawSphere(pos, 0.1f); // Draw small spheres at each card position
        }
    }
}


