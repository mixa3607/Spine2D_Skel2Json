using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpineModelExtractor.AdditionalClasses;
using SpineModelExtractor.SkelClasses;

namespace SpineModelExtractor
{
    class SkelSerializer
    {
        private static readonly JsonSerializer DefaultSerializer = JsonSerializer.Create(new JsonSerializerSettings() {DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
        

        public static JToken Serialize(object serObject, string propName)
        {
            JToken root = null;
            var jArrayContainer = new JObject();
            //if array
            if (serObject is ContainName[] serNamedArray)
            {
                foreach (var namedObject in serNamedArray)
                {
                    var node = Serialize(namedObject, "none");
                    jArrayContainer.Add(node);
                }

                root = new JProperty(propName, jArrayContainer);
            }
            //single object
            else if (serObject is ContainName serNamedObject)
            {
                var fields = serObject.GetType().GetProperties();
                foreach (var fieldInfo in fields)
                {
                    //if ignored attr then skip this field
                    if (fieldInfo.GetCustomAttributes(true).OfType<JsonIgnoreAttribute>().Any())
                        continue;
                    var jToken = Serialize(fieldInfo.GetValue(serObject), fieldInfo.Name);
                    jArrayContainer.Add(jToken);
                }
                root = new JProperty(serNamedObject.Name, jArrayContainer);
            }
            //alg for non Named members
            else
            {
                try
                {
                    root = new JProperty(propName, JToken.FromObject(serObject));
                }
                catch
                {
                    root = new JProperty(propName, serObject);
                }
            }

            return root;
        }


        public static JObject SerializeSkeleton(SerSkeleton serSkeleton)
        {
            var root = JObject.FromObject(serSkeleton);
            return root;
        }

        public static JArray SerializeSlots(SerSlot[] serSlots)
        {
            var root = new JArray();
            foreach (var serSlot in serSlots)
            {
                root.Add(JObject.FromObject(serSlot, DefaultSerializer));
            }
            return root;
        }

        public static JObject SerializeBones(SerBone[] serBones)
        {
            var root = new JObject();
            foreach (var serBone in serBones)
            {
                root.Add(JObject.FromObject(serBone, DefaultSerializer));
            }
            return root;
        }

        
        public static JObject SerializeSkins(SerSkin[] serSkins)
        {
            var root = new JObject();
            foreach (var serSkin in serSkins)
            {
                root.Add(new JProperty(serSkin.Name, SerializeSkinSlots(serSkin.Slots)));
            }
            
            return root;
        }

        public static JObject SerializeAnimations(SerAnimation[] serAnimations)
        {
            var root = new JObject();
            foreach (var serAnimation in serAnimations)
            {
                root.Add(new JProperty(serAnimation.Name, SerializeAnimation(serAnimation)));
            }

            return root;
        }

        public static JToken SerializeAnimation(SerAnimation serAnimation)
        {
            var root = new JObject();
            var mainDict = new Dictionary<string, JToken>();
            mainDict.Add("bones", SerializeAnimationBones(serAnimation.Bones));

            return root;
        }

        public static JToken SerializeAnimationBones(SerAnimationBone[] serAnimationBones)
        {
            //var root = new JObject();
            var rootDict = new Dictionary<string, JObject>();
            foreach (var serAnimationBone in serAnimationBones)
            {
                var props = new Dictionary<string, JArray>();
                if (serAnimationBone.Rotate != null)
                {
                    props.Add("rotate", JArray.FromObject(serAnimationBone.Rotate.Frames, DefaultSerializer));
                }
                if (serAnimationBone.Scale != null)
                {
                    props.Add("scale", JArray.FromObject(serAnimationBone.Scale.Frames, DefaultSerializer));
                }
                if (serAnimationBone.Shear != null)
                {
                    props.Add("shear", JArray.FromObject(serAnimationBone.Shear.Frames, DefaultSerializer));
                }
                if (serAnimationBone.Translate != null)
                {
                    props.Add("translate", JArray.FromObject(serAnimationBone.Translate.Frames, DefaultSerializer));
                }
                rootDict.Add(serAnimationBone.Name, JObject.FromObject(props));
            }

            Console.WriteLine(JsonConvert.SerializeObject(rootDict, Formatting.Indented, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore}));

            return JObject.FromObject(rootDict, DefaultSerializer);
        }
        #region SerializeSkin private sub functions

        private static JToken SerializeSkinSlots(SerSkinSlot[] slots)
        {
            var root = new JObject();
            foreach (var serSkinSlot in slots)
            {
                root.Add(new JProperty(serSkinSlot.Name, SerializeSkinSlotsAttachments(serSkinSlot.Attachments)));
            }

            return root;
        }

        private static JToken SerializeSkinSlotsAttachments(SerAttachment[] attachments)
        {
            var root = new JObject();
            foreach (var attachment in attachments)
            {
                root.Add(new JProperty(attachment.Name, JObject.FromObject(attachment, DefaultSerializer)));
            }

            return root;
        }

        #endregion
    }
}
