﻿// Decompiled with JetBrains decompiler
// Type: MobileTec.FaceAPI.PoC.FaceClient.Contract.ServiceError
// Assembly: MobileTec.FaceAPI.PoC.FaceClient, Version=1.2.1.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: E1C430B0-5B4F-40C0-B7F6-ACC82F4C9CFC
// Assembly location: C:\Users\jangu\Downloads\FaceAPI_MVC\packages\MobileTec.FaceAPI.PoC.FaceClient.1.2.1.2\lib\portable-net45+wp80+win8+wpa81+aspnetcore50\MobileTec.FaceAPI.PoC.FaceClient.dll

using System.Runtime.Serialization;

namespace MobileTec.FaceAPI.PoC.FaceClient.Contract
{
  [DataContract]
  public class ServiceError
  {
    [DataMember(Name = "statusCode")]
    public string ErrorCode { get; set; }

    [DataMember(Name = "message")]
    public string Message { get; set; }
  }
}
