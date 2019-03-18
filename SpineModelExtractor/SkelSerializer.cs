using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpineModelExtractor.AdditionalClasses;
using SpineModelExtractor.SkelClasses;

namespace SpineModelExtractor
{
    class SkelSerializer
    {
        private static readonly JsonSerializer DefaultIgnoreSerializer = JsonSerializer.Create(new JsonSerializerSettings() {DefaultValueHandling = DefaultValueHandling.Ignore});
        

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
                root.Add(JObject.FromObject(serSlot, DefaultIgnoreSerializer));
            }
            return root;
        }

        public static JObject SerializeBones(SerBone[] serBones)
        {
            var root = new JObject();
            foreach (var serBone in serBones)
            {
                root.Add(JObject.FromObject(serBone, DefaultIgnoreSerializer));
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
                root.Add(new JProperty(attachment.Name, JObject.FromObject(attachment, DefaultIgnoreSerializer)));
            }

            return root;
        }

        #endregion
    }
}
