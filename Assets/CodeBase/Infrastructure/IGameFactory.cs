using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 position);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
    }
}