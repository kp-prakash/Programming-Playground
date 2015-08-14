namespace Serializer
{
    #region References
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    #endregion

    /// <summary>
    /// Serializes and deserializes objects from / to XML.
    /// </summary>
    /// <typeparam name="T">Target Type</typeparam>
    public class Serializer<T> where T : new()
    {
        #region Public Methods and Operators

        /// <summary>
        /// Deserializes a XML string into an object. Default encoding is <c>UTF8</c>.
        /// </summary>        
        /// <param name="xml">The xml to deserialize.</param>
        /// <param name="settings">XML serialization settings. <see cref="System.Xml.XmlReaderSettings"/></param>
        /// <returns>An object of Type <c>T</c>.</returns>
        public static T Deserialize(string xml, XmlReaderSettings settings = null)
        {
            return Deserialize(xml, Encoding.UTF8, settings);
        }

        /// <summary>
        /// Deserializes a XML string into an object.
        /// </summary>
        /// <param name="xml">The xml to deserialize.</param>
        /// <param name="encoding">Character encoding for the generated XML.</param>
        /// <param name="settings">XML serialization settings. <see cref="System.Xml.XmlReaderSettings"/></param>
        /// <returns>An object of Type <c>T</c>.</returns>
        public static T Deserialize(string xml, Encoding encoding, XmlReaderSettings settings = null)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentException("XML cannot be null or empty.", "xml");
            }
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var memoryStream = new MemoryStream(encoding.GetBytes(xml)))
            {
                using (XmlReader xmlReader = XmlReader.Create(memoryStream, settings))
                {
                    return (T)xmlSerializer.Deserialize(xmlReader);
                }
            }
        }

        /// <summary>
        /// Deserializes a XML file.
        /// </summary>
        /// <param name="fileName">The filename of the XML file to deserialize</param>
        /// <param name="settings">XML serialization settings. <see cref="System.Xml.XmlReaderSettings"/></param>
        /// <returns>An object of type <c>T</c></returns>
        public static T DeserializeFromFile(string fileName, XmlReaderSettings settings = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("XML filename cannot be null or empty.", "filename");
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Cannot find XML file to deserialize.", fileName);
            }
            if (null == settings)
            {
                settings = new XmlReaderSettings();
            }
            // Create the stream writer with the specified encoding
            using (XmlReader xmlReader = XmlReader.Create(fileName, settings))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(xmlReader);
            }
        }

        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <param name="source">The object to serialize</param>
        /// <param name="namespaces">Namespaces to include in serialization</param>
        /// <param name="settings">XML serialization settings. <see cref="System.Xml.XmlWriterSettings"/></param>
        /// <returns>A XML string that represents the object to be serialized</returns>
        public static string Serialize(
            T source, XmlSerializerNamespaces namespaces = null, XmlWriterSettings settings = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Object to serialize cannot be null.");
            }
            if (null == settings)
            {
                settings = GetIndentedSettings();
            }
            string xml;
            using (var memoryStream = new MemoryStream())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(xmlWriter, source, namespaces);
                    memoryStream.Position = 0; // Reset the stream before reading back.
                    using (var streamReader = new StreamReader(memoryStream))
                    {
                        xml = streamReader.ReadToEnd();
                    }
                }
            }
            return xml;
        }

        /// <summary>
        /// Serialize an object to a XML file
        /// </summary>
        /// <param name="source">The object to serialize</param>
        /// <param name="filename">The file to generate</param>
        /// <param name="namespaces">Namespaces to include in serialization</param>
        /// <param name="settings">XML serialization settings. <see cref="System.Xml.XmlWriterSettings"/></param>
        public static void SerializeToFile(
            T source, string filename, XmlSerializerNamespaces namespaces = null, XmlWriterSettings settings = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Object to serialize cannot be null.");
            }
            if (null == settings)
            {
                settings = GetIndentedSettings();
            }
            using (XmlWriter xmlWriter = XmlWriter.Create(filename, settings))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(xmlWriter, source, namespaces);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the default indentation settings.
        /// Default indentation is \t - 'tab'.
        /// </summary>
        /// <returns>An instance of XmlWriterSettings.</returns>
        private static XmlWriterSettings GetIndentedSettings()
        {
            var xmlWriterSettings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
            return xmlWriterSettings;
        }

        #endregion
    }
}