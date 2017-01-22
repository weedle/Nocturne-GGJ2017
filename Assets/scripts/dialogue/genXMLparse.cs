using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class genXMLparse {


	public static void Serialize(object item, string path) {
		XmlSerializer serializer = new XmlSerializer (item.GetType());
		StreamWriter writer = new StreamWriter (path);
		serializer.Serialize (writer.BaseStream, item);
		writer.Close ();
	}


	public static dialogue Deserialize<dialogue>(string path){
		XmlSerializer serializer = new XmlSerializer (typeof(dialogue));
		StreamReader reader = new StreamReader (path);
		dialogue deserialized = (dialogue)serializer.Deserialize (reader.BaseStream);
		reader.Close ();
		return deserialized;
	}
}
