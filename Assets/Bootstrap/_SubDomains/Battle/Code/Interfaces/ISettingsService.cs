using Bootstrap._SubDomains.Battle.Code.Settings;
using Cysharp.Threading.Tasks;

namespace Bootstrap._SubDomains.Battle.Code.Interfaces
{
    public interface ISettingsService
    {
        UniTask LoadScenes(SceneSettings settings);
        void UnloadAllScenes();
        void UnloadScene(SceneSettings.SceneAssetReference scene);
    }
}