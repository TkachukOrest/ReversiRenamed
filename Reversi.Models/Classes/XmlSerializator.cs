using System;
using System.Text;
using System.Xml;

namespace Reversi.GameEngine
{
    public class XmlSerializator
    {
        public static bool WriteToXML(GameState state, string fileName)
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
                        matrix += state.Field[i, j] + " ";
                    }
                }

                using (XmlTextWriter writer = new XmlTextWriter(fileName, null))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Field");
                    writer.WriteElementString("Matrix", matrix.ToString());
                    writer.WriteElementString("Tips", state.EnabledTips.ToString());
                    writer.WriteElementString("CurrentMove", state.CurrentMove.ToString());
                    writer.WriteElementString("IsFirstAIMove", state.FirstMoveAI.ToString());

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
        public static bool ReadFromXML(ref GameState state, string fileName)
        {
            bool result = false; string name = "";
            int[,] matrix = new int[Field.N, Field.N];
            int move = 0;
            bool tips = false;
            bool firstMoveAI = false;
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
                                    case "IsFirstAIMove":
                                        firstMoveAI = bool.Parse(reader.Value);
                                        break;
                                }
                                break;
                        }
                    }
                }
                state.Field.CopyMatr(matrix);
                state.EnabledTips = tips;
                state.CurrentMove = move;
                state.FirstMoveAI = firstMoveAI;
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

