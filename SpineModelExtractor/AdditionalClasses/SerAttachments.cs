using System.ComponentModel;
using Newtonsoft.Json;

namespace SpineModelExtractor.AdditionalClasses
{
    //default is "region"
    //"skinnedmesh" => "weightedmesh"
    //"weightedmesh" => "mesh"
    //"weightedlinkedmesh" => "linkedmesh"
    //"mesh" == "skinnedmesh"


    
    public class ContainName
    {
        [JsonIgnore]
        public string Name { get; set; }
    }
    public class SerAttachment : ContainName
    {
        [JsonIgnore]
        public int SlotIndex { get; set; }

        [JsonProperty("type")]
        [DefaultValue("region")]
        public string Type { get; set; }
    }

    public class SerClipping: SerAttachment //clipping
    {
        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("vertexCount")]
        public int VertexCount { get; set; }

        [JsonProperty("vertices")]
        public float[] Vertices { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class SerRegion : SerAttachment //region
    {

        [JsonProperty("width")]
        [DefaultValue(32.0)]
        public float Width { get; set; }    //def 32

        [JsonProperty("height")]
        [DefaultValue(32.0)]
        public float Height { get; set; }     //def 32

        [JsonProperty("x")]
        [DefaultValue(0.0)]
        public float X { get; set; }      //def 0

        [JsonProperty("y")]
        [DefaultValue(0.0)]
        public float Y { get; set; }      //def 0

        [JsonProperty("scaleX")]
        [DefaultValue(1.0)]
        public float ScaleX { get; set; } //def 1

        [JsonProperty("scaleY")]
        [DefaultValue(1.0)]
        public float ScaleY { get; set; } //def 1

        [JsonProperty("rotation")]
        [DefaultValue(0.0)]
        public float Rotation { get; set; } //def 0

        [JsonProperty("color")]
        [DefaultValue(null)]
        public string Color  { get; set; }  //def null (rrggbbaa hex)
    }

    public class SerBoundingbox
    {

    }

    public class SerMesh : SerAttachment    //mesh and linkedmash
    {
        [JsonProperty("uvs")]
        public float[] UVs { get; set; }

        [JsonProperty("triangles")]
        public int[] Triangles { get; set; }

        [JsonProperty("vertices")]
        public float[] Vertices { get; set; }

        [JsonProperty("hull")]
        public int Hull { get; set; }

        [JsonProperty("edges")]
        public int[] Edges { get; set; }

        [JsonProperty("width")]
        [DefaultValue(0.0)]
        public float Width { get; set; }

        [JsonProperty("height")]
        [DefaultValue(0.0)]
        public float Height { get; set; }
        
        [JsonProperty("parent")]
        [DefaultValue(null)]
        public string Parent { get; set; }

        [JsonProperty("skin")]
        [DefaultValue(null)]
        public string Skin { get; set; }

        [JsonProperty("deform")]
        [DefaultValue(true)]
        public bool Deform { get; set; }

        [JsonProperty("color")]
        [DefaultValue(null)]
        public string Color { get; set; }  //def null (rrggbbaa hex)
    }
    public class SerLinkedMesh : SerAttachment
    {

    }
    public class SerPath : SerAttachment
    {

    }
    public class SerPoint : SerAttachment
    {

    }
    

    
}
