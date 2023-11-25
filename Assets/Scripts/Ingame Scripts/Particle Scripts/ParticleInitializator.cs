using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleInitializator : MonoBehaviour
{
    [SerializeField] private GameObject _dirtParticlePrefab;
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnDirtCultivatingStageCompleted>(ParticleHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivatingStageCompleted>(ParticleHandler);
    }

    private void ParticleHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnDirtCultivatingStageCompleted onDirtCultivatingStageCompleted)
        {
            Instantiate(_dirtParticlePrefab).transform.position = onDirtCultivatingStageCompleted.CultivatedTile.transform.position;
        }
    }
}
