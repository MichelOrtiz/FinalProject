using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSlot : MonoBehaviour
{
    [SerializeField]private SaveFile partida = null;
    private string filePath;
    [SerializeField] private int slot;
    #region UIelements
    [SerializeField]private Text lblName;
    [SerializeField]private Text lblTime;
    [SerializeField]private Text lblZone;
    [SerializeField]private Button btnNewGame;
    [SerializeField]private Button btnLoadGame;
    [SerializeField]private Button btnDeleteGame;
    [SerializeField]private InputCreateGame createInput;
    #endregion
    private void Start() {
        partida = null;
    }
    void UpdateUI() {
        if(partida!=null){
            //Debug.Log("hay partida en slot " + slot);
            partida.slotFile = slot;
            btnNewGame.gameObject.SetActive(false);
            btnLoadGame.gameObject.SetActive(true);
            btnDeleteGame.gameObject.SetActive(true);
            lblName.gameObject.SetActive(true);
            lblTime.gameObject.SetActive(true);
            lblZone.gameObject.SetActive(true);

            lblName.text = "Nombre: ";
            lblName.text += partida.namePlayer;
            lblTime.text = "Tiempo: ";
            lblTime.text += partida.timeHoursPlayed + ":" + partida.timeMinutesPlayed + ":" + partida.timeSecondsPlayed;
            lblZone.text = "Zona: ";
            lblZone.text += partida.sceneToLoad;
        }else{
            //Debug.Log("no hay partida en slot " + slot);
            btnNewGame.gameObject.SetActive(true);
            btnLoadGame.gameObject.SetActive(false);
            btnDeleteGame.gameObject.SetActive(false);
            lblName.gameObject.SetActive(false);
            lblTime.gameObject.SetActive(false);
            lblZone.gameObject.SetActive(false);
        }
    }
    public void CreateBtn(){
        createInput.SetGameSlot(this);
        createInput.gameObject.SetActive(true);
    }
    public void CrearPartida(string name){
        partida = new SaveFile(name,slot);
        SaveFilesManager.instance.WriteSaveFile(partida,filePath);
        UpdateUI();
    }
    public void CargarPartida(){
        //Boy ... cargar cosas
        SaveFilesManager.instance.currentSaveSlot = partida;
        SaveFilesManager.instance.SetStartSession(DateTime.Now);
        SceneController.instance.Load(SaveFilesManager.instance.currentSaveSlot);
    }
    public void BorrarPartida(){
        partida = null;
        SaveFilesManager.instance.DeleteSaveFile(filePath);
        UpdateUI();
    }
    public void SetSlot(int slot){
        this.slot = slot;
        filePath = Application.dataPath + "/Partida" + slot;
        partida = SaveFilesManager.instance.LoadSaveFile(filePath);
        UpdateUI();
    }
}
