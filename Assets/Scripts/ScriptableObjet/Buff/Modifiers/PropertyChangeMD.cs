using UnityEngine;

[CreateAssetMenu(fileName = "new BuffModifier", menuName = "Game/BuffSys/BuffModifier/PropertyChange")]
public class SpeedChangeMD : BaseBuffModifier
{
    [SerializeField] float _speedUp;

    public override void Apply(BuffItem buffInfo)
    {
        var targetProperty = buffInfo.Target.GetComponent<Character>();

        targetProperty.AddVelocity(_speedUp);
    }
}