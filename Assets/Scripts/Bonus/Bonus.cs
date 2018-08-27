﻿using System;
using UnityEngine;

[Flags]
public enum BonusEffect {
    None = 0,
    Star = 1 << 0,
    Magnet = 1 << 1,
    Bomb = 1 << 2,
    Boost = 1 << 3,
    Shield = 1 << 4,
    TimeDelay = 1 << 5
}

public abstract class Bonus : MonoBehaviour {
    public int reward = 0;
    public float effectTime = 5f;
    public BonusEffect type = BonusEffect.None;

    protected Color color = Color.white;

    static public BonusEffect Effects = BonusEffect.None;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerSphere")) {
            SelfBonusEffect();
            GameController.instance.EncreaseScore(reward, color);
            PlayerController.instance.BonusCollected(this);
            Destroy(gameObject);
        }
    }

    protected virtual void SelfBonusEffect() {
        AddEffect(type);
    }

    static public void AddEffect(BonusEffect effect) {
        Effects |= effect;
    }

    static public void RemoveEffect(BonusEffect effect) {
        Effects &= ~effect;
    }

    static public bool HasEffect(BonusEffect effect) {
        return (Effects & effect) != BonusEffect.None;
    }

    static public int BonusAccessibility(BonusEffect type) {
        switch (type) {
            case BonusEffect.Star: {
                return 0;
            }
            case BonusEffect.Magnet: {
                return 50;
            }
            case BonusEffect.Bomb: {
                return 150;
            }
            case BonusEffect.Boost: {
                return 250;
            }
            case BonusEffect.Shield: {
                return 350;
            }
            case BonusEffect.TimeDelay: {
                return 450;
            }
            default: return 0;
        }
    }
}
