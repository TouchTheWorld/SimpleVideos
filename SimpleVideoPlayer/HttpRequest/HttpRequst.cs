using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVideoPlayer.HttpRequest
{
    public class HttpRequst
    {
        public static async Task<string> MainPageRequest(int page ,string sign)
        {
            string result = null;
            string api = "https://route.showapi.com/255-1?type=41&showapi_appid=38540&showapi_sign=" + sign + "&page=" +page.ToString();
            HttpClient httpclient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await httpclient.GetAsync(api);
            result = await response.Content.ReadAsStringAsync();
            int a1 = result.LastIndexOf("\"showapi_res_code\":0");

            if(a1 == -1)
            {
                int a = result.IndexOf("正确的签名是");
                int b = result.IndexOf("\"", a);
                string s2 = result.Substring(a + 6, b - a - 6);
                await Task.Factory.StartNew(async () => { result = await MainPageRequest(page, s2); });
            }
            while (a1 == -1 && result.LastIndexOf("\"showapi_res_code\":0") == -1) //先暂时用这方法...等下试试延时
            {
            }
            return result;
        }
    }
}
