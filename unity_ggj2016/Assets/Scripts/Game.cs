using UnityEngine;
using System.Collections;

public class TotalScore {
	public int totalActionsPerformed;
	public int totalActionsExpected;

	public TotalScore() {
		this.totalActionsPerformed = 0;
		this.totalActionsExpected = 0;
	}
}

public class TurnInfo {
	public int numActionsPerformed;
	public int numActionsExpected;
	public ArrayList buttons;

	public TurnInfo() {
		this.numActionsPerformed = 0;
		this.numActionsExpected = 0;
		this.buttons = new ArrayList();
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
            }  
            return Game._instance;
        } 
    }

	public float secondsToWait = 3.0f;
	public TurnInfo turnInfo = new TurnInfo();
	public TotalScore totalScore = new TotalScore();
	private TurnEnd turnEnd = new TurnEnd();

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
}
