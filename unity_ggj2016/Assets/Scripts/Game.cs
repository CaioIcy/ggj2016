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
                Game._instance.following = GameObject.FindWithTag("following").GetComponent<Following>();
                Game._instance.shouldStateUpdate = true;
                Game._instance.summoner = GameObject.FindWithTag("summoner").GetComponent<Summoner>();
            }  
            return Game._instance;
        } 
    }

	private bool shouldStateUpdate = false;
	public TurnInfo turnInfo = new TurnInfo();
	public TotalScore totalScore = new TotalScore();
	private TurnEnd turnEnd = new TurnEnd();
	public bool isPlayerTurn = false;
	public ObjUi objUi;
	public Following following;
	public float ratioToSuccess = 0.5f;
	public float currentRatio = 0.0f;
	public float stunDuration = 1.0f;
	public Summoner summoner;
	public bool stunned = false;

	// difficultyModifier == 1.0f __ no change
	// difficultyModifier <  1.0f __ increase ratio (easier)
	// difficultyModifier >  1.0f __ decrease ratio (harder)
	public float difficultyModifier = 1.0f;

	private void Update () {
		// due to the singleton, there are two updates for game running :v
		if(shouldStateUpdate) {
			StateManager.Instance.gameState.Update();
		}
	}

	public TurnEnd CalculateTurnSuccess(TurnEndCheck tec) {
		this.turnEnd.successful = false;

		switch(tec) {
			// turn time is up, fill the rest of the buttons with failure
			case TurnEndCheck.OutOfTime:
				// fallthrough
				goto case TurnEndCheck.CompletedActions;
			case TurnEndCheck.CompletedActions:
				float CCC = (float) this.turnInfo.currentIdx;
				float EEE = (float) this.turnInfo.numActionsExpected;
				float PPP = (float) this.turnInfo.numActionsPerformed;
				float ratioTop = CCC;
				float ratioBottom = 0.0f;
				if(this.turnInfo.numActionsExpected > this.turnInfo.numActionsPerformed) {
					ratioBottom = EEE + (EEE - PPP);
				}
				else {
					ratioBottom = EEE + (PPP - EEE);
				}
				ratioBottom *= difficultyModifier;

				currentRatio = ratioTop / ratioBottom;

				// Debug.Log("e." + EEE + " -- p." + PPP + " -- c." + CCC);
				// Debug.Log("ratio: " + currentRatio + " = " + ratioTop + " / " + ratioBottom);
				if(currentRatio > ratioToSuccess) {
					this.turnEnd.successful = true;
				}
				break;
			case TurnEndCheck.NotYet:
				Debug.Log("turn hasnt ended. shouldnt calculate success");
				Debug.Break();
				break;
			default:
				Debug.Log("unknown turn end check");
				Debug.Break();
				break;
		}

		return this.turnEnd;
	}

	public bool IsGameEnd() {
		bool turnsLimitReached = (this.totalScore.totalTurns >= 5);
		bool outOfFollowers = (this.following.Size() <= 0);
		return (turnsLimitReached || outOfFollowers);
	}

	public void ResetTurn() {
		if(this.totalScore.totalTurns == 0) {
			Game.Instance.FirstTurn();
		}

		this.currentRatio = 0.0f;
		this.turnEnd = new TurnEnd();

		this.totalScore.totalActionsPerformed += this.turnInfo.numActionsPerformed;
		this.totalScore.totalActionsExpected += this.turnInfo.numActionsExpected;
		++this.totalScore.totalTurns;

		this.turnInfo.numActionsExpected = 6; // change
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
			// Debug.Log("OK(" + this.turnInfo.numActionsPerformed + ")! " + btn + " idx " + this.turnInfo.currentIdx);
			++this.turnInfo.currentIdx;
			drawId = DrawId.HIT;
		}
		// wrong
		else {
			// Debug.Log("ERR(" + this.turnInfo.numActionsPerformed + ")! " + btn + " idx " + this.turnInfo.currentIdx);
			// objUi
			drawId = DrawId.MISS;
			this.stunned = true;
			this.SetText("MISS! STUNNED");
		}
		DrawButtons(drawId);
	}

	public void DrawButtons(DrawId drawId) {
		this.objUi.Clear();
		this.objUi.Begin();

		for(int i = 0; i < this.turnInfo.buttons.Count; ++i) {
			if(i == this.turnInfo.currentIdx) {
				if(drawId == DrawId.MISS) {
					this.objUi.Add(this.objUi.btn_bg_red, (Action.ButtonId)this.turnInfo.buttons[i], true);
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

	public void FirstTurn() {
		this.following.Add(6);
	}

	public bool turnOver = false;
	public bool TriggerWaitForTurnOver() {
		this.isPlayerTurn = false;
		StartCoroutine("WaitForSecsTurnOver", 2.0f);
		return this.turnOver;
	}

	public void StopWaitForTurnOver() {
		StopCoroutine("WaitForSecsTurnOver");
	}

	IEnumerator WaitForSecsTurnOver(float seconds) {
		// this.objUi.ClearCloud();
		yield return new WaitForSeconds(seconds);
		// Game.Instance.SetText("done!");
		this.turnOver = true;
	}

	public void TriggerWaitForStun() {
		StartCoroutine("WaitForSecsStunOver", this.stunDuration);
	}
	public void StopWaitForStunOver() {
		StopCoroutine("WaitForSecsStunOver");
	}

	IEnumerator WaitForSecsStunOver(float seconds) {
		this.SetText("stunned");
		yield return new WaitForSeconds(seconds);
		CancelStun();
		this.SetText("go");
	}

	public void CancelStun() {
		this.stunned = false;
		DrawButtons(DrawId.NEUTRAL);
	}

	public void PlaySummonAnimation() {
		GameObject summonObj = this.GetSummonBasedOnPoints();
		Instantiate(summonObj);
	}

	private GameObject GetSummonBasedOnPoints() {
		return this.summoner.summons[0];
	}

	public void SetText(string str) {
		this.objUi.helpText.text = str;
	}
}
