using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputReader _reader;
    void Start()
    {
        _reader.EnableGameplay(true);
    }

    void OnDisable()
    {
        _reader.EnableGameplay(false);
    }
}
