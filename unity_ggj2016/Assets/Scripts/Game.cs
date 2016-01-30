using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalScore {
	public int totalActionsPerformed;
	public int totalActionsExpected;
	public int totalTurns;

	public TotalScore() {
		this.totalActionsPerformed = 0;
		this.totalActionsExpected = 0;
		this.totalTurns = 0;
	}
}

public class TurnInfo {
	public int numActionsPerformed;
	public int numActionsExpected;
	public ArrayList buttons;
	public float timePerformed;
	public float timeExpected;
	public int currentIdx;

	public TurnInfo() {
		this.numActionsPerformed = 0;
		this.numActionsExpected = 0;
		this.buttons = new ArrayList();
		this.timePerformed = 0.0f;
		this.timeExpected = 0.0f;
		this.currentIdx = 0;
	}
}

public class TurnEnd {
	public bool successful;

	public TurnEnd() {
		this.successful = false;
	}
}



public class Game : MonoBehaviour {

    // Singleton pattern implementation
    private static Game _instance = null;	
    protected Game() {}
    public static Game Instance { 
        get {
            if (Game._instance == null) {
            	GameObject obj = new GameObject();
                Game._instance = obj.AddComponent<Game>();
                Game._instance.objUi = GameObject.FindWithTag("objui").GetComponent<ObjUi>();
            }  
            return Game._instance;
        } 
    }

	public float secondsToWait = 3.0f;
	public TurnInfo turnInfo = new TurnInfo();
	public TotalScore totalScore = new TotalScore();
	private TurnEnd turnEnd = new TurnEnd();
	public bool isPlayerTurn = false;
	public ObjUi objUi;

	private void Update () {
		StateManager.Instance.gameState.Update();
	}

	public TurnEnd CalculateTurnSuccess() {
		// implement me
		this.turnEnd.successful = false;
		return this.turnEnd;
	}

	public bool IsGameEnd() {
		// implement me
		return false;
	}

	public void ResetTurn() {
		this.turnEnd = new TurnEnd();

		this.totalScore.totalActionsPerformed += this.turnInfo.numActionsPerformed;
		this.totalScore.totalActionsExpected += this.turnInfo.numActionsExpected;
		++this.totalScore.totalTurns;

		this.turnInfo.numActionsExpected = 4; // change
		this.turnInfo.timeExpected = 5.0f; // change
	
		this.turnInfo.numActionsPerformed = 0;
		this.turnInfo.timePerformed = 0.0f;
		this.turnInfo.currentIdx = 0;

		this.turnInfo.buttons.Clear();
	}

	public void ReceiveAction(Action.ButtonId btn) {
		++this.turnInfo.numActionsPerformed;
		DrawId drawId = DrawId.MISS;
		// correct
		if((Action.ButtonId)this.turnInfo.buttons[this.turnInfo.currentIdx] == btn) {
			Debug.Log("OK(" + this.turnInfo.numActionsPerformed + ")! " + btn + " idx " + this.turnInfo.currentIdx);
			++this.turnInfo.currentIdx;
			drawId = DrawId.HIT;
		}
		// wrong
		else {
			Debug.Log("ERR(" + this.turnInfo.numActionsPerformed + ")! " + btn + " idx " + this.turnInfo.currentIdx);
			// objUi
			drawId = DrawId.MISS;
		}
		DrawButtons(drawId);
	}

	public void DrawButtons(DrawId drawId) {
		this.objUi.Clear();

		for(int i = 0; i < this.turnInfo.buttons.Count; ++i) {
			if(i == this.turnInfo.currentIdx) {
				if(drawId == DrawId.MISS) {
					this.objUi.Add(this.objUi.btn_bg_red, (Action.ButtonId)this.turnInfo.buttons[i]);
				}
				else {
					this.objUi.Add(this.objUi.btn_bg_black, (Action.ButtonId)this.turnInfo.buttons[i]);
				}
			}
			else if (i < this.turnInfo.currentIdx){
				this.objUi.Add(this.objUi.btn_bg_green, (Action.ButtonId)this.turnInfo.buttons[i]);
			}
			else if (i > this.turnInfo.currentIdx){
				this.objUi.Add(this.objUi.btn_bg_black, (Action.ButtonId)this.turnInfo.buttons[i]);
			}
		}
	}
}
