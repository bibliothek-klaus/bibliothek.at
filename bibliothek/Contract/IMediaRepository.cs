using bibliothek.Models;
using System.Collections.Generic;

namespace bibliothek.Contracts
{
    public interface IMediaRepository
    {
        Dictionary<string, string> GetMediaTypes();
        MediaStatus GetMediaStatus();
        List<MediaItem> GetMediaItems(string search);
        List<MediaItem> GetNewMediaItems();
        List<MediaItem> GetMediaItemsByMediaType(string mediaType);
        MediaItem GetMediaItem(int id);
        List<AvailableMediaItem> CheckIsbnsAvailable(List<string> isbns);
        List<MediaItem> GetPopularMediaItems();
    }
}