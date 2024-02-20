using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable // 데미지를 받을 수 있는 경우
{
    public void TakeDamage(int damage);
}
