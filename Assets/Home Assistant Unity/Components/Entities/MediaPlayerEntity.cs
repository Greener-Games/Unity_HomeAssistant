public class MediaPlayerEntity : Entity
{
    const string AppNameKey = "app_name";
    public string AppName =>  currentStateObject.GetAttributeValue(AppNameKey, "");
    
    const string EntityPictureKey = "entity_picture";
    public string EntityPicture =>  currentStateObject.GetAttributeValue(EntityPictureKey, "");
    
    const string MediaArtistKey = "meddia_artist";                                              
    public string MediaArtist =>  currentStateObject.GetAttributeValue(MediaArtistKey, "");    
    
    const string MediaTitleKey = "media_title";                                           
    public string MediaTitle =>  currentStateObject.GetAttributeValue(MediaTitleKey, ""); 
    
    const string MediaDurationKey = "media_duration";
    public double MediaDuration =>  currentStateObject.GetAttributeValue(MediaDurationKey,0);
    
    const string MediaPositionKey = "media_position";
    public double MediaPosition =>  currentStateObject.GetAttributeValue(MediaPositionKey, 0);
}