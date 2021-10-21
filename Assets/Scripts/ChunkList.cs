using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ChunkList : ScriptableObject {

    public Chunk[] chunks;

    void OnValidate() {
        chunks = chunks
            .Where(c => !!c)
            .Distinct()
            .ToArray();    
    }
}
