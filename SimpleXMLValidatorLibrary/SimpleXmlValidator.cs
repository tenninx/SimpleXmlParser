namespace SimpleXMLValidatorLibrary
{
    public class SimpleXmlValidator
    {
        /// <summary>
        /// Linear processing of XML string
        /// </summary>
        /// <param name="p_strXml">Input XML string to validate</param>
        /// <returns>Validity of the input</returns>
        public static bool DetermineXml(string p_strXml)
        {
            // Perform the basic check
            if (!PerformBasicCheck(p_strXml)) return false;

            // Local variables
            Stack<string> _objTagStack = new Stack<string>();
            string _strReadString = string.Empty;

            do
            {
                // If a tag...
                if (p_strXml[0].Equals('<'))
                    p_strXml = p_strXml.Substring(1);
                else
                {
                    // If data, discard it and continue
                    p_strXml = p_strXml.Substring(p_strXml.IndexOf("<"));
                    continue;
                }

                // Read the tag
                _strReadString = p_strXml.Substring(0, p_strXml.IndexOf(">"));

                // Check if the tag is valid
                if (!ProcessTag(_objTagStack, _strReadString)) return false;

                // Discard the processed string in linear way
                p_strXml = p_strXml.Substring(_strReadString.Length + 1);
            }
            while (p_strXml.Length > 0);

            // Invalid if unclosed tags exist
            if (_objTagStack.Count != 0) return false;

            return true;
        }

        private static bool ProcessTag(Stack<string> p_objTagStack, string p_strReadString)
        {
            // Invalid if empty tag name
            if (p_strReadString.Length == 0) return false;

            // Check if closing tag
            if (p_strReadString[0].Equals('/'))
            {
                // Invalid if no more tags in stack to close
                if (p_objTagStack.Count == 0) return false;
                // Validate if current closing tag matches the supposed opening tag
                return p_objTagStack.Pop().Equals(p_strReadString.Substring(1));
            }
            else
                // Push the opening tag to stack
                p_objTagStack.Push(p_strReadString);

            return true;
        }

        private static bool PerformBasicCheck(string p_strXml)
        {
            // Trim leading and trailing whitespaces
            p_strXml = p_strXml.Trim();

            // Check if the string starts with < and ends with > char.
            return p_strXml.StartsWith("<") && p_strXml.EndsWith(">");
        }
    }
}