using System.ComponentModel;
using Newtonsoft.Json;
using SpineModelExtractor.SkelClasses;

namespace SpineModelExtractor.AdditionalClasses
{


    public class SerAnimationBone : ContainName
    {
        public SerAnimBoneRotate[] Rotates { get; set; }

        public SerAnimBoneTranslate[] Translates { get; set; }

        public SerAnimBoneScale[] Scales { get; set; }

        public SerAnimBoneShear[] Shears { get; set; }
    }

    //name: rotate
    public class SerAnimBoneRotate : SerAnimBoneRotateFramesContainer
    {
        
    }

    //name: translate
    public class SerAnimBoneTranslate : SerAnimBoneFramesContainer
    {

    }

    //name: scale
    public class SerAnimBoneScale : SerAnimBoneFramesContainer
    {

    }

    //name: shear
    public class SerAnimBoneShear : SerAnimBoneFramesContainer
    {

    }

    public abstract class SerAnimBoneRotateFramesContainer
    {
        public SerAnimBoneRotateFrame[] Frames { get; set; }
    }

    public abstract class SerAnimBoneFramesContainer
    {
        public SerAnimBoneFrame[] Frames { get; set; }
    }

    //public class SerAnimBoneFrame : SerCurvedMember
    //{
    //
    //}
    public class SerAnimBoneRotateFrame : SerTimeLineMember
    {
        [JsonProperty("angle")]
        [DefaultValue(null)]
        public float Angle { get; set; }

        [JsonProperty("curve")]
        [DefaultValue(null)]
        public float[] Curve { get; set; }

        [JsonIgnore]
        [DefaultValue(null)]
        public bool? IsStepped { get; set; }
    }

    public class SerAnimBoneFrame : SerTimeLineMember
    {
        [JsonProperty("curve")]
        [DefaultValue(null)]
        public float[] Curve { get; set; }

        [JsonProperty("x")]
        [DefaultValue(null)]
        public float X { get; set; }

        [JsonProperty("y")]
        [DefaultValue(null)]
        public float Y { get; set; }

        [JsonIgnore]
        [DefaultValue(null)]
        public bool? IsStepped { get; set; }
    }
}
