using System.ComponentModel;
using Newtonsoft.Json;

namespace SpineModelExtractor.SkelClasses
{
    class SerBones
    {
        [JsonProperty("bones")]
        public SerBone[] Bones { get; set; }
    }
    public class SerBone
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("y")]
        [DefaultValue(0.0)]
        public float Y { get; set; }

        [JsonProperty("x")]
        [DefaultValue(0.0)]
        public float X { get; set; }

        [JsonProperty("color")] //not used
        [DefaultValue(null)]
        public string Color { get; set; }

        [JsonProperty("scaleX")]
        [DefaultValue(1.0)]
        public float ScaleX { get; set; }

        [JsonProperty("scaleY")]
        [DefaultValue(1.0)]
        public float ScaleY { get; set; }

        [JsonProperty("shearX")]
        [DefaultValue(0.0)]
        public float ShearX { get; set; }

        [JsonProperty("shearY")]
        [DefaultValue(0.0)]
        public float ShearY { get; set; }

        [JsonProperty("rotation")]
        [DefaultValue(0.0)]
        public float Rotation { get; set; }

        [JsonProperty("transform")]
        [DefaultValue("Normal")]
        public string Transform { get; set; }

        [JsonProperty("length")]
        [DefaultValue(0.0)]
        public float Length { get; set; }
    }
}
