// Decompiled with JetBrains decompiler
// Type: MobileTec.FaceAPI.PoC.FaceClient.IFaceServiceClient
// Assembly: MobileTec.FaceAPI.PoC.FaceClient, Version=1.2.1.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: E1C430B0-5B4F-40C0-B7F6-ACC82F4C9CFC
// Assembly location: C:\Users\jangu\Downloads\FaceAPI_MVC\packages\MobileTec.FaceAPI.PoC.FaceClient.1.2.1.2\lib\portable-net45+wp80+win8+wpa81+aspnetcore50\MobileTec.FaceAPI.PoC.FaceClient.dll

using MobileTec.FaceAPI.PoC.FaceClient.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MobileTec.FaceAPI.PoC.FaceClient
{
  public interface IFaceServiceClient
  {
    HttpRequestHeaders DefaultRequestHeaders { get; }

    Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string faceListId, string imageUrl, string userData = null, FaceRectangle targetFace = null);

    Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string faceListId, Stream imageStream, string userData = null, FaceRectangle targetFace = null);

    Task<AddPersistedFaceResult> AddPersonFaceAsync(string personGroupId, Guid personId, string imageUrl, string userData = null, FaceRectangle targetFace = null);

    Task<AddPersistedFaceResult> AddPersonFaceAsync(string personGroupId, Guid personId, Stream imageStream, string userData = null, FaceRectangle targetFace = null);

    Task CreateFaceListAsync(string faceListId, string name, string userData);

    Task<CreatePersonResult> CreatePersonAsync(string personGroupId, string name, string userData = null);

    Task CreatePersonGroupAsync(string personGroupId, string name, string userData = null);

    Task DeleteFaceFromFaceListAsync(string faceListId, Guid persistedFaceId);

    Task DeleteFaceListAsync(string faceListId);

    Task DeletePersonAsync(string personGroupId, Guid personId);

    Task DeletePersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId);

    Task DeletePersonGroupAsync(string personGroupId);

    Task<MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]> DetectAsync(string imageUrl, bool returnFaceId = true, bool returnFaceLandmarks = false, IEnumerable<FaceAttributeType> returnFaceAttributes = null);

    Task<MobileTec.FaceAPI.PoC.FaceClient.Contract.Face[]> DetectAsync(Stream imageStream, bool returnFaceId = true, bool returnFaceLandmarks = false, IEnumerable<FaceAttributeType> returnFaceAttributes = null);

    Task<SimilarFace[]> FindSimilarAsync(Guid faceId, Guid[] faceIds, int maxNumOfCandidatesReturned = 20);

    Task<SimilarFace[]> FindSimilarAsync(Guid faceId, Guid[] faceIds, FindSimilarMatchMode mode, int maxNumOfCandidatesReturned = 20);

    Task<SimilarPersistedFace[]> FindSimilarAsync(Guid faceId, string faceListId, int maxNumOfCandidatesReturned = 20);

    Task<SimilarPersistedFace[]> FindSimilarAsync(Guid faceId, string faceListId, FindSimilarMatchMode mode, int maxNumOfCandidatesReturned = 20);

    Task<FaceList> GetFaceListAsync(string faceListId);

    Task<Person> GetPersonAsync(string personGroupId, Guid personId);

    Task<PersonFace> GetPersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId);

    Task<PersonGroup> GetPersonGroupAsync(string personGroupId);

    [Obsolete("use ListPersonGroupsAsync instead")]
    Task<PersonGroup[]> GetPersonGroupsAsync();

    Task<PersonGroup[]> ListPersonGroupsAsync(string start = "", int top = 1000);

    Task<TrainingStatus> GetPersonGroupTrainingStatusAsync(string personGroupId);

    Task<Person[]> GetPersonsAsync(string personGroupId);

    Task<GroupResult> GroupAsync(Guid[] faceIds);

    Task<IdentifyResult[]> IdentifyAsync(string personGroupId, Guid[] faceIds, int maxNumOfCandidatesReturned = 1);

    Task<IdentifyResult[]> IdentifyAsync(string personGroupId, Guid[] faceIds, float confidenceThreshold, int maxNumOfCandidatesReturned = 1);

    Task<FaceListMetadata[]> ListFaceListsAsync();

    Task TrainPersonGroupAsync(string personGroupId);

    Task UpdateFaceListAsync(string faceListId, string name, string userData);

    Task UpdatePersonAsync(string personGroupId, Guid personId, string name, string userData = null);

    Task UpdatePersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId, string userData = null);

    Task UpdatePersonGroupAsync(string personGroupId, string name, string userData = null);

    Task<VerifyResult> VerifyAsync(Guid faceId1, Guid faceId2);

    Task<VerifyResult> VerifyAsync(Guid faceId, string personGroupId, Guid personId);
  }
}
