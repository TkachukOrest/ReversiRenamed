using System;
using System.Text;
using System.Xml;

namespace Reversi.GameEngine
{
    public class XmlSerializator
    {
        public static bool WriteToXML(Field field, bool enabledTips, int currentMove, string fileName)
        {
            bool result = false;
            try
            {
                //create matrix string
                string matrix = "";
                for (int i = 0; i < Field.N; i++)
                {
                    for (int j = 0; j < Field.N; j++)
                    {
                        matrix += field[i, j] + " ";
                    }
                }

                using (XmlTextWriter writer = new XmlTextWriter(fileName, null))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Field");
                    writer.WriteElementString("Matrix", matrix.ToString());
                    writer.WriteElementString("Tips", enabledTips.ToString());
                    writer.WriteElementString("CurrentMove", currentMove.ToString());

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public static bool ReadFromXML(ref Field field, ref bool enabledTips, ref int currentMove, string fileName)
        {
            bool result = false; string name = "";
            int[,] matrix = new int[Field.N, Field.N];
            int move = 0;
            bool tips = enabledTips;
            try
            {
                using (XmlTextReader reader = new XmlTextReader(fileName))
                {
                    StringBuilder s = new StringBuilder();
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                name = reader.Name;
                                break;
                            case XmlNodeType.Text:
                                switch (name)
                                {
                                    case "Matrix":
                                        string[] matr = reader.Value.Split(new char[] { ' ' });
                                        for (int i = 0; i < Field.N; i++)
                                        {
                                            for (int j = 0; j < Field.N; j++)
                                            {
                                                matrix[i, j] = int.Parse(matr[i * Field.N + j]);
                                            }
                                        }
                                        break;
                                    case "Tips":
                                        tips = bool.Parse(reader.Value);
                                        break;
                                    case "CurrentMove":
                                        move = int.Parse(reader.Value);
                                        break;
                                }
                                break;
                        }
                    }
                }
                field.CopyMatr(matrix);
                enabledTips = tips;
                currentMove = move;
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

