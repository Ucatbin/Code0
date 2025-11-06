using ThisGame.Entity.HealthSystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] MoveData _data;
    public MoveModel Model;

    void Initialize()
    {
        Model = new MoveModel(_data);
    }
}
