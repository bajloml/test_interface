using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WPF_CrossComm_Client.Structures;

namespace WPF_CrossComm_Client
{
    class XML_Handler
    {
        //Create default Settings File
        public void Create_XMLfile( String FileName,
                                    String Path,
                                    int NumOfFormatVar,
                                    String RootElementName,
                                    String NumFormatVarElementName,
                                    String NumForVarAttributeName,
                                    String FormatElementName,
                                    String FormatParElementName,
                                    String FormatParAttributeName,
                                    String FormatParAttributeValue,
                                    String PointerVarElementName,
                                    String PointerVarAttributeName,
                                    String PointerVarAttributeValue,
                                    String TransferReqElementName,
                                    String TransferReqAttributeName,
                                    String TransferReqAttributeValue,
                                    String ParValidElementName,
                                    String ParValidAttributeName,
                                    String ParValidAttributeValue,
                                    String ParameterToSendElementName,
                                    String ParameterToSendAttributeName,
                                    String ParameterToSendAttributeValue)
        {
            XDocument Xml_SettingsFile = new XDocument();

                // Add root element
                XElement Element = new XElement(RootElementName);
                Xml_SettingsFile.Add(Element);

                // Add element on root element
                Element = new XElement(NumFormatVarElementName);
                XAttribute attribute = new XAttribute(NumForVarAttributeName, NumOfFormatVar.ToString());
                Element.Add(attribute);
                Xml_SettingsFile.Element(RootElementName).Add(Element);

                // Add element on root element
                Element = new XElement(PointerVarElementName);
                attribute = new XAttribute(PointerVarAttributeName, PointerVarAttributeValue);
                Element.Add(attribute);
                Xml_SettingsFile.Element(RootElementName).Add(Element);

                // Add element on root element
                Element = new XElement(TransferReqElementName);
                attribute = new XAttribute(TransferReqAttributeName, TransferReqAttributeValue);
                Element.Add(attribute);
                Xml_SettingsFile.Element(RootElementName).Add(Element);

                // Add element on root element
                Element = new XElement(ParValidElementName);
                attribute = new XAttribute(ParValidAttributeName, ParValidAttributeValue);
                Element.Add(attribute);
                Xml_SettingsFile.Element(RootElementName).Add(Element);

                // Add element on root element
                Element = new XElement(ParameterToSendElementName);
                attribute = new XAttribute(ParameterToSendAttributeName, ParameterToSendAttributeValue);
                Element.Add(attribute);
                Xml_SettingsFile.Element(RootElementName).Add(Element);

                // add elements on a subelement
                XElement ElementParameters;

                Element = new XElement(FormatElementName);
                Xml_SettingsFile.Element(RootElementName).Add(Element);

                for (int i = 0; i < NumOfFormatVar; i++)
                {
                    ElementParameters = new XElement(FormatParElementName);
                    attribute = new XAttribute(FormatParAttributeName, FormatParAttributeName + i.ToString());

                    ElementParameters.Add(attribute);
                    attribute = new XAttribute(FormatParAttributeValue, i.ToString());
                    ElementParameters.Add(attribute);
                    Element.Add(ElementParameters);
                }

            Xml_SettingsFile.Save(FileName);
        }


        public void Create_XMLfileStruc(FormatParametersVariables formatVar)
        {
            XDocument Xml_SettingsFile = new XDocument();

            // Add root element
            XElement Element = new XElement(formatVar.RootElementName);
            Xml_SettingsFile.Add(Element);
            Xml_SettingsFile.Save(formatVar.SettingsFileName);

            AddElementOnXML(formatVar.SettingsFileName, formatVar.path, formatVar.RootElementName, true, "", formatVar.NumFormatVarElementName,     formatVar.NumForVarAttributeName,       formatVar.NumberOfFormatVar.ToString());
            AddElementOnXML(formatVar.SettingsFileName, formatVar.path, formatVar.RootElementName, true, "", formatVar.PointerStringElementName,    formatVar.PointerStringAttributeName,   formatVar.PointerStringAttributeValue);
            AddElementOnXML(formatVar.SettingsFileName, formatVar.path, formatVar.RootElementName, true, "", formatVar.TransferReqElementName,      formatVar.TransferReqAttributeName,     formatVar.TransferReqAttributeValue);
            AddElementOnXML(formatVar.SettingsFileName, formatVar.path, formatVar.RootElementName, true, "", formatVar.ParValidElementName,         formatVar.ParValidAttributeName,        formatVar.ParValidAttributeValue);
            AddElementOnXML(formatVar.SettingsFileName, formatVar.path, formatVar.RootElementName, true, "", formatVar.ParameterToSendElementName,  formatVar.ParameterToSendAttributeName, formatVar.ParameterToSendAttributeValue);
            AddElementOnXML(formatVar.SettingsFileName, formatVar.path, formatVar.RootElementName, true, "", formatVar.FormatElementName,           "",                                      "");

            for (int i = 0; i < Convert.ToInt32(formatVar.NumberOfFormatVar); i++)
            {
                AddElementOnXML         (formatVar.SettingsFileName, formatVar.path, formatVar.RootElementName, false, formatVar.FormatElementName, formatVar.FormatParElementName, formatVar.FormatParAttributeName, formatVar.FormatParAttributeName+i.ToString());
            }

            Xml_SettingsFile.Save(formatVar.path + formatVar.SettingsFileName);

            AddAttributeOnElement(formatVar.SettingsFileName, formatVar.path, false, formatVar.FormatElementName, formatVar.FormatParElementName, formatVar.FormatParAttributeValue, "0");

            Xml_SettingsFile.Save(formatVar.path + formatVar.SettingsFileName);
        }

        public void Create_XMLfile(String FileName, String Path, String RootElementName)
        {
            XDocument Xml_SettingsFile  = new XDocument();
            XComment Comment            = new XComment("Settings file for KUKA");
            Xml_SettingsFile.Add(Comment);
            XElement RootElement = new XElement(RootElementName);
            Xml_SettingsFile.Add(RootElement);
            Xml_SettingsFile.Save(FileName, SaveOptions.None);
        }

        public void AddElementOnXML(String FileName, String Path, String RootElementName, bool AddOnRootElement, String ElementOnWhichToAddName, String ElementToAddName, String AttributeName, String AttributeValue)
        {
            XDocument Xml_SettingsFile          = new XDocument();

            if (AddOnRootElement)
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile                = XDocument.Load(reader);
                    XElement ElementToAdd           = new XElement(ElementToAddName);

                    if (AttributeName != "")
                    {
                        XAttribute attribute = new XAttribute(AttributeName, AttributeValue);
                        ElementToAdd.Add(attribute);
                    }

                    Xml_SettingsFile.Root.Add(ElementToAdd, "\n");
                }
            }
            else
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile                = XDocument.Load(reader);
                    XElement ElementToAdd           = new XElement(ElementToAddName);

                    if (AttributeName != "")
                    {
                        XAttribute attribute = new XAttribute(AttributeName, AttributeValue);
                        ElementToAdd.Add(attribute);
                    }

                    Xml_SettingsFile.Root.Element(ElementOnWhichToAddName).Add(ElementToAdd, "\n");
                }
            }

            Xml_SettingsFile.Save(FileName, SaveOptions.None);
        }

        public void AddAttributeOnElement(String FileName, String Path, Boolean AddOnRootElement, String ElementName1, String ElementOnWhichToAddName, String AttributeName, String AttributeValue)
        {
            XDocument Xml_SettingsFile = new XDocument();

            if (AddOnRootElement)
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile = XDocument.Load(reader);

                    if (AttributeName != "")
                    {
                        //Read textboxes variables and values and write save them to the Settings.xml file
                        IEnumerable<XElement> IEnumerable_FormatParVal = from el in Xml_SettingsFile.Root.Elements(ElementOnWhichToAddName) select el;

                        foreach (XElement element in IEnumerable_FormatParVal)
                        {
                            XAttribute attribute = new XAttribute(AttributeName, AttributeValue);
                            element.Add(attribute);
                        }
                    }
                }
            }
            else
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile = XDocument.Load(reader);

                    if (AttributeName != "")
                    {
                        //Read textboxes variables and values and write save them to the Settings.xml file
                        IEnumerable<XElement> IEnumerable_FormatParVal = from el in Xml_SettingsFile.Root.Elements(ElementName1).Elements(ElementOnWhichToAddName) select el;

                        foreach (XElement element in IEnumerable_FormatParVal)
                        {
                            XAttribute attribute = new XAttribute(AttributeName, AttributeValue);
                            element.Add(attribute);
                        }
                    }
                }
            }
            Xml_SettingsFile.Save(FileName, SaveOptions.None);
        }

        // for given xml file returns elements attributes as a list of string
        public List<string> ReturnListOfXMLAttributes(String FileName, String Path, String ElementName, String SubElementName, String AttributeName)
        {
            XDocument Xml_SettingsFile = new XDocument();
            List<string> List_FormatElements = new List<string>();
            if (SubElementName == "")
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile = XDocument.Load(reader);

                    IEnumerable<XElement> IEnumerable_FormatElements = from el in Xml_SettingsFile.Root.Elements(ElementName) where ((string)el.Attribute(AttributeName) != "") select el;
                    List_FormatElements = IEnumerable_FormatElements.Select(element => element.Attribute(AttributeName).Value.ToString()).ToList();
                }
            }
            else
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile = XDocument.Load(reader);

                    IEnumerable<XElement> IEnumerable_FormatElements = from el in Xml_SettingsFile.Root.Elements(ElementName).Elements(SubElementName) where ((string)el.Attribute(AttributeName) != "") select el;
                    List_FormatElements = IEnumerable_FormatElements.Select(element => element.Attribute(AttributeName).Value.ToString()).ToList();
                }
            }
            return List_FormatElements;
        }

        //Saves the parameters after closing the application
        public void SaveParameters(String FileName, String Path, String ElementName, String SubElementName, String AttributeName, String[] AttributeValue)
        {
            XDocument Xml_SettingsFile = new XDocument();
            List<string> List_FormatElements = new List<string>();
            if (SubElementName == "")
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile = XDocument.Load(reader);

                    //Read textboxes variables and values and write save them to the Settings.xml file
                    IEnumerable<XElement> IEnumerable_FormatParVal = from el in Xml_SettingsFile.Root.Elements(ElementName) select el;

                    int i = 0;
                    foreach (XElement element in IEnumerable_FormatParVal)
                    {
                        element.Attribute(AttributeName).Value = AttributeValue[i];
                        i++;
                    }
                }
            }
            else
            {
                using (XmlReader reader = XmlReader.Create(Path + "\\" + FileName))
                {
                    Xml_SettingsFile = XDocument.Load(reader);

                    //Read textboxes variables and values and write save them to the Settings.xml file
                    IEnumerable<XElement> IEnumerable_FormatParVal = from el in Xml_SettingsFile.Root.Elements(ElementName).Elements(SubElementName) select el;

                    int i = 0;
                    foreach (XElement element in IEnumerable_FormatParVal)
                    {
                        element.Attribute(AttributeName).Value = AttributeValue[i];
                        i++;
                    }
                }
            }
            Xml_SettingsFile.Save(Path + "\\" + FileName);
        }


    }
}
