// Decompiled with JetBrains decompiler
// Type: MobileTec.FaceAPI.PoC.FaceClient.FaceServiceClient
// Assembly: MobileTec.FaceAPI.PoC.FaceClient, Version=1.2.1.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: E1C430B0-5B4F-40C0-B7F6-ACC82F4C9CFC
// Assembly location: C:\Users\jangu\Downloads\FaceAPI_MVC\packages\MobileTec.FaceAPI.PoC.FaceClient.1.2.1.2\lib\portable-net45+wp80+win8+wpa81+aspnetcore50\MobileTec.FaceAPI.PoC.FaceClient.dll

using MobileTec.FaceAPI.PoC.FaceClient.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MobileTec.FaceAPI.PoC.FaceClient
{
  public class FaceServiceClient : IDisposable, IFaceServiceClient
  {
    private const string DEFAULT_API_ROOT = "https://api.projectoxford.ai/face/v1.0";
    private const string JsonContentTypeHeader = "application/json";
    private const string StreamContentTypeHeader = "application/octet-stream";
    private const string SubscriptionKeyName = "ocp-apim-subscription-key";
    private const string DetectQuery = "detect";
    private const string VerifyQuery = "verify";
    private const string TrainQuery = "train";
    private const string TrainingQuery = "training";
    private const string PersonGroupsQuery = "persongroups";
    private const string PersonsQuery = "persons";
    private const string FacesQuery = "faces";
    private const string PersistedFacesQuery = "persistedfaces";
    private const string FaceListsQuery = "facelists";
    private const string FindSimilarsQuery = "findsimilars";
    private const string IdentifyQuery = "identify";
    private const string GroupQuery = "group";
    private static JsonSerializerSettings s_settings;
    private readonly string _subscriptionKey;
    private readonly string _apiRoot;
    private HttpClient _httpClient;

    protected virtual string ServiceHost
    {
      get
      {
        return this._apiRoot;
      }
    }

    public HttpRequestHeaders DefaultRequestHeaders
    {
      get
      {
        return this._httpClient.DefaultRequestHeaders;
      }
    }

    static FaceServiceClient()
    {
      JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
      int num1 = 0;
     // serializerSettings.ContractResolver.ResolveContract((DateFormatHandling) num1);
      int num2 = 1;
    //  serializerSettings.set_NullValueHandling((NullValueHandling) num2);
      CamelCasePropertyNamesContractResolver contractResolver = new CamelCasePropertyNamesContractResolver();
    //  serializerSettings.set_ContractResolver((IContractResolver) contractResolver);
      FaceServiceClient.s_settings = serializerSettings;
    }

    public FaceServiceClient(string subscriptionKey)
      : this(subscriptionKey, "https://westcentralus.api.cognitive.microsoft.com/face/v1.0")
    {
    }

    public FaceServiceClient(string subscriptionKey, string apiRoot)
    {
      this._subscriptionKey = subscriptionKey;
      string str;
      if (apiRoot == null)
        str = (string) null;
      else
        str = apiRoot.TrimEnd('/');
      this._apiRoot = str;
      this._httpClient = new HttpClient();
      this._httpClient.DefaultRequestHeaders.Add("ocp-apim-subscription-key", subscriptionKey);
    }

    ~FaceServiceClient()
    {
      this.Dispose(false);
    }

    public static string GetAttributeString(IEnumerable<FaceAttributeType> types)
    {
      return string.Join(",", types.Select<FaceAttributeType, string>((Func<FaceAttributeType, string>) (attr =>
      {
        string str = attr.ToString();
        return char.ToLowerInvariant(str[0]).ToString() + str.Substring(1);
      })).ToArray<string>());
    }

    public async Task<MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]> DetectAsync(string imageUrl, bool returnFaceId = true, bool returnFaceLandmarks = false, IEnumerable<FaceAttributeType> returnFaceAttributes = null)
    {
      if (returnFaceAttributes != null)
        return await this.SendRequestAsync<object, MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]>(HttpMethod.Post, string.Format("{0}/{1}?returnFaceId={2}&returnFaceLandmarks={3}&returnFaceAttributes={4}", (object) this.ServiceHost, (object) "detect", (object) returnFaceId, (object) returnFaceLandmarks, (object) FaceServiceClient.GetAttributeString(returnFaceAttributes)), (object) new
        {
          url = imageUrl
        });
      return await this.SendRequestAsync<object, MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]>(HttpMethod.Post, string.Format("{0}/{1}?returnFaceId={2}&returnFaceLandmarks={3}", (object) this.ServiceHost, (object) "detect", (object) returnFaceId, (object) returnFaceLandmarks), (object) new
      {
        url = imageUrl
      });
    }

    public async Task<MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]> DetectAsync(Stream imageStream, bool returnFaceId = true, bool returnFaceLandmarks = false, IEnumerable<FaceAttributeType> returnFaceAttributes = null)
    {
      if (returnFaceAttributes != null)
        return await this.SendRequestAsync<Stream, MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]>(HttpMethod.Post, string.Format("{0}/{1}?returnFaceId={2}&returnFaceLandmarks={3}&returnFaceAttributes={4}", (object) this.ServiceHost, (object) "detect", (object) returnFaceId, (object) returnFaceLandmarks, (object) FaceServiceClient.GetAttributeString(returnFaceAttributes)), imageStream);
      return await this.SendRequestAsync<Stream, MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]>(HttpMethod.Post, string.Format("{0}/{1}?returnFaceId={2}&returnFaceLandmarks={3}", (object) this.ServiceHost, (object) "detect", (object) returnFaceId, (object) returnFaceLandmarks), imageStream);
    }

    public async Task<VerifyResult> VerifyAsync(Guid faceId1, Guid faceId2)
    {
      return await this.SendRequestAsync<object, VerifyResult>(HttpMethod.Post, string.Format("{0}/{1}", new object[2]
      {
        (object) this.ServiceHost,
        (object) "verify"
      }), (object) new
      {
        faceId1 = faceId1,
        faceId2 = faceId2
      });
    }

    public async Task<VerifyResult> VerifyAsync(Guid faceId, string personGroupId, Guid personId)
    {
      return await this.SendRequestAsync<object, VerifyResult>(HttpMethod.Post, string.Format("{0}/{1}", new object[2]
      {
        (object) this.ServiceHost,
        (object) "verify"
      }), (object) new
      {
        faceId = faceId,
        personGroupId = personGroupId,
        personId = personId
      });
    }

    public async Task<IdentifyResult[]> IdentifyAsync(string personGroupId, Guid[] faceIds, int maxNumOfCandidatesReturned = 1)
    {
      return await this.IdentifyAsync(personGroupId, faceIds, 0.5f, maxNumOfCandidatesReturned);
    }

    public async Task<IdentifyResult[]> IdentifyAsync(string personGroupId, Guid[] faceIds, float confidenceThreshold, int maxNumOfCandidatesReturned = 1)
    {
      return await this.SendRequestAsync<object, IdentifyResult[]>(HttpMethod.Post, string.Format("{0}/{1}", new object[2]
      {
        (object) this.ServiceHost,
        (object) "identify"
      }), (object) new
      {
        personGroupId = personGroupId,
        faceIds = faceIds,
        maxNumOfCandidatesReturned = maxNumOfCandidatesReturned,
        confidenceThreshold = confidenceThreshold
      });
    }

    public async Task CreatePersonGroupAsync(string personGroupId, string name, string userData = null)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Put, string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId), (object) new
      {
        name = name,
        userData = userData
      });
    }

    public async Task<PersonGroup> GetPersonGroupAsync(string personGroupId)
    {
      return await this.SendRequestAsync<object, PersonGroup>(HttpMethod.Get, string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId), (object) null);
    }

    public async Task UpdatePersonGroupAsync(string personGroupId, string name, string userData = null)
    {
      object obj = await this.SendRequestAsync<object, object>(new HttpMethod("PATCH"), string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId), (object) new
      {
        name = name,
        userData = userData
      });
    }

    public async Task DeletePersonGroupAsync(string personGroupId)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Delete, string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId), (object) null);
    }

    [Obsolete("use ListPersonGroupsAsync instead")]
    public async Task<PersonGroup[]> GetPersonGroupsAsync()
    {
      return await this.ListPersonGroupsAsync("", 1000);
    }

    public async Task<PersonGroup[]> ListPersonGroupsAsync(string start = "", int top = 1000)
    {
      return await this.SendRequestAsync<object, PersonGroup[]>(HttpMethod.Get, string.Format("{0}/{1}?start={2}$top={3}", (object) this.ServiceHost, (object) "persongroups", (object) start, (object) top.ToString((IFormatProvider) CultureInfo.InvariantCulture)), (object) null);
    }

    public async Task TrainPersonGroupAsync(string personGroupId)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Post, string.Format("{0}/{1}/{2}/{3}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "train"), (object) null);
    }

    public async Task<TrainingStatus> GetPersonGroupTrainingStatusAsync(string personGroupId)
    {
      return await this.SendRequestAsync<object, TrainingStatus>(HttpMethod.Get, string.Format("{0}/{1}/{2}/{3}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "training"), (object) null);
    }

    public async Task<CreatePersonResult> CreatePersonAsync(string personGroupId, string name, string userData = null)
    {
      return await this.SendRequestAsync<object, CreatePersonResult>(HttpMethod.Post, string.Format("{0}/{1}/{2}/{3}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons"), (object) new
      {
        name = name,
        userData = userData
      });
    }

    public async Task<Person> GetPersonAsync(string personGroupId, Guid personId)
    {
      return await this.SendRequestAsync<object, Person>(HttpMethod.Get, string.Format("{0}/{1}/{2}/{3}/{4}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons", (object) personId), (object) null);
    }

    public async Task UpdatePersonAsync(string personGroupId, Guid personId, string name, string userData = null)
    {
      object obj = await this.SendRequestAsync<object, object>(new HttpMethod("PATCH"), string.Format("{0}/{1}/{2}/{3}/{4}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons", (object) personId), (object) new
      {
        name = name,
        userData = userData
      });
    }

    public async Task DeletePersonAsync(string personGroupId, Guid personId)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Delete, string.Format("{0}/{1}/{2}/{3}/{4}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons", (object) personId), (object) null);
    }

    public async Task<Person[]> GetPersonsAsync(string personGroupId)
    {
      return await this.SendRequestAsync<object, Person[]>(HttpMethod.Get, string.Format("{0}/{1}/{2}/{3}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons"), (object) null);
    }

    public async Task<AddPersistedFaceResult> AddPersonFaceAsync(string personGroupId, Guid personId, string imageUrl, string userData = null, FaceRectangle targetFace = null)
    {
      string format = "{0}/{1}/{2}/{3}/{4}/{5}?userData={6}&targetFace={7}";
      object[] objArray = new object[8]
      {
        (object) this.ServiceHost,
        (object) "persongroups",
        (object) personGroupId,
        (object) "persons",
        (object) personId,
        (object) "persistedfaces",
        (object) userData,
        null
      };
      int index = 7;
      string str;
      if (targetFace != null)
        str = string.Format("{0},{1},{2},{3}", (object) targetFace.Left, (object) targetFace.Top, (object) targetFace.Width, (object) targetFace.Height);
      else
        str = string.Empty;
      objArray[index] = (object) str;
      return await this.SendRequestAsync<object, AddPersistedFaceResult>(HttpMethod.Post, string.Format(format, objArray), (object) new
      {
        url = imageUrl
      });
    }

    public async Task<AddPersistedFaceResult> AddPersonFaceAsync(string personGroupId, Guid personId, Stream imageStream, string userData = null, FaceRectangle targetFace = null)
    {
      string format = "{0}/{1}/{2}/{3}/{4}/{5}?userData={6}&targetFace={7}";
      object[] objArray = new object[8]
      {
        (object) this.ServiceHost,
        (object) "persongroups",
        (object) personGroupId,
        (object) "persons",
        (object) personId,
        (object) "persistedfaces",
        (object) userData,
        null
      };
      int index = 7;
      string str;
      if (targetFace != null)
        str = string.Format("{0},{1},{2},{3}", (object) targetFace.Left, (object) targetFace.Top, (object) targetFace.Width, (object) targetFace.Height);
      else
        str = string.Empty;
      objArray[index] = (object) str;
      return await this.SendRequestAsync<Stream, AddPersistedFaceResult>(HttpMethod.Post, string.Format(format, objArray), imageStream);
    }

    public async Task<PersonFace> GetPersonFaceAsync(string personGroupId, Guid personId, Guid persistedFace)
    {
      return await this.SendRequestAsync<object, PersonFace>(HttpMethod.Get, string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons", (object) personId, (object) "persistedfaces", (object) persistedFace), (object) null);
    }

    public async Task UpdatePersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId, string userData)
    {
      object obj = await this.SendRequestAsync<object, object>(new HttpMethod("PATCH"), string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons", (object) personId, (object) "persistedfaces", (object) persistedFaceId), (object) new
      {
        userData = userData
      });
    }

    public async Task DeletePersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Delete, string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", (object) this.ServiceHost, (object) "persongroups", (object) personGroupId, (object) "persons", (object) personId, (object) "persistedfaces", (object) persistedFaceId), (object) null);
    }

    public async Task<SimilarFace[]> FindSimilarAsync(Guid faceId, Guid[] faceIds, int maxNumOfCandidatesReturned = 20)
    {
      return await this.FindSimilarAsync(faceId, faceIds, FindSimilarMatchMode.matchPerson, maxNumOfCandidatesReturned);
    }

    public async Task<SimilarFace[]> FindSimilarAsync(Guid faceId, Guid[] faceIds, FindSimilarMatchMode mode, int maxNumOfCandidatesReturned = 20)
    {
      return await this.SendRequestAsync<object, SimilarFace[]>(HttpMethod.Post, string.Format("{0}/{1}", new object[2]
      {
        (object) this.ServiceHost,
        (object) "findsimilars"
      }), (object) new
      {
        faceId = faceId,
        faceIds = faceIds,
        maxNumOfCandidatesReturned = maxNumOfCandidatesReturned,
        mode = mode.ToString()
      });
    }

    public async Task<SimilarPersistedFace[]> FindSimilarAsync(Guid faceId, string faceListId, int maxNumOfCandidatesReturned = 20)
    {
      return await this.FindSimilarAsync(faceId, faceListId, FindSimilarMatchMode.matchPerson, maxNumOfCandidatesReturned);
    }

    public async Task<SimilarPersistedFace[]> FindSimilarAsync(Guid faceId, string faceListId, FindSimilarMatchMode mode, int maxNumOfCandidatesReturned = 20)
    {
      return await this.SendRequestAsync<object, SimilarPersistedFace[]>(HttpMethod.Post, string.Format("{0}/{1}", new object[2]
      {
        (object) this.ServiceHost,
        (object) "findsimilars"
      }), (object) new
      {
        faceId = faceId,
        faceListId = faceListId,
        maxNumOfCandidatesReturned = maxNumOfCandidatesReturned,
        mode = mode.ToString()
      });
    }

    public async Task<GroupResult> GroupAsync(Guid[] faceIds)
    {
      return await this.SendRequestAsync<object, GroupResult>(HttpMethod.Post, string.Format("{0}/{1}", new object[2]
      {
        (object) this.ServiceHost,
        (object) "group"
      }), (object) new{ faceIds = faceIds });
    }

    public async Task CreateFaceListAsync(string faceListId, string name, string userData)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Put, string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "facelists", (object) faceListId), (object) new
      {
        name = name,
        userData = userData
      });
    }

    public async Task<FaceList> GetFaceListAsync(string faceListId)
    {
      return await this.SendRequestAsync<object, FaceList>(HttpMethod.Get, string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "facelists", (object) faceListId), (object) null);
    }

    public async Task<FaceListMetadata[]> ListFaceListsAsync()
    {
      return await this.SendRequestAsync<object, FaceListMetadata[]>(HttpMethod.Get, string.Format("{0}/{1}", new object[2]
      {
        (object) this.ServiceHost,
        (object) "facelists"
      }), (object) null);
    }

    public async Task UpdateFaceListAsync(string faceListId, string name, string userData)
    {
      object obj = await this.SendRequestAsync<object, object>(new HttpMethod("PATCH"), string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "facelists", (object) faceListId), (object) new
      {
        name = name,
        userData = userData
      });
    }

    public async Task DeleteFaceListAsync(string faceListId)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Delete, string.Format("{0}/{1}/{2}", (object) this.ServiceHost, (object) "facelists", (object) faceListId), (object) null);
    }

    public async Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string faceListId, string imageUrl, string userData = null, FaceRectangle targetFace = null)
    {
      string format = "{0}/{1}/{2}/{3}?userData={4}&targetFace={5}";
      object[] objArray = new object[6]
      {
        (object) this.ServiceHost,
        (object) "facelists",
        (object) faceListId,
        (object) "persistedfaces",
        (object) userData,
        null
      };
      int index = 5;
      string str;
      if (targetFace != null)
        str = string.Format("{0},{1},{2},{3}", (object) targetFace.Left, (object) targetFace.Top, (object) targetFace.Width, (object) targetFace.Height);
      else
        str = string.Empty;
      objArray[index] = (object) str;
      return await this.SendRequestAsync<object, AddPersistedFaceResult>(HttpMethod.Post, string.Format(format, objArray), (object) new
      {
        url = imageUrl
      });
    }

    public async Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string faceListId, Stream imageStream, string userData = null, FaceRectangle targetFace = null)
    {
      string format = "{0}/{1}/{2}/{3}?userData={4}&targetFace={5}";
      object[] objArray = new object[6]
      {
        (object) this.ServiceHost,
        (object) "facelists",
        (object) faceListId,
        (object) "persistedfaces",
        (object) userData,
        null
      };
      int index = 5;
      string str;
      if (targetFace != null)
        str = string.Format("{0},{1},{2},{3}", (object) targetFace.Left, (object) targetFace.Top, (object) targetFace.Width, (object) targetFace.Height);
      else
        str = string.Empty;
      objArray[index] = (object) str;
      return await this.SendRequestAsync<object, AddPersistedFaceResult>(HttpMethod.Post, string.Format(format, objArray), (object) imageStream);
    }

    public async Task DeleteFaceFromFaceListAsync(string faceListId, Guid persistedFaceId)
    {
      object obj = await this.SendRequestAsync<object, object>(HttpMethod.Delete, string.Format("{0}/{1}/{2}/{3}/{4}", (object) this.ServiceHost, (object) "facelists", (object) faceListId, (object) "persistedfaces", (object) persistedFaceId), (object) null);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this._httpClient == null)
        return;
      this._httpClient.Dispose();
      this._httpClient = (HttpClient) null;
    }

    private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(HttpMethod httpMethod, string requestUrl, TRequest requestBody)
    {
      HttpRequestMessage request = new HttpRequestMessage(httpMethod, this.ServiceHost);
      request.RequestUri = new Uri(requestUrl);
      if ((object) (TRequest) requestBody != null)
      {
        if ((object) (TRequest) requestBody is Stream)
        {
          request.Content = (HttpContent) new StreamContent((object) (TRequest) requestBody as Stream);
          request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        }
        else
          request.Content = (HttpContent) new StringContent(JsonConvert.SerializeObject((object) (TRequest) requestBody, FaceServiceClient.s_settings), Encoding.UTF8, "application/json");
      }
      HttpResponseMessage response = await this._httpClient.SendAsync(request);
      if (response.IsSuccessStatusCode)
      {
        string str = (string) null;
        if (response.Content != null)
          str = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(str) ? default (TResponse) : (TResponse) JsonConvert.DeserializeObject<TResponse>(str, FaceServiceClient.s_settings);
      }
      if (response.Content != null && response.Content.Headers.ContentType.MediaType.Contains("application/json"))
      {
        string str = await response.Content.ReadAsStringAsync();
        ClientError clientError = (ClientError) JsonConvert.DeserializeObject<ClientError>(str);
        if (clientError.Error != null)
          throw new FaceAPIException(clientError.Error.ErrorCode, clientError.Error.Message, response.StatusCode);
        ServiceError serviceError = (ServiceError) JsonConvert.DeserializeObject<ServiceError>(str);
        if (clientError != null)
          throw new FaceAPIException(serviceError.ErrorCode, serviceError.Message, response.StatusCode);
        throw new FaceAPIException("Unknown", "Unknown Error", response.StatusCode);
      }
      response.EnsureSuccessStatusCode();
      return default (TResponse);
    }
  }
}
