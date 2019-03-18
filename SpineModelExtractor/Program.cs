using System;
using Spine;
using System.IO;
using Newtonsoft.Json;
using SpineModelExtractor.AdditionalClasses;


namespace SpineModelExtractor
{

    class Program
    {
        static void Main(string[] args)
        {
            bool isJsonLoad = false;
            SkeletonData skeletonData = null;

            if (isJsonLoad)
                skeletonData = GetSkeletonFromJson();
            else
                skeletonData = GetSkeletonFromSkel();

           
            var parser = new Parser(skeletonData);
            
            //animations section
            var serAnimations = parser.GetAnimations();

            //skeleton section
            var serSkeleton = parser.GetSkeleton();

            
            //bones section
            var serBones = parser.GetBones();
            
            //slots section
            var serSlots = parser.GetSlots();
            
            //skins section
            var serSkins = parser.GetSkins();
            var json = JsonConvert.SerializeObject(SkelSerializer.SerializeSkins(serSkins), Formatting.Indented, new JsonSerializerSettings() {DefaultValueHandling = DefaultValueHandling.Ignore});
            //SkelSerializer.SerializeSlots(serSlots).ToString();
            //var jsonObj = new JObject();
            //jsonObj.Add(new JProperty("skeleton", 
            //    new JArray(
            //        new JProperty("hash", ""), 
            //        new JProperty("spine", ""), 
            //        new JProperty("width", ""), 
            //        new JProperty("height", ""), 
            //        new JProperty("images", "")
            //    )));

            //Console.WriteLine(jsonObj.ToString());
        }

        

        

        static SkeletonData GetSkeletonFromSkel()
        {
            string folder = "models/hero/export/";
            string files = folder + "hero";
            string skelFile = files + "-pro.skel";
            string atlasFile = files + ".atlas";
            string jsonFile = files + "-pro.json";


            TexLoader textureLoader = new TexLoader();
            Atlas atlas = new Atlas(atlasFile, textureLoader);

            var skeletonBinary = new SkeletonBinary(new Atlas[] { atlas });
            var skeletonBinaryData = skeletonBinary.ReadSkeletonData(skelFile);
            return skeletonBinaryData;

        }

        static SkeletonData GetSkeletonFromJson()
        {
            string folder = "models/hero/export/";
            string files = folder + "hero";
            string skelFile = files + "-pro.skel";
            string atlasFile = files + ".atlas";
            string jsonFile = files + "-pro.json";


            TexLoader textureLoader = new TexLoader();
            Atlas atlas = new Atlas(atlasFile, textureLoader);

            var skeleton = new SkeletonJson(new Atlas[]{atlas});
            var skeletonData = skeleton.ReadSkeletonData(jsonFile);
            
            return skeletonData;

        }


        static void SomeF()
        {
            string folder = "models/coin/export/";
            string files = folder + "coin";
            string skelFile = files + "-pro.skel";
            string atlasFile = files + ".atlas";
            string jsonSkelFile = files + "-pro.json";


            TexLoader textureLoader = new TexLoader();
            Atlas atlas = new Atlas(atlasFile, textureLoader);

            var skeletonBinary = new SkeletonBinary(new Atlas[] { atlas });
            var skeletonBinaryData = skeletonBinary.ReadSkeletonData(skelFile);
            var jsonSkeletonBinary = JsonConvert.SerializeObject(skeletonBinaryData);
            File.WriteAllText(skelFile + ".json", jsonSkeletonBinary);

            var skeletonJson = new SkeletonJson(new Atlas[] { atlas });
            var skeletonJsonData = skeletonJson.ReadSkeletonData(jsonSkelFile);
            var jsonSkeletonJson = JsonConvert.SerializeObject(skeletonJsonData);
            File.WriteAllText(jsonSkelFile + ".json", jsonSkeletonJson);
            //Console.WriteLine(json);
        }
    }

    class TexLoader : TextureLoader
    {
        public void Load(AtlasPage atlasPage, string imgPath)
        {
            Console.WriteLine("Load: " + imgPath);
        }
        public void Unload(object objParam)
        {
            Console.WriteLine("Unload");
        }
    }
}
