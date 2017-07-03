// Decompiled with JetBrains decompiler
// Type: MobileTec.FaceAPI.PoC.FaceClient.Contract.FaceAttributes
// Assembly: MobileTec.FaceAPI.PoC.FaceClient, Version=1.2.1.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: E1C430B0-5B4F-40C0-B7F6-ACC82F4C9CFC
// Assembly location: C:\Users\jangu\Downloads\FaceAPI_MVC\packages\MobileTec.FaceAPI.PoC.FaceClient.1.2.1.2\lib\portable-net45+wp80+win8+wpa81+aspnetcore50\MobileTec.FaceAPI.PoC.FaceClient.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MobileTec.FaceAPI.PoC.FaceClient.Contract
{
  public class FaceAttributes
  {
    public double Age { get; set; }

    public string Gender { get; set; }

    public HeadPose HeadPose { get; set; }

    public double Smile { get; set; }

    public FacialHair FacialHair { get; set; }

    [JsonConverter(typeof (StringEnumConverter))]
    public Glasses Glasses { get; set; }
  }
}
