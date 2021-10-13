using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Lol : ScriptableObject
{
    public Object[] scenes;
    private string[] names;

    private void OnValidate() {
        names = scenes.Select(s => s.name).ToArray();
    }
}