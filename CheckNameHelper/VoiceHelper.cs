using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace CheckNameHelper
{
    public class VoiceHelper
    {
        public Stream GetVoiceFile(string text)
        {
            HttpWebRequest req = null;
            MemoryStream fs = new MemoryStream();
            try
            {
                req = (HttpWebRequest)WebRequest.Create(
                    "http://translate.google.com.hk/translate_tts?ie=UTF-8&q=" + 
                    text + "&tl=zh-CN&total=1&idx=0&textlen="+text.Length.ToString());
                
              
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    Stream st = res.GetResponseStream();
                  
                    st.CopyTo(fs);
                    st.Close();
                    return fs;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                throw;
            }
            return null;

        }
       
    }
}
