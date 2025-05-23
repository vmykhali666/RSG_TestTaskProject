﻿using Content.Features.AIModule.Scripts.Entity;

namespace Content.Features.StorageModule.Scripts
{
    public interface IStorageFactory
    {
        public IStorage CreateStorage(StorageSettings storageSettings);
    }
}