using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool spawnUsed = false;

    public bool IsUsed() {  return spawnUsed; }
    public void SetSpawnUsed(bool used) { spawnUsed = used; }
}
