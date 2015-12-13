using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Xcendant.Auth.Models.Entities;

namespace Xcendent.Auth.Providers
{
    public class FaceBookUserDetailsProvider : IUserDetailsProvider
    {
        public XcendentUser GetRegisteredUserInfo(ClaimsIdentity identity)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetUserInfo(Dictionary<string, string> properties)
        {
            string graphAddress = "https://graph.facebook.com/" + properties["user_id"] + "?fields=id,email&access_token=" + Uri.EscapeDataString(properties["access_token"]);
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage graphResponse = await httpClient.GetAsync(graphAddress);
            graphResponse.EnsureSuccessStatusCode();
            var text = await graphResponse.Content.ReadAsStringAsync();
            JObject user = JObject.Parse(text);

            string pictureDataAddress = "https://graph.facebook.com/" + properties["user_id"] + "/picture?width=500&height=500&redirect=false&access_token=" + Uri.EscapeDataString(properties["access_token"]);
            HttpResponseMessage pictureResponse = await httpClient.GetAsync(pictureDataAddress);
            pictureResponse.EnsureSuccessStatusCode();
            var picData = await pictureResponse.Content.ReadAsStringAsync();
            JObject pictureData = JObject.Parse(picData);
            user.Add("picture", pictureData.GetValue("data"));
            return user;
        }
    }
}