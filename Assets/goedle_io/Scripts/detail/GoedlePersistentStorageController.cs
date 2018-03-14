using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace goedle_sdk.detail
{
public class GoedlePersistentStorageController { 
    public void save(GoedlePersonStorage stored_person, string persistentDataPath) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (persistentDataPath + "/goedle_person.gd");
        bf.Serialize(file, stored_person);
        file.Close();
    }

    public GoedlePersonStorage load(string persistentDataPath) {
        if(File.Exists(persistentDataPath + "/goedle_person.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(persistentDataPath + "/goedle_person.gd", FileMode.Open);
            GoedlePersonStorage stored_person = (GoedlePersonStorage)bf.Deserialize(file);
            file.Close();
            return stored_person;
            }
        else{
            GoedlePersonStorage goedle_person = new GoedlePersonStorage(true);
            save(goedle_person, persistentDataPath);
            return goedle_person;
            }
    }
    }
}