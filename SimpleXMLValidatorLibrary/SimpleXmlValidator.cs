namespace SimpleXMLValidatorLibrary
{
    public class SimpleXmlValidator
    {
        /// <summary>
        /// Linear processing of XML string
        /// </summary>
        /// <param name="xml">XML string</param>
        /// <returns>Validity of the input</returns>
        public static bool DetermineXml(string xml)
        {
            // Perform the basic check
            if (!PerformBasicCheck(xml)) return false;

            // Local variables
            Stack<string> tagStack = new Stack<string>();
            bool isClosing = false;
            string readString = string.Empty;

            do
            {
                // If a tag...
                if (xml[0].Equals('<'))
                    xml = xml.Substring(1);
                else
                {
                    // If data, discard it and continue
                    xml = xml.Substring(xml.IndexOf("<"));
                    continue;
                }

                // Read the tag
                readString = xml.Substring(0, xml.IndexOf(">"));

                // Check if the tag is valid
                if (!ProcessTag(tagStack, ref isClosing, readString)) return false;

                // Discard the processed string in linear way
                xml = xml.Substring(readString.Length + 1);
            }
            while (xml.Length > 0);

            // Invalid if unclosed tags exist
            if (tagStack.Count != 0) return false;

            return true;
        }

        private static bool ProcessTag(Stack<string> TagStack, ref bool isClosing, string readString)
        {
            // Invalid if empty tag, or no more tags in stack
            if (readString.Length == 0 || (isClosing && (TagStack.Count == 0 || !readString[0].Equals('/'))))
                return false;

            // Check if closing tag and validate the tag
            if (readString[0].Equals('/'))
            {
                isClosing = true;
                return TagStack.Pop().Equals(readString.Substring(1));
            }
            else
                // Push the tag to stack
                TagStack.Push(readString);

            return true;
        }

        private static bool PerformBasicCheck(string xml)
        {
            // Trim leading and trailing whitespaces
            xml = xml.Trim();

            // Check if the string starts with < and ends with > char.
            return xml.StartsWith("<") && xml.EndsWith(">");
        }
    }
}