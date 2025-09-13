using UnityEngine;

[CreateAssetMenu(fileName = "new BuffModel", menuName = "Game/BuffSys/BuffModel")]
public class PropertyChangeMD : BaseBuffModel
{
    [SerializeField] float _speedUp;
    [SerializeField] float _speedAccelUp;

    public override void Apply(BuffItem buffInfo)
    {
        var targetProperty = buffInfo.Target.GetComponent<EntityContoller_Main>().PropertySO;

        targetProperty.MaxGroundMoveSpeed += _speedUp;
        targetProperty.GroundAccel += _speedAccelUp;
    }
}