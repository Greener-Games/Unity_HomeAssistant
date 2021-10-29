

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[EntityWorldGraphic("Media Marker")]
[EntityUiElement("Media Popup")]
public class MediaPlayerEntity : Entity
{
    const string AppNameKey = "app_name";
    public string AppName =>  currentStateObject.GetAttributeValue(AppNameKey, "");
    
    const string EntityPictureKey = "entity_picture";
    public string EntityPictureURL =>  currentStateObject.GetAttributeValue(EntityPictureKey, "");
    
    const string MediaArtistKey = "media_artist";                                              
    public string MediaArtist =>  currentStateObject.GetAttributeValue(MediaArtistKey, "");    
    
    const string MediaTitleKey = "media_title";                                           
    public string MediaTitle =>  currentStateObject.GetAttributeValue(MediaTitleKey, ""); 
    
    const string MediaAlbumKey = "media_album_name";                                               
    public string MediaAlbum =>  currentStateObject.GetAttributeValue(MediaAlbumKey, "");     
    
    const string MediaDurationKey = "media_duration";
    public double MediaDuration =>  currentStateObject.GetAttributeValue<double>(MediaDurationKey,0);
    
    const string MediaUpdatedPositionKey = "media_position";
    public double MediaUpdatedPosition =>  currentStateObject.GetAttributeValue<double>(MediaUpdatedPositionKey, 0);
    
    const string MediaUpdatePositionTimeKey = "media_position_updated_at";
    public DateTime MediaUpdatePositionTime =>  currentStateObject.GetAttributeValue(MediaUpdatePositionTimeKey, DateTime.Now);
    
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
        while (gameObject.activeSelf)
        {
            actualMediaPosition += Time.deltaTime;
            mediaPositionUpdated?.Invoke(actualMediaPosition);
            yield return new WaitForEndOfFrame();

            if (actualMediaPosition > (MediaDuration + 1))
            {
                FetchLiveData();
                StopCoroutine(mediaTimeTracker);
            }
        }
    }
}