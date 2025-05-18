// using UnityEngine;

// public class DogAnimator : MonoBehaviour
// {
//     [SerializeField] private Transcript transcript;
//     [SerializeField] private Animation dogAnimation;
//     [SerializeField] private string keyword = "hello";

//     void Start()
//     {
//         if (transcript == null || dogAnimation == null)
//         {
//             Debug.LogWarning("hours in silence");
//             enabled = false;
//             return;
//         }

//         transcript.OnNewPlayerResponse += HandlePlayerMessage;
//     }

//     void OnDestroy()
//     {
//         if (transcript != null)
//          if (message.ToLower().Contains(keyword.ToLower()))
//         {
//             dogAnimation.Play();  
//             Debug.Log("Dog shake animation playing");
//         }
//     }
// }      transcript.OnNewPlayerResponse -= HandlePlayerMessage;
//     }

//     private void HandlePlayerMessage(string message)
//     {
//         if (string.IsNullOrWhiteSpace(message))
//             return;

//      
using UnityEngine;



public class DogAnimator : MonoBehaviour
{
    [SerializeField] private Transcript       transcript;
    [SerializeField] private Animation        dogAnimation;
    [SerializeField] private JetsIntegrated   jets;
    [SerializeField] private string[]         keywords  = { "sit", "roll", "hello", "stand", "act" };
    [SerializeField] private AnimationClip[]  clips;

    private string queuedClipName;
    private bool   queued = false;

    void Start()
    {
        if (dogAnimation == null)
        {
            Debug.LogWarning("DogAnimator requires an Animation component. Disabling.");
            enabled = false;
            return;
        }
        if (keywords.Length != clips.Length)
            Debug.LogError($"Keyword count ({keywords.Length}) != clip count ({clips.Length})");
        for (int i = 0; i < clips.Length; i++)
        {
            var clip = clips[i];
            if (clip != null && dogAnimation.GetClip(clip.name) == null)
                dogAnimation.AddClip(clip, clip.name);
        }

        if (transcript != null && jets != null)
        {
            transcript.OnNewPlayerResponse += QueueIfKeyword;
            jets.OnBotAudioStarted   += PlayQueuedAnimation;
        }
    }

    public void Configure(Transcript t, JetsIntegrated j)
    {
        if (transcript != null)
            transcript.OnNewPlayerResponse -= QueueIfKeyword;
        if (jets != null)
            jets.OnBotAudioStarted   -= PlayQueuedAnimation;

        transcript = t;
        jets = j;

        if (transcript != null && jets != null)
        {
            transcript.OnNewPlayerResponse += QueueIfKeyword;
            jets.OnBotAudioStarted       += PlayQueuedAnimation;
        }
    }

    void OnDestroy()
    {
        if (transcript != null)
            transcript.OnNewPlayerResponse -= QueueIfKeyword;
        if (jets != null)
            jets.OnBotAudioStarted   -= PlayQueuedAnimation;
    }

    private void QueueIfKeyword(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        var msg = message.ToLower();
        for (int i = 0; i < keywords.Length; i++)
        {
            if (!string.IsNullOrEmpty(keywords[i]) 
             && clips[i] != null 
             && msg.Contains(keywords[i].ToLower()))
            {
                queuedClipName = clips[i].name;
                queued = true;
                Debug.Log($"Queued '{queuedClipName}' for keyword '{keywords[i]}'");
                return;
            }
        }
    }

    private void PlayQueuedAnimation()
    {
        if (!queued) return;
        dogAnimation.Play(queuedClipName);
        Debug.Log($"Playing dog animation: {queuedClipName}");
        queued = false;
    }
}



