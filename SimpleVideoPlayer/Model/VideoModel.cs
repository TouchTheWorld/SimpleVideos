using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVideoPlayer.Model
{
    public class VideoModel
    {
        public int showapi_res_code { get; set; }
        public string showapi_res_error { get; set; }
        public Showapi_res_body showapi_res_body { get; set; }

        public class Showapi_res_body
        {
            public int ret_code { get; set; }
            public Pagebean pagebean { get; set; }
        }

        public class Pagebean
        {
            public int allPages { get; set; }
            public ObservableCollection<Contentlist> contentlist { get; set; }
            public int currentPage { get; set; }
            public int allNum { get; set; }
            public int maxResult { get; set; }
        }

        public class Contentlist
        {

            public string text { get; set; }
            public string hate { get; set; }
            public string videotime { get; set; }
            public string voicetime { get; set; }
            public string weixin_url { get; set; }
            public string profile_image { get; set; }
            public string width { get; set; }
            public string voiceuri { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string love { get; set; }
            public string height { get; set; }
            public string video_uri { get; set; }
            public string voicelength { get; set; }
            public string name { get; set; }
            public DateTime create_time { get; set; }
        }

    }
 }
