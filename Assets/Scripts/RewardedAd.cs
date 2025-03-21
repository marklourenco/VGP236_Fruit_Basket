using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Unity.VisualScripting;

public class RewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField]
    private Button showAdButton = null;
    [SerializeField]
    private string androidAdId = ""; // going to be registered in the Unity dashboard
    
    void Start()
    {
        showAdButton.interactable = false;
    }

    private void OnEnable()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log($"Loading Ad: {androidAdId}");
        Advertisement.Load(androidAdId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad Loaded: {adUnitId}");
        if (!GameManager.Instance.GetRewarded())
        {
            if (adUnitId.Equals(androidAdId))
            {
                showAdButton.interactable = true;
                showAdButton.onClick.RemoveAllListeners();
                showAdButton.onClick.AddListener(ShowAd);
            }
        }
    }

    public void ShowAd()
    {
        showAdButton.interactable = false;
        Advertisement.Show(androidAdId, this);
    }

    public void OnUnityAdsShowComplete(string adUnityId, UnityAdsShowCompletionState showCompletionState)
    {
        if(showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Complete");
            // if you are giving a reward, revive, restart, doubling points...
            // it is done here
            GameManager.Instance.Continue();
            Advertisement.Load(androidAdId, this);
            GameManager.Instance.FlipRewarded();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnityId, UnityAdsLoadError error, string message) // UnityAdsShowError error, string message
    {
        Debug.Log($"Error loading Unity Ad {adUnityId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnityId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Unity Ad {adUnityId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnityId)
    {
        // Pause Game
        // Undo in show complete
        GameManager.Instance.PauseGame();
    }

    public void OnUnityAdsShowClick(string adUnityId)
    {
        // if ad is clicked, bonus reward
    }
}
