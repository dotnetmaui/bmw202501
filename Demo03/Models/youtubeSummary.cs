using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo03.Models
{
    [Table("youtubeSummary")]
    public class YoutubeSummary : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("created_at")]
        public DateTime CreateAt { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("data")]
        public DataModel Data { get; set; }

        [Column("origintext")]
        public string OriginText { get; set; }

    }

    public class DataModel
    {
        [Column("main_topic")]
        public string MainTopic { get; set; }         // main_topic
        [Column("core_Message")]
        public string CoreMessage { get; set; }      // core_Message
        [Column("video_Type")]
        public string VideoType { get; set; }        // video_Type
        [Column("keywords")]
        public string Keywords { get; set; }         // keywords
    }
}
