using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace GetListIDs
{
    public class GetListIDsLevelModule : LevelModule 
    {
        private string title;
        private static bool alreadyWritten = false;

        public override IEnumerator OnLoadCoroutine()
        {
            if (!alreadyWritten)
            {
                List<string> iD;
                int i = 0;
                foreach (string enumName in Enum.GetNames(typeof(Catalog.Category)))
                {
                    title = enumName;
                    //Debug.Log("GetListIDs : Title : \"" + title + "\"");
                    iD = Catalog.GetAllID((Catalog.Category)i);

                    // Create file
                    string pathToFolder = Environment.CurrentDirectory + "\\BladeAndSorcery_Data\\StreamingAssets\\Mods\\GetListIDs\\IDs";
                    string pathToFile = pathToFolder + "\\" + title + ".txt";
                    if(!Directory.Exists(pathToFolder))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(pathToFolder);
                    }
                    // Open writer
                    StreamWriter sw = File.CreateText(pathToFile);
                    if (i.ToString() != ((Catalog.Category)i).ToString())
                    {
                        foreach (string str in iD)
                        {
                            // Write IDs
                            sw.WriteLine(str);
                            //Debug.Log("GetListIDs ID : \"" + str + "\"");
                        }
                    }
                    sw.Flush();
                    sw.Close();
                    i++;
                }
                alreadyWritten = true;
            }
            return base.OnLoadCoroutine();
        }
    }
}
