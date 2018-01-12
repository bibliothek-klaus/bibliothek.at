using bibliothek.at.Models;
using System.Collections.Generic;

namespace bibliothek.at.Contracts
{
    public interface IMediaRepository
    {
        MediaStatus GetMediaStatus();
        List<MediaItem> GetMediaItems(string search);
        List<MediaItem> GetNewMediaItems();
        MediaItem GetMediaItem(int id);
        List<AvailableMediaItem> CheckIsbnsAvailable(List<string> isbns);
    }
}