using AIVisionExplorer.ComputerVision;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace AIVisionExplorer.Helpers
{
    public static class ComputerVisionHelper
    {
        public async static Task<ImageAnalysisResult> AnalyzeImageAsync(byte[] bytes)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.ComputerVisionApiSubscriptionKey);

            var payload = new HttpBufferContent(bytes.AsBuffer());
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");

            string visualFeatures = "Color,ImageType,Tags,Categories,Description,Adult";

            var results = await client.PostAsync(new Uri($"{Common.CoreConstants.CognitiveServicesBaseUrl}/vision/v1.0/analyze?visualFeatures={visualFeatures}"), payload);

            ImageAnalysisResult result = null;

            try
            {
                var analysisResults = await results.Content.ReadAsStringAsync();

                var imageAnalysisResult = JsonConvert.DeserializeObject<ImageAnalysisInfo>(analysisResults);

                result = new ImageAnalysisResult()
                {
                    id = imageAnalysisResult.requestId,
                    details = imageAnalysisResult,
                    caption = imageAnalysisResult.description.captions.FirstOrDefault().text,
                    tags = imageAnalysisResult.description.tags.ToList(),
                };

            }
            catch (Exception ex) { }
            return result;
        }
    }
}