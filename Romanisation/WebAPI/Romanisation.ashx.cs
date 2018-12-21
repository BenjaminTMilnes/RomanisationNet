using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using KoreanRomanisation;

namespace Romanisation.WebAPI
{
    public class RomanisationRequest
    {
        public string RomanisationType { get; set; }
        public string Korean { get; set; }
    }

    public class Romanisation : IHttpHandler
    {
        private McCuneReischauerRomanisation mccuneReischauerRomanisation;
        private RevisedRomanisation revisedRomanisation;
        private SimplifiedRomanisation simplifiedRomanisation;
        private YaleRomanisation yaleRomanisation;

        public Romanisation()
        {
            mccuneReischauerRomanisation = new McCuneReischauerRomanisation();
            revisedRomanisation = new RevisedRomanisation();
            simplifiedRomanisation = new SimplifiedRomanisation();
            yaleRomanisation = new YaleRomanisation();
        }

        public void ProcessRequest(HttpContext context)
        {
            using (var sr = new System.IO.StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                var content = sr.ReadToEnd();

                var romanisationRequest = JsonConvert.DeserializeObject<RomanisationRequest>(content);

                try
                {
                    if (romanisationRequest.RomanisationType == "mccune-reischauer")
                    {
                        context.Response.Write(mccuneReischauerRomanisation.RomaniseText(romanisationRequest.Korean));
                    }
                    else if (romanisationRequest.RomanisationType == "revised")
                    {
                        context.Response.Write(revisedRomanisation.RomaniseText(romanisationRequest.Korean));
                    }
                    else if (romanisationRequest.RomanisationType == "simplified")
                    {
                        context.Response.Write(simplifiedRomanisation.RomaniseText(romanisationRequest.Korean));
                    }
                    else if (romanisationRequest.RomanisationType == "yale")
                    {
                        context.Response.Write(yaleRomanisation.RomaniseText(romanisationRequest.Korean));
                    }
                }
                catch { }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}