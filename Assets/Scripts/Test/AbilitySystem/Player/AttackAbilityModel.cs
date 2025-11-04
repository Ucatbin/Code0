using ThisGame.AbilitySystem;
using ThisGame.EntitySystem;

public class AttackAbilityModel : AbilityModel<AttackAbilityData>
{
    public override void Excute(IEntityModel entity)
    {
        if (!CanExecute(entity)) return;
        base.Excute(entity);
    }
    public override void EndExecute(IEntityModel entity)
    {
        base.EndExecute(entity);
    }
}
