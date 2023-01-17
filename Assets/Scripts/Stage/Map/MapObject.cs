using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class MapObject : MonoBehaviour
    {
        public SpawnPoint SpawnPoint => _spawnPoint ??= FindSpawnPoint();
        [SerializeField] SpawnPoint _spawnPoint;

        public MapObject Create()
        {
            return GameObject.Instantiate(this);
        }

        private SpawnPoint FindSpawnPoint()
        {
            return GameObject.FindObjectOfType<SpawnPoint>();
        }
    }
}