namespace clsLBKLibrary
{
    public class Video
    {
        public string Description { get; set; }
        public string FileName { get; set; }
        public string Link { get; set; }
        public int SortOrder { get; set; }
        public string ThumbnailLink { get; set; }
    }
}

    //[Description] [nvarchar](255) NULL,
    //[FileName] [nvarchar](255) NOT NULL,
    //[Link] [nvarchar](255) NOT NULL,
    //[SortOrder] [int] NOT NULL,
    //[ThumbnailLink] [nvarchar](255) NULL,