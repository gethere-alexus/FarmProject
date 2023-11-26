using System;
using UnityEngine;

public class ParticleInitializator : MonoBehaviour
{
    [SerializeField] private GameObject _dirtParticlePrefab;
    [SerializeField] private GameObject _seedParticlePrefab;
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnDirtCultivatingStageCompleted>(ParticleHandler);
        GlobalEventBus.Sync.Subscribe<OnTilePlanted>(ParticleHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivatingStageCompleted>(ParticleHandler);
        GlobalEventBus.Sync.Unsubscribe<OnTilePlanted>(ParticleHandler);
    }

    private void ParticleHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnDirtCultivatingStageCompleted onDirtCultivatingStageCompleted)
        {
            Instantiate(_dirtParticlePrefab).transform.position = onDirtCultivatingStageCompleted.CultivatedTile.transform.position;
        }
        else if (eventArgs is OnTilePlanted onTilePlanted)
        {
            Instantiate(_seedParticlePrefab).transform.position = onTilePlanted.PlantedTile.transform.position;
        }
    }
}
