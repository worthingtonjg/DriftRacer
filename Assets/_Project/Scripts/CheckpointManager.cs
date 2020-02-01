using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<GameObject> Children;

    public static List<GameObject> Checkpoints;
    // Start is called before the first frame update
    void Start()
    {
        Checkpoints = new List<GameObject>();
        Checkpoints.AddRange(Children);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
