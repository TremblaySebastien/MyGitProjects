using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SauvegardeManager
 {
    static SauvegardeManager()
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
    }

    private static Sauvegarde _sauvegarde;
    private const string default_save = "GameContentLvl1.json";

    public static List<ISauvegardable> ElementsToRefresh = new List<ISauvegardable>();
    private static object pox;

    public static Sauvegarde sauvegarde
    {
        get { if (_sauvegarde == null) { ImportData(default_save); } return _sauvegarde; }
        private set { _sauvegarde = value; }
    }

    public static void ImportData(string filename)
    {
        string content = File.ReadAllText(filename);
        sauvegarde = JsonConvert.DeserializeObject<Sauvegarde>(content);
        SauvegardeManager.ElementsToRefresh.ForEach(x => x.ApplySauvegarde());
    }

    public static void ExportData(string filename)
    {
        Sauvegarde save = new Sauvegarde();
        var pos = GameObject.FindObjectOfType<CharacterMovement>().transform.position;
        save.m_playerX = pos.x;
        save.m_playerY = pos.y;
        save.m_Speed = 30;
        save.m_IsMoving = false;
        save.m_BombDuration = 1;
        save.m_Tiles = MapGenerator.m_Tiles;
        save.m_PlayerPos = MapGenerator.m_PlayerPos;
        save.BombQty = Inventory.bombQty;
        
        string saveJson = JsonConvert.SerializeObject(save, Formatting.Indented);

        File.WriteAllText(filename, saveJson);
    }
}