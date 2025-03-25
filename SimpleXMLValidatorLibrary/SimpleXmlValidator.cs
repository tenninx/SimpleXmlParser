using SimpleXmlValidatorLibrary;

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
            SimpleStack _objTagStack = new SimpleStack();

            do
            {
                // If a tag...
                if (p_strXml[0].Equals('<'))
                    p_strXml = p_strXml.Substring(1);
                // If data, discard it and continue
                else
                {
                    if (!DiscardData(ref p_strXml)) return false;
                    continue;
                }

                // Read the tag
                string _strReadString = p_strXml.Substring(0, p_strXml.IndexOf(">"));

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

        /// <summary>
        /// Trim all leading and trailing whitespaces and check if the string starts with '<' and ends with '>'
        /// </summary>
        /// <param name="p_strXml">Input XML string to validate</param>
        /// <returns>Validity of the input</returns>
        private static bool PerformBasicCheck(string p_strXml)
        {
            // Trim leading and trailing whitespaces
            p_strXml = p_strXml.Trim();

            // Check if the string starts with < and ends with > char.
            return p_strXml.StartsWith("<") && p_strXml.EndsWith(">");
        }

        /// <summary>
        /// Discard the data between tags and validate if next tag is present
        /// </summary>
        /// <param name="p_strXml">Input XML string</param>
        /// <returns>Presence of the next tag</returns>
        private static bool DiscardData(ref string p_strXml)
        {
            int _intNextTag = p_strXml.IndexOf("<");
            // Invalid if no more tag after data
            if (_intNextTag == -1) return false;
            // Discard the data before next tag
            p_strXml = p_strXml.Substring(_intNextTag);
            return true;
        }

        /// <summary>
        /// Process the read tag and push into stack when opening or pop when closing
        /// </summary>
        /// <param name="p_objTagStack">Stack storing all the read tags</param>
        /// <param name="p_strReadString">Name of the read tag</param>
        /// <returns></returns>
        private static bool ProcessTag(SimpleStack p_objTagStack, string p_strReadString)
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

        /// <summary>
        /// Recursive processing of XML string
        /// </summary>
        /// <param name="p_strXml">Input XML string to validate</param>
        /// <returns>Validity of the input</returns>
        public static bool DetermineXmlRecursive(string p_strXml)
        {
            // Perform the basic check
            if (!PerformBasicCheck(p_strXml)) return false;

            // Read the first tag and push it to stack
            return DetermineXmlRecursive(p_strXml.Substring(p_strXml.IndexOf(">") + 1), new SimpleStack() { p_strXml.Substring(1, p_strXml.IndexOf(">") - 1) });
        }

        /// <summary>
        /// Private recursive processing of XML string
        /// </summary>
        /// <param name="p_strXml">Remaining XML string to validate</param>
        /// <param name="p_objTagStack">Unclosed tag stack</param>
        /// <returns>Validity of the input</returns>
        private static bool DetermineXmlRecursive(string p_strXml, SimpleStack p_objTagStack)
        {
            // XML is valid when fully processed and no unclosed tags
            if (p_strXml.Length == 0 && p_objTagStack.Count == 0) return true;
            // XML is invalid when fully processed but unclosed tags exist
            else if (p_strXml.Length == 0 && p_objTagStack.Count > 0) return false;

            // If data, discard it
            if (!p_strXml[0].Equals('<') && !DiscardData(ref p_strXml)) return false;

            // Discard the leading '<' character
            p_strXml = p_strXml.Substring(1);

            // If closing tag, pop and validate from stack
            if (p_strXml.Length > 1 && p_strXml[0].Equals('/'))
            {
                string _strReadString = p_strXml.Substring(1, p_strXml.IndexOf('>') - 1);
                // Validate if current closing tag matches the supposed opening tag
                if (!p_objTagStack.Pop().Equals(_strReadString)) return false;
            }
            // If opening tag, push to stack
            else
            {
                string _strReadString = p_strXml.Substring(0, p_strXml.IndexOf('>'));
                p_objTagStack.Push(_strReadString);
            }

            // Discard processed tag
            p_strXml = p_strXml.Substring(p_strXml.IndexOf('>') + 1);

            // Recursive invocation
            return DetermineXmlRecursive(p_strXml, p_objTagStack);
        }
    }
}