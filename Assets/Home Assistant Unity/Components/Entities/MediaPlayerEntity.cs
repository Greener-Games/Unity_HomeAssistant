#region

using System;
using System.Collections;
using Requests;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

#endregion

[EntityWorldGraphic("Media Marker")]
[EntityUiElement("Media Popup")]
public class MediaPlayerEntity : Entity
{
    const string AppNameKey = "app_name";
    const string EntityPictureKey = "entity_picture";
    const string MediaArtistKey = "media_artist";
    const string MediaTitleKey = "media_title";
    const string MediaAlbumKey = "media_album_name";
    const string MediaDurationKey = "media_duration";
    const string MediaUpdatedPositionKey = "media_position";
    const string MediaUpdatePositionTimeKey = "media_position_updated_at";

    public string AppName => currentStateObject.GetAttributeValue(AppNameKey, "");
    public string EntityPictureURL => currentStateObject.GetAttributeValue(EntityPictureKey, "");
    public string MediaArtist => currentStateObject.GetAttributeValue(MediaArtistKey, "");
    public string MediaTitle => currentStateObject.GetAttributeValue(MediaTitleKey, "");
    public string MediaAlbum => currentStateObject.GetAttributeValue(MediaAlbumKey, "");
    public double MediaDuration => currentStateObject.GetAttributeValue<double>(MediaDurationKey);
    public double MediaUpdatedPosition => currentStateObject.GetAttributeValue<double>(MediaUpdatedPositionKey);
    public DateTime MediaUpdatePositionTime => currentStateObject.GetAttributeValue(MediaUpdatePositionTimeKey, DateTime.Now);

    public bool isPlaying => State == "playing";
    //Used to track the position of the current media for when its needed.
    Coroutine mediaTimeTracker;
    float actualMediaPosition;
    public UnityAction<float> mediaPositionUpdated;

    protected override void ProcessData()
    {
        base.ProcessData();

        float distance = (float)(DateTime.Now - MediaUpdatePositionTime).TotalSeconds;
        actualMediaPosition = distance + (float)MediaUpdatedPosition;

        if (mediaTimeTracker != null)
        {
            StopCoroutine(mediaTimeTracker);
        }

        mediaTimeTracker = StartCoroutine(UpdateMediaPosition());
    }

    IEnumerator UpdateMediaPosition()
    {
        while (gameObject.activeSelf && isPlaying)
        {
            actualMediaPosition += Time.deltaTime;
            mediaPositionUpdated?.Invoke(actualMediaPosition);
            yield return new WaitForEndOfFrame();

            if (actualMediaPosition > MediaDuration + 1)
            {
                FetchLiveData();
                StopCoroutine(mediaTimeTracker);
            }
        }
    }

    [Button]
    public async void Play()
    {
        await EntityRequest(ServiceClient.CallService("media_player","media_play",new { entity_id = this.entityId}), 1.5f);
    }
    
    [Button]
    public async void Pause()
    {
        await EntityRequest(ServiceClient.CallService("media_player","media_pause",new { entity_id = this.entityId}));
    }
    
    public void TogglePlayPause()
    {
        if (isPlaying)
        {
            Pause();
        }
        else
        {
            Play();
        }
    }
    
        [Button]
        public async void Next()
        {
            await EntityRequest(ServiceClient.CallService("media_player","media_next_track",new { entity_id = this.entityId}), 1.5f);
        }
        
        [Button]
        public async void Previous()
        {
            await EntityRequest(ServiceClient.CallService("media_player","media_previous_track",new { entity_id = this.entityId}),1.5f);
        }                                                                                                                                         
}