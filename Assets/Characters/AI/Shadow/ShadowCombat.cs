using UnityEngine;

public class ShadowCombat : CharacterCombat
{
    public GameObject BulletPrefab;
    
    public override void Die()
    {
        Game.Current.GameEvents.LevelCompleted();
        //TODO: Particle Effect
        //TODO: Animation
        //TODO: End game event + listener on player to add shadow
    }

    private void AnimateDeath()
    {
        
    }
    
    //TODO: 3 attack sequences on repeat
}