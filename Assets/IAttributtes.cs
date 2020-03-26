using UnityEngine;

public interface IAttributtes
{
     MainAttribute vitality{ get; set; }
     MainAttribute strength{ get; set; }
     MainAttribute agility{ get; set; }
     MainAttribute intelligence{ get; set; }

     CommonAttribute physicalAttack{ get; set; }
     CommonAttribute magicPower{ get; set; }

     CommonAttribute health{ get; set; }
     CommonAttribute mana{ get; set; }

     CommonAttribute physicalDefense{ get; set; }
     CommonAttribute magicalDefense{ get; set; }

     CommonAttribute criticalChance{ get; set; }
     CommonAttribute criticalDamage{ get; set; }

     CommonAttribute evasion{ get; set; }
     CommonAttribute accuracy{ get; set; }
     CommonAttribute movementSpeed{ get; set; }

     CommonAttribute healthRegen{ get; set; }
     CommonAttribute manaRegen{ get; set; }

     CommonAttribute fieldOfViewAngle{ get; set; }
     CommonAttribute fieldOfViewRangeLight{ get; set; }
     CommonAttribute fieldOfViewRangeDark{ get; set; }
     CommonAttribute turnRate{ get; set; }

     TimeAttribute channelingTimeReduction{ get; set; }
     TimeAttribute castingTimeReduction{ get; set; }
     TimeAttribute cooldownTimeReduction{ get; set; }

     AttackSpeedAttribute attackSpeed{ get; set; }
}

