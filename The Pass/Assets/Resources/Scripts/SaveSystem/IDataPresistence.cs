using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPresistence
{
    //tylko odczytujemy dane
    void LoadData(GameData data);
    //Modyfikujemy dane wi�c ref
    void SaveData(ref GameData data);    
}
