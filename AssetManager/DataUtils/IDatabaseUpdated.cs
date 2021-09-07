using System;

namespace AssetManager.DataUtils
{
    public interface IDatabaseUpdated
    {
        event EventHandler UpdatedDatabase;
    }
}