using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFences : MonoBehaviour
{
    [SerializeField] private bool _breakable;
    [SerializeField] private int _durability = 12;

    //All the meshes
    [SerializeField] private Mesh _broken1;
    [SerializeField] private Mesh _broken2;
    [SerializeField] private Mesh _broken3;

    MeshFilter _mesh;

    void Start()
    {
        _mesh = GetComponent<MeshFilter>();
    }

    public void DecreaseDurability()
    {
        if (!_breakable)
            return;

        _durability--;

        switch (_durability)
        {
            case 9:
                _mesh.mesh = _broken1;
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Fence break", gameObject);
                break;
            case 6:
                _mesh.mesh = _broken2;
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Fence break", gameObject);
                break;
            case 3:
                _mesh.mesh = _broken3;
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Fence break", gameObject);
                break;
            case 0:
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Fence break", gameObject);
                Destroy(gameObject, 0f);
                break;
            default:
                break;
        }
    }
}
